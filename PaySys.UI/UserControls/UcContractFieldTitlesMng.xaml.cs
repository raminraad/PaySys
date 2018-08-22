using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using System.Windows.Forms;
using System.Windows.Shell;
using PaySys.CalcLib.Delegates;
using PaySys.Globalization;
using PaySys.ModelAndBindLib;
using PaySys.ModelAndBindLib.Model;
using MessageBox = System.Windows.Forms.MessageBox;
using MessageBoxOptions = System.Windows.Forms.MessageBoxOptions;
using UserControl = System.Windows.Controls.UserControl;

namespace PaySys.UI.UC
{
	/// <summary>Interaction logic for UcContractFieldTitlesMng.xaml</summary>
	public partial class UcContractFieldTitlesMng : UserControl
	{

		private readonly string _enterTitleMessage;

		public UcContractFieldTitlesMng()
		{
			InitializeComponent();
			_enterTitleMessage = ResourceAccessor.Messages.GetString("EnterContractFieldTitle");
			ListViewGroupContractField.Items.Filter = o => ((ContractField) o).Year == PaySysSetting.CurrentYear;
		}

		public static readonly DependencyProperty ReadOnlyOfFieldsProperty = DependencyProperty.Register( "ReadOnlyOfFields", typeof(bool), typeof(UcContractFieldTitlesMng), new PropertyMetadata( default(bool) ) );

		public bool ReadOnlyOfFields { get { return (bool) GetValue( ReadOnlyOfFieldsProperty ); } set { SetValue( ReadOnlyOfFieldsProperty, value ); } }

		private void ButtonAdd_OnClick(object sender, RoutedEventArgs e)
		{
			/*var newTitle = string.Empty;
			if(InputBox.Show(_enterTitleMessage, ref newTitle) == DialogResult.OK)
			{
				CurrentSubGroup.MainGroup.ContractFields.Add(new ContractField
				{
					ContractFieldTitle = newTitle,
					Year = PaySysSetting.CurrentYear
				});
				SaveContext.Invoke();
				CollectionViewSource.GetDefaultView(ListViewGroupContractField.ItemsSource).Refresh();
			}*/
		}

		private void ButtonEdit_OnClick(object sender, RoutedEventArgs e)
		{
			/*var selectedTitle = ListViewGroupContractField.SelectedItem as ContractField;
			var newTitle = selectedTitle?.ContractFieldTitle.Title;
			if(InputBox.Show(_enterTitleMessage, ref newTitle) == DialogResult.OK)
			{
				selectedTitle.Title = newTitle;
				SaveContext.Invoke();
				CollectionViewSource.GetDefaultView(ListViewGroupContractField.ItemsSource).Refresh();
			}*/
		}

		private void ButtonDelete_OnClick(object sender, RoutedEventArgs e)
		{
			if(MessageBox.Show(ResourceAccessor.Messages.GetString("DeleteConfirmationOfItem"), "", MessageBoxButtons.YesNo,
			                   MessageBoxIcon.Question, MessageBoxDefaultButton.Button2, MessageBoxOptions.RightAlign) ==
			   DialogResult.Yes)
			{
				CurrentMainGroup.ContractFields.Remove((ContractField) ListViewGroupContractField.SelectedItem);
				
				CollectionViewSource.GetDefaultView(ListViewGroupContractField.ItemsSource).Refresh();
			}
		}

		#region DependencyProperties

		public static readonly DependencyProperty CurrentMainGroupProperty = DependencyProperty.Register( "CurrentMainGroup", typeof(MainGroup), typeof(UcContractFieldTitlesMng), new PropertyMetadata( default(MainGroup) ) );

		public MainGroup CurrentMainGroup { get { return (MainGroup) GetValue( CurrentMainGroupProperty ); } set { SetValue( CurrentMainGroupProperty, value ); } }


		#endregion
	}
}