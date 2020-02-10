using XamarinTV.Views;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.PlatformConfiguration.WindowsSpecific;
using FormsApplication = Xamarin.Forms.Application;

namespace XamarinTV
{
    public partial class App : FormsApplication
    {
        static MainPage _mainPage;
        public App()
        {
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
