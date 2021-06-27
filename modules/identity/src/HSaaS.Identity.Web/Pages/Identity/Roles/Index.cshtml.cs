using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp.DependencyInjection;
using VoloIdentity = Volo.Abp.Identity.Web.Pages.Identity.Roles;

namespace HSaaS.Identity.Web.Pages.Identity.Roles
{
    public class IndexModel : VoloIdentity.IndexModel
    {
        public override Task<IActionResult> OnGetAsync()
        {
            return Task.FromResult<IActionResult>(Page());
        }

        public override Task<IActionResult> OnPostAsync()
        {
            return Task.FromResult<IActionResult>(Page());
        }
    }
}