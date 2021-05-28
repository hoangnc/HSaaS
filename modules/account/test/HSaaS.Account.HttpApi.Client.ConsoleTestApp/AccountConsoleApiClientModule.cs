using Volo.Abp.Http.Client.IdentityModel;
using Volo.Abp.Modularity;

namespace HSaaS.Account
{
    [DependsOn(
        typeof(AccountHttpApiClientModule),
        typeof(AbpHttpClientIdentityModelModule)
        )]
    public class AccountConsoleApiClientModule : AbpModule
    {
        
    }
}
