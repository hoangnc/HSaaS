using HSaaS.Dashboard.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace HSaaS.Dashboard
{
    public abstract class DashboardController : AbpController
    {
        protected DashboardController()
        {
            LocalizationResource = typeof(DashboardResource);
        }
    }
}
