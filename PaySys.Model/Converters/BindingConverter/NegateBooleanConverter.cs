﻿using System;
using System.Windows.Data;

namespace PaySys.Model.BindingConverter
{
	[ValueConversion(typeof(bool),typeof(bool))]
	public class NegateBooleanConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			return !(bool)value;
		}
		public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			return !(bool)value;
		}
	}
}
