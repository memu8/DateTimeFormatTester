using Xamarin.Forms;
using Xamarin.Essentials;
namespace DateTimeFormatTester
{
	public partial class DatePage : ContentPage
	{
		public DatePage()
		{
			InitializeComponent();
			var dep = DependencyService.Get<ILocalize>();
			if (dep != null)
			{
				datephoneculture.Text = "Phone Culture: " + dep.GetCurrentCultureInfo();
			}
			else
			{
				var s = System.Globalization.CultureInfo.CurrentCulture.Name;
				datephoneculture.Text = "Phone Culture: " + s;
			}
		}
		void dateformat_Completed(System.Object sender, System.EventArgs e)
		{
			var text = ((Entry)sender).Text;
			datepicker.Format = text;
		}
	}
}