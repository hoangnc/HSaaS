using Volo.Abp.Reflection;

namespace MasterData.Permissions
{
    public class MasterDataPermissions
    {
        public const string GroupName = "MasterData";

        public static class Companies
        {
            public const string Default = GroupName + ".Companies";
            public const string Create = Default + ".Create";
            public const string Update = Default + ".Update";
            public const string Delete = Default + ".Delete";

            public const string ManageConnectionStrings = Default + ".ManageConnectionStrings";
        }

        public static class Departments
        {
            public const string Default = GroupName + ".Departments";
            public const string Create = Default + ".Create";
            public const string Update = Default + ".Update";
            public const string Delete = Default + ".Delete";

            public const string ManageConnectionStrings = Default + ".ManageConnectionStrings";
        }

        public static class DocumentTypes
        {
            public const string Default = GroupName + ".DocumentTypes";
            public const string Create = Default + ".Create";
            public const string Update = Default + ".Update";
            public const string Delete = Default + ".Delete";

            public const string ManageConnectionStrings = Default + ".ManageConnectionStrings";
        }

        public static class Modules
        {
            public const string Default = GroupName + ".Modules";
            public const string Create = Default + ".Create";
            public const string Update = Default + ".Update";
            public const string Delete = Default + ".Delete";

            public const string ManageConnectionStrings = Default + ".ManageConnectionStrings";
        }

        public static string[] GetAll()
        {
            return ReflectionHelper.GetPublicConstantsRecursively(typeof(MasterDataPermissions));
        }
    }
}