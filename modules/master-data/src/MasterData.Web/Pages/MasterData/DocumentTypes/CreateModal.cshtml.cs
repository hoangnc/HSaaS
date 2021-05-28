using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using MasterData.Companies;
using MasterData.DocumentTypes;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp.Auditing;
using Volo.Abp.ObjectExtending;
using Volo.Abp.Validation;


namespace MasterData.Web.Pages.MasterData.DocumentTypes
{
    public class CreateModalModel : MasterDataPageModel
    {
        [BindProperty]
        public DocumentTypeInfoViewModel DocumentTypeInfo { get; set; }


        protected IDocumentTypeAppService DocumentTypeAppService { get; }

        public CreateModalModel(IDocumentTypeAppService documentTypeAppService)
        {
            DocumentTypeAppService = documentTypeAppService;
        }

        public virtual async Task<IActionResult> OnGetAsync()
        {
            DocumentTypeInfo = new DocumentTypeInfoViewModel();

            return Page();
        }

        public virtual async Task<NoContentResult> OnPostAsync()
        {
            ValidateModel();

            var input = ObjectMapper.Map<DocumentTypeInfoViewModel, DocumentTypeCreateDto>(DocumentTypeInfo);

            await DocumentTypeAppService.CreateAsync(input);

            return NoContent();
        }

        public class DocumentTypeInfoViewModel : ExtensibleObject
        {
            [Required]
            public string Code { get; set; }

            [Required]   
            public string Name { get; set; }
            
        }
    }
}
