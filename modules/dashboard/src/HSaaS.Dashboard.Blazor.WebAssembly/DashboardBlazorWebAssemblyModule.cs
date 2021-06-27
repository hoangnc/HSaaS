using Volo.Abp.AspNetCore.Components.WebAssembly.Theming;
using Volo.Abp.Modularity;

namespace HSaaS.Dashboard.Blazor.WebAssembly
{
    [DependsOn(
        typeof(DashboardBlazorModule),
        typeof(DashboardHttpApiClientModule),
        typeof(AbpAspNetCoreComponentsWebAssemblyThemingModule)
        )]
    public class DashboardBlazorWebAssemblyModule : AbpModule
    {
        
    }
}