using System.Windows.Input;
using XamarinTV.Models;
using XamarinTV.ViewModels.Base;

namespace XamarinTV.ViewModels
{
    public class VideoPlayerViewModel : BaseViewModel
    {
        Video _video;
        double _volume;
        bool _isPlaying;

        public VideoPlayerViewModel()
        {
            Volume = 0.2d;
            IsPlaying = true;
        }

        public Video Video
        {
            get { return _video; }
            set
            {
                if (SetProperty(ref _video, value))
                {
                    OnPropertyChanged(nameof(VideoSource));
                }
            }
        }


        public double Volume
        {
            get => _volume;
            set
            {
                SetProperty(ref _volume, value);
            }
        }

        public string VideoSource
        {
            get
            {
                if (Video == null)
                    return null;

                return "video.mp4";
            }
        }

        public bool IsPlaying
        {
            get => _isPlaying;
            set
            {
                SetProperty(ref _isPlaying, value);
            }
        }

        public ICommand CloseCommand { get; set; }
    }
}