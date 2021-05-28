using HSaaS.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace HSaaS.Permissions
{
    public class HSaaSPermissionDefinitionProvider : PermissionDefinitionProvider
    {
        public override void Define(IPermissionDefinitionContext context)
        {
            var myGroup = context.AddGroup(HSaaSPermissions.GroupName, L("Permission:HSaaS"));
        }

        private static LocalizableString L(string name)
        {
            return LocalizableString.Create<HSaaSResource>(name);
        }
    }
}