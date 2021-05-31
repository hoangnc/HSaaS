using Microsoft.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace MasterData.EntityFrameworkCore
{
    public class MasterDataHttpApiHostMigrationsDbContext : AbpDbContext<MasterDataHttpApiHostMigrationsDbContext>
    {
        public MasterDataHttpApiHostMigrationsDbContext(DbContextOptions<MasterDataHttpApiHostMigrationsDbContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ConfigureMasterData();
        }
    }
}
