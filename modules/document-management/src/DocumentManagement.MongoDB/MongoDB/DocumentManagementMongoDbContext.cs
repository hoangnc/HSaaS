using Volo.Abp.Data;
using Volo.Abp.MongoDB;

namespace DocumentManagement.MongoDB
{
    [ConnectionStringName(DocumentManagementDbProperties.ConnectionStringName)]
    public class DocumentManagementMongoDbContext : AbpMongoDbContext, IDocumentManagementMongoDbContext
    {
        /* Add mongo collections here. Example:
         * public IMongoCollection<Question> Questions => Collection<Question>();
         */

        protected override void CreateModel(IMongoModelBuilder modelBuilder)
        {
            base.CreateModel(modelBuilder);

            modelBuilder.ConfigureDocumentManagement();
        }
    }
}