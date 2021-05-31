using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace HSaaS.EntityFrameworkCore
{
    public class HSaaSMasterDataApiHostMigrationsDbContextFactory : IDesignTimeDbContextFactory<HSaaSMasterDataApiHostMigrationsDbContext>
    {
        public HSaaSMasterDataApiHostMigrationsDbContext CreateDbContext(string[] args)
        {
            var configuration = BuildConfiguration();

            var builder = new DbContextOptionsBuilder<HSaaSMasterDataApiHostMigrationsDbContext>()
                .UseSqlServer(configuration.GetConnectionString("HSaaS_MasterData"));

            return new HSaaSMasterDataApiHostMigrationsDbContext(builder.Options);
        }

        private static IConfigurationRoot BuildConfiguration()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false);

            return builder.Build();
        }
    }
}
