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
using MasterData.Web.Pages.MasterData.Companies;
using MasterData.Web.Pages.MasterData;
using MasterData.Companies;

namespace MasterData.Web.Pages.MasterData.Companies
{
    public class EditModalModel : MasterDataPageModel
    {
        [BindProperty]
        public CompanyInfoViewModel CompanyInfo { get; set; }

        protected ICompanyAppService CompanyAppService { get; }

        public EditModalModel(ICompanyAppService companyAppService)
        {
            CompanyAppService = companyAppService;
        }

        public virtual async Task<IActionResult> OnGetAsync(long id)
        {
            CompanyInfo = ObjectMapper.Map<CompanyDto, CompanyInfoViewModel>(await CompanyAppService.GetAsync(id));

            return Page();
        }

        public virtual async Task<IActionResult> OnPostAsync()
        {
            ValidateModel();

            var input = ObjectMapper.Map<CompanyInfoViewModel, CompanyUpdateDto>(CompanyInfo);
           
            await CompanyAppService.UpdateAsync(CompanyInfo.Id, input);

            return NoContent();
        }

        public class CompanyInfoViewModel : ExtensibleObject, IHasConcurrencyStamp
        {
            [HiddenInput]
            public long Id { get; set; }

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
