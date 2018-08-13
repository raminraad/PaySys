using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using PaySys.UI.Modals;
using UserControl = System.Windows.Controls.UserControl;

namespace PaySys.UI.UC
{
	/// <summary>
	/// Interaction logic for UcTaxTableMng.xaml
	/// </summary>
	public partial class UcTaxTableMng : UserControl
	{
		public DelegateSaveContext SaveContext { set; get; }

		public UcTaxTableMng()
		{
			InitializeComponent();

//			ListViewTaxItem.Items.Filter = o => ((Misc) o).Year == PaySysSetting.CurrentYear && ((Misc) o).Month == PaySysSetting.CurrentMonth;
		}

		public static readonly DependencyProperty CurrentSubGroupProperty = DependencyProperty.Register("CurrentSubGroup", typeof(SubGroup), typeof(UcTaxTableMng), new PropertyMetadata(default(SubGroup)));

		public SubGroup CurrentSubGroup
		{
			get
			{
				return (SubGroup) GetValue(CurrentSubGroupProperty);
			}
			set
			{
				SetValue(CurrentSubGroupProperty, value);
			}
		}

		private void BtnAdd_OnClick(object sender, RoutedEventArgs e)
		{
			var message = ResourceAccessor.Messages.GetString("EnterTaxRowValueTo");
			var stringValue = string.Empty;
			if(InputBox.Show(message, ref stringValue) == DialogResult.OK)
				if(int.TryParse(stringValue, out int numericValue))
				{
					CurrentSubGroup.CurrenTaxTable.TaxRows.Add(new TaxRow
					{
						ValueTo = numericValue
					});
					SaveContext.Invoke();
					ListViewTaxItem.GetBindingExpression(ItemsControl.ItemsSourceProperty)?.UpdateTarget();
				}
		}

		private void BtnDelete_OnClick(object sender, RoutedEventArgs e)
		{
			if(PaySysMessage.GetDeleteItemConfirmation() == MessageBoxResult.Yes)
			{
				CurrentSubGroup.CurrenTaxTable.TaxRows.Remove((TaxRow) ListViewTaxItem.SelectedItem);
				SaveContext.Invoke();
				ListViewTaxItem.GetBindingExpression(ItemsControl.ItemsSourceProperty)?.UpdateTarget();
			}
		}

		private void BtnEditValueTo_Click(object sender, RoutedEventArgs e)
		{
			var message = ResourceAccessor.Messages.GetString("EnterTaxRowValueTo");
			var stringValue = ((TaxRow) ListViewTaxItem.SelectedItem).ValueTo.ToString("F0");
			if(InputBox.Show(message, ref stringValue) == DialogResult.OK)
				if(int.TryParse(stringValue, out int numericValue))
				{
					((TaxRow) ListViewTaxItem.SelectedItem).ValueTo = numericValue;
					SaveContext.Invoke();
					ListViewTaxItem.GetBindingExpression(ItemsControl.ItemsSourceProperty)?.UpdateTarget();
				}
		}

		private void BtnEditRate_OnClick(object sender, RoutedEventArgs e)
		{
			var message = ResourceAccessor.Messages.GetString("EnterTaxRowFactor");
			var stringValue = ((TaxRow) ListViewTaxItem.SelectedItem).Factor.ToString("F0");
			if(InputBox.Show(message, ref stringValue) == DialogResult.OK)
				if(int.TryParse(stringValue, out int numericValue))
				{
					((TaxRow) ListViewTaxItem.SelectedItem).Factor = numericValue;
					SaveContext.Invoke();
					ListViewTaxItem.GetBindingExpression(ItemsControl.ItemsSourceProperty)?.UpdateTarget();
				}
		}
	}
}