using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.DependencyInjection;
using MasterData.Localization;
using MasterData.Web.Menus;
using Volo.Abp.AspNetCore.Mvc.Localization;
using Volo.Abp.AspNetCore.Mvc.UI.Theme.Shared;
using Volo.Abp.AutoMapper;
using Volo.Abp.Modularity;
using Volo.Abp.UI.Navigation;
using Volo.Abp.VirtualFileSystem;
using MasterData.Permissions;
using Volo.Abp.AspNetCore.Mvc.UI.Theme.Shared.PageToolbars;
using Volo.Abp.Localization;
using Volo.Abp.Threading;
using Volo.Abp.ObjectExtending.Modularity;

namespace MasterData.Web
{
    [DependsOn(
        typeof(MasterDataHttpApiModule),
        typeof(AbpAspNetCoreMvcUiThemeSharedModule),
        typeof(AbpAutoMapperModule)
        )]
    public class MasterDataWebModule : AbpModule
    {
        private static readonly OneTimeRunner OneTimeRunner = new OneTimeRunner();
        public override void PreConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.PreConfigure<AbpMvcDataAnnotationsLocalizationOptions>(options =>
            {
                options.AddAssemblyResource(typeof(MasterDataResource), typeof(MasterDataWebModule).Assembly);
            });

            PreConfigure<IMvcBuilder>(mvcBuilder =>
            {
                mvcBuilder.AddApplicationPartIfNotExists(typeof(MasterDataWebModule).Assembly);
            });
        }

        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpNavigationOptions>(options =>
            {
                options.MenuContributors.Add(new MasterDataMenuContributor());
            });

            Configure<AbpVirtualFileSystemOptions>(options =>
            {
                options.FileSets.AddEmbedded<MasterDataWebModule>();
            });

            context.Services.AddAutoMapperObjectMapper<MasterDataWebModule>();
            Configure<AbpAutoMapperOptions>(options =>
            {
                options.AddMaps<MasterDataWebModule>(validate: true);
            });

            Configure<RazorPagesOptions>(options =>
            {
                //Configure authorization.
                /*options.Conventions.AuthorizePage("/MasterData/Companies/Index", MasterDataPermissions.Companies.Default);
                options.Conventions.AuthorizePage("/MasterData/Companies/CreateModal", MasterDataPermissions.Companies.Create);
                options.Conventions.AuthorizePage("/MasterData/Companies/EditModal", MasterDataPermissions.Companies.Update);
                
                options.Conventions.AuthorizePage("/MasterData/Departments/Index", MasterDataPermissions.Departments.Default);
                options.Conventions.AuthorizePage("/MasterData/Departments/CreateModal", MasterDataPermissions.Departments.Create);
                options.Conventions.AuthorizePage("/MasterData/Departments/EditModal", MasterDataPermissions.Departments.Update);

                options.Conventions.AuthorizePage("/MasterData/DocumentTypes/Index", MasterDataPermissions.DocumentTypes.Default);
                options.Conventions.AuthorizePage("/MasterData/DocumentTypes/CreateModal", MasterDataPermissions.DocumentTypes.Create);
                options.Conventions.AuthorizePage("/MasterData/DocumentTypes/EditModal", MasterDataPermissions.DocumentTypes.Update);*/
            });

            Configure<AbpPageToolbarOptions>(options =>
            {
                options.Configure<Pages.MasterData.Companies.IndexModel>(
                    toolbar =>
                    {
                        toolbar.AddButton(
                            LocalizableString.Create<MasterDataResource>("NewCompany"),
                            icon: "plus",
                            name: "CreateCompany",
                            requiredPolicyName: MasterDataPermissions.Companies.Create
                        );
                    }
                );

                options.Configure<Pages.MasterData.Departments.IndexModel>(
                    toolbar =>
                    {
                        toolbar.AddButton(
                            LocalizableString.Create<MasterDataResource>("NewDepartment"),
                            icon: "plus",
                            name: "CreateDepartment",
                            requiredPolicyName: MasterDataPermissions.Departments.Create
                        );
                    }
                );

                options.Configure<Pages.MasterData.DocumentTypes.IndexModel>(
                    toolbar =>
                    {
                        toolbar.AddButton(
                            LocalizableString.Create<MasterDataResource>("NewDocumentType"),
                            icon: "plus",
                            name: "CreateDocumentType",
                            requiredPolicyName: MasterDataPermissions.DocumentTypes.Create
                        );
                    }
                );

                options.Configure<Pages.MasterData.Modules.IndexModel>(
                    toolbar =>
                    {
                        toolbar.AddButton(
                            LocalizableString.Create<MasterDataResource>("NewModule"),
                            icon: "plus",
                            name: "CreateModule",
                            requiredPolicyName: MasterDataPermissions.Modules.Create
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
                        "MasterData",
                        "Company",
                        createFormTypes: new[] { typeof(Pages.MasterData.Companies.CreateModalModel.CompanyInfoViewModel) },
                        editFormTypes: new[] { typeof(Pages.MasterData.Companies.EditModalModel.CompanyInfoViewModel) }
                    );

                /*ModuleExtensionConfigurationHelper
                    .ApplyEntityConfigurationToUi(
                        "MasterData",
                        "Department",
                        createFormTypes: new[] { typeof(Pages.MasterData.Companies.CreateModalModel.CompanyInfoViewModel) },
                        editFormTypes: new[] { typeof(Pages.MasterData.Companies.EditModalModel.CompanyInfoViewModel) }
                    );*/
            });
        }
    }
}
