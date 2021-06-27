using Volo.Abp.AspNetCore.Components.Server.Theming;
using Volo.Abp.Modularity;

namespace HSaaS.Identity.Blazor.Server
{
    [DependsOn(
        typeof(AbpAspNetCoreComponentsServerThemingModule),
        typeof(IdentityBlazorModule)
        )]
    public class IdentityBlazorServerModule : AbpModule
    {
        
    }
}