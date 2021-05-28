using Localization.Resources.AbpUi;
using MasterData.Localization;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Localization;
using Volo.Abp.Modularity;
using Microsoft.Extensions.DependencyInjection;

namespace MasterData
{
    [DependsOn(
        typeof(MasterDataApplicationContractsModule),
        typeof(AbpAspNetCoreMvcModule))]
    public class MasterDataHttpApiModule : AbpModule
    {
        public override void PreConfigureServices(ServiceConfigurationContext context)
        {
            PreConfigure<IMvcBuilder>(mvcBuilder =>
            {
                mvcBuilder.AddApplicationPartIfNotExists(typeof(MasterDataHttpApiModule).Assembly);
            });
        }

        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpLocalizationOptions>(options =>
            {
                options.Resources
                    .Get<MasterDataResource>()
                    .AddBaseTypes(typeof(AbpUiResource));
            });
        }
    }
}
