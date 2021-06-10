using System;
using Microsoft.AspNetCore.Authentication.OAuth.Claims;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using StackExchange.Redis;
using Microsoft.OpenApi.Models;
using Volo.Abp;
using Volo.Abp.AspNetCore.Authentication.OAuth;
using Volo.Abp.AspNetCore.Mvc.Client;
using Volo.Abp.Autofac;
using Volo.Abp.FeatureManagement;
using Volo.Abp.Http.Client.IdentityModel.Web;
using Volo.Abp.Identity;
using Volo.Abp.Identity.Web;
using Volo.Abp.Localization;
using Volo.Abp.Modularity;
using Volo.Abp.MultiTenancy;
using Volo.Abp.PermissionManagement;
using Volo.Abp.TenantManagement;
using Volo.Abp.TenantManagement.Web;
using Volo.Abp.UI.Navigation;
using Volo.Blogging;
using MasterData;
using MasterData.Web;
using HSaaS.MultiTenancy;
using Abp.AspNetCore.Mvc.UI.Theme.AdminLTE;
using Volo.Abp.UI.Navigation.Urls;
using Volo.Abp.AspNetCore.Mvc.Localization;
using Volo.Abp.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.Extensions.Hosting;
using Volo.Abp.AspNetCore.Mvc.UI.Theme.Shared;
using Microsoft.AspNetCore.Hosting;
using Volo.Abp.VirtualFileSystem;
using System.IO;
using Microsoft.Extensions.Configuration;
using Volo.Abp.Caching;
using Volo.Abp.AutoMapper;
using DocumentManagement;
using DocumentManagement.Web;
using System.Linq;
using System.Threading.Tasks;
using BackendAdminApp.Host.Localization;
using BackendAdminApp.Shared;

namespace BackendAdminApp.Host
{
    [DependsOn(
        typeof(AbpAutofacModule),
        typeof(AbpAspNetCoreMvcClientModule),
        typeof(AbpAspNetCoreAuthenticationOpenIdConnectModule),
        typeof(AbpAspNetCoreMvcUiAdminLTEThemeModule),
        typeof(AbpAspNetCoreAuthenticationOAuthModule),
        typeof(AbpHttpClientIdentityModelWebModule),
        typeof(AbpIdentityHttpApiClientModule),
        typeof(AbpIdentityWebModule),
        typeof(AbpTenantManagementHttpApiClientModule),
        typeof(AbpTenantManagementWebModule),
        typeof(BloggingApplicationContractsModule),
        typeof(AbpPermissionManagementHttpApiClientModule),
        typeof(MasterDataHttpApiClientModule),
        typeof(MasterDataWebModule),
        typeof(BackendAdminAppSharedModule),
        typeof(DocumentManagementHttpApiClientModule),
        typeof(DocumentManagementWebModule),
        typeof(AbpFeatureManagementHttpApiClientModule)
        )]
    public class BackendAdminAppHostModule : AbpModule
    {
        public override void PreConfigureServices(ServiceConfigurationContext context)
        {
            /*PreConfigure<AbpMvcDataAnnotationsLocalizationOptions>(options =>
            {
                options.AddAssemblyResource(
                    typeof(BackendAdminAppResource),
                    typeof(BackendAdminAppHostModule).Assembly
                );
            });*/

            PreConfigure<IMvcBuilder>(mvcBuilder =>
            {
                mvcBuilder.AddApplicationPartIfNotExists(typeof(BackendAdminAppHostModule).Assembly);
            });
        }
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            var hostingEnvironment = context.Services.GetHostingEnvironment();
            var configuration = context.Services.GetConfiguration();

            ConfigureMenu(configuration);
            ConfigureVirtualFileSystem(hostingEnvironment);
            ConfigureCache(configuration);
            ConfigureUrls(configuration);
            ConfigureAuthentication(context, configuration);
            ConfigureAutoMapper();
            ConfigureSwaggerServices(context.Services);
            ConfigureMultiTenancy();
            ConfigureRedis(context, configuration, hostingEnvironment);     
            ConfigureLocalizationServices();
        }
        private void ConfigureMenu(IConfiguration configuration)
        {
            Configure<AbpNavigationOptions>(options =>
            {
                options.MenuContributors.Add(new BackendAdminAppMenuContributor(configuration));
            });
        }

        private void ConfigureCache(IConfiguration configuration)
        {
            Configure<AbpDistributedCacheOptions>(options =>
            {
                options.KeyPrefix = "HSaaS:";
            });
        }

        private void ConfigureUrls(IConfiguration configuration)
        {
            Configure<AppUrlOptions>(options =>
            {
                options.Applications["MVC"].RootUrl = configuration["App:SelfUrl"];
            });
        }

        private void ConfigureMultiTenancy()
        {
            Configure<AbpMultiTenancyOptions>(options =>
            {
                options.IsEnabled = MultiTenancyConsts.IsEnabled;
            });
        }

        private void ConfigureLocalizationServices()
        {
            Configure<AbpLocalizationOptions>(options =>
            {
                options.Languages.Add(new LanguageInfo("vi", "vi", "Tiếng Việt", "famfamfam-flag-vn"));
                options.Languages.Add(new LanguageInfo("en", "en", "English", "famfamfam-flag-england"));
                options.Languages.Add(new LanguageInfo("zh-Hans", "zh-Hans", "简体中文", "famfamfam-flag-zh"));
            });
        }

