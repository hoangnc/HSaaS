using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;
using Volo.Abp.UI.Navigation;

namespace PublicWebSite.Host
{
    public class PublicWebSiteMenuContributor : IMenuContributor
    {
        private readonly IConfiguration _configuration;
        public PublicWebSiteMenuContributor(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public Task ConfigureMenuAsync(MenuConfigurationContext context)
        {
            if (context.Menu.Name != StandardMenus.Main)
            {
                return Task.CompletedTask;
            }

            return Task.CompletedTask;
        }
    }
}
