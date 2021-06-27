using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Identity;
using Volo.Abp.Identity.Web.Pages.Identity;
using Volo.Abp.ObjectExtending;
using Volo.Abp.Validation;
using VoloIdentity = Volo.Abp.Identity.Web.Pages.Identity.Roles;

namespace HSaaS.Identity.Web.Pages.Identity.Roles
{
    public class EditModalModel : VoloIdentity.EditModalModel
    {
        public EditModalModel(IIdentityRoleAppService identityRoleAppService) : base(identityRoleAppService)
        {
        }

        public override async Task OnGetAsync(Guid id)
        {
            Role = ObjectMapper.Map<IdentityRoleDto, RoleInfoModel>(
                await IdentityRoleAppService.GetAsync(id)
            );
        }

        public override async Task<IActionResult> OnPostAsync()
        {
            ValidateModel();

            var input = ObjectMapper.Map<RoleInfoModel, IdentityRoleUpdateDto>(Role);
            await IdentityRoleAppService.UpdateAsync(Role.Id, input);

            return NoContent();
        }
    }
}