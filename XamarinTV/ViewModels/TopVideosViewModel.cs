using System.Collections.ObjectModel;
using System.Windows.Input;
using XamarinTV.Models;
using XamarinTV.Services;
using XamarinTV.ViewModels.Base;
using Xamarin.Forms;

namespace XamarinTV.ViewModels
{
    public class TopVideosViewModel : BaseViewModel
    {
        ObservableCollection<VideoCarouselItem> _topVideos;
        VideoCarouselItem _current;

        public TopVideosViewModel()
        {
            LoadTopVideosAsync();
        }

        public ObservableCollection<VideoCarouselItem> TopVideos
        {
            get { return _topVideos; }
            set { SetProperty(ref _topVideos, value); }
        }

        public ICommand VideoPlayerCommand => new Command(OpenVideo);

        public VideoCarouselItem Current
        {
            get { return _current; }
            set { SetProperty(ref _current, value); }
        }

        async void LoadTopVideosAsync()
        {
            var topVideos = await FakeXamarinTvService.Instance.GetPopularAsync();

            TopVideos = new ObservableCollection<VideoCarouselItem>();

            foreach (var video in topVideos)
            {
                TopVideos.Add(new VideoCarouselItem(video));
            }
        }

        void OpenVideo()
        {
            if (Current != null)
                MainViewModel.Instance.OpenVideoPlayerWindow(Current.Video);
        }
    }
}