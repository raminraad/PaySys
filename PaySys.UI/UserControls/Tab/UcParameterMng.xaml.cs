using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
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
using PaySys.ModelAndBindLib.Model;
using PaySys.UI.Dialogs;
using PaySys.UI.Modals;
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
			ListViewParameter.Items.Filter = o => ((Parameter)o).Year == 97&& ((Parameter)o).Month == 007;
		}

		public static readonly DependencyProperty CurrentSubGroupProperty = DependencyProperty.Register("CurrentSubGroup", typeof(SubGroup), typeof(UcParameterMng), new PropertyMetadata(default(SubGroup)));

		public SubGroup CurrentSubGroup
		{
			get => (SubGroup) GetValue(CurrentSubGroupProperty);
			set => SetValue(CurrentSubGroupProperty, value);
		}

		private void BtnEditValue_OnClick(object sender, RoutedEventArgs e)
		{
			var message = ResourceAccessor.Messages.GetString("EnterParameterValue");
			var stringValue = string.Empty;
			if(InputBox.Show(message, ref stringValue) == DialogResult.OK)
				if(int.TryParse(stringValue, out int numericValue))
				{
					((Parameter) ListViewParameter.SelectedItem).Value = numericValue;
					SaveContext.Invoke();
					CollectionViewSource.GetDefaultView(ListViewParameter.ItemsSource).Refresh();
				}
		}

		private void BtnAddParameterInvolvedContractField_OnClick(object sender, RoutedEventArgs e)
		{
			var dialog = new WinSelectItem(ResourceAccessor.Messages.GetString("SelectContractField"))
			{
				ListViewItemsSource = CurrentSubGroup.ContractFields.Where(contractField => contractField.Year == 97 && !((List<ParameterInvolvedContractField>) ListViewParameterInvolvedContractFields.ItemsSource).Select(involvedContractField => involvedContractField.ContractField).Contains(contractField))
			};
			if(dialog.ShowDialog() == true)
			{
				((Parameter) ListViewParameter.SelectedItem).ParameterInvolvedContractFields.Add(new ParameterInvolvedContractField
				{
					ContractField = (ContractField) dialog.SelectedItem,
					Parameter = (Parameter) ListViewParameter.SelectedItem
				});
				SaveContext.Invoke();
				CollectionViewSource.GetDefaultView(ListViewParameterInvolvedContractFields.ItemsSource).Refresh();
			}
		}

		private void BtnAddParameterInvolvedMiscPayment_OnClick(object sender, RoutedEventArgs e)
		{
			var dialog = new WinSelectItem(ResourceAccessor.Messages.GetString("SelectMiscPayment"),"MiscTitle.Title")
			{
				ListViewItemsSource = CurrentSubGroup.MiscsOfTypePayment.Where(misc => misc.Year == 97 && !((List<ParameterInvolvedMisc>) ListViewParameterInvolvedMiscPayments.ItemsSource).Select(involvedMisc => involvedMisc.Misc).Contains(misc))
			};
			if(dialog.ShowDialog() == true)
			{
				((Parameter) ListViewParameter.SelectedItem).ParameterInvolvedMiscs.Add(new ParameterInvolvedMisc
				{
					Misc = (Misc) dialog.SelectedItem,
					Parameter = (Parameter) ListViewParameter.SelectedItem
				});
				SaveContext.Invoke();
				CollectionViewSource.GetDefaultView(ListViewParameterInvolvedMiscPayments.ItemsSource).Refresh();
			}
		}

		private void BtnDeleteParameterInvolvedMiscPayment_OnClick(object sender, RoutedEventArgs e)
		{
			if(PaySysMessage.GetDeleteItemConfirmation() != MessageBoxResult.Yes)
				return;

			((Parameter) ListViewParameter.SelectedItem).ParameterInvolvedMiscs.Remove((ParameterInvolvedMisc) ListViewParameterInvolvedMiscPayments.SelectedItem);
			SaveContext.Invoke();
			CollectionViewSource.GetDefaultView(ListViewParameterInvolvedMiscPayments.ItemsSource).Refresh();
		}

		private void BtnDeleteParameterInvolvedContractField_OnClick(object sender, RoutedEventArgs e)
		{
			if(PaySysMessage.GetDeleteItemConfirmation() != MessageBoxResult.Yes)
				return;

			((Parameter) ListViewParameter.SelectedItem).ParameterInvolvedContractFields.Remove((ParameterInvolvedContractField) ListViewParameterInvolvedContractFields.SelectedItem);
			SaveContext.Invoke();
			CollectionViewSource.GetDefaultView(ListViewParameterInvolvedContractFields.ItemsSource).Refresh();
		}

	}
}