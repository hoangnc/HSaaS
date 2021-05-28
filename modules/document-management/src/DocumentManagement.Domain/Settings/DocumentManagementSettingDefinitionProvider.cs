using Volo.Abp.Settings;

namespace DocumentManagement.Settings
{
    public class DocumentManagementSettingDefinitionProvider : SettingDefinitionProvider
    {
        public override void Define(ISettingDefinitionContext context)
        {
            /* Define module settings here.
             * Use names from DocumentManagementSettings class.
             */
            context.Add(new SettingDefinition( 
                DocumentManagementSettings.UploadFilePath,
                @"D:\DocumentManagement\UploadFile"
            ));
        }
    }
}