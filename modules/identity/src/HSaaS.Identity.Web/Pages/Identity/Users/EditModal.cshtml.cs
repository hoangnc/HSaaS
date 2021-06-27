using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp.Auditing;
using Volo.Abp.Domain.Entities;
using Volo.Abp.ObjectExtending;
using Volo.Abp.Validation;
using Volo.Abp.Identity;
using VoloIdentity = Volo.Abp.Identity.Web.Pages.Identity.Users;
using Microsoft.AspNetCore.Mvc.Rendering;
using MasterData.UserDepartments;
using MasterData.Departments;
using Volo.Abp.DependencyInjection;

namespace HSaaS.Identity.Web.Pages.Identity.Users
{
    public class EditModalModel : VoloIdentity.EditModalModel
    {
        [BindProperty]
        public List<SelectListItem> Departments { get; set; }

        [BindProperty]
        public AssignedDepartmentViewModel Department { get; set; }
        protected IUserDepartmentAppService UserDepartmentAppService { get; }
        protected IDepartmentAppService DepartmentAppService { get; }

        public EditModalModel(IIdentityUserAppService identityUserAppService,
            IUserDepartmentAppService userDepartmentAppService,
            IDepartmentAppService departmentAppService) : base(identityUserAppService)
        {
            UserDepartmentAppService = userDepartmentAppService;
            DepartmentAppService = departmentAppService;
        }

        public override async Task<IActionResult> OnGetAsync(Guid id)
        {
            UserInfo = ObjectMapper.Map<IdentityUserDto, UserInfoViewModel>(await IdentityUserAppService.GetAsync(id));

            Roles = ObjectMapper.Map<IReadOnlyList<IdentityRoleDto>, AssignedRoleViewModel[]>((await IdentityUserAppService.GetAssignableRolesAsync()).Items);

            var userRoleNames = (await IdentityUserAppService.GetRolesAsync(UserInfo.Id)).Items.Select(r => r.Name).ToList();
            foreach (var role in Roles)
            {
                if (userRoleNames.Contains(role.Name))
                {
                    role.IsAssigned = true;
                }
            }

            var departmentDtoList = (await DepartmentAppService.GetListAsync(new GetDepartmentsInput
            {
                MaxResultCount = 1000,
                SkipCount = 0
            }));

            Department = ObjectMapper.Map<UserDepartmentDto, AssignedDepartmentViewModel>(await UserDepartmentAppService.GetByUserNameAsync(UserInfo.UserName));

            Departments = departmentDtoList.Items.Select(d =>
            {

                if (Department != null && !Department.DepartmentCode.IsNullOrEmpty())
                {
                    return new SelectListItem
                    {
                        Selected = true,
                        Text = d.Name,
                        Value = d.Code
                    };
                }
            
                return new SelectListItem
                {
                    Text = d.Name,
                    Value = d.Code
                };
            }).ToList();

            return Page();
        }

        public override async Task<IActionResult> OnPostAsync()
        {
            ValidateModel();

            var input = ObjectMapper.Map<UserInfoViewModel, IdentityUserUpdateDto>(UserInfo);
            input.RoleNames = Roles.Where(r => r.IsAssigned).Select(r => r.Name).ToArray();
            await IdentityUserAppService.UpdateAsync(UserInfo.Id, input);

            var userDepartment = new UserDepartmentUpdateDto
            {
                UserName = input.UserName,
                DepartmentCode = Department.DepartmentCode
            };

            await UserDepartmentAppService.UpdateAsync(Guid.Empty, userDepartment);

            return NoContent();
        }
    }
    public class AssignedDepartmentViewModel
    {
        public Guid Id { get; set; }
        public string DepartmentCode { get; set; }
    }
}
