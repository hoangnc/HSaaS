 namespace DocumentManagement
{
    public static class DocumentManagementDbProperties
    {
        public static string DbTablePrefix { get; set; } = "DocumentManagement";

        public static string DbSchema { get; set; } = null;

        public const string ConnectionStringName = "HSaaS_DocumentManagement";
    }
}
