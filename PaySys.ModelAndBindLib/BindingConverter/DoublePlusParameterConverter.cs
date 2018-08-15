using System;
using System.Windows.Data;
using Arash;

namespace PaySys.ModelAndBindLib.BindingConverter
{
	/// <summary>
	/// Converts DateTime values to PersianDate values and vice versa, to be used in XAML
	/// </summary>
	[ValueConversion(typeof(DateTime), typeof(PersianDate))]
	public class DoublePlusParameterConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			if( value == null )
				value = 0;
			if( parameter == null )
				parameter = 0;
			return ( (double) value + double.Parse( parameter.ToString() )  );
		}

		public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			if (value == null) return null;
			PersianDate pDate = (PersianDate)value;
			return pDate.ToDateTime();
		}
	}

}
