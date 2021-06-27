using JetBrains.Annotations;
using Volo.Abp.MongoDB;

namespace HSaaS.Identity.MongoDB
{
    public class IdentityMongoModelBuilderConfigurationOptions : AbpMongoModelBuilderConfigurationOptions
    {
        public IdentityMongoModelBuilderConfigurationOptions(
            [NotNull] string collectionPrefix = "")
            : base(collectionPrefix)
        {
        }
    }
}