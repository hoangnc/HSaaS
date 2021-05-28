using System;
using Volo.Abp;
using Volo.Abp.MongoDB;

namespace DocumentManagement.MongoDB
{
    public static class DocumentManagementMongoDbContextExtensions
    {
        public static void ConfigureDocumentManagement(
            this IMongoModelBuilder builder,
            Action<AbpMongoModelBuilderConfigurationOptions> optionsAction = null)
        {
            Check.NotNull(builder, nameof(builder));

            var options = new DocumentManagementMongoModelBuilderConfigurationOptions(
                DocumentManagementDbProperties.DbTablePrefix
            );

            optionsAction?.Invoke(options);
        }
    }
}