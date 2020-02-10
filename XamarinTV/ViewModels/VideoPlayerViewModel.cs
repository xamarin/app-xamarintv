using System.Windows.Input;
using XamarinTV.Models;
using XamarinTV.ViewModels.Base;

namespace XamarinTV.ViewModels
{
    public class VideoPlayerViewModel : BaseViewModel
    {
        Video _video;
        double _volume;

        public VideoPlayerViewModel()
        {
            Volume = 0.2d;
        }

        public Video Video
        {
            get { return _video; }
            set
            {
                if(SetProperty(ref _video, value))
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

        public ICommand CloseCommand { get; set; }
    }
}