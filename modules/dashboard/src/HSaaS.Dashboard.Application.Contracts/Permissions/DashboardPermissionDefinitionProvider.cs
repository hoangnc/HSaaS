using HSaaS.Dashboard.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace HSaaS.Dashboard.Permissions
{
    public class DashboardPermissionDefinitionProvider : PermissionDefinitionProvider
    {
        public override void Define(IPermissionDefinitionContext context)
        {
            var myGroup = context.AddGroup(DashboardPermissions.GroupName, L("Permission:Dashboard"));
        }

        private static LocalizableString L(string name)
        {
            return LocalizableString.Create<DashboardResource>(name);
        }
    }
}