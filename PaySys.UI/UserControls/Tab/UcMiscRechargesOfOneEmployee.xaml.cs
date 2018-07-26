using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using PaySys.UI.Commands;

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

		private void CvsFilterCurrentEmployeesMiscRecharges(object sender, FilterEventArgs e)
		{
			var rec = e.Item as MiscRecharge;
			if(rec == null)
				return;

			e.Accepted = rec.Employee.Equals(SmpUcRibbonSelector.SelectedItem);
		}

		private void ButtonSet_OnClick(object sender, RoutedEventArgs e)
		{
			var sg = DataContext as SubGroup;
			var recs = sg.TempMiscRechargesOfEmployees.Where(r => r.Employee == SmpUcRibbonSelector.SelectedItem);
		}

		private void SmpUcRibbonSelector_OnSelectedItemChanged(object sender, RoutedEventArgs e)
		{
			CollectionViewSource.GetDefaultView(ListViewMiscs.ItemsSource).Refresh();
		}

		private void TextBoxRechargeValue_OnLostFocus(object sender, RoutedEventArgs e)
		{
//			var currentRecharge = (sender as TextBox).Tag as MiscRecharge;
//			TextBoxTemp.Text = $"{currentRecharge.MiscRechargeId} : {currentRecharge.Value}";
		}

		private void UcMiscRechargesOfOneEmployee_OnLoaded(object sender, RoutedEventArgs e)
		{
			(this.Resources["CvsRechargesOfEmployee"] as CollectionViewSource).Filter += CvsFilterCurrentEmployeesMiscRecharges;
			SmpUcFormStateLabel.CurrentState = FormCurrentState.Select;
		}

		private void CommandBinding_OnCanExecute(object sender, CanExecuteRoutedEventArgs e)
		{
			if(e.Command as RoutedUICommand == PaySysCommands.Edit)
				e.CanExecute = SmpUcFormStateLabel?.EnabledOfCrudButtons ?? false;
			else if(e.Command as RoutedUICommand == PaySysCommands.Save)
				e.CanExecute = SmpUcFormStateLabel?.EnabledOfSaveCancelButtons ?? false;
			else if(e.Command as RoutedUICommand == PaySysCommands.Cancel)
				e.CanExecute = SmpUcFormStateLabel?.EnabledOfSaveCancelButtons ?? false;
			else if(e.Command as RoutedUICommand == PaySysCommands.Reload)
				e.CanExecute = SmpUcFormStateLabel?.EnabledOfCrudButtons ?? false;
			e.Handled = true;
		}

		private void CommandBinding_OnExecuted(object sender, ExecutedRoutedEventArgs e)
		{
			if(e.Command as RoutedUICommand == PaySysCommands.Edit)
				SmpUcFormStateLabel.CurrentState = FormCurrentState.Edit;
			else if(e.Command as RoutedUICommand == PaySysCommands.Cancel)
				SmpUcFormStateLabel.CurrentState = FormCurrentState.Select;
			else if(e.Command as RoutedUICommand == PaySysCommands.Reload)
				; //todo: implement reload command
		}
	}
}