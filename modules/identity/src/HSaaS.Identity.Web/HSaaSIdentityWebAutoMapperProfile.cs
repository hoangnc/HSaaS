using HSaaS.Identity.Web.Pages.Identity.Users;
using MasterData.UserDepartments;
using Volo.Abp.Identity.Web;

namespace HSaaS.Identity.Web
{
    public class HSaaSIdentityWebAutoMapperProfile : AbpIdentityWebAutoMapperProfile
    {
        public HSaaSIdentityWebAutoMapperProfile()
        {
            CreateMap<UserDepartmentDto, AssignedDepartmentViewModel>();
        }
    }
}