using System.IO;
using System.Threading.Tasks;
using DocumentManagement.Localization;
using Microsoft.AspNetCore.Http;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Settings;

namespace DocumentManagement
{
    public abstract class DocumentManagementController : AbpController
    {
        protected DocumentManagementController(ISettingProvider settingProvider = null)
        {
            LocalizationResource = typeof(DocumentManagementResource);
        }

    }
}
