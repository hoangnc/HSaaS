using HSaaS.Dashboard.Localization;
using Volo.Abp.Application.Services;

namespace HSaaS.Dashboard
{
    public abstract class DashboardAppService : ApplicationService
    {
        protected DashboardAppService()
        {
            LocalizationResource = typeof(DashboardResource);
            ObjectMapperContext = typeof(DashboardApplicationModule);
        }
    }
}
