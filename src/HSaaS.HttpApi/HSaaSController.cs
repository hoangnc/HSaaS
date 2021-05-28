using HSaaS.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace HSaaS
{
    public abstract class HSaaSController : AbpController
    {
        protected HSaaSController()
        {
            LocalizationResource = typeof(HSaaSResource);
        }
    }
}
