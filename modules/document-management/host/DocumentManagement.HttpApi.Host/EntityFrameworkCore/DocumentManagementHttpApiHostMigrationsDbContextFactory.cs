using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace DocumentManagement.EntityFrameworkCore
{
    public class DocumentManagementHttpApiHostMigrationsDbContextFactory : IDesignTimeDbContextFactory<DocumentManagementHttpApiHostMigrationsDbContext>
    {
        public DocumentManagementHttpApiHostMigrationsDbContext CreateDbContext(string[] args)
        {
            var configuration = BuildConfiguration();

            var builder = new DbContextOptionsBuilder<DocumentManagementHttpApiHostMigrationsDbContext>()
                .UseSqlServer(configuration.GetConnectionString("DocumentManagement"));

            return new DocumentManagementHttpApiHostMigrationsDbContext(builder.Options);
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
