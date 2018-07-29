using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using PaySys.CalcLib.Delegates;
using PaySys.Globalization;
using PaySys.ModelAndBindLib;
using PaySys.ModelAndBindLib.Model;
using PaySys.UI.Dialogs;
using PaySys.UI.ExtensionMethods;
using PaySys.UI.Modals;

namespace PaySys.UI.UC
{
	/// <summary>
	/// Interaction logic for UcMissionFormulaMng.xaml
	/// </summary>
	public partial class UcMissionFormulaMng : UserControl
	{
		public DelegateSaveContext SaveContext { set; get; }

		public UcMissionFormulaMng()
		{
			InitializeComponent();
		}

		public static readonly DependencyProperty CurrentSubGroupProperty = DependencyProperty.Register("CurrentSubGroup", typeof(SubGroup), typeof(UcMissionFormulaMng), new PropertyMetadata(default(SubGroup)));

		public SubGroup CurrentSubGroup
		{
			get => (SubGroup) GetValue(CurrentSubGroupProperty);
			set => SetValue(CurrentSubGroupProperty, value);
		}

		private void BtnEdit_OnClick(object sender, RoutedEventArgs e)
		{
		}

		private void BtnAddMissionFormulaInvolvedContractField_OnClick(object sender, RoutedEventArgs e)
		{
			var dialog = new WinSelectItem(ResourceAccessor.Messages.GetString("SelectContractField"))
			{
				ListViewItemsSource = CurrentSubGroup.ContractFields.Where(contractField => contractField.Year == PaySysSetting.CurrentYear && !((List<MissionFormulaInvolvedContractField>) ListViewContractField.ItemsSource).Select(involvedContractField => involvedContractField.ContractField).Contains(contractField))
			};
			if(dialog.ShowDialog() == true)
			{
				CurrentSubGroup.CurrenMissionFormula.MissionFormulaInvolvedContractFields.Add(new MissionFormulaInvolvedContractField
				{
					ContractField = (ContractField) dialog.SelectedItem,
				});
				SaveContext.Invoke();
				CollectionViewSource.GetDefaultView(ListViewContractField.ItemsSource).Refresh();
			}
		}

		private void BtnDeleteMissionFormulaInvolvedContractField_OnClick(object sender, RoutedEventArgs e)
		{
			if (PaySysMessage.GetDeleteItemConfirmation() != MessageBoxResult.Yes)
				return;

			CurrentSubGroup.CurrenMissionFormula.MissionFormulaInvolvedContractFields.Remove((MissionFormulaInvolvedContractField)ListViewContractField.SelectedItem);
			SaveContext.Invoke();
			CollectionViewSource.GetDefaultView(ListViewContractField.ItemsSource).Refresh();
		}

		private void BtnSave_Click(object sender, RoutedEventArgs e)
		{
			foreach(var textbox in GridMain.FindVisualChildren<TextBox>())
				textbox.GetBindingExpression(TextBox.TextProperty)?.UpdateSource();

			SaveContext.Invoke();
		}

		private void BtnCancel_Click(object sender, RoutedEventArgs e)
		{
			foreach(var textbox in GridMain.FindVisualChildren<TextBox>())
				textbox.GetBindingExpression(TextBox.TextProperty)?.UpdateTarget();
		}
	}
}