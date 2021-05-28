using System;
using Volo.Abp;
using Volo.Abp.MongoDB;

namespace HSaaS.MongoDB
{
    public static class HSaaSMongoDbContextExtensions
    {
        public static void ConfigureHSaaS(
            this IMongoModelBuilder builder,
            Action<AbpMongoModelBuilderConfigurationOptions> optionsAction = null)
        {
            Check.NotNull(builder, nameof(builder));

            var options = new HSaaSMongoModelBuilderConfigurationOptions(
                HSaaSDbProperties.DbTablePrefix
            );

            optionsAction?.Invoke(options);
        }
    }
}