using System;
using System.Globalization;
using System.Windows.Data;
using PaySys.Model.Entities;

namespace PaySys.Model.BindingConverter
{
	public class ContractFieldDisplayConverter : IValueConverter
	{
		/// <summary>
		/// Used for displaying contract fields in ListView
		/// </summary>
		/// <param name="value"></param>
		/// <param name="targetType"></param>
		/// <param name="parameter"></param>
		/// <param name="culture"></param>
		/// <returns></returns>
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			var contractField = value as ContractField;
			if (contractField == null)
				return string.Empty;
			var result=string.Empty;
			result += $"{contractField.Title}";
			result += "  [";
			result += $"{contractField.CurrentExpenseArticle.Code}";
			result += "]";
			return result;
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
		
	}
}