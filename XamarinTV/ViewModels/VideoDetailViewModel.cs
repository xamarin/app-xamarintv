using System;
using Xamarin.Forms;
using XamarinTV.Models;
using XamarinTV.ViewModels.Base;

namespace XamarinTV.ViewModels
{
    public class VideoDetailViewModel : BaseViewModel
    {
        Video _selectedVideo;
        int _selectedViewModelIndex;
        bool _isMuted;
        double _previousVolume;
        double _volume;

        public Command MuteVideo { get; }

        public VideoDetailViewModel()
        {
            MuteVideo = new Command(OnMuteVideo);
            Volume = 0.2d;
        }

        void OnMuteVideo(object obj)
        {
            if(_isMuted)
            {
                Volume = _previousVolume;
                _isMuted = false;
            }
            else
            {
                _previousVolume = Volume;
                _isMuted = true;
                Volume = 0d;
            }
        }

        public double Volume
        {
            get => _volume;
            set
            {
                if(SetProperty(ref _volume, value) && value > 0d)
                {
                    _isMuted = false;
                }
            }
        }

        public Video SelectedVideo
        {
            get => _selectedVideo;
            set => SetProperty(ref _selectedVideo, value);
        }

        public string Title
        {
            get { return _selectedVideo.Title; }
        }

        public string ViewCount
        {
            get { return $"{_selectedVideo.ViewCount} views"; }
        }

        public string Description
        {
            get { return _selectedVideo.Description; }
        }

        public int SelectedViewModelIndex
        {
            get { return _selectedViewModelIndex; }
            set { SetProperty(ref _selectedViewModelIndex, value); }
        }
    }
}