using XamarinTV.Models;
using XamarinTV.ViewModels.Base;
using XamarinTV.Views;
using System;
using Xamarin.Forms;
using Xamarin.Forms.DualScreen;

namespace XamarinTV.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        BaseViewModel _pane1;
        BaseViewModel _pane2;

        readonly Lazy<BrowseVideosViewModel> _browseVideosViewModel;
        readonly Lazy<SearchVideosViewModel> _searchVideosViewModel;
        readonly Lazy<VideoPlayerViewModel> _videoPlayerViewModel;
        readonly Lazy<SettingsViewModel> _settingsViewModel;
        readonly Lazy<VideoDetailViewModel> _videoDetailViewModel;

        static readonly Lazy<MainViewModel> _mainViewModel = new Lazy<MainViewModel>(() => new MainViewModel());

        public static MainViewModel Instance => _mainViewModel.Value;
        public BrowseVideosViewModel BrowseVideosViewModel => _browseVideosViewModel.Value;
        public SearchVideosViewModel SearchVideosViewModel => _searchVideosViewModel.Value;
        public VideoPlayerViewModel VideoPlayerViewModel => _videoPlayerViewModel.Value;
        public SettingsViewModel SettingsViewModel => _settingsViewModel.Value;
        public VideoDetailViewModel VideoDetailViewModel => _videoDetailViewModel.Value;

        TwoPaneViewTallModeConfiguration _tallModeConfiguration;
        TwoPaneViewWideModeConfiguration _wideModeConfiguration;
        TwoPaneViewMode _twoPaneViewMode;
        double _minWideModeWidth;
        double _minTallModeHeight;
        private GridLength _pane1Length;
        private GridLength _pane2Length;
        bool _settingsActive = false;

        public Command<Video> PlayVideoCommand { get; }
        public Command OpenSettingWindowCommand { get; }

        private MainViewModel()
        {
            _videoDetailViewModel = new Lazy<VideoDetailViewModel>(OnCreateVideoDetailsViewModel);
            _browseVideosViewModel = new Lazy<BrowseVideosViewModel>(OnCreateBrowseVideosViewModel);
            _searchVideosViewModel = new Lazy<SearchVideosViewModel>(OnCreateSearchVideosViewModel);
            _videoPlayerViewModel = new Lazy<VideoPlayerViewModel>(OnCreateVideoPlayerViewModel);
            _settingsViewModel = new Lazy<SettingsViewModel>(OnCreateSettingsViewModel);
            PlayVideoCommand = new Command<Video>(OnPlayVideo);
            OpenSettingWindowCommand = new Command(OpenSettingWindow);
            UpdateLayouts();
        }

        public BaseViewModel Pane1
        {
            get => _pane1;
            set
            {
                if (SetProperty(ref _pane1, value))
                {
                    _pane1?.OnDisappearing();
                    value?.OnAppearing();
                }
            }
        }

        public BaseViewModel Pane2
        {
            get => _pane2;
            set
            {
                if (SetProperty(ref _pane2, value))
                {
                    _pane2?.OnDisappearing();
                    value?.OnAppearing();
                    UpdateLayouts();
                }
            }
        }

        public TwoPaneViewTallModeConfiguration TallModeConfiguration
        {
            get => _tallModeConfiguration;
            set => SetProperty(ref _tallModeConfiguration, value);
        }

        public TwoPaneViewWideModeConfiguration WideModeConfiguration
        {
            get => _wideModeConfiguration;
            set => SetProperty(ref _wideModeConfiguration, value);
        }

        public TwoPaneViewMode TwoPaneViewMode
        {
            get => _twoPaneViewMode;
            set
            {
                if (SetProperty(ref _twoPaneViewMode, value))
                {
                    UpdateLayouts();
                }
            }
        }

        public double MinWideModeWidth
        {
            get => _minWideModeWidth;
            set => SetProperty(ref _minWideModeWidth, value);
        }

        public double MinTallModeHeight
        {
            get => _minTallModeHeight;
            set => SetProperty(ref _minTallModeHeight, value);
        }

        public GridLength Pane1Length
        {
            get => _pane1Length;
            set => SetProperty(ref _pane1Length, value);
        }

        public GridLength Pane2Length
        {
            get => _pane2Length;
            set => SetProperty(ref _pane2Length, value);
        }

        public bool DeviceIsSpanned => DualScreenInfo.Current.SpanMode != TwoPaneViewMode.SinglePane;

        public void UpdateLayouts()
        {
            if (VideoPlayerViewModel.Video != null)
            {
                if(Device.RuntimePlatform == Device.UWP)
                    MinTallModeHeight = 800;
                else
                    MinTallModeHeight = 600;

                MinWideModeWidth = 4000;
                Pane1Length = GridLength.Auto;
                Pane2Length = GridLength.Star;

                if(_settingsActive)
                    Pane1 = SettingsViewModel;
                else
                    Pane1 = VideoPlayerViewModel;

                Pane2 = VideoDetailViewModel;
                TallModeConfiguration = TwoPaneViewTallModeConfiguration.TopBottom;
                
                if (!DeviceIsSpanned)
                    WideModeConfiguration = TwoPaneViewWideModeConfiguration.SinglePane;
                else
                    WideModeConfiguration = TwoPaneViewWideModeConfiguration.LeftRight;
            }
            else
            {
                Pane1Length = new GridLength(2, GridUnitType.Star);
                Pane2Length = new GridLength(3, GridUnitType.Star);
                MinTallModeHeight = 0;
                MinWideModeWidth = 4000;

                if (_settingsActive)
                    Pane1 = SettingsViewModel;
                else
                    Pane1 = BrowseVideosViewModel;

                Pane2 = SearchVideosViewModel;

                if (!DeviceIsSpanned)
                {
                    TallModeConfiguration = TwoPaneViewTallModeConfiguration.SinglePane;
                    WideModeConfiguration = TwoPaneViewWideModeConfiguration.SinglePane;
                }
                else
                {
                    TallModeConfiguration = TwoPaneViewTallModeConfiguration.TopBottom;
                    WideModeConfiguration = TwoPaneViewWideModeConfiguration.LeftRight;
                }
            }
        }

        public override void OnAppearing()
        {
            base.OnAppearing();

            Pane1?.OnAppearing();
            Pane2?.OnAppearing();
        }

        BrowseVideosViewModel OnCreateBrowseVideosViewModel()
        {
            BrowseVideosViewModel viewModel = new BrowseVideosViewModel();
            return viewModel;
        }

        VideoDetailViewModel OnCreateVideoDetailsViewModel()
        {
            VideoDetailViewModel viewModel = new VideoDetailViewModel();

            viewModel.PropertyChanged += (sender, args) =>
            {
                if (args.PropertyName == nameof(VideoDetailViewModel.Volume))
                    VideoPlayerViewModel.Volume = VideoDetailViewModel.Volume;
            };

            return viewModel;
        }

        SearchVideosViewModel OnCreateSearchVideosViewModel()
        {
            SearchVideosViewModel viewModel = new SearchVideosViewModel();
            return viewModel;
        }

        VideoPlayerViewModel OnCreateVideoPlayerViewModel()
        {
            VideoPlayerViewModel viewModel = new VideoPlayerViewModel
            {
                CloseCommand = new Command(OnClosePlayingVideo)
            };
            return viewModel;
        }

        void OnClosePlayingVideo(object obj)
        {
            VideoPlayerViewModel.Video = null;
            VideoDetailViewModel.SelectedVideo = null;

            UpdateLayouts();
        }

        void OnPlayVideo(Video video)
        {
            VideoPlayerViewModel.Video = video;
            VideoDetailViewModel.SelectedVideo = video;
            UpdateLayouts();
        }

        SettingsViewModel OnCreateSettingsViewModel()
        {
            var viewModel = new SettingsViewModel();
            viewModel.CloseCommand = new Command(() =>
            {
                _settingsActive = false;
                UpdateLayouts();
            });
            return viewModel;
        }

        public void OpenSettingWindow()
        {
            _settingsActive = true;
            UpdateLayouts();
        }

        public void OpenVideoPlayerWindow(Video video)
        {
            OnPlayVideo(video);
        }
    }
}