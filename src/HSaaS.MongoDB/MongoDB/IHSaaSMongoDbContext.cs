using Volo.Abp.Data;
using Volo.Abp.MongoDB;

namespace HSaaS.MongoDB
{
    [ConnectionStringName(HSaaSDbProperties.ConnectionStringName)]
    public interface IHSaaSMongoDbContext : IAbpMongoDbContext
    {
        /* Define mongo collections here. Example:
         * IMongoCollection<Question> Questions { get; }
         */
    }
}
