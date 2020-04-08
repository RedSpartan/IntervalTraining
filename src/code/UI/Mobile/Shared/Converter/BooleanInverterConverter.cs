using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace RedSpartan.IntervalTraining.UI.Mobile.Shared.Converter
{
    [Preserve(AllMembers = true)]
    public class BooleanInverterConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool b)
                return !b;
            throw new ArgumentException("No a boolean value in value");
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool b)
                return !b;
            throw new ArgumentException("No a boolean value in value");
        }
    }
}
