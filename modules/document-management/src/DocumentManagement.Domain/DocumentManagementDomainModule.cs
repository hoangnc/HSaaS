using DocumentManagement.Documents;
using Volo.Abp.AutoMapper;
using Volo.Abp.Data;
using Volo.Abp.Domain;
using Volo.Abp.Modularity;
using Volo.Abp.ObjectExtending;
using Volo.Abp.ObjectExtending.Modularity;
using Volo.Abp.Threading;

namespace DocumentManagement
{
    [DependsOn(
        typeof(AbpDddDomainModule),
        typeof(DocumentManagementDomainSharedModule)        
    )]
    [DependsOn(typeof(AbpDataModule))]
    [DependsOn(typeof(AbpAutoMapperModule))]
    public class DocumentManagementDomainModule : AbpModule
    {
        private static readonly OneTimeRunner OneTimeRunner = new OneTimeRunner();

        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            
        }

        public override void PostConfigureServices(ServiceConfigurationContext context)
        {
            OneTimeRunner.Run(() =>
            {
                ModuleExtensionConfigurationHelper.ApplyEntityConfigurationToEntity(
                    DocumentManagementModuleExtensionConsts.ModuleName,
                    DocumentManagementModuleExtensionConsts.EntityNames.Document,
                    typeof(Document)
                );
            });
        }
    }
}
