using Localization.Resources.AbpUi;
using HSaaS.Localization;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Localization;
using Volo.Abp.Modularity;
using Microsoft.Extensions.DependencyInjection;
using HSaaS.Dashboard;

namespace HSaaS
{
    [DependsOn(
        typeof(HSaaSApplicationContractsModule),
        typeof(AbpAspNetCoreMvcModule))]
    [DependsOn(typeof(DashboardHttpApiModule))]
    public class HSaaSHttpApiModule : AbpModule
    {
        public override void PreConfigureServices(ServiceConfigurationContext context)
        {
            PreConfigure<IMvcBuilder>(mvcBuilder =>
            {
                mvcBuilder.AddApplicationPartIfNotExists(typeof(HSaaSHttpApiModule).Assembly);
            });
        }

        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpLocalizationOptions>(options =>
            {
                options.Resources
                    .Get<HSaaSResource>()
                    .AddBaseTypes(typeof(AbpUiResource));
            });
        }
    }
}
