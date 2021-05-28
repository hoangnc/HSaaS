namespace HSaaS
{
    public static class HSaaSDbProperties
    {
        public static string DbTablePrefix { get; set; } = "HSaaS";

        public static string DbSchema { get; set; } = null;

        public const string ConnectionStringName = "HSaaS";
    }
}
