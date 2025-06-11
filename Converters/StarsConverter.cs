using System;
using System.Globalization;
using Microsoft.Maui.Controls;

namespace MovieSwipeApp.Converters
{
    public class StarsConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int r = (int)(value ?? 0);
            r = Math.Clamp(r, 0, 5);
            return new string('★', r) + new string('☆', 5 - r);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            => 0;
    }
}
