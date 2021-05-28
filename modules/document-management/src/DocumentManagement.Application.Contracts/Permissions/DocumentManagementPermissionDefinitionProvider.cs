using DocumentManagement.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;
using Volo.Abp.MultiTenancy;

namespace DocumentManagement.Permissions
{
    public class DocumentManagementPermissionDefinitionProvider : PermissionDefinitionProvider
    {
        public override void Define(IPermissionDefinitionContext context)
        {
            var moduleGroup = context.AddGroup(DocumentManagementPermissions.GroupName, L("Permission:DocumentManagement"));
            moduleGroup.AddPermission(DocumentManagementPermissions.Documents.Default, L("Permission:Default"), multiTenancySide: MultiTenancySides.Host);
            moduleGroup.AddPermission(DocumentManagementPermissions.Documents.Create, L("Permission:Create"), multiTenancySide: MultiTenancySides.Host);
            moduleGroup.AddPermission(DocumentManagementPermissions.Documents.Update, L("Permission:Update"), multiTenancySide: MultiTenancySides.Host);
            moduleGroup.AddPermission(DocumentManagementPermissions.Documents.Review, L("Permission:Review"), multiTenancySide: MultiTenancySides.Host);
        }

        private static LocalizableString L(string name)
        {
            return LocalizableString.Create<DocumentManagementResource>(name);
        }
    }
}