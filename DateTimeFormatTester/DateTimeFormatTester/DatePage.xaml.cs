using Xamarin.Forms;
using Xamarin.Essentials;
using System.Windows.Input;

namespace DateTimeFormatTester
{
	public partial class DatePage : ContentPage
	{
		public ICommand TapCommand => new Command<string>(async (url) => await Launcher.OpenAsync(url));

		public DatePage()
		{
			InitializeComponent();
			BindingContext = this;
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