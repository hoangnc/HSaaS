using Microsoft.Extensions.DependencyInjection;
using MasterData.Blazor.Menus;
using Volo.Abp.AspNetCore.Components.WebAssembly.Theming.Routing;
using Volo.Abp.AutoMapper;
using Volo.Abp.Modularity;
using Volo.Abp.UI.Navigation;

namespace MasterData.Blazor
{
    [DependsOn(
        typeof(MasterDataHttpApiClientModule),
        typeof(AbpAutoMapperModule)
        )]
    public class MasterDataBlazorModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddAutoMapperObjectMapper<MasterDataBlazorModule>();

            Configure<AbpAutoMapperOptions>(options =>
            {
                options.AddProfile<MasterDataBlazorAutoMapperProfile>(validate: true);
            });

            Configure<AbpNavigationOptions>(options =>
            {
                options.MenuContributors.Add(new MasterDataMenuContributor());
            });

            Configure<AbpRouterOptions>(options =>
            {
                options.AdditionalAssemblies.Add(typeof(MasterDataBlazorModule).Assembly);
            });
        }
    }
}