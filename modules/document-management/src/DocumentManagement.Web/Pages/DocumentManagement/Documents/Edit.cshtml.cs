using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DocumentManagement.Appendixes;
using DocumentManagement.Documents;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Volo.Abp.Domain.Entities;
using Volo.Abp.ObjectExtending;

namespace DocumentManagement.Web.Pages.DocumentManagement.Documents
{
    public class EditModel : DocumentManagementPageModel
    {
        [BindProperty]
        public DocumentInfoViewModel DocumentInfo { get; set; }

        protected IDocumentAppService DocumentAppService { get; }

        public EditModel(
            IDocumentAppService documentAppService
            )
        {
            DocumentAppService = documentAppService;
        }

        public virtual async Task<IActionResult> OnGetAsync(long id)
        {
            DocumentInfo = ObjectMapper.Map<DocumentDto, DocumentInfoViewModel>(await DocumentAppService.GetAsync(id));

            return Page();
        }

        [Serializable]
        public class DocumentInfoViewModel : ExtensibleObject, IHasConcurrencyStamp
        {
            [HiddenInput]
            public long Id { get; set; }
            public string Code { get; set; }
            public string CompanyCode { get; set; }
            public string CompanyName { get; set; }
            public string DepartmentCode { get; set; }
            public string DepartmentName { get; set; }
            public string Module { get; set; }
            public string DocumentType { get; set; }
            public string Name { get; set; }
            public string FileName { get; set; }
            public string DocumentNumber { get; set; }
            public string ReviewNumber { get; set; }
            public string Description { get; set; }
            public string ContentChange { get; set; }
            public string Drafter { get; set; }
            public string Auditor { get; set; }
            public string Approver { get; set; }
            public DateTime? EffectiveDate { get; set; }
            public DateTime? ReviewDate { get; set; }
            public string AppliedToEntire { get; set; }
            public string IssuedToEntire { get; set; }
            public string ReplaceFor { get; set; }
            public DateTime? ReplaceEffectiveDate { get; set; }
            public string RelateToDocuments { get; set; }
            public string RelateToDocumentNames { get; set; }
            public bool DDCAudited { get; set; }
            public string FolderName { get; set; }
            public string LinkFile { get; set; }
            public int StatusId { get; set; }
            public int FormType { get; set; }
            public bool Active { get; set; }
            public int IssuedStatusId { get; set; }
            public string ConcurrencyStamp { get; set; }
            public List<AppendixDto> Appendixes { get; set; }

        }
    }
}
