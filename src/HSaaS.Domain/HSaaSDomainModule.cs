using Volo.Abp.Domain;
using Volo.Abp.Modularity;
using HSaaS.Dashboard;

namespace HSaaS
{
    [DependsOn(
        typeof(AbpDddDomainModule),
        typeof(HSaaSDomainSharedModule)
    )]
    [DependsOn(typeof(DashboardDomainModule))]
    public class HSaaSDomainModule : AbpModule
    {

    }
}
