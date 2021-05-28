using Volo.Abp.Reflection;

namespace DocumentManagement.Permissions
{
    public class DocumentManagementPermissions
    {
        public const string GroupName = "DocumentManagement";
        public static class Documents
        {
            public const string Default = GroupName + ".Documents";
            public const string Create = Default + ".Create";
            public const string Update = Default + ".Update";
            public const string Delete = Default + ".Delete";
            public const string Review = Default + ".Review";

            public const string ManageFeatures = Default + ".ManageFeatures";
            public const string ManageConnectionStrings = Default + ".ManageConnectionStrings";
        }

        public static string[] GetAll()
        {
            return ReflectionHelper.GetPublicConstantsRecursively(typeof(DocumentManagementPermissions));
        }
    }
}