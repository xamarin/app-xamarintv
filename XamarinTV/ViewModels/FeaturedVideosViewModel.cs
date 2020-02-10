using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using XamarinTV.Models;
using XamarinTV.Services;
using XamarinTV.ViewModels.Base;

namespace XamarinTV.ViewModels
{
    public class FeaturedVideosViewModel : BaseViewModel
    {
        static bool _isFirstLoaded;
        ObservableCollection<VideoGroup> _videos;

        public FeaturedVideosViewModel()
        {
            LoadFeaturedVideosAsync();
        }

        public ObservableCollection<VideoGroup> Videos
        {
            get { return _videos; }
            set { SetProperty(ref _videos, value); }
        }

        public async void LoadFeaturedVideosAsync()
        {
            if (!_isFirstLoaded)
                IsBusy = true;

            var videoGroups = FakeXamarinTvService.Instance.GetVideoGroups();

            Videos = new ObservableCollection<VideoGroup>();

            foreach (var videoGroup in videoGroups)
            {
                Videos.Add(videoGroup);
            }

            if (!_isFirstLoaded)
            {
                await Task.Delay(GetRandomLoadingTime());

                IsBusy = false;
            }

            _isFirstLoaded = true;
        }

        int GetRandomLoadingTime()
        {
            var random = new Random();
            return random.Next(3000, 5000);
        }
    }
}