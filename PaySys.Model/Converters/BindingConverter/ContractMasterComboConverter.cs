using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using PaySys.Globalization;

namespace PaySys.Model.BindingConverter
{
	public class ContractMasterComboConverter : IMultiValueConverter
	{
		/// <summary>
		/// 
		/// </summary>
		/// <param name="values">ContractNo,DateExport,DateExecution,IsCurrent</param>
		/// <param name="targetType"></param>
		/// <param name="parameter"></param>
		/// <param name="culture"></param>
		/// <returns></returns>
		public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
		{
			var safeValues = new List<string>();
			values.ToList().ForEach(o => safeValues.Add(o!= DependencyProperty.UnsetValue?o.ToString():string.Empty));
			var dateLabelPrefix = ResourceAccessor.Labels.GetString("Date");
			string[] labels =
			{
				ResourceAccessor.FieldicContractMaster.GetString("ContractNo"),
				ResourceAccessor.FieldicContractMaster.GetString("DateExport")?.Replace(dateLabelPrefix, ""),
				ResourceAccessor.FieldicContractMaster.GetString("DateExecution")?.Replace(dateLabelPrefix, ""),
				ResourceAccessor.Labels.GetString("IsCurrentContractForCombobox")
			};
			var dateConverter = new DateFormatterConverter();
			var currentContractLabel = string.Empty;
			if(values[3]!=null && values[3]!= DependencyProperty.UnsetValue)
			{
				currentContractLabel = (bool)values[3] ? labels[3] : string.Empty;
			}
			return
				$"{labels[0]} {safeValues[0]}  {labels[1]} {dateConverter.Convert(safeValues[1], null, null, null)}  {labels[2]} {dateConverter.Convert(values[2], null, null, null)}  {currentContractLabel} ";
		}

		public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}