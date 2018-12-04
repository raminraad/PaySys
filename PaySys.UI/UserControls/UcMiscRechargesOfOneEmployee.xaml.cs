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
	///     UcMiscRechargesOfOneEmployee.xaml
	/// </summary>
	public partial class UcMiscRechargesOfOneEmployee
	{
		public static readonly DependencyProperty ReadOnlyOfListItemsProperty = DependencyProperty.Register( "ReadOnlyOfListItems", typeof(bool), typeof(UcMiscRechargesOfOneEmployee), new PropertyMetadata( default(bool) ) );

		public UcMiscRechargesOfOneEmployee()
		{
			InitializeComponent();
		}

		private CollectionViewSource CvsOfSubGroupMiscRecharges => Resources["CvsMiscRecharges"] as CollectionViewSource;

		public bool ReadOnlyOfListItems
		{
			get => (bool) GetValue( ReadOnlyOfListItemsProperty );
			set => SetValue( ReadOnlyOfListItemsProperty, value );
		}

		public void UcMiscRechargesOfOneEmployee_OnDataContextChanged( object sender, DependencyPropertyChangedEventArgs e )
		{
			RefreshCvs( e.NewValue );
		}


		public void RefreshCvs(object source=null)
		{
			CvsOfSubGroupMiscRecharges.Source = ( ( source ?? DataContext ) as SubGroup )?.TempMiscRechargesOfEmployees;
		}

		#region CLR Events

		private void CvsFilterMiscRechargesOfCurrentEmployee( object sender, FilterEventArgs e )
		{
			var rec = e.Item as MiscRecharge;
			if( rec == null )
				return;

			e.Accepted = rec.Employee.Equals( DataGridEmployees.SelectedItem as Employee );
		}

		

		private void UcMiscRechargesOfOneEmployee_OnInitialized( object sender, System.EventArgs e )
		{
			CvsOfSubGroupMiscRecharges.SortDescriptions.Add( new SortDescription( "Misc.MiscTitle.Title", ListSortDirection.Ascending ) );
			CvsOfSubGroupMiscRecharges.Filter += CvsFilterMiscRechargesOfCurrentEmployee;
		}

		private void ListItemGotFocus( object sender, RoutedEventArgs e )
		{
			ListViewMiscRecharges.SelectedItem = ( sender as ListViewItem ).Content;
			( sender as ListViewItem ).FindVisualChildren<TextBox>().FirstOrDefault().SelectAll();
		}

		#endregion


	    private void DataGridEmployees_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
	    {
	        CollectionViewSource.GetDefaultView(ListViewMiscRecharges.ItemsSource)?.Refresh();
        }
	}
}