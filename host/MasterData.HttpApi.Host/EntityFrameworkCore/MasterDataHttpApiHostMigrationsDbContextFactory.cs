using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace MasterData.EntityFrameworkCore
{
    public class MasterDataHttpApiHostMigrationsDbContextFactory : IDesignTimeDbContextFactory<MasterDataHttpApiHostMigrationsDbContext>
    {
        public MasterDataHttpApiHostMigrationsDbContext CreateDbContext(string[] args)
        {
            var configuration = BuildConfiguration();

            var builder = new DbContextOptionsBuilder<MasterDataHttpApiHostMigrationsDbContext>()
                .UseSqlServer(configuration.GetConnectionString("MasterData"));

            return new MasterDataHttpApiHostMigrationsDbContext(builder.Options);
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
