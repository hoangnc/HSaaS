namespace HSaaS.Dashboard
{
    public static class DashboardDbProperties
    {
        public static string DbTablePrefix { get; set; } = "Dashboard";

        public static string DbSchema { get; set; } = null;

        public const string ConnectionStringName = "Dashboard";
    }
}
