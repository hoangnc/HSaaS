using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Http.Client;
using Volo.Abp.Modularity;

namespace MasterData
{
    [DependsOn(
        typeof(MasterDataApplicationContractsModule),
        typeof(AbpHttpClientModule))]
    public class MasterDataHttpApiClientModule : AbpModule
    {
        public const string RemoteServiceName = "HSaaS_MasterData";

        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddHttpClientProxies(
                typeof(MasterDataApplicationContractsModule).Assembly,
                RemoteServiceName
            );
        }
    }
}
