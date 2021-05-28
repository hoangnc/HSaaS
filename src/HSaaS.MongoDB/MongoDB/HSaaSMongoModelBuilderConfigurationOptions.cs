using JetBrains.Annotations;
using Volo.Abp.MongoDB;

namespace HSaaS.MongoDB
{
    public class HSaaSMongoModelBuilderConfigurationOptions : AbpMongoModelBuilderConfigurationOptions
    {
        public HSaaSMongoModelBuilderConfigurationOptions(
            [NotNull] string collectionPrefix = "")
            : base(collectionPrefix)
        {
        }
    }
}