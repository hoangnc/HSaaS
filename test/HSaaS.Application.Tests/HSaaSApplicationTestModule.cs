using Volo.Abp.Modularity;

namespace HSaaS
{
    [DependsOn(
        typeof(HSaaSApplicationModule),
        typeof(HSaaSDomainTestModule)
        )]
    public class HSaaSApplicationTestModule : AbpModule
    {

    }
}
