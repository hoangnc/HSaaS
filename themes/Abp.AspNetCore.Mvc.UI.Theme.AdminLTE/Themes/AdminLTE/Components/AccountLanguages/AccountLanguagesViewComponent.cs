using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Localization;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp.AspNetCore.Mvc;
using System.Globalization;

namespace Abp.AspNetCore.Mvc.UI.Theme.AdminLTE.Themes.AdminLTE.Components.AccountLanguages
{
    public class AccountLanguagesViewComponent : AbpViewComponent
    {
        private readonly ILanguageProvider _languageProvider;

        public AccountLanguagesViewComponent(ILanguageProvider languageProvider)
        {
            _languageProvider = languageProvider;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var languages = await _languageProvider.GetLanguagesAsync();
            var currentLanguage = languages.FindByCulture(
                CultureInfo.CurrentCulture.Name,
                CultureInfo.CurrentUICulture.Name
            );

            var model = new LanguageSelectionViewModel
            {
                CurrentLanguage = currentLanguage,
                Languages = languages.Where(l =>l != currentLanguage).ToList()
                .ToList(),
                CurrentUrl = Request.Path
            };

            return View("~/Themes/AdminLTE/Components/AccountLanguages/Default.cshtml", model); ;
        }
    }
}
