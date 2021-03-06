﻿#region
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using Arash.PersianDateControls;
using PaySys.Model.Entities;
using PaySys.Model.ExtensionMethods;
using ValueType = PaySys.Model.Enums.ValueType;

#endregion

namespace PaySys.UI.UserControls
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

	    private void UcMonthlyDataOfOneEmployee_OnDataContextChanged( object sender, DependencyPropertyChangedEventArgs e )
		{
			RefreshCvs( e.NewValue );
		}
		public void RefreshCvs(object source=null)
		{
			var newSource = ( source ?? DataContext ) as SubGroup;
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

			e.Accepted = var.Employee.Equals( DataGridEmployees.SelectedItem as Employee );
		}

		private void FilterMiscOfSelectedEmployee( object sender, FilterEventArgs e )
		{
			var var = e.Item as MiscValueForEmployee;
			if( var == null )
				return;

			e.Accepted = var.Employee.Equals(DataGridEmployees.SelectedItem as Employee);
		}

	    private void UcMonthlyDataOfOneEmployee_OnInitialized( object sender, System.EventArgs e )
		{
			CvsOfEmployeeVariableValues.SortDescriptions.Add( new SortDescription( "Variable.Title", ListSortDirection.Ascending ) );
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
			if( Equals( listView, ListViewVariables ) && (listView.SelectedItem as VariableValueForEmployee)?.Variable?.ValueType==ValueType.Date)
				( sender as ListViewItem ).FindVisualChildren<PersianDatePicker>().FirstOrDefault()?.FindVisualChildren<TextBox>()?.FirstOrDefault()?.SelectAll();
			else
			( sender as ListViewItem ).FindVisualChildren<TextBox>().FirstOrDefault()?.SelectAll();
		}

		#endregion


	    private void DataGridEmployees_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
	    {
	        CollectionViewSource.GetDefaultView(ListViewVariables.ItemsSource)?.Refresh();
	        CollectionViewSource.GetDefaultView(ListViewMiscPayments.ItemsSource)?.Refresh();
	        CollectionViewSource.GetDefaultView(ListViewMiscDebts.ItemsSource)?.Refresh();
        }
	}
}