using Volo.Abp.Reflection;

namespace HSaaS.Account.Permissions
{
    public class AccountPermissions
    {
        public const string GroupName = "Account";

        public static string[] GetAll()
        {
            return ReflectionHelper.GetPublicConstantsRecursively(typeof(AccountPermissions));
        }
    }
}