        private void ConfigureVirtualFileSystem(IWebHostEnvironment hostingEnvironment)
        {
            Configure<AbpVirtualFileSystemOptions>(options =>
            {
                options.FileSets.AddEmbedded<BackendAdminAppHostModule>();
            });

            if (hostingEnvironment.IsDevelopment())
            {
                Configure<AbpVirtualFileSystemOptions>(options =>
                {
                    options.FileSets.ReplaceEmbeddedByPhysical<DocumentManagementWebModule>(Path.Combine(hostingEnvironment.ContentRootPath, $"..{Path.DirectorySeparatorChar}..{Path.DirectorySeparatorChar}modules{Path.DirectorySeparatorChar}document-management{Path.DirectorySeparatorChar}src{Path.DirectorySeparatorChar}DocumentManagement.Web"));
                    options.FileSets.ReplaceEmbeddedByPhysical<AbpAspNetCoreMvcUiAdminLTEThemeModule>(Path.Combine(hostingEnvironment.ContentRootPath, $"..{Path.DirectorySeparatorChar}..{Path.DirectorySeparatorChar}themes{Path.DirectorySeparatorChar}Abp.AspNetCore.Mvc.UI.Theme.AdminLTE"));
                });
            }
        }

        private void ConfigureAuthentication(ServiceConfigurationContext context, IConfiguration configuration)
        {
            context.Services.AddAuthentication(options =>
            {
                options.DefaultScheme = "Cookies";
                options.DefaultChallengeScheme = "oidc";
            })
                .AddCookie("Cookies", options =>
                {
                    options.ExpireTimeSpan = TimeSpan.FromDays(365);
                    options.ForwardChallenge = "oidc";
                })
                .AddOpenIdConnect("oidc", options =>
                {
                    options.Authority = configuration["AuthServer:Authority"];
                    options.ClientId = configuration["AuthServer:ClientId"];
                    options.ClientSecret = configuration["AuthServer:ClientSecret"];
                    options.RequireHttpsMetadata = Convert.ToBoolean(configuration["AuthServer:RequireHttpsMetadata"]);
                    options.SignInScheme = "Cookies";
                    options.ResponseType = OpenIdConnectResponseType.CodeIdToken;
                    options.SaveTokens = true;
                    options.GetClaimsFromUserInfoEndpoint = true;
                
                    options.Scope.Add("role");
                    options.Scope.Add("email");
                    options.Scope.Add("phone");
                    options.Scope.Add("BackendAdminAppGateway");
                    options.Scope.Add("IdentityService");
                    options.Scope.Add("MasterDataService");
                    options.Scope.Add("DocumentManagementService");
                    options.Scope.Add("TenantManagementService");
                    options.ClaimActions.MapAbpClaimTypes();

                    options.Events.OnTokenValidated = (async context =>
                    {
                        var claimsFromOidcProvider = context.Principal.Claims.ToList();
                        await Task.CompletedTask;
                    });

                    options.Events.OnRedirectToIdentityProviderForSignOut = (async context => {
                        string redirectUri = context.Properties.RedirectUri;
                        await Task.CompletedTask;
                    });
                });
        }

        private void ConfigureAutoMapper()
        {
            Configure<AbpAutoMapperOptions>(options =>
            {
                options.AddMaps<BackendAdminAppHostModule>();
            });
        }

        private void ConfigureSwaggerServices(IServiceCollection services)
        {
            services.AddSwaggerGen(
                options =>
                {
                    options.SwaggerDoc("v1", new OpenApiInfo { Title = "HSaaS Backend Admin Application API", Version = "v1" });
                    options.DocInclusionPredicate((docName, description) => true);
                }
            );
        }

        private void ConfigureRedis(
            ServiceConfigurationContext context,
            IConfiguration configuration,
            IWebHostEnvironment hostingEnvironment)
        {
            if (!hostingEnvironment.IsDevelopment())
            {
                var redis = ConnectionMultiplexer.Connect(configuration["Redis:Configuration"]);
                context.Services
                    .AddDataProtection()
                    .PersistKeysToStackExchangeRedis(redis, "HSaaS-Protection-Keys");
            }
        }

        public override void OnApplicationInitialization(ApplicationInitializationContext context)
        {
            var app = context.GetApplicationBuilder();
            var env = context.GetEnvironment();

            app.UseAbpRequestLocalization();
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseErrorPage();
                app.UseHsts();
            }

            app.UseCorrelationId();
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseCookiePolicy();
            app.UseAuthentication();

            if (MultiTenancyConsts.IsEnabled)
            {
                app.UseMultiTenancy();
            }

            app.UseAuthorization();
            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "Backend Admin Application API");
            });

            /*app.MapWhen(
                ctx => 
                       ctx.Request.Path.ToString().StartsWith("/Abp/ApplicationConfigurationScript"),
                app2 =>
                {
                    app2.UseRouting();
                    app2.UseConfiguredEndpoints();
                }
            );*/
            app.UseConfiguredEndpoints();
        }
    }
}
