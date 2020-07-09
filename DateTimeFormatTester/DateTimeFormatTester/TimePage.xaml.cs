using System;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Essentials;

namespace DateTimeFormatTester
{
	public partial class TimePage : ContentPage
	{
		public ICommand TapCommand => new Command<string>(async (url) => await Launcher.OpenAsync(url));

		public TimePage()
		{
			InitializeComponent();
			BindingContext = this;
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
			//timepicker.Time = new TimeSpan(8, 10, 00);
		}

		void timeformat_Completed(System.Object sender, System.EventArgs e)
		{
			var text = ((Entry)sender).Text;
			timepicker.Format = text;
		}

        void timeSetting_Completed(System.Object sender, System.EventArgs e)
        {
			var time = ((Entry)sender).Text;
			string[] divided = time.Split(',');
			int[] sep_times = Array.ConvertAll(divided, int.Parse);
			timepicker.Time = new TimeSpan(sep_times[0], sep_times[1], 00);
        }
    }
}