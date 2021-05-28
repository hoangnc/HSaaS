using JetBrains.Annotations;
using Volo.Abp.MongoDB;

namespace DocumentManagement.MongoDB
{
    public class DocumentManagementMongoModelBuilderConfigurationOptions : AbpMongoModelBuilderConfigurationOptions
    {
        public DocumentManagementMongoModelBuilderConfigurationOptions(
            [NotNull] string collectionPrefix = "")
            : base(collectionPrefix)
        {
        }
    }
}