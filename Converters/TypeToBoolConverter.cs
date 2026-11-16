using System;
using System.Globalization;
using Microsoft.Maui.Controls;

namespace FITFINAL.Converters
{
    public class TypeToBoolConverter : IValueConverter
    {
        // true si es gasto → muestra categoría
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var type = value?.ToString();
            return type == "Gasto";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) =>
            throw new NotImplementedException();
    }
}
