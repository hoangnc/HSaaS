using Volo.Abp.Domain;
using Volo.Abp.Modularity;

namespace HSaaS.Dashboard
{
    [DependsOn(
        typeof(AbpDddDomainModule),
        typeof(DashboardDomainSharedModule)
    )]
    public class DashboardDomainModule : AbpModule
    {

    }
}
