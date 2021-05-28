﻿using HSaaS.Account.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace HSaaS.Account
{
    /* Domain tests are configured to use the EF Core provider.
     * You can switch to MongoDB, however your domain tests should be
     * database independent anyway.
     */
    [DependsOn(
        typeof(AccountEntityFrameworkCoreTestModule)
        )]
    public class AccountDomainTestModule : AbpModule
    {
        
    }
}
