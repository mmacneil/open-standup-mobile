using System;
using Xamarin.Forms;

namespace OpenStandup.Mobile.Converters
{
    public class UserIdIsMeBoolConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (!(value is string otherUserId))
            {
                return null;
            }

            if (!(parameter is string myUserId))
            {
                return null;
            }

            return otherUserId == myUserId;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
