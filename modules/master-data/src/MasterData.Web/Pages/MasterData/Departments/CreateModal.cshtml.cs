using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using MasterData.Companies;
using MasterData.Departments;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp.Auditing;
using Volo.Abp.ObjectExtending;
using Volo.Abp.Validation;


namespace MasterData.Web.Pages.MasterData.Departments
{
    public class CreateModalModel : MasterDataPageModel
    {
        [BindProperty]
        public DepartmentInfoViewModel DepartmentInfo { get; set; }


        protected IDepartmentAppService DepartmentAppService { get; }

        public CreateModalModel(IDepartmentAppService departmentAppService)
        {
            DepartmentAppService = departmentAppService;
        }

        public virtual async Task<IActionResult> OnGetAsync()
        {
            DepartmentInfo = new DepartmentInfoViewModel();

            return Page();
        }

        public virtual async Task<NoContentResult> OnPostAsync()
        {
            ValidateModel();

            var input = ObjectMapper.Map<DepartmentInfoViewModel, DepartmentCreateDto>(DepartmentInfo);

            await DepartmentAppService.CreateAsync(input);

            return NoContent();
        }

        public class DepartmentInfoViewModel : ExtensibleObject
        {
            [Required]
            public string Code { get; set; }

            [Required]   
            public string Name { get; set; }

            [Required]
            [EmailAddress]
            public string EmailAddress { get; set; }
            
        }
    }
}
