using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.AutoMapper;
using Volo.Abp.Modularity;
using Volo.Abp.Application;
using DocumentManagement.Documents;
using Volo.Abp;

namespace DocumentManagement
{
    [DependsOn(
        typeof(DocumentManagementDomainModule),
        typeof(DocumentManagementApplicationContractsModule),
        typeof(AbpDddApplicationModule),
        typeof(AbpAutoMapperModule)
        )]
    public class DocumentManagementApplicationModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddAutoMapperObjectMapper<DocumentManagementApplicationModule>();
            context.Services.AddSingleton<IMineMappingService>(new MimeMappingService());

            Configure<AbpAutoMapperOptions>(options =>
            {
                options.AddProfile<DocumentManagementApplicationAutoMapperProfile>(validate: true);
            });
        }
    }
}
