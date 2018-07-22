using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using PaySys.ModelAndBindLib.Model;

namespace PaySys.UI.UC
{
	/// <summary>
	/// Interaction logic for UcMiscRechargesOfOneEmployee.xaml
	/// </summary>
	public partial class UcMiscRechargesOfOneEmployee
	{
		public UcMiscRechargesOfOneEmployee()
		{
			InitializeComponent();
		}

		private void ButtonSet_OnClick(object sender, RoutedEventArgs e)
		{
			var subGroup = DataContext as SubGroup;
			var employee = SmpUcRibbonSelector.SelectedItem as Employee;
			var cvs = FindResource("Cvs") as CollectionViewSource;
			
			if(subGroup == null || employee == null )
				return;

			var query = from msc in subGroup.Miscs.Where(misc => misc.Year == 97)
			            join rec in employee.EmployeeMiscRecharges.Where(rec => rec.Year==97 && rec.Month==007) on msc.MiscId equals rec.Misc.MiscId into empRecs
			            from empRec in empRecs.DefaultIfEmpty()
			            select new 
			            {
							Misc = msc,
							Value = empRec?.Value??0
			            };

				string s = "";
			foreach(var rec in query)
			{
				s += $"Misc: {rec.Misc.MiscTitle.Title}\n";
				s += $"Value: {rec.Value}\n";
				s += "--------------------\n";
			}
				MessageBox.Show(s);
		}

		private void ButtonSet2_OnClick(object sender, RoutedEventArgs e)
		{
/*			var index = "";
			InputBox.Show("Index", ref index);
			MessageBox.Show(CurrentEmployee.ContractMasters.FirstOrDefault(master => master.IsCurrentContract).SubGroup.Miscs.ElementAt(Convert.ToInt16(index)).MiscTitle.Title);
			SmpUcRibbonSelector.SelectedItem = CurrentEmployee.ContractMasters.FirstOrDefault(master => master.IsCurrentContract).SubGroup.Miscs.ElementAt(Convert.ToInt16(index));*/
		}

		private void SmpUcRibbonSelector_OnSelectedItemChanged(object sender, RoutedEventArgs e)
		{
			

		}
	}
}