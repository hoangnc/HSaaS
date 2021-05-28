using DocumentManagement.Appendixes;
using DocumentManagement.Documents;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore.DependencyInjection;
using Volo.Abp.Modularity;

namespace DocumentManagement.EntityFrameworkCore
{
    [DependsOn(
        typeof(DocumentManagementDomainModule),
        typeof(AbpEntityFrameworkCoreModule)
    )]
    public class DocumentManagementEntityFrameworkCoreModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddAbpDbContext<DocumentManagementDbContext>(options =>
            {
                /* Add custom repositories here. Example:
                 * options.AddRepository<Question, EfCoreQuestionRepository>();
                 */

                options.AddDefaultRepositories<IDocumentManagementDbContext>();
                options.AddRepository<Document, EfCoreDocumentRepository>();
                options.AddRepository<Appendix, EfCoreAppendixRepository>();
            });

            Configure<AbpEntityOptions>(options =>
            {
                options.Entity<Document>(documentOptions =>
                {
                    documentOptions.DefaultWithDetailsFunc = query => query.Include(o => o.Appendixes);
                });
            });
        }
    }
}