using Volo.Abp.Data;
using Volo.Abp.MongoDB;

namespace MasterData.MongoDB
{
    [ConnectionStringName(MasterDataDbProperties.ConnectionStringName)]
    public class MasterDataMongoDbContext : AbpMongoDbContext, IMasterDataMongoDbContext
    {
        /* Add mongo collections here. Example:
         * public IMongoCollection<Question> Questions => Collection<Question>();
         */

        protected override void CreateModel(IMongoModelBuilder modelBuilder)
        {
            base.CreateModel(modelBuilder);

            modelBuilder.ConfigureMasterData();
        }
    }
}