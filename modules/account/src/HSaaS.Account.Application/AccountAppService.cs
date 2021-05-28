﻿using HSaaS.Account.Localization;
using Volo.Abp.Application.Services;

namespace HSaaS.Account
{
    public abstract class AccountAppService : ApplicationService
    {
        protected AccountAppService()
        {
            LocalizationResource = typeof(AccountResource);
            ObjectMapperContext = typeof(AccountApplicationModule);
        }
    }
}
