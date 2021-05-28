using Volo.Abp.AspNetCore.Components.WebAssembly.Theming;
using Volo.Abp.Modularity;

namespace HSaaS.Blazor.WebAssembly
{
    [DependsOn(
        typeof(HSaaSBlazorModule),
        typeof(HSaaSHttpApiClientModule),
        typeof(AbpAspNetCoreComponentsWebAssemblyThemingModule)
        )]
    public class HSaaSBlazorWebAssemblyModule : AbpModule
    {
        
    }
}