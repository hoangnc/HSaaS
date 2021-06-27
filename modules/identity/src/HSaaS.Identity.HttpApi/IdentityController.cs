using HSaaS.Identity.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace HSaaS.Identity
{
    public abstract class IdentityController : AbpController
    {
        protected IdentityController()
        {
            LocalizationResource = typeof(IdentityResource);
        }
    }
}
