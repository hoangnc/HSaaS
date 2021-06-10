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
using MasterData.Web.Pages.MasterData.Departments;
using MasterData.Web.Pages.MasterData;
using MasterData.Departments;

namespace MasterData.Web.Pages.MasterData.Departments
{
    public class EditModalModel : MasterDataPageModel
    {
        [BindProperty]
        public DepartmentInfoViewModel DepartmentInfo { get; set; }

        protected IDepartmentAppService DepartmentAppService { get; }

        public EditModalModel(IDepartmentAppService departmentAppService)
        {
            DepartmentAppService = departmentAppService;
        }

        public virtual async Task<IActionResult> OnGetAsync(Guid id)
        {
            DepartmentInfo = ObjectMapper.Map<DepartmentDto, DepartmentInfoViewModel>(await DepartmentAppService.GetAsync(id));

            return Page();
        }

        public virtual async Task<IActionResult> OnPostAsync()
        {
            ValidateModel();

            var input = ObjectMapper.Map<DepartmentInfoViewModel, DepartmentUpdateDto>(DepartmentInfo);
           
            await DepartmentAppService.UpdateAsync(DepartmentInfo.Id, input);

            return NoContent();
        }

        public class DepartmentInfoViewModel : ExtensibleObject, IHasConcurrencyStamp
        {
            [HiddenInput]
            public Guid Id { get; set; }

            [HiddenInput]
            public string ConcurrencyStamp { get; set; }

            [Required]
            public string Code { get; set; }

            [Required]
            public string Name { get; set; }

            [Required]
            public string EmailAddress { get; set; }
        }
    }
}
