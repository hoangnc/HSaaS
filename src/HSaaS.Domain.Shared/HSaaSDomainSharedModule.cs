using Volo.Abp.Modularity;
using Volo.Abp.Localization;
using HSaaS.Localization;
using Volo.Abp.Localization.ExceptionHandling;
using Volo.Abp.Validation;
using Volo.Abp.Validation.Localization;
using Volo.Abp.VirtualFileSystem;
using HSaaS.Dashboard;

namespace HSaaS
{
    [DependsOn(
        typeof(AbpValidationModule)
    )]
    [DependsOn(typeof(DashboardDomainSharedModule))]
    public class HSaaSDomainSharedModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpVirtualFileSystemOptions>(options =>
            {
                options.FileSets.AddEmbedded<HSaaSDomainSharedModule>();
            });

            Configure<AbpLocalizationOptions>(options =>
            {
                options.Resources
                    .Add<HSaaSResource>("en")
                    .AddBaseTypes(typeof(AbpValidationResource))
                    .AddVirtualJson("/Localization/HSaaS");
            });

            Configure<AbpExceptionLocalizationOptions>(options =>
            {
                options.MapCodeNamespace("HSaaS", typeof(HSaaSResource));
            });
        }
    }
}
