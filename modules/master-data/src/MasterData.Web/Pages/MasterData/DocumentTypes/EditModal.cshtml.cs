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
using MasterData.Web.Pages.MasterData.DocumentTypes;
using MasterData.Web.Pages.MasterData;
using MasterData.DocumentTypes;

namespace MasterData.Web.Pages.MasterData.DocumentTypes
{
    public class EditModalModel : MasterDataPageModel
    {
        [BindProperty]
        public DocumentTypeInfoViewModel DocumentTypeInfo { get; set; }

        protected IDocumentTypeAppService DocumentTypeAppService { get; }

        public EditModalModel(IDocumentTypeAppService documentTypeAppService)
        {
            DocumentTypeAppService = documentTypeAppService;
        }

        public virtual async Task<IActionResult> OnGetAsync(Guid id)
        {
            DocumentTypeInfo = ObjectMapper.Map<DocumentTypeDto, DocumentTypeInfoViewModel>(await DocumentTypeAppService.GetAsync(id));

            return Page();
        }

        public virtual async Task<IActionResult> OnPostAsync()
        {
            ValidateModel();

            var input = ObjectMapper.Map<DocumentTypeInfoViewModel, DocumentTypeUpdateDto>(DocumentTypeInfo);
           
            await DocumentTypeAppService.UpdateAsync(DocumentTypeInfo.Id, input);

            return NoContent();
        }

        public class DocumentTypeInfoViewModel : ExtensibleObject, IHasConcurrencyStamp
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
