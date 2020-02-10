using System.Collections.ObjectModel;
using XamarinTV.Models;
using XamarinTV.Services;
using XamarinTV.ViewModels.Base;

namespace XamarinTV.ViewModels
{
    public class TrendingViewModel : BaseViewModel
    {
        ObservableCollection<Video> _videos;

        public TrendingViewModel()
        {
            LoadTrendingVideosAsync();
        }

        public ObservableCollection<Video> Videos
        {
            get { return _videos; }
            set { SetProperty(ref _videos, value); }
        }

        async void LoadTrendingVideosAsync()
        {
            IsBusy = true;

            var videos = await FakeXamarinTvService.Instance.GetPopularAsync();
            Videos = new ObservableCollection<Video>(videos);

            IsBusy = false;
        }
    }
}