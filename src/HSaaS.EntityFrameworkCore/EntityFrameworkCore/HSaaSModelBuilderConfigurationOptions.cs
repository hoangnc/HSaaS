using JetBrains.Annotations;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace HSaaS.EntityFrameworkCore
{
    public class HSaaSModelBuilderConfigurationOptions : AbpModelBuilderConfigurationOptions
    {
        public HSaaSModelBuilderConfigurationOptions(
            [NotNull] string tablePrefix = "",
            [CanBeNull] string schema = null)
            : base(
                tablePrefix,
                schema)
        {

        }
    }
}