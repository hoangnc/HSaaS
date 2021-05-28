using Volo.Abp.Modularity;

namespace HSaaS.Account
{
    [DependsOn(
        typeof(AccountApplicationModule),
        typeof(AccountDomainTestModule)
        )]
    public class AccountApplicationTestModule : AbpModule
    {

    }
}
