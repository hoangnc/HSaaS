using System;
using Volo.Abp.ObjectExtending.Modularity;

namespace Volo.Abp.ObjectExtending
{
    public class DocumentManagementModuleExtensionConfiguration : ModuleExtensionConfiguration
    {
        public DocumentManagementModuleExtensionConfiguration ConfigureTenant(
            Action<EntityExtensionConfiguration> configureAction)
        {
            return this.ConfigureEntity(
                DocumentManagementModuleExtensionConsts.EntityNames.Document,
                configureAction
            );
        }
    }
}