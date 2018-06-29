using System;
using System.Globalization;
using System.Windows.Data;

namespace PaySys.ModelAndBindLib.BindingConverter
{
	public class DateFormatterConverter:IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (value == null )
				return string.Empty;
			if ( value.ToString().Length < 8)
				return string.Empty;
			var date = value.ToString();
			var year = date.Substring(0,4);
			var month = date.Substring(4,2);
			var day = date.Substring(6,2);
			return $"{year}/{month}/{day}";
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (value == null)
				return string.Empty;
			var pureDate = value.ToString().Replace(@"/", string.Empty);
			return pureDate.Length < 8 ? string.Empty : pureDate;
		}
	}
}
