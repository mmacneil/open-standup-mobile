using CleanXF.SharedKernel.Extensions;
using System;
using Xamarin.Forms;

namespace CleanXF.Mobile.Converters
{
    public class StringBooleanConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return !((string)value).IsNullOrEmpty();
        }
        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
