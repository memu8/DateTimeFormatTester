using System;
using Xamarin.Forms;
namespace DateTimeFormatTester
{
	public partial class TimePage : ContentPage
	{
		public TimePage()
		{
			InitializeComponent();
			var dep = DependencyService.Get<ILocalize>();
			if (dep != null)
			{
				timephoneculture.Text = "Phone Culture: " + dep.GetCurrentCultureInfo();
			}
			else
			{
				var s = System.Globalization.CultureInfo.CurrentCulture.Name;
				timephoneculture.Text = "Phone Culture: " + s;
			}
		}

		void timeformat_Completed(System.Object sender, System.EventArgs e)
		{
			var text = ((Entry)sender).Text;
			timepicker.Format = text;
		}
	}
}