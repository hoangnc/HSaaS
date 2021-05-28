using System.Threading.Tasks;
using HSaaS.Localization;
using Volo.Abp.UI.Navigation;

namespace HSaaS.Blazor.Host
{
    public class HSaaSHostMenuContributor : IMenuContributor
    {
        public Task ConfigureMenuAsync(MenuConfigurationContext context)
        {
            if(context.Menu.DisplayName != StandardMenus.Main)
            {
                return Task.CompletedTask;
            }

            var l = context.GetLocalizer<HSaaSResource>();

            context.Menu.Items.Insert(
                0,
                new ApplicationMenuItem(
                    "HSaaS.Home",
                    l["Menu:Home"],
                    "/",
                    icon: "fas fa-home"
                )
            );

            return Task.CompletedTask;
        }
    }
}
