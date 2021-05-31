using Microsoft.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace DocumentManagement.EntityFrameworkCore
{
    public class DocumentManagementHttpApiHostMigrationsDbContext : AbpDbContext<DocumentManagementHttpApiHostMigrationsDbContext>
    {
        public DocumentManagementHttpApiHostMigrationsDbContext(DbContextOptions<DocumentManagementHttpApiHostMigrationsDbContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ConfigureDocumentManagement();
        }
    }
}
