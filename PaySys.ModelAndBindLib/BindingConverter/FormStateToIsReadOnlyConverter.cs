using System;
using System.Globalization;
using System.Windows.Data;
using PaySys.ModelAndBindLib.Entities;
using PaySys.ModelAndBindLib.Enums;

namespace PaySys.ModelAndBindLib.BindingConverter
{
	public class FormStateToIsReadOnlyConverter:IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			var currentState = (FormCurrentState) value;
			return !(currentState == FormCurrentState.Edit || currentState == FormCurrentState.Add);
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}
