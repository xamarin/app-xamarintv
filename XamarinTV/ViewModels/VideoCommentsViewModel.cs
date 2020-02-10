using System.Collections.ObjectModel;
using XamarinTV.Models;
using XamarinTV.Services;
using XamarinTV.ViewModels.Base;

namespace XamarinTV.ViewModels
{
    public class VideoCommentsViewModel : BaseViewModel
    {
        ObservableCollection<VideoComment> _comments;

        public VideoCommentsViewModel()
        {
            LoadCommentsAsync();
        }

        public ObservableCollection<VideoComment> Comments
        {
            get { return _comments; }
            set { SetProperty(ref _comments, value); }
        }

        async void LoadCommentsAsync()
        {
            IsBusy = true;

            var comments = await FakeXamarinTvService.Instance.GetVideoCommentsAsync(1);
            Comments = new ObservableCollection<VideoComment>(comments);

            IsBusy = false;
        }
    }
}