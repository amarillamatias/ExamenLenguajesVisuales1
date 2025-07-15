using System;
using System.Globalization;
using System.Windows.Data;

namespace ExamenLenguajesVisuales1.Converters
{
    public class NullToEmptyConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value ?? string.Empty;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return string.IsNullOrWhiteSpace(value as string) ? null : value;
        }
    }
}
