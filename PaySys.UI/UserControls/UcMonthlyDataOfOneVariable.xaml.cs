﻿#region
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using PaySys.Model.Entities;
using PaySys.Model.ExtensionMethods;
#endregion

namespace PaySys.UI.UserControls
{
	public partial class UcMonthlyDataOfOneVariable : UserControl
	{
		

		public UcMonthlyDataOfOneVariable()
		{
			InitializeComponent();
		}

		public static readonly DependencyProperty ReadOnlyOfListItemsProperty = DependencyProperty.Register( "ReadOnlyOfListItems", typeof(bool), typeof(UcMonthlyDataOfOneVariable), new PropertyMetadata( default(bool) ) );

		public bool ReadOnlyOfListItems
		{
			get => (bool) GetValue( ReadOnlyOfListItemsProperty );
		    set => SetValue( ReadOnlyOfListItemsProperty, value );
		}

		private CollectionViewSource Cvs => Resources["Cvs"] as CollectionViewSource;

	    private void UcMonthlyDataOfOneVariable_OnDataContextChanged( object sender, DependencyPropertyChangedEventArgs e )
		{
			RefreshCvs( e.NewValue );
		}
		public void RefreshCvs(object source=null)
		{
			Cvs.Source = ( ( source ?? DataContext ) as SubGroup )?.TempVariableValuesOfEmployees;
		}
		#region CLR Events

		private void FilterCvs( object sender, FilterEventArgs e )
		{
			var var = e.Item as VariableValueForEmployee;
			if( var == null )
				return;

			e.Accepted = var.Variable.Equals( DataGridVariables.SelectedItem as Variable);
		}

	    private void UcMonthlyDataOfOneVariable_OnInitialized( object sender, System.EventArgs e )
		{
			Cvs.SortDescriptions.Add( new SortDescription( "Employee.FullName", ListSortDirection.Ascending ) );
			Cvs.Filter += FilterCvs;
		}

		private void ListItemGotFocus( object sender, RoutedEventArgs e )
		{
			ListViewOfItems.SelectedItem = ( sender as ListViewItem ).Content;
			( sender as ListViewItem ).FindVisualChildren<TextBox>().FirstOrDefault()?.SelectAll();
		}

		#endregion


	    private void DataGridVariables_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
	    {
	        CollectionViewSource.GetDefaultView(ListViewOfItems.ItemsSource)?.Refresh();
        }
	}
}