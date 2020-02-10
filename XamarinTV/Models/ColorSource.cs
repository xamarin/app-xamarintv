using Xamarin.Forms;

namespace XamarinTV.Models
{
    public class ColorSource : BindableObject
    {
        Color _color;
        bool _isSelected;

        public ColorSource(Color color)
        {
            Color = color;
        }

        public Color Color
        {
            get => _color;
            set
            {
                _color = value;
                OnPropertyChanged();
            }
        }

        public bool IsSelected
        {
            get => _isSelected;
            set
            {
                _isSelected = value;
                OnPropertyChanged();
            }
        }
    }
}