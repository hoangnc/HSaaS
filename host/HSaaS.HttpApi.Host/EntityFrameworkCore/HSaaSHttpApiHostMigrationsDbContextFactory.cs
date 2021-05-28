using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace HSaaS.EntityFrameworkCore
{
    public class HSaaSHttpApiHostMigrationsDbContextFactory : IDesignTimeDbContextFactory<HSaaSHttpApiHostMigrationsDbContext>
    {
        public HSaaSHttpApiHostMigrationsDbContext CreateDbContext(string[] args)
        {
            var configuration = BuildConfiguration();

            var builder = new DbContextOptionsBuilder<HSaaSHttpApiHostMigrationsDbContext>()
                .UseSqlServer(configuration.GetConnectionString("HSaaS"));

            return new HSaaSHttpApiHostMigrationsDbContext(builder.Options);
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
