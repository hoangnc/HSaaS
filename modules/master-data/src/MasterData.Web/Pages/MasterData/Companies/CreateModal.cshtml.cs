using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using MasterData.Companies;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp.Auditing;
using Volo.Abp.ObjectExtending;
using Volo.Abp.Validation;


namespace MasterData.Web.Pages.MasterData.Companies
{
    public class CreateModalModel : MasterDataPageModel
    {
        [BindProperty]
        public CompanyInfoViewModel CompanyInfo { get; set; }


        protected ICompanyAppService CompanyAppService { get; }

        public CreateModalModel(ICompanyAppService companyAppService)
        {
            CompanyAppService = companyAppService;
        }

        public virtual async Task<IActionResult> OnGetAsync()
        {
            CompanyInfo = new CompanyInfoViewModel();

            return Page();
        }

        public virtual async Task<NoContentResult> OnPostAsync()
        {
            ValidateModel();

            var input = ObjectMapper.Map<CompanyInfoViewModel, CompanyCreateDto>(CompanyInfo);

            await CompanyAppService.CreateAsync(input);

            return NoContent();
        }

        public class CompanyInfoViewModel : ExtensibleObject
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
