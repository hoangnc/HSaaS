using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.EntityFrameworkCore;
using MasterData.EntityFrameworkCore;
namespace HSaaS.EntityFrameworkCore
{
    public class HSaaSMasterDataApiHostMigrationsDbContext : AbpDbContext<HSaaSMasterDataApiHostMigrationsDbContext>
    {
        public HSaaSMasterDataApiHostMigrationsDbContext(DbContextOptions<HSaaSMasterDataApiHostMigrationsDbContext> options)
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
