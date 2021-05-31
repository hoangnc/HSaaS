using Volo.Abp.DependencyInjection;
using Volo.Abp.Ui.Branding;

namespace HSaaS
{
    [Dependency(ReplaceServices = true)]
    public class HSaaSBrandingIdentityServerProvider : DefaultBrandingProvider, ISingletonDependency
    {
        public override string AppName => "HSaaS Authentication";
    }
}
