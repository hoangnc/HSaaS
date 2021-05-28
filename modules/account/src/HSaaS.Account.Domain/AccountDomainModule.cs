using Volo.Abp.Domain;
using Volo.Abp.Modularity;

namespace HSaaS.Account
{
    [DependsOn(
        typeof(AbpDddDomainModule),
        typeof(AccountDomainSharedModule)
    )]
    public class AccountDomainModule : AbpModule
    {

    }
}
