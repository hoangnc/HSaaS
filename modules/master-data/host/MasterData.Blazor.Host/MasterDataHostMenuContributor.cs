using System.Threading.Tasks;
using MasterData.Localization;
using Volo.Abp.UI.Navigation;

namespace MasterData.Blazor.Host
{
    public class MasterDataHostMenuContributor : IMenuContributor
    {
        public Task ConfigureMenuAsync(MenuConfigurationContext context)
        {
            if(context.Menu.DisplayName != StandardMenus.Main)
            {
                return Task.CompletedTask;
            }

            var l = context.GetLocalizer<MasterDataResource>();

            context.Menu.Items.Insert(
                0,
                new ApplicationMenuItem(
                    "MasterData.Home",
                    l["Menu:Home"],
                    "/",
                    icon: "fas fa-home"
                )
            );

            return Task.CompletedTask;
        }
    }
}
