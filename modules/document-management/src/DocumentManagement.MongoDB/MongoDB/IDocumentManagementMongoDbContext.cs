using Volo.Abp.Data;
using Volo.Abp.MongoDB;

namespace DocumentManagement.MongoDB
{
    [ConnectionStringName(DocumentManagementDbProperties.ConnectionStringName)]
    public interface IDocumentManagementMongoDbContext : IAbpMongoDbContext
    {
        /* Define mongo collections here. Example:
         * IMongoCollection<Question> Questions { get; }
         */
    }
}
