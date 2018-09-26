using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Shell;
using PaySys.CalcLib.Delegates;
using PaySys.Globalization;
using PaySys.ModelAndBindLib;
using PaySys.ModelAndBindLib.Entities;
using MessageBox = System.Windows.Forms.MessageBox;
using MessageBoxOptions = System.Windows.Forms.MessageBoxOptions;
using UserControl = System.Windows.Controls.UserControl;

namespace PaySys.UI.UC
{
	/// <summary>Interaction logic for UcContractFieldTitlesMng.xaml</summary>
	public partial class UcContractFieldTitlesMng : UserControl
	{

		public UcContractFieldTitlesMng()
		{
			InitializeComponent();
			ListViewGroupContractField.Items.Filter = o => ( (ContractField) o ).Year == PaySysSetting.CurrentYear;
		}

		public static readonly DependencyProperty ReadOnlyOfFieldsProperty = DependencyProperty.Register( "ReadOnlyOfFields", typeof(bool), typeof(UcContractFieldTitlesMng), new PropertyMetadata( default(bool) ) );

		public bool ReadOnlyOfFields { get => (bool) GetValue( ReadOnlyOfFieldsProperty ); set => SetValue( ReadOnlyOfFieldsProperty, value ); }

		#region DependencyProperties

		public static readonly DependencyProperty CurrentMainGroupProperty = DependencyProperty.Register( "CurrentMainGroup", typeof(MainGroup), typeof(UcContractFieldTitlesMng), new PropertyMetadata( default(MainGroup) ) );

		public MainGroup CurrentMainGroup { get => (MainGroup) GetValue( CurrentMainGroupProperty ); set => SetValue( CurrentMainGroupProperty, value ); }

		public static readonly DependencyProperty ContractFieldsAllProperty = DependencyProperty.Register( "ContractFieldsAll", typeof(List<ContractField>), typeof(UcContractFieldTitlesMng), new PropertyMetadata( default(List<ContractField>) ) );

		public List<ContractField> ContractFieldsAll { get => (List<ContractField>) GetValue( ContractFieldsAllProperty );
		    set => SetValue( ContractFieldsAllProperty, value );
		}

		#endregion

		private void UcContractFieldTitlesMng_OnLoaded( object sender, RoutedEventArgs e )
		{
			SmpUcSuggesterTextBox.ItemsSource = ContractFieldsAll?.Where( c => c.Year == PaySysSetting.CurrentYear ).Select( c => c.Title )?.Distinct();
		}

		private void AddItem_CanExecute( object sender, CanExecuteRoutedEventArgs e )
		{
			if (CurrentMainGroup!=null)
			{
				e.CanExecute = !string.IsNullOrEmpty( SmpUcSuggesterTextBox.SelectedValue ) && !CurrentMainGroup.ContractFields.Select( c => c.Title ).Contains( SmpUcSuggesterTextBox.SelectedValue );
				e.Handled = true;
			}
		}

		private void AddItem_Execute( object target, ExecutedRoutedEventArgs e )
		{
			var param = e.Parameter.ToString().Trim();
			var newItem = new ContractField
			{
					Title = param,
					Alias = string.Empty,
					Index = 99,
					MainGroup = CurrentMainGroup,
					IndexInRetirementReport = 99,
					IsEditable = true,
					ExpenseArticleOfContractFieldForSubGroups = new List<ExpenseArticleOfContractFieldForSubGroup>(),
					Year = PaySysSetting.CurrentYear,
			};
			CurrentMainGroup.ContractFields.Add( newItem );

			ContractFieldsAll.Add( newItem );
			SmpUcSuggesterTextBox.SelectedValue = string.Empty;
			CollectionViewSource.GetDefaultView( ListViewGroupContractField.ItemsSource ).Refresh();
		}

		private void DeleteItem_Execute( object sender, ExecutedRoutedEventArgs e )
		{
			var item = e.Parameter as ContractField;

			if( item.IsInUse )
			{
				MessageBox.Show( ResourceAccessor.Messages.GetString( "DeleteItemUsers" ) );
				return;
			}

			if( MessageBox.Show( ResourceAccessor.Messages.GetString( "DeleteConfirmationOfItem" ), "", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2, MessageBoxOptions.RightAlign ) != DialogResult.Yes )
				return;

			CurrentMainGroup.ContractFields.Remove( item );

			//ToReview: Folllowing change doesn't reflect on context on parent form
			ContractFieldsAll.Remove( item );

			CollectionViewSource.GetDefaultView( ListViewGroupContractField.ItemsSource ).Refresh();
		}
	}
}