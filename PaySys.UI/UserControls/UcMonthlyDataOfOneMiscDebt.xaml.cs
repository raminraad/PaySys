#region

using System;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using PaySys.ModelAndBindLib.Entities;
using PaySys.UI.ExtensionMethods;

#endregion

namespace PaySys.UI.UC
{
	public partial class UcMonthlyDataOfOneMiscDebt : UserControl
	{
		public static readonly DependencyProperty ReadOnlyOfListItemsProperty = DependencyProperty.Register( "ReadOnlyOfListItems", typeof(bool), typeof(UcMonthlyDataOfOneMiscDebt), new PropertyMetadata( default(bool) ) );

		public UcMonthlyDataOfOneMiscDebt()
		{
			InitializeComponent();
		}

		public bool ReadOnlyOfListItems
		{
			get => (bool) GetValue( ReadOnlyOfListItemsProperty );
			set => SetValue( ReadOnlyOfListItemsProperty, value );
		}

		private CollectionViewSource Cvs => Resources["Cvs"] as CollectionViewSource;

		private void SmpUcRibbonSelector_OnListDataContextChanged( object sender, RoutedEventArgs e )
		{
			SmpUcRibbonSelector.SortDescription = "MiscTitle.Title";
		}

		private void UcMonthlyDataOfOneMiscDebt_OnDataContextChanged( object sender, DependencyPropertyChangedEventArgs e )
		{
			RefreshCvs( e.NewValue );
		}
		public void RefreshCvs(object source=null)
		{
			Cvs.Source = ( ( source ?? DataContext ) as SubGroup )?.TempMiscValuesOfEmployees.Where( m => !m.Misc.MiscTitle.IsPayment );
		}
		#region CLR Events

		private void FilterCvs( object sender, FilterEventArgs e )
		{
			var var = e.Item as MiscValueForEmployee;
			if( var == null )
				return;

			e.Accepted = var.Misc.Equals( SmpUcRibbonSelector.SelectedItem );
		}

		private void SmpUcRibbonSelector_OnSelectedItemChanged( object sender, RoutedEventArgs e )
		{
			CollectionViewSource.GetDefaultView( ListViewOfItems.ItemsSource )?.Refresh();
		}

		private void UcMonthlyDataOfOneMiscDebt_OnInitialized( object sender, EventArgs e )
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
	}
}