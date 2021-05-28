using System.Threading.Tasks;
using Volo.Abp.UI.Navigation;

namespace HSaaS.Web.Menus
{
    public class HSaaSMenuContributor : IMenuContributor
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
            context.Menu.AddItem(new ApplicationMenuItem(HSaaSMenus.Prefix, displayName: "HSaaS", "~/HSaaS", icon: "fa fa-globe"));

            return Task.CompletedTask;
        }
    }
}