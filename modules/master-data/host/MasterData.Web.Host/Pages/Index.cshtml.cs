using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;

namespace MasterData.Pages
{
    public class IndexModel : MasterDataPageModel
    {
        public void OnGet()
        {
            
        }

        public async Task OnPostLoginAsync()
        {
            await HttpContext.ChallengeAsync("oidc");
        }
    }
}