using Volo.Abp.DependencyInjection;
using Volo.Abp.Ui.Branding;

namespace HSaaS
{
    [Dependency(ReplaceServices = true)]
    public class HSaaSBrandingIdentityServerProvider : DefaultBrandingProvider, ITransientDependency
    {
        public override string AppName => "HSaaS Authentication";
    }
}
