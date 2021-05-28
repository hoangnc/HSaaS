using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.DependencyInjection;
using HSaaS.Account.Localization;
using HSaaS.Account.Web.Menus;
using Volo.Abp.AspNetCore.Mvc.Localization;
using Volo.Abp.AspNetCore.Mvc.UI.Theme.Shared;
using Volo.Abp.AutoMapper;
using Volo.Abp.Modularity;
using Volo.Abp.UI.Navigation;
using Volo.Abp.VirtualFileSystem;
using Volo.Abp.Identity.AspNetCore;
using Volo.Abp.ExceptionHandling;
using Volo.Abp.Account;
using Abp.AspNetCore.Mvc.UI.Theme.AdminLTE;

namespace HSaaS.Account.Web
{
    [DependsOn(
        typeof(AccountHttpApiModule),
        typeof(AbpAccountHttpApiModule),
        typeof(AbpIdentityAspNetCoreModule),    
        typeof(AbpAutoMapperModule),
        typeof(AbpAspNetCoreMvcUiThemeSharedModule),
        typeof(AbpExceptionHandlingModule)
        )]
    public class HSaaSAccountWebModule : AbpModule
    {
        public override void PreConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.PreConfigure<AbpMvcDataAnnotationsLocalizationOptions>(options =>
            {
                options.AddAssemblyResource(typeof(AccountResource), typeof(HSaaSAccountWebModule).Assembly);
            });

            PreConfigure<IMvcBuilder>(mvcBuilder =>
            {
                mvcBuilder.AddApplicationPartIfNotExists(typeof(HSaaSAccountWebModule).Assembly);
            });
        }

        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpNavigationOptions>(options =>
            {
                options.MenuContributors.Add(new AccountMenuContributor());
            });

            Configure<AbpVirtualFileSystemOptions>(options =>
            {
                options.FileSets.AddEmbedded<HSaaSAccountWebModule>();
            });

            context.Services.AddAutoMapperObjectMapper<HSaaSAccountWebModule>();
            Configure<AbpAutoMapperOptions>(options =>
            {
                options.AddMaps<HSaaSAccountWebModule>(validate: true);
            });

            Configure<RazorPagesOptions>(options =>
            {
                //Configure authorization.
            });
        }
    }
}
