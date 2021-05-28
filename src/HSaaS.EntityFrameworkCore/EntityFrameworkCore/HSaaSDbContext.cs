using Microsoft.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;

namespace HSaaS.EntityFrameworkCore
{
    [ConnectionStringName(HSaaSDbProperties.ConnectionStringName)]
    public class HSaaSDbContext : AbpDbContext<HSaaSDbContext>, IHSaaSDbContext
    {
        /* Add DbSet for each Aggregate Root here. Example:
         * public DbSet<Question> Questions { get; set; }
         */

        public HSaaSDbContext(DbContextOptions<HSaaSDbContext> options) 
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ConfigureHSaaS();
        }
    }
}