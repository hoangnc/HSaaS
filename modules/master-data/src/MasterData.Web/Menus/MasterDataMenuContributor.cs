using System.Threading.Tasks;
using MasterData.Localization;
using Volo.Abp.UI.Navigation;

namespace MasterData.Web.Menus
{
    public class MasterDataMenuContributor : IMenuContributor
    {
        public async Task ConfigureMenuAsync(MenuConfigurationContext context)
        {
            if (context.Menu.Name == StandardMenus.Main)
            {
                await ConfigureMainMenuAsync(context);
            }
        }

        private Task ConfigureMainMenuAsync(MenuConfigurationContext context)
        {
            //Add main menu items.
            var l = context.GetLocalizer<MasterDataResource>();

            var masterDataMenuItem = new ApplicationMenuItem(MasterDataMenus.Prefix, displayName: l["Menu:MasterData"], "~/MasterData", icon: "fa fa-database");
            context.Menu.AddItem(masterDataMenuItem);

            masterDataMenuItem.AddItem(new ApplicationMenuItem(MasterDataMenus.Company, l["Companies"], url: "~/MasterData/Companies"));
            masterDataMenuItem.AddItem(new ApplicationMenuItem(MasterDataMenus.Department, l["Departments"], url: "~/MasterData/Departments"));
            masterDataMenuItem.AddItem(new ApplicationMenuItem(MasterDataMenus.DocumentType, l["DocumentTypes"], url: "~/MasterData/DocumentTypes"));
            masterDataMenuItem.AddItem(new ApplicationMenuItem(MasterDataMenus.Module, l["Modules"], url: "~/MasterData/Modules"));

            return Task.CompletedTask;
        }
    }
}