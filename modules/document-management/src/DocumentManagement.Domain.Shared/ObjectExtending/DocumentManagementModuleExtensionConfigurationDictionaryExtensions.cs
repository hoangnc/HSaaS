using System;
using Volo.Abp.ObjectExtending.Modularity;

namespace Volo.Abp.ObjectExtending
{
    public static class DocumentManagementModuleExtensionConfigurationDictionaryExtensions
    {
        public static ModuleExtensionConfigurationDictionary ConfigureTenantManagement(
            this ModuleExtensionConfigurationDictionary modules,
            Action<DocumentManagementModuleExtensionConfiguration> configureAction)
        {
            return modules.ConfigureModule(
                DocumentManagementModuleExtensionConsts.ModuleName,
                configureAction
            );
        }
    }
}