using System;
using System.Globalization;
using System.Windows.Data;

namespace PaySys.Model.BindingConverter
{
    public class MathAddWithParameterConverter:IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var doubleValue = System.Convert.ToDouble(value);
            var doubleParameter = System.Convert.ToDouble(parameter);
            return doubleValue + doubleParameter;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
