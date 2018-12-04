using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using PaySys.Model.Entities;
using PaySys.Model.Static;
using UserControl = System.Windows.Controls.UserControl;

namespace PaySys.UI.UserControls
{
	public partial class UcParameterMng : UserControl
	{
		public UcParameterMng()
		{
			InitializeComponent();
			ListViewParameter.Items.Filter = o => ( (Parameter) o ).Year == PaySysSetting.CurrentYear && ( (Parameter) o ).Month == PaySysSetting.CurrentMonth;
            
			ListViewParameterInvolvedContractFields.Items.SortDescriptions.Add( new SortDescription( "Key.Title", ListSortDirection.Ascending ) );
			ListViewParameterInvolvedMiscPayments.Items.SortDescriptions.Add( new SortDescription("Key.MiscTitle.Title", ListSortDirection.Ascending ) );
		}

		public static readonly DependencyProperty ReadOnlyOfFieldsProperty = DependencyProperty.Register( "ReadOnlyOfFields", typeof(bool), typeof(UcParameterMng), new PropertyMetadata( default(bool) ) );

		public bool ReadOnlyOfFields { get => (bool) GetValue( ReadOnlyOfFieldsProperty );
		    set => SetValue( ReadOnlyOfFieldsProperty, value );
		}

		public static readonly DependencyProperty CurrentSubGroupProperty = DependencyProperty.Register( "CurrentSubGroup", typeof(SubGroup), typeof(UcParameterMng), new PropertyMetadata( default(SubGroup) ) );

		public SubGroup CurrentSubGroup { get => (SubGroup) GetValue( CurrentSubGroupProperty ); set => SetValue( CurrentSubGroupProperty, value ); }

	    private void ContractField_Checked( object sender, RoutedEventArgs e )
		{
			var prm = ListViewParameter.SelectedItem as Parameter;
			var cf = ( e.Source as System.Windows.Controls.Control )?.Tag as ContractField;
			prm?.ParameterInvolvedContractFields.Add( new ParameterInvolvedContractField
			{
					Parameter = prm,
					ContractField = cf
			} );
		}

		private void ContractField_UnChecked( object sender, RoutedEventArgs e )
		{
			var prm = ListViewParameter.SelectedItem as Parameter;
			var cf = ( e.Source as System.Windows.Controls.Control )?.Tag as ContractField;
			prm?.ParameterInvolvedContractFields.Remove( prm?.ParameterInvolvedContractFields.FirstOrDefault( f => f.ContractField.Equals( cf ) ) );
		}

		private void MiscPayment_Checked( object sender, RoutedEventArgs e )
		{
			var prm = ListViewParameter.SelectedItem as Parameter;
			var m = ( e.Source as System.Windows.Controls.Control )?.Tag as Misc;
			prm?.ParameterInvolvedMiscs.Add( new ParameterInvolvedMisc
			{
					Parameter = prm,
					Misc = m
			} );
		}

		private void MiscPayment_UnChecked( object sender, RoutedEventArgs e )
		{
			var prm = ListViewParameter.SelectedItem as Parameter;
			var m = ( e.Source as System.Windows.Controls.Control )?.Tag as Misc;
			prm?.ParameterInvolvedMiscs.Remove( prm?.ParameterInvolvedMiscs.FirstOrDefault( f => f.Misc.Equals( m ) ) );
		}

	    public void Refresh()
	    {
            CollectionViewSource.GetDefaultView(ListViewParameter.ItemsSource).Refresh();
	    }
	}
}