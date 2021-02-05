using System;
using System.Collections;
using OpenStandup.Common.Extensions;
using Xamarin.Forms;

namespace OpenStandup.Mobile.Converters
{
    public class CollectionToBoolConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value is ICollection collection)
            {
                return !collection.IsNullOrEmpty();
            }

            return false;
        }
        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
