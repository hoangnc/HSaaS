using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.AspNetCore.Mvc.Localization;
using Volo.Abp.AspNetCore.Mvc.UI.Bootstrap;
using Volo.Abp.AspNetCore.Mvc.UI.Theme.Shared.PageToolbars;
using Volo.Abp.AutoMapper;
using Volo.Abp.FeatureManagement;
using Volo.Abp.Localization;
using Volo.Abp.Modularity;
using Volo.Abp.ObjectExtending;
using Volo.Abp.ObjectExtending.Modularity;
using DocumentManagement.Localization;
using DocumentManagement.Web.Navigation;
using Volo.Abp.UI.Navigation;
using Volo.Abp.VirtualFileSystem;
using Volo.Abp.Threading;
using DocumentManagement.Permissions;
using DocumentManagement.Web.Pages.DocumentManagement.Documents;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using System.IO;
using System.Reflection;
using MasterData.Departments;
using MasterData;
using MasterData.DocumentTypes;

namespace DocumentManagement.Web
{
    [DependsOn(typeof(DocumentManagementApplicationModule))]
    [DependsOn(typeof(DocumentManagementHttpApiModule))]   
    [DependsOn(typeof(AbpAspNetCoreMvcUiBootstrapModule))]
    [DependsOn(typeof(AbpFeatureManagementWebModule))]
    [DependsOn(typeof(AbpAutoMapperModule))]
    [DependsOn(typeof(MasterDataHttpApiClientModule))]
    public class DocumentManagementWebModule : AbpModule
    {
        private static readonly OneTimeRunner OneTimeRunner = new OneTimeRunner();

        public override void PreConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.PreConfigure<AbpMvcDataAnnotationsLocalizationOptions>(options =>
            {
                options.AddAssemblyResource(typeof(DocumentManagementResource), typeof(DocumentManagementWebModule).Assembly);
            });

            PreConfigure<IMvcBuilder>(mvcBuilder =>
            {
                mvcBuilder.AddApplicationPartIfNotExists(typeof(DocumentManagementWebModule).Assembly);
            });
        }

        public override void ConfigureServices(ServiceConfigurationContext context)
        {
           
            Configure<AbpNavigationOptions>(options =>
            {
                options.MenuContributors.Add(new DocumentManagementWebMainMenuContributor(context.Services.GetServiceLazy<IDepartmentAppService>().Value,
                    context.Services.GetServiceLazy<IDocumentTypeAppService>().Value));
            });

            Configure<AbpVirtualFileSystemOptions>(options =>
            {
                options.FileSets.AddEmbedded<DocumentManagementWebModule>();
            });

            context.Services.AddAutoMapperObjectMapper<DocumentManagementWebModule>();
            Configure<AbpAutoMapperOptions>(options =>
            {
                options.AddProfile<DocumentManagementWebAutoMapperProfile>(validate: true);
            });

            Configure<RazorPagesOptions>(options =>
            {
                options.Conventions.AuthorizePage("/DocumentManagement/Documents/Index", DocumentManagementPermissions.Documents.Default);
                options.Conventions.AuthorizePage("/DocumentManagement/Documents/Detail", DocumentManagementPermissions.Documents.Default);
                options.Conventions.AuthorizePage("/DocumentManagement/Documents/OperationData", DocumentManagementPermissions.Documents.Default);
                options.Conventions.AuthorizePage("/DocumentManagement/Documents/Createl", DocumentManagementPermissions.Documents.Create);
                options.Conventions.AuthorizePage("/DocumentManagement/Documents/Edit", DocumentManagementPermissions.Documents.Update);
                options.Conventions.AuthorizePage("/DocumentManagement/Documents/Review", DocumentManagementPermissions.Documents.Review);
            });

            Configure<AbpPageToolbarOptions>(options =>
            {
                options.Configure<IndexModel>(
                    toolbar =>
                    {
                        toolbar.AddButton(
                            LocalizableString.Create<DocumentManagementResource>("ManageHostFeatures"),
                            icon: "cog",
                            name: "ManageHostFeatures",
                            requiredPolicyName: FeatureManagementPermissions.ManageHostFeatures
                        );

                        toolbar.AddButton(
                            LocalizableString.Create<DocumentManagementResource>("NewTenant"),
                            icon: "plus",
                            name: "CreateTenant",
                            requiredPolicyName: DocumentManagementPermissions.Documents.Create
                        );
                    }
                );
            });
        }

        public override void PostConfigureServices(ServiceConfigurationContext context)
        {
            OneTimeRunner.Run(() =>
            {
                ModuleExtensionConfigurationHelper
                    .ApplyEntityConfigurationToUi(
                        DocumentManagementModuleExtensionConsts.ModuleName,
                        DocumentManagementModuleExtensionConsts.EntityNames.Document,
                        createFormTypes: new[] { typeof(IndexModel) },
                        editFormTypes: new[] { typeof(IndexModel) }
                    );
            });
        }
    }
}