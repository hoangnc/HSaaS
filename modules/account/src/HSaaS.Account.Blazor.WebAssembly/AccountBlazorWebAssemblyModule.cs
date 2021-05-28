using Volo.Abp.AspNetCore.Components.WebAssembly.Theming;
using Volo.Abp.Modularity;

namespace HSaaS.Account.Blazor.WebAssembly
{
    [DependsOn(
        typeof(AccountBlazorModule),
        typeof(AccountHttpApiClientModule),
        typeof(AbpAspNetCoreComponentsWebAssemblyThemingModule)
        )]
    public class AccountBlazorWebAssemblyModule : AbpModule
    {
        
    }
}