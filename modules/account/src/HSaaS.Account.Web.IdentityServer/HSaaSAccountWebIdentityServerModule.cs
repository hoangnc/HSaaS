using IdentityServer4.Configuration;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Identity.AspNetCore;
using Volo.Abp.IdentityServer;
using Volo.Abp.Modularity;
using Volo.Abp.VirtualFileSystem;

namespace HSaaS.Account.Web
{
    [DependsOn(
        typeof(HSaaSAccountWebModule),
        typeof(AbpIdentityServerDomainModule)
        )]
    public class HSaaSAccountWebIdentityServerModule : AbpModule
    {
        public override void PreConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.PreConfigure<AbpIdentityAspNetCoreOptions>(options =>
            {
                options.ConfigureAuthentication = false;
            });

            PreConfigure<IMvcBuilder>(mvcBuilder =>
            {
                mvcBuilder.AddApplicationPartIfNotExists(typeof(HSaaSAccountWebIdentityServerModule).Assembly);
            });
        }

        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpVirtualFileSystemOptions>(options =>
            {
                options.FileSets.AddEmbedded<HSaaSAccountWebIdentityServerModule>();
            });

            Configure<IdentityServerOptions>(options =>
            {
                options.UserInteraction.ConsentUrl = "/Consent";
                options.UserInteraction.ErrorUrl = "/Account/Error";
            });

            //TODO: Try to reuse from AbpIdentityAspNetCoreModule
            context.Services
                .AddAuthentication(o =>
                {
                    o.DefaultScheme = IdentityConstants.ApplicationScheme;
                    o.DefaultSignInScheme = IdentityConstants.ExternalScheme;
                })
                .AddIdentityCookies();
        }
    }
}
