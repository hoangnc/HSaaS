using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;

namespace MasterData.EntityFrameworkCore
{
    [ConnectionStringName(MasterDataDbProperties.ConnectionStringName)]
    public interface IMasterDataDbContext : IEfCoreDbContext
    {
        /* Add DbSet for each Aggregate Root here. Example:
         * DbSet<Question> Questions { get; }
         */
    }
}