using Volo.Abp.Application;
using Volo.Abp.Modularity;
using Volo.Abp.Authorization;

namespace MasterData
{
    [DependsOn(
        typeof(MasterDataDomainSharedModule),
        typeof(AbpDddApplicationContractsModule),
        typeof(AbpAuthorizationModule)
        )]
    public class MasterDataApplicationContractsModule : AbpModule
    {

    }
}
