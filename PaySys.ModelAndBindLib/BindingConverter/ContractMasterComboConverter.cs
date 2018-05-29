using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Data;
using PaySys.Globalization;

namespace PaySys.ModelAndBindLib.BindingConverter
{
	public class ContractMasterComboConverter : IMultiValueConverter
	{
		/// <summary>
		/// 
		/// </summary>
		/// <param name="values">ContractNo,DateExport,DateExecution</param>
		/// <param name="targetType"></param>
		/// <param name="parameter"></param>
		/// <param name="culture"></param>
		/// <returns></returns>
		public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
		{
			var dateLabelPrefix = ResourceAccessor.Labels.GetString("Date");
			string[] labels =
			{
				ResourceAccessor.FieldicContractMaster.GetString("ContractNo"),
				ResourceAccessor.FieldicContractMaster.GetString("DateExport").Replace(dateLabelPrefix, ""),
				ResourceAccessor.FieldicContractMaster.GetString("DateExecution").Replace(dateLabelPrefix, "")
			};
			var dateConverter = new DateFormatterConverter();
			return
				$"{labels[0]} {values[0]}  {labels[1]} {dateConverter.Convert(values[1], null, null, null)}  {labels[2]} {dateConverter.Convert(values[2], null, null, null)}";
		}

		public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}