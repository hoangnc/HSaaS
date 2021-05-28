using HSaaS.Localization;
using Volo.Abp.Application.Services;

namespace HSaaS
{
    public abstract class HSaaSAppService : ApplicationService
    {
        protected HSaaSAppService()
        {
            LocalizationResource = typeof(HSaaSResource);
            ObjectMapperContext = typeof(HSaaSApplicationModule);
        }
    }
}
