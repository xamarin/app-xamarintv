using System;
using System.Collections.ObjectModel;
using XamarinTV.Models;
using XamarinTV.Services;
using XamarinTV.ViewModels.Base;

namespace XamarinTV.ViewModels
{
    public class KeepWatchingViewModel : BaseViewModel
    {
        ObservableCollection<SavedVideo> _videos;

        public KeepWatchingViewModel()
        {
            LoadKeepWatchingVideosAsync();
        }

        public ObservableCollection<SavedVideo> Videos
        {
            get { return _videos; }
            set { SetProperty(ref _videos, value); }
        }

        async void LoadKeepWatchingVideosAsync()
        {
            IsBusy = true;

            var videos = await FakeXamarinTvService.Instance.GetLatestAsync();
            Videos = new ObservableCollection<SavedVideo>();

            foreach (var video in videos)
            {
                Videos.Add(new SavedVideo
                {
                    Video = video,
                    Viewed = GetRandomWatchedTime(video)
                });
            }

            IsBusy = false;
        }

        int GetRandomWatchedTime(Video video)
        {
            var duration = video.Duration;
            var random = new Random();
            int watchedTime = random.Next(duration);

            return watchedTime;
        }
    }
}