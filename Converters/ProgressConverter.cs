using System;
using System.Globalization;
using Microsoft.Maui.Controls;

namespace FITFINAL.Converters
{
    public class ProgressConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                // progress
                decimal progress = System.Convert.ToDecimal(value);

                // target (viene como string o BindingExpression)
                decimal target = System.Convert.ToDecimal(parameter);

                if (target > 0)
                    return (double)(progress / target);
            }
            catch
            {
                return 0;
            }

            return 0;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            => throw new NotImplementedException();
    }
}