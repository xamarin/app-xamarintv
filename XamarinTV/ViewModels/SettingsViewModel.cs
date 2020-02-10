using System;
using System.Collections.Generic;
using System.Windows.Input;
using XamarinTV.Models;
using XamarinTV.Styles;
using XamarinTV.ViewModels.Base;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace XamarinTV.ViewModels
{
    public class SettingsViewModel : BaseViewModel
    {
        bool _isDarkMode;
        List<OpenSource> _openSourceList;

        public SettingsViewModel()
        {
            LoadThemesSettings();
            LoadOpenSourceList();
        }

        public bool IsDarkMode
        {
            get { return _isDarkMode; }
            set { SetProperty(ref _isDarkMode, value); }
        }

        public List<OpenSource> OpenSourceList
        {
            get { return _openSourceList; }
            set { SetProperty(ref _openSourceList, value); }
        }

        public ICommand ThemeLigthCommand => new Command(ThemeLigth);

        public ICommand ThemeDarkCommand => new Command(ThemeDark);

        public ICommand ViewCodeCommand => new Command(ViewCode);

        public ICommand OpenGitHubCommand => new Command<string>(OpenGitHub);

        public ICommand CloseCommand { get; set; }

        void ThemeLigth()
        {
            IsDarkMode = false;
            Application.Current.Resources = new LightTheme();
            App.AppTheme = "light";
        }

        void ThemeDark()
        {
            IsDarkMode = true;
            Application.Current.Resources = new DarkTheme();
            App.AppTheme = "dark";
        }

        void LoadThemesSettings()
        {
            IsDarkMode = true;
        }

        void LoadOpenSourceList()
        {
            OpenSourceList = new List<OpenSource>()
            {
                new OpenSource { Title = "Xamarin.Forms", Url = "https://docs.microsoft.com/xamarin/xamarin-forms/" },
                new OpenSource { Title = "Xamarin.Essentials", Url = "https://github.com/xamarin/Essentials" },
                new OpenSource { Title = "FFImageLoading", Url = "https://github.com/luberda-molinet/FFImageLoading" },
                new OpenSource { Title = "PancakeView", Url = "https://github.com/sthewissen/Xamarin.Forms.PancakeView" },
                new OpenSource { Title = "Sharpnado", Url = "https://github.com/roubachof/Sharpnado.Presentation.Forms" },
                new OpenSource { Title = "StateSquid", Url = "https://github.com/sthewissen/Xamarin.Forms.StateSquid" },
                new OpenSource { Title = "Xamanimation", Url = "https://github.com/jsuarezruiz/Xamanimation" },
            };
        }

        void ViewCode()
        {
            // Open GitHub with Xamarin.Essentials' Browser API
            string url = "https://github.com/microsoft/surface-duo-sdk-xamarin-samples";
            Browser.OpenAsync(new Uri(url));
        }

        void OpenGitHub(string url)
        {
            Browser.OpenAsync(new Uri(url));
        }
    }
}