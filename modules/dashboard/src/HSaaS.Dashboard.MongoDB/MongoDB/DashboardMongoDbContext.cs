using Volo.Abp.Data;
using Volo.Abp.MongoDB;

namespace HSaaS.Dashboard.MongoDB
{
    [ConnectionStringName(DashboardDbProperties.ConnectionStringName)]
    public class DashboardMongoDbContext : AbpMongoDbContext, IDashboardMongoDbContext
    {
        /* Add mongo collections here. Example:
         * public IMongoCollection<Question> Questions => Collection<Question>();
         */

        protected override void CreateModel(IMongoModelBuilder modelBuilder)
        {
            base.CreateModel(modelBuilder);

            modelBuilder.ConfigureDashboard();
        }
    }
}