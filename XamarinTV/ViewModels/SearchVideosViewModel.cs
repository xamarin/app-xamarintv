using System.Collections.ObjectModel;
using XamarinTV.Models;
using XamarinTV.Services;
using XamarinTV.ViewModels.Base;

namespace XamarinTV.ViewModels
{
    public class SearchVideosViewModel : BaseViewModel
    {
        ObservableCollection<string> _recentSearches;
        ObservableCollection<Video> _mostSearchedVideos;

        public SearchVideosViewModel()
        {
            LoadRecentSearchesAsync();
        }

        public ObservableCollection<string> RecentSearches
        {
            get { return _recentSearches; }
            set { SetProperty(ref _recentSearches, value); }
        }

        public ObservableCollection<Video> MostSearchedVideos
        {
            get { return _mostSearchedVideos; }
            set { SetProperty(ref _mostSearchedVideos, value); }
        }

        async void LoadRecentSearchesAsync()
        {
            IsBusy = true;

            var recentSearches = await FakeXamarinTvService.Instance.GetRecentSearchesAsync();
            var mostSearchedVideos = await FakeXamarinTvService.Instance.GetVideosAsync();

            RecentSearches = new ObservableCollection<string>();
            MostSearchedVideos = new ObservableCollection<Video>();

            foreach (var search in recentSearches)
            {
                RecentSearches.Add(search);
            }

            foreach (var video in mostSearchedVideos)
            {
                MostSearchedVideos.Add(video);
            }

            IsBusy = false;
        }
    }
}