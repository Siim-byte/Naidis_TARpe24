using Naidis_TARpe24.Resources.Localization;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace Naidis_TARpe24.Services
{
    public static class LanguageService
    {
        public static event Action? LanguageChanged;

        public static void ChangeLanguage(string languageCode)
        {
            var culture = new CultureInfo(languageCode);
            CultureInfo.DefaultThreadCurrentCulture = culture;
            CultureInfo.DefaultThreadCurrentUICulture = culture;

            AppResources.Culture = culture;
            LanguageChanged?.Invoke();
        }
    }
}
