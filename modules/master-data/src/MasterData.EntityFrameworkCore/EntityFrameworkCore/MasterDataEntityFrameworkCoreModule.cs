using MasterData.Companies;
using MasterData.Departments;
using MasterData.DocumentTypes;
using MasterData.Modules;
using MasterData.UserDepartments;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace MasterData.EntityFrameworkCore
{
    [DependsOn(
        typeof(MasterDataDomainModule),
        typeof(AbpEntityFrameworkCoreModule)
    )]
    public class MasterDataEntityFrameworkCoreModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddAbpDbContext<MasterDataDbContext>(options =>
            {
                /* Add custom repositories here. Example:
                 * options.AddRepository<Question, EfCoreQuestionRepository>();
                 */

                options.AddRepository<Company, EfCoreCompanyRepository>();
                options.AddRepository<Department, EfCoreDepartmentRepository>();
                options.AddRepository<DocumentType, EfCoreDocumentTypeRepository>();
                options.AddRepository<Module, EfCoreModuleRepository>();
                options.AddRepository<UserDepartment, EfCoreUserDepartmentRepository>();
            });
        }
    }
}