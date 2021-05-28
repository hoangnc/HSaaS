using Volo.Abp.Data;
using Volo.Abp.MongoDB;

namespace MasterData.MongoDB
{
    [ConnectionStringName(MasterDataDbProperties.ConnectionStringName)]
    public interface IMasterDataMongoDbContext : IAbpMongoDbContext
    {
        /* Define mongo collections here. Example:
         * IMongoCollection<Question> Questions { get; }
         */
    }
}
