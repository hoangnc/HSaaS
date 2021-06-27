using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Identity.Localization;

namespace HSaaS.Identity.Web.Pages
{
    /* Inherit your PageModel classes from this class.
     */
    public abstract class IdentityPageModel : AbpPageModel
    {
        protected IdentityPageModel()
        {
            LocalizationResourceType = typeof(IdentityResource);
            ObjectMapperContext = typeof(HSaaSIdentityWebModule);
        }
    }
}