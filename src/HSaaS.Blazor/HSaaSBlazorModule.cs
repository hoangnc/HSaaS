using Microsoft.Extensions.DependencyInjection;
using HSaaS.Blazor.Menus;
using Volo.Abp.AspNetCore.Components.Web.Theming;
using Volo.Abp.AspNetCore.Components.Web.Theming.Routing;
using Volo.Abp.AutoMapper;
using Volo.Abp.Modularity;
using Volo.Abp.UI.Navigation;
using HSaaS.Dashboard.Blazor;

namespace HSaaS.Blazor
{
    [DependsOn(
        typeof(HSaaSApplicationContractsModule),
        typeof(AbpAspNetCoreComponentsWebThemingModule),
        typeof(AbpAutoMapperModule)
        )]
    [DependsOn(typeof(DashboardBlazorServerModule))]
    public class HSaaSBlazorModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddAutoMapperObjectMapper<HSaaSBlazorModule>();

            Configure<AbpAutoMapperOptions>(options =>
            {
                options.AddProfile<HSaaSBlazorAutoMapperProfile>(validate: true);
            });

            Configure<AbpNavigationOptions>(options =>
            {
                options.MenuContributors.Add(new HSaaSMenuContributor());
            });

            Configure<AbpRouterOptions>(options =>
            {
                options.AdditionalAssemblies.Add(typeof(HSaaSBlazorModule).Assembly);
            });
        }
    }
}