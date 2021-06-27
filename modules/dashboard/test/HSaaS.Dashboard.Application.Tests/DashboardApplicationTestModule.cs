using Volo.Abp.Modularity;

namespace HSaaS.Dashboard
{
    [DependsOn(
        typeof(DashboardApplicationModule),
        typeof(DashboardDomainTestModule)
        )]
    public class DashboardApplicationTestModule : AbpModule
    {

    }
}
