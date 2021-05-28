using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;

namespace HSaaS.Pages
{
    public class IndexModel : HSaaSPageModel
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