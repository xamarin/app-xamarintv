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

        TwoPaneViewMode _twoPaneViewMode;
        bool _settingsActive = false;
        private bool spannedWithVideo;
        private bool notSpannedWithVideo;
        private bool spannedWithoutVideo;
        private bool notSpannedWithoutVideo;

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

        public bool DeviceIsSpanned => DualScreenInfo.Current.SpanMode != TwoPaneViewMode.SinglePane;

        public bool SpannedWithVideo { get => spannedWithVideo; set => SetProperty(ref spannedWithVideo, value); }
        public bool NotSpannedWithVideo { get => notSpannedWithVideo; set => SetProperty(ref notSpannedWithVideo, value); }
        public bool SpannedWithoutVideo { get => spannedWithoutVideo; set => SetProperty(ref spannedWithoutVideo, value); }
        public bool NotSpannedWithoutVideo { get => notSpannedWithoutVideo; set => SetProperty(ref notSpannedWithoutVideo, value); }

        public void UpdateLayouts()
        {
            SpannedWithVideo = VideoPlayerViewModel.Video != null && DeviceIsSpanned;
            NotSpannedWithVideo = VideoPlayerViewModel.Video != null && !DeviceIsSpanned;
            SpannedWithoutVideo = VideoPlayerViewModel.Video == null && DeviceIsSpanned;
            NotSpannedWithoutVideo = VideoPlayerViewModel.Video == null && !DeviceIsSpanned;

            if (VideoPlayerViewModel.Video != null)
            {
                if (_settingsActive)
                    Pane1 = SettingsViewModel;
                else
                    Pane1 = VideoPlayerViewModel;

                Pane2 = VideoDetailViewModel;
            }
            else
            {
                if (_settingsActive)
                    Pane1 = SettingsViewModel;
                else
                    Pane1 = BrowseVideosViewModel;

                Pane2 = SearchVideosViewModel;
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