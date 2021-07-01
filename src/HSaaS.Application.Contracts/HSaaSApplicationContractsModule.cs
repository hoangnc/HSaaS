using Volo.Abp.Application;
using Volo.Abp.Modularity;
using Volo.Abp.Authorization;

namespace HSaaS
{
    [DependsOn(
        typeof(HSaaSDomainSharedModule),
        typeof(AbpDddApplicationContractsModule),
        typeof(AbpAuthorizationModule)
        )]
    public class HSaaSApplicationContractsModule : AbpModule
    {

    }
}
