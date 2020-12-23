using System;
using Xamarin.Forms;

namespace OpenStandup.Mobile.Converters
{
    public class StringToUriImageSourceConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (!(value is string imageName))
            {
                return null;
            }

            if (!(parameter is string imagePath))
            {
                return null;
            }

            return new UriImageSource
            {
                Uri = new Uri($"{imagePath}/{imageName}"),
                CachingEnabled = true,
                CacheValidity = new TimeSpan(5, 0, 0, 0)
            };
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
