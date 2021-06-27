using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.AutoMapper;
using Volo.Abp.Modularity;
using Volo.Abp.Application;
using HSaaS.Dashboard;

namespace HSaaS
{
    [DependsOn(
        typeof(HSaaSDomainModule),
        typeof(HSaaSApplicationContractsModule),
        typeof(AbpDddApplicationModule),
        typeof(AbpAutoMapperModule)
        )]
    [DependsOn(typeof(DashboardApplicationModule))]
    public class HSaaSApplicationModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddAutoMapperObjectMapper<HSaaSApplicationModule>();
            Configure<AbpAutoMapperOptions>(options =>
            {
                options.AddMaps<HSaaSApplicationModule>(validate: true);
            });
        }
    }
}
