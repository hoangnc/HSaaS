using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Http.Client;
using Volo.Abp.Modularity;

namespace HSaaS.Account
{
    [DependsOn(
        typeof(AccountApplicationContractsModule),
        typeof(AbpHttpClientModule))]
    public class AccountHttpApiClientModule : AbpModule
    {
        public const string RemoteServiceName = "Account";

        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddHttpClientProxies(
                typeof(AccountApplicationContractsModule).Assembly,
                RemoteServiceName
            );
        }
    }
}
