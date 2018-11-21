using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using PaySys.ModelAndBindLib.Entities;
using PaySys.ModelAndBindLib.Enums;

namespace PaySys.ModelAndBindLib.BindingConverter
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
