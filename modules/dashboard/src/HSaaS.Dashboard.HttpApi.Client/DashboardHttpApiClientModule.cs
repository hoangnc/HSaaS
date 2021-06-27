using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Http.Client;
using Volo.Abp.Modularity;

namespace HSaaS.Dashboard
{
    [DependsOn(
        typeof(DashboardApplicationContractsModule),
        typeof(AbpHttpClientModule))]
    public class DashboardHttpApiClientModule : AbpModule
    {
        public const string RemoteServiceName = "Dashboard";

        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddHttpClientProxies(
                typeof(DashboardApplicationContractsModule).Assembly,
                RemoteServiceName
            );
        }
    }
}
