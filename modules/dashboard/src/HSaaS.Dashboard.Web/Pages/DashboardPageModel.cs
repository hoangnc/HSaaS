using HSaaS.Dashboard.Localization;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;

namespace HSaaS.Dashboard.Web.Pages
{
    /* Inherit your PageModel classes from this class.
     */
    public abstract class DashboardPageModel : AbpPageModel
    {
        protected DashboardPageModel()
        {
            LocalizationResourceType = typeof(DashboardResource);
            ObjectMapperContext = typeof(DashboardWebModule);
        }
    }
}