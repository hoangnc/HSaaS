using MasterData.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace MasterData
{
    public abstract class MasterDataController : AbpController
    {
        protected MasterDataController()
        {
            LocalizationResource = typeof(MasterDataResource);
        }
    }
}
