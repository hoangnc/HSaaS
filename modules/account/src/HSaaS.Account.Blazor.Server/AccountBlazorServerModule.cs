using Volo.Abp.AspNetCore.Components.Server.Theming;
using Volo.Abp.Modularity;

namespace HSaaS.Account.Blazor.Server
{
    [DependsOn(
        typeof(AbpAspNetCoreComponentsServerThemingModule),
        typeof(AccountBlazorModule)
        )]
    public class AccountBlazorServerModule : AbpModule
    {
        
    }
}