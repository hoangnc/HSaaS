using System.Threading.Tasks;
using Volo.Abp.UI.Navigation;

namespace HSaaS.Dashboard.Blazor.Menus
{
    public class DashboardMenuContributor : IMenuContributor
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
            context.Menu.AddItem(new ApplicationMenuItem(DashboardMenus.Prefix, displayName: "Dashboard", "/Dashboard", icon: "fa fa-globe"));
            
            return Task.CompletedTask;
        }
    }
}