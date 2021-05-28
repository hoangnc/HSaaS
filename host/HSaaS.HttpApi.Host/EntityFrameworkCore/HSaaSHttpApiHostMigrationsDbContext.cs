using Microsoft.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
using MasterData.EntityFrameworkCore;
namespace HSaaS.EntityFrameworkCore
{
    public class HSaaSHttpApiHostMigrationsDbContext : AbpDbContext<HSaaSHttpApiHostMigrationsDbContext>
    {
        public HSaaSHttpApiHostMigrationsDbContext(DbContextOptions<HSaaSHttpApiHostMigrationsDbContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ConfigureHSaaS();
        }
    }
}
