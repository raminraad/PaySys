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
	/// <summary>
	///     Interaction logic for
	///     UcMiscRechargesOfOneMisc.xaml
	/// </summary>
	public partial class UcMiscRechargesOfOneMisc
	{
		public static readonly DependencyProperty ReadOnlyOfListItemsProperty = DependencyProperty.Register( "ReadOnlyOfListItems", typeof(bool), typeof(UcMiscRechargesOfOneMisc), new PropertyMetadata( default(bool) ) );

		public UcMiscRechargesOfOneMisc()
		{
			InitializeComponent();
		}

		public bool ReadOnlyOfListItems
		{
			get => (bool) GetValue( ReadOnlyOfListItemsProperty );
			set => SetValue( ReadOnlyOfListItemsProperty, value );
		}

		private CollectionViewSource CvsOfSubGroupMiscRecharges => Resources["CvsMiscRecharges"] as CollectionViewSource;

		public void RefreshCvs(object source=null)
		{
			CvsOfSubGroupMiscRecharges.Source = ( ( source ?? DataContext ) as SubGroup )?.TempMiscRechargesOfEmployees;
		}


		private void UcMiscRechargesOfOneMisc_OnDataContextChanged( object sender, DependencyPropertyChangedEventArgs e )
		{
			RefreshCvs( e.NewValue );
		}

		#region CLR Events

		private void CvsFilterMiscRechargesOfCurrentMisc( object sender, FilterEventArgs e )
		{
			var rec = e.Item as MiscRecharge;
			if( rec == null )
				return;

			e.Accepted = rec.Misc.Equals( DataGridMiscs.SelectedItem as Misc );
		}

		private void UcMiscRechargesOfOneMisc_OnInitialized( object sender, System.EventArgs e )
		{
			CvsOfSubGroupMiscRecharges.SortDescriptions.Add( new SortDescription( "Employee.LuffName", ListSortDirection.Ascending ) );
			CvsOfSubGroupMiscRecharges.Filter += CvsFilterMiscRechargesOfCurrentMisc;
		}

		private void ListItemGotFocus( object sender, RoutedEventArgs e )
		{
			ListViewMiscRecharges.SelectedItem = ( sender as ListViewItem ).Content;
			( sender as ListViewItem ).FindVisualChildren<TextBox>().FirstOrDefault().SelectAll();
		}

		#endregion


	    private void DataGridMiscs_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
	    {
	        CollectionViewSource.GetDefaultView(ListViewMiscRecharges.ItemsSource)?.Refresh();
        }
	}
}