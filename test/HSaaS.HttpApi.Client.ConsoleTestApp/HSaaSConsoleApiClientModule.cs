using Volo.Abp.Http.Client.IdentityModel;
using Volo.Abp.Modularity;

namespace HSaaS
{
    [DependsOn(
        typeof(HSaaSHttpApiClientModule),
        typeof(AbpHttpClientIdentityModelModule)
        )]
    public class HSaaSConsoleApiClientModule : AbpModule
    {
        
    }
}
