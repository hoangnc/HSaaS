using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp.Auditing;
using Volo.Abp.ObjectExtending;
using Volo.Abp.Validation;
using Volo.Abp.Identity;
using MasterData.UserDepartments;
using Microsoft.AspNetCore.Mvc.Rendering;
using MasterData.Departments;
using Volo.Abp.Uow;
using VoloIdentity = Volo.Abp.Identity.Web.Pages.Identity.Users;
using Volo.Abp.DependencyInjection;

namespace HSaaS.Identity.Web.Pages.Identity.Users
{
    public class CreateModalModel : VoloIdentity.CreateModalModel
    {
        [BindProperty]
        public List<SelectListItem> Departments { get; set; }

        [BindProperty]
        public AssignedDepartmentViewModel Department { get; set; }

        protected IUserDepartmentAppService UserDepartmentAppService { get; }
        protected IDepartmentAppService DepartmentAppService {get;} 
        public CreateModalModel(IIdentityUserAppService identityUserAppService,
            IUserDepartmentAppService userDepartmentAppService,
            IDepartmentAppService departmentAppService) : base(identityUserAppService)
        {
            UserDepartmentAppService = userDepartmentAppService;
            DepartmentAppService = departmentAppService;
        }

        public override async Task<IActionResult> OnGetAsync()
        {
            UserInfo = new UserInfoViewModel();

            var roleDtoList = (await IdentityUserAppService.GetAssignableRolesAsync()).Items;

            Roles = ObjectMapper.Map<IReadOnlyList<IdentityRoleDto>, AssignedRoleViewModel[]>(roleDtoList);

            foreach (var role in Roles)
            {
                role.IsAssigned = role.IsDefault;
            }

            var departmentDtoList = (await DepartmentAppService.GetListAsync(new GetDepartmentsInput { 
                MaxResultCount = 1000,
                SkipCount = 0
            }));

            Departments = departmentDtoList.Items.Select(d => new SelectListItem { 
                Text = d.Name,
                Value = d.Code
            }).ToList();

            return Page();
        }

        [UnitOfWork]
        public override async Task<NoContentResult> OnPostAsync()
        {
            ValidateModel();

            var input = ObjectMapper.Map<UserInfoViewModel, IdentityUserCreateDto>(UserInfo);
            input.RoleNames = Roles.Where(r => r.IsAssigned).Select(r => r.Name).ToArray();

            await IdentityUserAppService.CreateAsync(input);

            var userDepartment = new UserDepartmentCreateDto
            {
                UserName = input.UserName,
                DepartmentCode = Department.DepartmentCode
            };

            await UserDepartmentAppService.CreateAsync(userDepartment);

            return NoContent();
        }

        public class AssignedDepartmentViewModel
        {
            public string DepartmentCode { get; set; }
        }
    }
}
