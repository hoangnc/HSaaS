using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;

namespace HSaaS.Dashboard.EntityFrameworkCore
{
    [ConnectionStringName(DashboardDbProperties.ConnectionStringName)]
    public interface IDashboardDbContext : IEfCoreDbContext
    {
        /* Add DbSet for each Aggregate Root here. Example:
         * DbSet<Question> Questions { get; }
         */
    }
}