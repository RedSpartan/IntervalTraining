using System;
using System.Globalization;
using Xamarin.Forms;

namespace RedSpartan.IntervalTraining.UI.Mobile.Shared.Converter
{
    public class TimespanToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var result = string.Empty;
            if(value is TimeSpan timeSpan)
            {
                result = timeSpan.ToString(@"mm\:ss");
            }
            return result;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
