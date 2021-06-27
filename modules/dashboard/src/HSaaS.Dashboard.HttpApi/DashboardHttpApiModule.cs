using Localization.Resources.AbpUi;
using HSaaS.Dashboard.Localization;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Localization;
using Volo.Abp.Modularity;
using Microsoft.Extensions.DependencyInjection;

namespace HSaaS.Dashboard
{
    [DependsOn(
        typeof(DashboardApplicationContractsModule),
        typeof(AbpAspNetCoreMvcModule))]
    public class DashboardHttpApiModule : AbpModule
    {
        public override void PreConfigureServices(ServiceConfigurationContext context)
        {
            PreConfigure<IMvcBuilder>(mvcBuilder =>
            {
                mvcBuilder.AddApplicationPartIfNotExists(typeof(DashboardHttpApiModule).Assembly);
            });
        }

        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpLocalizationOptions>(options =>
            {
                options.Resources
                    .Get<DashboardResource>()
                    .AddBaseTypes(typeof(AbpUiResource));
            });
        }
    }
}
