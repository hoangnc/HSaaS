using MasterData.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;
using Volo.Abp.MultiTenancy;

namespace MasterData.Permissions
{
    public class MasterDataPermissionDefinitionProvider : PermissionDefinitionProvider
    {
        public override void Define(IPermissionDefinitionContext context)
        {
            var moduleGroup = context.AddGroup(MasterDataPermissions.GroupName, L("Permission:MasterData"));
            
            var companyPermissions = moduleGroup.AddPermission(MasterDataPermissions.Companies.Default, L("Permission:Company"), multiTenancySide: MultiTenancySides.Host);
            companyPermissions.AddChild(MasterDataPermissions.Companies.Create, L("Permission:Company:Create"));
            companyPermissions.AddChild(MasterDataPermissions.Companies.Update, L("Permission:Company:Edit"));
            companyPermissions.AddChild(MasterDataPermissions.Companies.Delete, L("Permission:Company:Delete"));

            var departmentPermissions = moduleGroup.AddPermission(MasterDataPermissions.Departments.Default, L("Permission:Department"), multiTenancySide: MultiTenancySides.Host);
            departmentPermissions.AddChild(MasterDataPermissions.Departments.Create, L("Permission:Department:Create"));
            departmentPermissions.AddChild(MasterDataPermissions.Departments.Update, L("Permission:Department:Edit"));
            departmentPermissions.AddChild(MasterDataPermissions.Departments.Delete, L("Permission:Department:Delete"));

            var documentTypePermissions = moduleGroup.AddPermission(MasterDataPermissions.DocumentTypes.Default, L("Permission:DocumentType"), multiTenancySide: MultiTenancySides.Host);
            documentTypePermissions.AddChild(MasterDataPermissions.DocumentTypes.Create, L("Permission:DocumentType:Create"));
            documentTypePermissions.AddChild(MasterDataPermissions.DocumentTypes.Update, L("Permission:DocumentType:Edit"));
            documentTypePermissions.AddChild(MasterDataPermissions.DocumentTypes.Delete, L("Permission:DocumentType:Delete"));

            var modulePermissions = moduleGroup.AddPermission(MasterDataPermissions.Modules.Default, L("Permission:Module"), multiTenancySide: MultiTenancySides.Host);
            modulePermissions.AddChild(MasterDataPermissions.Modules.Create, L("Permission:Module:Create"));
            modulePermissions.AddChild(MasterDataPermissions.Modules.Update, L("Permission:Module:Edit"));
            modulePermissions.AddChild(MasterDataPermissions.Modules.Delete, L("Permission:Module:Delete"));
        }

        private static LocalizableString L(string name)
        {
            return LocalizableString.Create<MasterDataResource>(name);
        }
    }
}