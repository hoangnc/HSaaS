using Volo.Abp.Reflection;

namespace HSaaS.Permissions
{
    public class HSaaSPermissions
    {
        public const string GroupName = "HSaaS";

        public static string[] GetAll()
        {
            return ReflectionHelper.GetPublicConstantsRecursively(typeof(HSaaSPermissions));
        }
    }
}