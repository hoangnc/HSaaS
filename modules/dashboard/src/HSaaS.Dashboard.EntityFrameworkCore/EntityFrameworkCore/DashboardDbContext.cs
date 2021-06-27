using Microsoft.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;

namespace HSaaS.Dashboard.EntityFrameworkCore
{
    [ConnectionStringName(DashboardDbProperties.ConnectionStringName)]
    public class DashboardDbContext : AbpDbContext<DashboardDbContext>, IDashboardDbContext
    {
        /* Add DbSet for each Aggregate Root here. Example:
         * public DbSet<Question> Questions { get; set; }
         */

        public DashboardDbContext(DbContextOptions<DashboardDbContext> options) 
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ConfigureDashboard();
        }
    }
}