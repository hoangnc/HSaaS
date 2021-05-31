using Volo.Abp.Ui.Branding;
using Volo.Abp.DependencyInjection;

namespace HSaaS
{
    [Dependency(ReplaceServices = true)]
    public class HSaaSBrandingProvider : DefaultBrandingProvider
    {
        public override string AppName => "HSaaS";
    }
}
