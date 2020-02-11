using XamarinTV.Views;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.PlatformConfiguration.WindowsSpecific;
using FormsApplication = Xamarin.Forms.Application;
using System.Collections.Generic;

namespace XamarinTV
{
    public partial class App : FormsApplication
    {
        static MainPage _mainPage;
        public App()
        {
            Xamarin.Forms.Device.SetFlags(new List<string>() { "StateTriggers_Experimental", "IndicatorView_Experimental", "CarouselView_Experimental", "MediaElement_Experimental" });

            InitializeComponent();

            if (_mainPage != null)
            {
                _mainPage.BindingContext = null;
                _mainPage.Content = null;
                _mainPage = null;
            }

            _mainPage = new MainPage();
            MainPage = _mainPage;


            On<Windows>().SetImageDirectory("Assets");
        }

        public static string AppTheme { get; set; }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
