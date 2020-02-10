using System;
using System.Collections.ObjectModel;
using XamarinTV.Models;
using XamarinTV.Services;
using XamarinTV.ViewModels.Base;

namespace XamarinTV.ViewModels
{
    public class RelatedContentViewModel : BaseViewModel
    {
        ObservableCollection<Video> _videos;

        public RelatedContentViewModel()
        {
            LoadVideosByListNameAsync("Xamarin 101");
        }

        public ObservableCollection<Video> Videos
        {
            get { return _videos; }
            set { SetProperty(ref _videos, value); }
        }

        async void LoadVideosByListNameAsync(string listName)
        {
            IsBusy = true;

            var videoGroups = await FakeXamarinTvService.Instance.GetVideosByListNameAsync(listName);

            Videos = new ObservableCollection<Video>();

            foreach (var videoGroup in videoGroups)
            {
                Videos.Add(videoGroup);
            }

            IsBusy = false;
        }
    }
}