using MasterData.Localization;
using Volo.Abp.Application.Services;

namespace MasterData
{
    public abstract class MasterDataAppService : ApplicationService
    {
        protected MasterDataAppService()
        {
            LocalizationResource = typeof(MasterDataResource);
            ObjectMapperContext = typeof(MasterDataApplicationModule);
        }
    }
}
