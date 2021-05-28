using Volo.Abp.Application;
using Volo.Abp.Modularity;
using Volo.Abp.Authorization;
using Volo.Abp.Threading;
using Volo.Abp.ObjectExtending.Modularity;
using Volo.Abp.ObjectExtending;
using DocumentManagement.Documents;
using Volo.Abp.VirtualFileSystem;

namespace DocumentManagement
{
    [DependsOn(
        typeof(AbpDddApplicationContractsModule),
        typeof(DocumentManagementDomainSharedModule),
        typeof(AbpAuthorizationModule)
        )]
    public class DocumentManagementApplicationContractsModule : AbpModule
    {
        private static readonly OneTimeRunner OneTimeRunner = new OneTimeRunner();

        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpVirtualFileSystemOptions>(options =>
            {
                options.FileSets.AddEmbedded<DocumentManagementApplicationContractsModule>();
            });
        }

        public override void PostConfigureServices(ServiceConfigurationContext context)
        {
            /*OneTimeRunner.Run(() =>
            {
                ModuleExtensionConfigurationHelper
                    .ApplyEntityConfigurationToApi(
                        DocumentManagementModuleExtensionConsts.ModuleName,
                        DocumentManagementModuleExtensionConsts.EntityNames.Document,
                        getApiTypes: new[] { typeof(DocumentDto) }
                        createApiTypes: new[] { typeof(TenantCreateDto) },
                        updateApiTypes: new[] { typeof(TenantUpdateDto) }
                    );
            });*/
        }
    }
}
