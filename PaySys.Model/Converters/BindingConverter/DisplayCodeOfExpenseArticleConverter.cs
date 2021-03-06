﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows.Data;
using PaySys.Model.Entities;

namespace PaySys.Model.BindingConverter
{
	public class DisplayCodeOfExpenseArticleConverter:IValueConverter
	{
		#region Implementation of IValueConverter

		public object Convert( object value, Type targetType, object parameter, CultureInfo culture )
		{
			var exp = value as List<ExpenseArticle>;
			var enumerable = exp.Select( e => e.Code );
			return enumerable;
		}

		public object ConvertBack( object value, Type targetType, object parameter, CultureInfo culture )
		{
			throw new NotImplementedException();
		}

		#endregion
	}
}
