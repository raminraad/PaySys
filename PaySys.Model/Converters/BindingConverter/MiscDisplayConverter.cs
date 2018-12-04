using System;
using System.Globalization;
using System.Windows.Data;
using PaySys.Model.Entities;

namespace PaySys.Model.BindingConverter
{
	public class MiscDisplayConverter : IValueConverter
	{
		/// <summary>
		/// Used for displaying Miscs in ListView
		/// </summary>
		/// <param name="value"></param>
		/// <param name="targetType"></param>
		/// <param name="parameter"></param>
		/// <param name="culture"></param>
		/// <returns></returns>
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			var misc = value as Misc;
			if (misc == null)
				return string.Empty;
			var result = string.Empty;
			result += $"{misc.MiscTitle.Title}";
			result += "  [";
			result += $"{misc.ExpenseArticle.Code}";
			result += "]";
			return result;
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}

	}
}