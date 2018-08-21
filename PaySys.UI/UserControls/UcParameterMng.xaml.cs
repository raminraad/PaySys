using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using PaySys.CalcLib.Delegates;
using PaySys.Globalization;
using PaySys.ModelAndBindLib;
using PaySys.ModelAndBindLib.Model;
using MessageBox = System.Windows.MessageBox;
using UserControl = System.Windows.Controls.UserControl;

namespace PaySys.UI.UC
{
	/// <summary>
	/// Interaction logic for UcParameterMng.xaml
	/// </summary>
	public partial class UcParameterMng : UserControl
	{
		public DelegateSaveContext SaveContext { set; get; }

		public UcParameterMng()
		{
			InitializeComponent();
			ListViewParameter.Items.Filter = o => ( (Parameter) o ).Year == PaySysSetting.CurrentYear && ( (Parameter) o ).Month == PaySysSetting.CurrentMonth;

			ListViewParameterInvolvedContractFields.Items.SortDescriptions.Add( new SortDescription( "Key.ContractFieldTitle.Title", ListSortDirection.Ascending ) );
		}

		public static readonly DependencyProperty ReadOnlyOfFieldsProperty = DependencyProperty.Register( "ReadOnlyOfFields", typeof(bool), typeof(UcParameterMng), new PropertyMetadata( default(bool) ) );

		public bool ReadOnlyOfFields { get { return (bool) GetValue( ReadOnlyOfFieldsProperty ); } set { SetValue( ReadOnlyOfFieldsProperty, value ); } }
		public List<MiscTitle> MiscTitlesAll { get; set; }

		public List<ContractFieldTitle> ContractFieldTitlesTitlesAll { get; set; }

		public static readonly DependencyProperty CurrentSubGroupProperty = DependencyProperty.Register( "CurrentSubGroup", typeof(SubGroup), typeof(UcParameterMng), new PropertyMetadata( default(SubGroup) ) );

		public SubGroup CurrentSubGroup { get => (SubGroup) GetValue( CurrentSubGroupProperty ); set => SetValue( CurrentSubGroupProperty, value ); }

		private void AddContractField_CanExecute( object sender, CanExecuteRoutedEventArgs e )
		{
		}

		private void AddMiscPayment_CanExecute( object sender, CanExecuteRoutedEventArgs e )
		{
		}

		private void AddContractField_Execute( object sender, ExecutedRoutedEventArgs e )
		{
			( (Parameter) ListViewParameter.SelectedItem ).ParameterInvolvedContractFields.Add( new ParameterInvolvedContractField
			{
					SubGroupContractField = CurrentSubGroup.SubGroupContractFields.First( c => c.ContractFieldTitle.Title == e.Parameter.ToString() ),
					Parameter = (Parameter) ListViewParameter.SelectedItem
			} );
			SaveContext.Invoke();
			CollectionViewSource.GetDefaultView( ListViewParameterInvolvedContractFields.ItemsSource ).Refresh();
		}

		private void AddMiscPayment_Execute( object sender, ExecutedRoutedEventArgs e )
		{
		}

		private void ContractField_Checked( object sender, RoutedEventArgs e )
		{
			var prm = ListViewParameter.SelectedItem as Parameter;
			var cf = ( e.Source as System.Windows.Controls.Control )?.Tag as SubGroupContractField;
			prm?.ParameterInvolvedContractFields.Add( new ParameterInvolvedContractField
			{
					Parameter = prm,
					SubGroupContractField = cf
			} );
		}

		private void ContractField_UnChecked( object sender, RoutedEventArgs e )
		{
			var prm = ListViewParameter.SelectedItem as Parameter;
			var cf = ( e.Source as System.Windows.Controls.Control )?.Tag as SubGroupContractField;
			prm?.ParameterInvolvedContractFields.Remove( prm?.ParameterInvolvedContractFields.FirstOrDefault( f => f.SubGroupContractField.Equals( cf ) ) );
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
	}
}