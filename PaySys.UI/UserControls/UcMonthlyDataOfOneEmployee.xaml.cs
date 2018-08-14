#region

using System;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using Arash.PersianDateControls;
using PaySys.ModelAndBindLib.Model;
using PaySys.UI.ExtensionMethods;
using ValueType = PaySys.ModelAndBindLib.Model.ValueType;

#endregion

namespace PaySys.UI.UC
{
	public partial class UcMonthlyDataOfOneEmployee : UserControl
	{
		public static readonly DependencyProperty ReadOnlyOfListItemsProperty = DependencyProperty.Register( "ReadOnlyOfListItems", typeof(bool), typeof(UcMonthlyDataOfOneEmployee), new PropertyMetadata( default(bool) ) );

		public UcMonthlyDataOfOneEmployee()
		{
			InitializeComponent();
		}

		public bool ReadOnlyOfListItems
		{
			get => (bool) GetValue( ReadOnlyOfListItemsProperty );
			set => SetValue( ReadOnlyOfListItemsProperty, value );
		}

		private CollectionViewSource CvsOfEmployeeVariableValues => Resources["CvsOfEmployeeVariableValues"] as CollectionViewSource;

		private CollectionViewSource CvsOfEmployeeMiscPaymentValues => Resources["CvsOfEmployeeMiscPaymentValues"] as CollectionViewSource;

		private CollectionViewSource CvsOfEmployeeMiscDebtValues => Resources["CvsOfEmployeeMiscDebtValues"] as CollectionViewSource;

		private void SmpUcRibbonSelector_OnListDataContextChanged( object sender, RoutedEventArgs e )
		{
			SmpUcRibbonSelector.SortDescription = "FullName";
		}

		private void UcMonthlyDataOfOneEmployee_OnDataContextChanged( object sender, DependencyPropertyChangedEventArgs e )
		{
			RefreshCvs( e.NewValue );
		}
		public void RefreshCvs(object source=null)
		{
			var newSource = ( ( source ?? DataContext ) as SubGroup );
			CvsOfEmployeeVariableValues.Source = newSource?.TempVariableValuesOfEmployees;;
			CvsOfEmployeeMiscPaymentValues.Source = newSource?.TempMiscValuesOfEmployees.Where( m => m.Misc.MiscTitle.IsPayment );
			CvsOfEmployeeMiscDebtValues.Source = newSource?.TempMiscValuesOfEmployees.Where( m => !m.Misc.MiscTitle.IsPayment );
		}
		#region CLR Events

		private void FilterVariablesOfSelectedEmployee( object sender, FilterEventArgs e )
		{
			var var = e.Item as VariableValueForEmployee;
			if( var == null )
				return;

			e.Accepted = var.Employee.Equals( SmpUcRibbonSelector.SelectedItem );
		}

		private void FilterMiscOfSelectedEmployee( object sender, FilterEventArgs e )
		{
			var var = e.Item as MiscValueForEmployee;
			if( var == null )
				return;

			e.Accepted = var.Employee.Equals( SmpUcRibbonSelector.SelectedItem );
		}

		private void SmpUcRibbonSelector_OnSelectedItemChanged( object sender, RoutedEventArgs e )
		{
			CollectionViewSource.GetDefaultView( ListViewVariables.ItemsSource )?.Refresh();
			CollectionViewSource.GetDefaultView( ListViewMiscPayments.ItemsSource )?.Refresh();
			CollectionViewSource.GetDefaultView( ListViewMiscDebts.ItemsSource )?.Refresh();
		}

		private void UcMonthlyDataOfOneEmployee_OnInitialized( object sender, EventArgs e )
		{
			CvsOfEmployeeVariableValues.SortDescriptions.Add( new SortDescription( "SubGroupVariable.VariableTitle.Title", ListSortDirection.Ascending ) );
			CvsOfEmployeeVariableValues.Filter += FilterVariablesOfSelectedEmployee;

			CvsOfEmployeeMiscPaymentValues.SortDescriptions.Add( new SortDescription( "Misc.MiscTitle.Title", ListSortDirection.Ascending ) );
			CvsOfEmployeeMiscPaymentValues.Filter += FilterMiscOfSelectedEmployee;

			CvsOfEmployeeMiscDebtValues.SortDescriptions.Add( new SortDescription( "Misc.MiscTitle.Title", ListSortDirection.Ascending ) );
			CvsOfEmployeeMiscDebtValues.Filter += FilterMiscOfSelectedEmployee;
		}

		private void ListItemGotFocus( object sender, RoutedEventArgs e )
		{
			var listView = ItemsControl.ItemsControlFromItemContainer(sender as ListViewItem) as ListView;
			if( sender == null || listView ==null)
				return;

			listView.SelectedItem = ( sender as ListViewItem )?.Content;
			if( Equals( listView, ListViewVariables ) && (listView.SelectedItem as VariableValueForEmployee)?.SubGroupVariable?.VariableTitle?.ValueType==ValueType.Date)
				( sender as ListViewItem ).FindVisualChildren<PersianDatePicker>().FirstOrDefault()?.FindVisualChildren<TextBox>()?.FirstOrDefault()?.SelectAll();
			else
			( sender as ListViewItem ).FindVisualChildren<TextBox>().FirstOrDefault()?.SelectAll();
		}

		#endregion
	}
}