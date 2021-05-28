using System.Collections.Generic;
using Volo.Abp.Localization;
using Microsoft.AspNetCore.Http;

namespace Abp.AspNetCore.Mvc.UI.Theme.AdminLTE.Themes.AdminLTE.Components.AccountLanguages
{
    public class LanguageSelectionViewModel
    {
        public LanguageInfo CurrentLanguage { get; set; }

        public IReadOnlyList<LanguageInfo> Languages { get; set; }

        public PathString CurrentUrl { get; set; }
    }
}
