using Volo.Abp.Domain;
using Volo.Abp.Modularity;

namespace MasterData
{
    [DependsOn(
        typeof(AbpDddDomainModule),
        typeof(MasterDataDomainSharedModule)
    )]
    public class MasterDataDomainModule : AbpModule
    {

    }
}
