using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Identity;
using VoloIdentity = Volo.Abp.Identity.Web.Pages.Identity.Roles;

namespace HSaaS.Identity.Web.Pages.Identity.Roles
{
    public class CreateModalModel : VoloIdentity.CreateModalModel
    {
        public CreateModalModel(IIdentityRoleAppService identityRoleAppService) : base(identityRoleAppService)
        {

        }

        public override Task<IActionResult> OnGetAsync()
        {
            Role = new RoleInfoModel();
            
            return Task.FromResult<IActionResult>(Page());
        }

        public override async Task<IActionResult> OnPostAsync()
        {
            ValidateModel();

            var input = ObjectMapper.Map<RoleInfoModel, IdentityRoleCreateDto>(Role);
            await IdentityRoleAppService.CreateAsync(input);

            return NoContent();
        }
    }
}