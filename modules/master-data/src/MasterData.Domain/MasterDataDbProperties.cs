namespace MasterData
{
    public static class MasterDataDbProperties
    {
        public static string DbTablePrefix { get; set; } = "MasterData";

        public static string DbSchema { get; set; } = null;

        public const string ConnectionStringName = "HSaaS_MasterData";
    }
}
