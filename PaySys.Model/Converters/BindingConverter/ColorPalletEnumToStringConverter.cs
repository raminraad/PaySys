using System;
using System.Globalization;
using System.Windows.Data;
using PaySys.Model.Enums;

namespace PaySys.Model.BindingConverter
{
	public class ColorPalletEnumToStringConverter:IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			return value != null ? Enum.GetName(typeof(ColorPallet), value) : "";
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}
