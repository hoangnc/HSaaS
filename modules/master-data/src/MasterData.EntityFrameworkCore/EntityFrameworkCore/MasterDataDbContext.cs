using MasterData.Companies;
using MasterData.Departments;
using MasterData.DocumentTypes;
using MasterData.Modules;
using MasterData.UserDepartments;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;

namespace MasterData.EntityFrameworkCore
{
    [ConnectionStringName(MasterDataDbProperties.ConnectionStringName)]
    public class MasterDataDbContext : AbpDbContext<MasterDataDbContext>, IMasterDataDbContext
    {
        /* Add DbSet for each Aggregate Root here. Example:
         * public DbSet<Question> Questions { get; set; }
         */

        public DbSet<Company> Companies { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<DocumentType> DocumentTypes { get; set; }
        public DbSet<Module> Modules { get; set; }

        public DbSet<UserDepartment> UserDepartments {get;set;}

        public MasterDataDbContext(DbContextOptions<MasterDataDbContext> options) 
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ConfigureMasterData();
        }
    }
}