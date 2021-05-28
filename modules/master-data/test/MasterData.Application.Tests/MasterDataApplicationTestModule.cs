using Volo.Abp.Modularity;

namespace MasterData
{
    [DependsOn(
        typeof(MasterDataApplicationModule),
        typeof(MasterDataDomainTestModule)
        )]
    public class MasterDataApplicationTestModule : AbpModule
    {

    }
}
