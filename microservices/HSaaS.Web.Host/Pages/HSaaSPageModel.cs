using HSaaS.Localization;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;

namespace HSaaS.Pages
{
    public abstract class HSaaSPageModel : AbpPageModel
    {
        protected HSaaSPageModel()
        {
            LocalizationResourceType = typeof(HSaaSResource);
        }
    }
}