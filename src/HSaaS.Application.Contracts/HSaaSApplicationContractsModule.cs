using Volo.Abp.Application;
using Volo.Abp.Modularity;
using Volo.Abp.Authorization;
using HSaaS.Dashboard;

namespace HSaaS
{
    [DependsOn(
        typeof(HSaaSDomainSharedModule),
        typeof(AbpDddApplicationContractsModule),
        typeof(AbpAuthorizationModule)
        )]
    [DependsOn(typeof(DashboardApplicationContractsModule))]
    public class HSaaSApplicationContractsModule : AbpModule
    {

    }
}
