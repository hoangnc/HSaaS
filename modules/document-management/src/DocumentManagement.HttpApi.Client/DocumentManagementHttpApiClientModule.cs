using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Http.Client;
using Volo.Abp.Modularity;

namespace DocumentManagement
{
    [DependsOn(
        typeof(DocumentManagementApplicationContractsModule),
        typeof(AbpHttpClientModule))]
    public class DocumentManagementHttpApiClientModule : AbpModule
    {
        public const string RemoteServiceName = "HSaaS_DocumentManagement";

        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddHttpClientProxies(
                typeof(DocumentManagementApplicationContractsModule).Assembly,
                RemoteServiceName
            );
        }
    }
}
