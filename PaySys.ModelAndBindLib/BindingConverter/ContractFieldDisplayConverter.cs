using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using Bogus.Extensions;
using PaySys.Globalization;
using PaySys.ModelAndBindLib.Model;

namespace PaySys.ModelAndBindLib.BindingConverter
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
			var contractField = value as SubGroupContractField;
			if (contractField == null)
				return string.Empty;
			var result=string.Empty;
			result += $"{contractField.ContractFieldTitle.Title}";
			result += "  [";
			result += $"{contractField.CurrentExpenseArticle.Code}";
			result += " : ";
			result += $"{contractField.CurrentExpenseArticle.Title}";
			result += "]";
			return result;
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
		
	}
}