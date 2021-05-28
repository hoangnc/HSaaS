using Volo.Abp.Ui.Branding;
using Volo.Abp.DependencyInjection;

namespace MasterData
{
    [Dependency(ReplaceServices = true)]
    public class MasterDataBrandingProvider : DefaultBrandingProvider
    {
        public override string AppName => "MasterData";
    }
}
