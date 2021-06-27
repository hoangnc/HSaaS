using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.DependencyInjection;
using HSaaS.Identity.Web.Menus;
using Volo.Abp.AspNetCore.Mvc.Localization;
using Volo.Abp.AspNetCore.Mvc.UI.Theme.Shared;
using Volo.Abp.AutoMapper;
using Volo.Abp.Modularity;
using Volo.Abp.UI.Navigation;
using Volo.Abp.VirtualFileSystem;
using MasterData;

namespace HSaaS.Identity.Web
{
    [DependsOn(
        typeof(AbpAutoMapperModule),
        typeof(AbpAspNetCoreMvcUiThemeSharedModule)
        )]
    public class HSaaSIdentityWebModule : AbpModule
    {
        public override void PreConfigureServices(ServiceConfigurationContext context)
        {
            PreConfigure<IMvcBuilder>(mvcBuilder =>
            {
                mvcBuilder.AddApplicationPartIfNotExists(typeof(HSaaSIdentityWebModule).Assembly);
            });
        }

        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            /*Configure<AbpNavigationOptions>(options =>
            {
                options.MenuContributors.Add(new IdentityMenuContributor());
            });*/

            Configure<AbpVirtualFileSystemOptions>(options =>
            {
                options.FileSets.AddEmbedded<HSaaSIdentityWebModule>();
            });

            context.Services.AddAutoMapperObjectMapper<HSaaSIdentityWebAutoMapperProfile>();

            Configure<AbpAutoMapperOptions>(options =>
            {
                options.AddMaps<HSaaSIdentityWebModule>(validate: true);
                options.AddProfile<HSaaSIdentityWebAutoMapperProfile>(validate: true);
            });

            Configure<RazorPagesOptions>(options =>
            {
                //Configure authorization.
            });
        }
    }
}
