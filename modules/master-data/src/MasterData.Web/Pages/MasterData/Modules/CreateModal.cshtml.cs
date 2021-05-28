using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using MasterData.Companies;
using MasterData.Modules;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp.Auditing;
using Volo.Abp.ObjectExtending;
using Volo.Abp.Validation;


namespace MasterData.Web.Pages.MasterData.Modules
{
    public class CreateModalModel : MasterDataPageModel
    {
        [BindProperty]
        public ModuleInfoViewModel ModuleInfo { get; set; }


        protected IModuleAppService ModuleAppService { get; }

        public CreateModalModel(IModuleAppService moduleAppService)
        {
            ModuleAppService = moduleAppService;
        }

        public virtual async Task<IActionResult> OnGetAsync()
        {
            ModuleInfo = new ModuleInfoViewModel();

            return Page();
        }

        public virtual async Task<NoContentResult> OnPostAsync()
        {
            ValidateModel();

            var input = ObjectMapper.Map<ModuleInfoViewModel, ModuleCreateDto>(ModuleInfo);

            await ModuleAppService.CreateAsync(input);

            return NoContent();
        }

        public class ModuleInfoViewModel : ExtensibleObject
        {
            [Required]
            public string Code { get; set; }

            [Required]   
            public string Name { get; set; }
            
        }
    }
}
