using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;

namespace HSaaS.EntityFrameworkCore
{
    [ConnectionStringName(HSaaSDbProperties.ConnectionStringName)]
    public interface IHSaaSDbContext : IEfCoreDbContext
    {
        /* Add DbSet for each Aggregate Root here. Example:
         * DbSet<Question> Questions { get; }
         */
    }
}