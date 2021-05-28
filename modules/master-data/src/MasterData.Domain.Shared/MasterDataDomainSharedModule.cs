using Volo.Abp.Modularity;
using Volo.Abp.Localization;
using MasterData.Localization;
using Volo.Abp.Localization.ExceptionHandling;
using Volo.Abp.Validation;
using Volo.Abp.Validation.Localization;
using Volo.Abp.VirtualFileSystem;

namespace MasterData
{
    [DependsOn(
        typeof(AbpValidationModule)
    )]
    public class MasterDataDomainSharedModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpVirtualFileSystemOptions>(options =>
            {
                options.FileSets.AddEmbedded<MasterDataDomainSharedModule>();
            });

            Configure<AbpLocalizationOptions>(options =>
            {
                options.Resources
                    .Add<MasterDataResource>("en")
                    .AddBaseTypes(typeof(AbpValidationResource))
                    .AddVirtualJson("/Localization/MasterData");
            });

            Configure<AbpExceptionLocalizationOptions>(options =>
            {
                options.MapCodeNamespace("MasterData", typeof(MasterDataResource));
                options.MapCodeNamespace("Dt", typeof(MasterDataResource));
            });
        }
    }
}
