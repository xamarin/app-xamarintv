using XamarinTV.Services;
using System;
using System.Globalization;
using Xamarin.Forms;

namespace XamarinTV.Converters
{
    public class ViewModelToViewConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return null;

            return NavigationService.Instance.CreateAndBind(value);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}