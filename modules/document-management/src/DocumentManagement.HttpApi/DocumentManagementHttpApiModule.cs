using Localization.Resources.AbpUi;
using DocumentManagement.Localization;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Localization;
using Volo.Abp.Modularity;
using Microsoft.Extensions.DependencyInjection;

namespace DocumentManagement
{
    [DependsOn(
        typeof(DocumentManagementApplicationContractsModule),
        typeof(AbpAspNetCoreMvcModule))]
    public class DocumentManagementHttpApiModule : AbpModule
    {
        public override void PreConfigureServices(ServiceConfigurationContext context)
        {
            PreConfigure<IMvcBuilder>(mvcBuilder =>
            {
                mvcBuilder.AddApplicationPartIfNotExists(typeof(DocumentManagementHttpApiModule).Assembly);
            });
        }

        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpLocalizationOptions>(options =>
            {
                options.Resources
                    .Get<DocumentManagementResource>()
                    .AddBaseTypes(typeof(AbpUiResource));
            });
        }
    }
}
