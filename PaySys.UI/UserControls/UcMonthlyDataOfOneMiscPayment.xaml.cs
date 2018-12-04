#region
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
	public partial class UcMonthlyDataOfOneMiscPayment : UserControl
	{
		public static readonly DependencyProperty ReadOnlyOfListItemsProperty = DependencyProperty.Register( "ReadOnlyOfListItems", typeof(bool), typeof(UcMonthlyDataOfOneMiscPayment), new PropertyMetadata( default(bool) ) );

		public UcMonthlyDataOfOneMiscPayment()
		{
			InitializeComponent();
		}

		public bool ReadOnlyOfListItems
		{
			get => (bool) GetValue( ReadOnlyOfListItemsProperty );
			set => SetValue( ReadOnlyOfListItemsProperty, value );
		}

		private CollectionViewSource Cvs => Resources["Cvs"] as CollectionViewSource;

	    private void UcMonthlyDataOfOneMiscPayment_OnDataContextChanged( object sender, DependencyPropertyChangedEventArgs e )
		{
			RefreshCvs( e.NewValue );
		}
		public void RefreshCvs(object source=null)
		{
			Cvs.Source = ( ( source ?? DataContext ) as SubGroup )?.TempMiscValuesOfEmployees.Where( m => m.Misc.MiscTitle.IsPayment );
		}
		#region CLR Events

		private void FilterCvs( object sender, FilterEventArgs e )
		{
			var var = e.Item as MiscValueForEmployee;
			if( var == null )
				return;

			e.Accepted = var.Misc.Equals( DataGridMiscs.SelectedItem as Misc );
		}

	    private void UcMonthlyDataOfOneMiscPayment_OnInitialized( object sender, System.EventArgs e )
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


	    private void DataGridMiscs_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
	    {
	        CollectionViewSource.GetDefaultView(ListViewOfItems.ItemsSource)?.Refresh();
        }
	}
}