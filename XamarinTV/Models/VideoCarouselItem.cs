using Xamarin.Forms;

namespace XamarinTV.Models
{
    public class VideoCarouselItem : BindableObject
    {
        Video _video;
        double _scale;

        public VideoCarouselItem(Video video)
        {
            Video = video;
            Scale = 1;
        }

        public Video Video
        {
            get { return _video; }
            set
            {
                _video = value;
                OnPropertyChanged();
            }
        }
        
        public double Scale
        {
            get { return _scale; }
            set
            {
                _scale = value;
                OnPropertyChanged();
            }
        }
    }
}