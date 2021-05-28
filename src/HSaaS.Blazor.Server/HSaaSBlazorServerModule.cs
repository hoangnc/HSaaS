using Volo.Abp.AspNetCore.Components.Server.Theming;
using Volo.Abp.Modularity;

namespace HSaaS.Blazor.Server
{
    [DependsOn(
        typeof(AbpAspNetCoreComponentsServerThemingModule),
        typeof(HSaaSBlazorModule)
        )]
    public class HSaaSBlazorServerModule : AbpModule
    {
        
    }
}