using System;
using System.Globalization;
using Xamarin.Forms;

namespace XamarinTV.Converters
{
    public class VideoSourceConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return null;

            if (String.IsNullOrWhiteSpace(value.ToString()))
                return null;

            if(Device.RuntimePlatform == Device.UWP)
                return new Uri($"ms-appx:///Assets/{value}");
            else
                return new Uri($"ms-appx:///{value}");
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
