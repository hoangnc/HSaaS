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
using MasterData.Web.Pages.MasterData.Modules;
using MasterData.Web.Pages.MasterData;
using MasterData.DocumentTypes;
using MasterData.Modules;

namespace MasterData.Web.Pages.MasterData.Modules
{
    public class EditModalModel : MasterDataPageModel
    {
        [BindProperty]
        public ModuleInfoViewModel ModuleInfo { get; set; }

        protected IModuleAppService ModuleAppService { get; }

        public EditModalModel(IModuleAppService moduleAppService)
        {
            ModuleAppService = moduleAppService;
        }

        public virtual async Task<IActionResult> OnGetAsync(Guid id)
        {
            ModuleInfo = ObjectMapper.Map<ModuleDto, ModuleInfoViewModel>(await ModuleAppService.GetAsync(id));

            return Page();
        }

        public virtual async Task<IActionResult> OnPostAsync()
        {
            ValidateModel();

            var input = ObjectMapper.Map<ModuleInfoViewModel, ModuleUpdateDto>(ModuleInfo);
           
            await ModuleAppService.UpdateAsync(ModuleInfo.Id, input);

            return NoContent();
        }

        public class ModuleInfoViewModel : ExtensibleObject, IHasConcurrencyStamp
        {
            [HiddenInput]
            public Guid Id { get; set; }

            [HiddenInput]
            public string ConcurrencyStamp { get; set; }

            [Required]
            public string Code { get; set; }

            [Required]
            public string Name { get; set; }
        }
    }
}
