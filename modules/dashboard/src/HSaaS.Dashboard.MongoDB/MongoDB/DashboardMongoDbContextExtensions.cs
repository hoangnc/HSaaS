using System;
using Volo.Abp;
using Volo.Abp.MongoDB;

namespace HSaaS.Dashboard.MongoDB
{
    public static class DashboardMongoDbContextExtensions
    {
        public static void ConfigureDashboard(
            this IMongoModelBuilder builder,
            Action<AbpMongoModelBuilderConfigurationOptions> optionsAction = null)
        {
            Check.NotNull(builder, nameof(builder));

            var options = new DashboardMongoModelBuilderConfigurationOptions(
                DashboardDbProperties.DbTablePrefix
            );

            optionsAction?.Invoke(options);
        }
    }
}