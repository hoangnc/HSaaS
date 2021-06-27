using Volo.Abp.AspNetCore.Components.Server.Theming;
using Volo.Abp.Modularity;

namespace HSaaS.Dashboard.Blazor.Server
{
    [DependsOn(
        typeof(AbpAspNetCoreComponentsServerThemingModule),
        typeof(DashboardBlazorModule)
        )]
    public class DashboardBlazorServerModule : AbpModule
    {
        
    }
}