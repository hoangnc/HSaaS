using Volo.Abp.Data;
using Volo.Abp.MongoDB;

namespace HSaaS.Dashboard.MongoDB
{
    [ConnectionStringName(DashboardDbProperties.ConnectionStringName)]
    public interface IDashboardMongoDbContext : IAbpMongoDbContext
    {
        /* Define mongo collections here. Example:
         * IMongoCollection<Question> Questions { get; }
         */
    }
}
