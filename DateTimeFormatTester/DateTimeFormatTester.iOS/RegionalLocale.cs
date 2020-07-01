using System;
using Xamarin.Forms;
using System.Globalization;
using Foundation;
[assembly: Dependency(typeof(DateTimeFormatTester.iOS.RegionalLocale))]
namespace DateTimeFormatTester.iOS
{
    public class RegionalLocale : ILocalize
    {
        public string GetCurrentCultureInfo()
        {
            string iOSLocale = NSLocale.CurrentLocale.CountryCode;
            string iOSLanguage = NSLocale.CurrentLocale.LanguageCode;
            return iOSLanguage + "-" + iOSLocale;
        }
    }
}