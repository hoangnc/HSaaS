using Volo.Abp.Modularity;

namespace HSaaS.Identity
{
    [DependsOn(
        typeof(IdentityApplicationModule),
        typeof(IdentityDomainTestModule)
        )]
    public class IdentityApplicationTestModule : AbpModule
    {

    }
}
