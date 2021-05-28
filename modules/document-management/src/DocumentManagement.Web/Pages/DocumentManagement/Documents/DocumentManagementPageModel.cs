using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;

namespace DocumentManagement.Web.Pages.DocumentManagement.Documents
{
    public abstract class DocumentManagementPageModel : AbpPageModel
    {
        public DocumentManagementPageModel()
        {
            ObjectMapperContext = typeof(DocumentManagementWebModule);
        }
    }
}