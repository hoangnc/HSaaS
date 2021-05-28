using JetBrains.Annotations;
using Volo.Abp.MongoDB;

namespace MasterData.MongoDB
{
    public class MasterDataMongoModelBuilderConfigurationOptions : AbpMongoModelBuilderConfigurationOptions
    {
        public MasterDataMongoModelBuilderConfigurationOptions(
            [NotNull] string collectionPrefix = "")
            : base(collectionPrefix)
        {
        }
    }
}