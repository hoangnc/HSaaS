using Volo.Abp.Domain;
using Volo.Abp.Modularity;

namespace HSaaS
{
    [DependsOn(
        typeof(AbpDddDomainModule),
        typeof(HSaaSDomainSharedModule)
    )]
    public class HSaaSDomainModule : AbpModule
    {

    }
}
