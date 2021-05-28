using Volo.Abp.Application;
using Volo.Abp.Modularity;
using Volo.Abp.Authorization;

namespace HSaaS.Account
{
    [DependsOn(
        typeof(AccountDomainSharedModule),
        typeof(AbpDddApplicationContractsModule),
        typeof(AbpAuthorizationModule)
        )]
    public class AccountApplicationContractsModule : AbpModule
    {

    }
}
