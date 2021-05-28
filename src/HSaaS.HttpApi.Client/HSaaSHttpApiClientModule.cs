using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Http.Client;
using Volo.Abp.Modularity;

namespace HSaaS
{
    [DependsOn(
        typeof(HSaaSApplicationContractsModule),
        typeof(AbpHttpClientModule))]
    public class HSaaSHttpApiClientModule : AbpModule
    {
        public const string RemoteServiceName = "HSaaS";

        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddHttpClientProxies(
                typeof(HSaaSApplicationContractsModule).Assembly,
                RemoteServiceName
            );
        }
    }
}
