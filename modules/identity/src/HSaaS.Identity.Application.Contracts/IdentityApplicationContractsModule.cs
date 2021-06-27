using Volo.Abp.Application;
using Volo.Abp.Modularity;
using Volo.Abp.Authorization;

namespace HSaaS.Identity
{
    [DependsOn(
        typeof(IdentityDomainSharedModule),
        typeof(AbpDddApplicationContractsModule),
        typeof(AbpAuthorizationModule)
        )]
    public class IdentityApplicationContractsModule : AbpModule
    {

    }
}
