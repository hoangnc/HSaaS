using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.AutoMapper;
using Volo.Abp.Modularity;
using Volo.Abp.Application;

namespace MasterData
{
    [DependsOn(
        typeof(MasterDataDomainModule),
        typeof(MasterDataApplicationContractsModule),
        typeof(AbpDddApplicationModule),
        typeof(AbpAutoMapperModule)
        )]
    public class MasterDataApplicationModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddAutoMapperObjectMapper<MasterDataApplicationModule>();
            Configure<AbpAutoMapperOptions>(options =>
            {
                options.AddMaps<MasterDataApplicationModule>(validate: true);
                options.AddProfile<MasterDataApplicationAutoMapperProfile>(validate: true);
            });
        }
    }
}
