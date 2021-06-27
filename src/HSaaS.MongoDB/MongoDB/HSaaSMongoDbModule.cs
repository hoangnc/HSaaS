using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Modularity;
using Volo.Abp.MongoDB;
using HSaaS.Dashboard.MongoDB;

namespace HSaaS.MongoDB
{
    [DependsOn(
        typeof(HSaaSDomainModule),
        typeof(AbpMongoDbModule)
        )]
    [DependsOn(typeof(DashboardMongoDbModule))]
    public class HSaaSMongoDbModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddMongoDbContext<HSaaSMongoDbContext>(options =>
            {
                /* Add custom repositories here. Example:
                 * options.AddRepository<Question, MongoQuestionRepository>();
                 */
            });
        }
    }
}
