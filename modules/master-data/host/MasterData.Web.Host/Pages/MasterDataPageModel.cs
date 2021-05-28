using MasterData.Localization;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;

namespace MasterData.Pages
{
    public abstract class MasterDataPageModel : AbpPageModel
    {
        protected MasterDataPageModel()
        {
            LocalizationResourceType = typeof(MasterDataResource);
        }
    }
}