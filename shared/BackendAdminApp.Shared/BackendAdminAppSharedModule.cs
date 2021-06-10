using BackendAdminApp.Host.Localization;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp;
using Volo.Abp.Localization;
using Volo.Abp.Localization.ExceptionHandling;
using Volo.Abp.Modularity;
using Volo.Abp.Validation;
using Volo.Abp.Validation.Localization;
using Volo.Abp.VirtualFileSystem;

namespace BackendAdminApp.Shared
{
    [DependsOn(
       typeof(AbpValidationModule)
   )]
    public class BackendAdminAppSharedModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            ConfigureVirtualFileSystems();
            ConfigureLocalizationServices();
            ConfigureExceptionLocalizations();
            
        }

        private void ConfigureVirtualFileSystems()
        {
            Configure<AbpVirtualFileSystemOptions>(options =>
            {
                options.FileSets.AddEmbedded<BackendAdminAppSharedModule>("BackendAdminApp.Shared");
            });
        }

        private void ConfigureLocalizationServices()
        {
            Configure<AbpLocalizationOptions>(options =>
            {
                options.Resources
                   .Add<BackendAdminAppResource>("en")
                   .AddBaseTypes(typeof(AbpValidationResource))
                   .AddVirtualJson("/Localization/BackendAdmin");
            });
        }

        private void ConfigureExceptionLocalizations()
        {
            Configure<AbpExceptionLocalizationOptions>(options =>
            {
                options.MapCodeNamespace("BackendAdminApp", typeof(BackendAdminAppResource));
            });
        }
    }
}
