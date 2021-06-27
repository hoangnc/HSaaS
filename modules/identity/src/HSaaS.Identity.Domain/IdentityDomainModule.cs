using Volo.Abp.Domain;
using Volo.Abp.Modularity;

namespace HSaaS.Identity
{
    [DependsOn(
        typeof(AbpDddDomainModule),
        typeof(IdentityDomainSharedModule)
    )]
    public class IdentityDomainModule : AbpModule
    {

    }
}
