using System;
using Volo.Abp;
using Volo.Abp.MongoDB;

namespace MasterData.MongoDB
{
    public static class MasterDataMongoDbContextExtensions
    {
        public static void ConfigureMasterData(
            this IMongoModelBuilder builder,
            Action<AbpMongoModelBuilderConfigurationOptions> optionsAction = null)
        {
            Check.NotNull(builder, nameof(builder));

            var options = new MasterDataMongoModelBuilderConfigurationOptions(
                MasterDataDbProperties.DbTablePrefix
            );

            optionsAction?.Invoke(options);
        }
    }
}