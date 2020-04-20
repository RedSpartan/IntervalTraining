using System;
using System.Globalization;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace RedSpartan.IntervalTraining.UI.Mobile.Shared.Converter
{
    [Preserve(AllMembers = true)]
    public class BooleanToTransparency : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if(value is bool isTrue && parameter is Color color)
            {
                if (isTrue)
                {
                    return Color.FromHex("#003333");
                }
                else
                {
                    var transparent = color.MultiplyAlpha(0.2);
                    return transparent;
                }
            }
            throw new NotImplementedException();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
