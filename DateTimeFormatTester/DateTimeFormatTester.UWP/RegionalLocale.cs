using System;
using Xamarin.Forms;
using System.Globalization;

[assembly: Dependency(typeof(DateTimeFormatTester.UWP.RegionalLocale))]
namespace DateTimeFormatTester.UWP
{
    public class RegionalLocale : ILocalize
    {
        public string GetCurrentCultureInfo()
        { 
            string UWPLocale = Windows.System.UserProfile.GlobalizationPreferences.HomeGeographicRegion;
            string UWPLanguage = Windows.System.UserProfile.GlobalizationPreferences.Languages[0].ToString().Substring(0,2);
            return UWPLanguage + "-" + UWPLocale;
        }
    }
}