using Xamarin.Forms;
using System.Globalization;
namespace DateTimeFormatTester
{
	public class App : Application
	{
		public App()
		{
			MainPage = new DateTimeFormatTester.MainPage();
		}
		protected override void OnStart()
		{
			// Handle when your app starts
		}
		protected override void OnSleep()
		{
			// Handle when your app sleeps
		}
		protected override void OnResume()
		{
			// Handle when your app resumes
		}
	}
	public interface ILocalize
	{
		string GetCurrentCultureInfo();
	}
}