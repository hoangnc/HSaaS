using HSaaS.Dashboard.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace HSaaS.Dashboard
{
    /* Domain tests are configured to use the EF Core provider.
     * You can switch to MongoDB, however your domain tests should be
     * database independent anyway.
     */
    [DependsOn(
        typeof(DashboardEntityFrameworkCoreTestModule)
        )]
    public class DashboardDomainTestModule : AbpModule
    {
        
    }
}
