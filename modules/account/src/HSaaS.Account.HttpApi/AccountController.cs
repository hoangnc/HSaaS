using HSaaS.Account.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace HSaaS.Account
{
    public abstract class AccountController : AbpController
    {
        protected AccountController()
        {
            LocalizationResource = typeof(AccountResource);
        }
    }
}
