using HSaaS.Localization;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;

namespace HSaaS.Web.Pages
{
    /* Inherit your PageModel classes from this class.
     */
    public abstract class HSaaSPageModel : AbpPageModel
    {
        protected HSaaSPageModel()
        {
            LocalizationResourceType = typeof(HSaaSResource);
            ObjectMapperContext = typeof(HSaaSWebModule);
        }
    }
}