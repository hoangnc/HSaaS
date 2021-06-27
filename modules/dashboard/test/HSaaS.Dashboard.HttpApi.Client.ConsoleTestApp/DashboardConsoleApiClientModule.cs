using Volo.Abp.Http.Client.IdentityModel;
using Volo.Abp.Modularity;

namespace HSaaS.Dashboard
{
    [DependsOn(
        typeof(DashboardHttpApiClientModule),
        typeof(AbpHttpClientIdentityModelModule)
        )]
    public class DashboardConsoleApiClientModule : AbpModule
    {
        
    }
}
