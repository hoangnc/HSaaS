using MasterData.Localization;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;

namespace MasterData.Web.Pages
{
    /* Inherit your PageModel classes from this class.
     */
    public abstract class MasterDataPageModel : AbpPageModel
    {
        protected MasterDataPageModel()
        {
            LocalizationResourceType = typeof(MasterDataResource);
            ObjectMapperContext = typeof(MasterDataWebModule);
        }
    }
}