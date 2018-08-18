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
using PaySys.UI.Dialogs;
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
			ListViewGroupContractField.Items.Filter = o => ((SubGroupContractField) o).Year == PaySysSetting.CurrentYear;
		}

		public DelegateSaveContext SaveContext { set; get; }

		private void BtnAdd_OnClick(object sender, RoutedEventArgs e)
		{
			/*var newTitle = string.Empty;
			if(InputBox.Show(_enterTitleMessage, ref newTitle) == DialogResult.OK)
			{
				CurrentSubGroup.SubGroupContractFields.Add(new SubGroupContractField
				{
					ContractFieldTitle = newTitle,
					Year = PaySysSetting.CurrentYear
				});
				SaveContext.Invoke();
				CollectionViewSource.GetDefaultView(ListViewGroupContractField.ItemsSource).Refresh();
			}*/
		}

		private void BtnEdit_OnClick(object sender, RoutedEventArgs e)
		{
			/*var selectedTitle = ListViewGroupContractField.SelectedItem as SubGroupContractField;
			var newTitle = selectedTitle?.ContractFieldTitle.Title;
			if(InputBox.Show(_enterTitleMessage, ref newTitle) == DialogResult.OK)
			{
				selectedTitle.Title = newTitle;
				SaveContext.Invoke();
				CollectionViewSource.GetDefaultView(ListViewGroupContractField.ItemsSource).Refresh();
			}*/
		}

		private void BtnDelete_OnClick(object sender, RoutedEventArgs e)
		{
			if(MessageBox.Show(ResourceAccessor.Messages.GetString("DeleteConfirmationOfItem"), "", MessageBoxButtons.YesNo,
			                   MessageBoxIcon.Question, MessageBoxDefaultButton.Button2, MessageBoxOptions.RightAlign) ==
			   DialogResult.Yes)
			{
				CurrentSubGroup.SubGroupContractFields.Remove((SubGroupContractField) ListViewGroupContractField.SelectedItem);
				SaveContext.Invoke();
				CollectionViewSource.GetDefaultView(ListViewGroupContractField.ItemsSource).Refresh();
			}
		}

		private void BtnChangeExpenseArticle_OnClick(object sender, RoutedEventArgs e)
		{
			WinSelectItem selectExpenseArticleDialog =
				new WinSelectItem(ResourceAccessor.Messages.GetString("SelectExpenseArticle"))
				{
					ListViewItemsSource = ExpenseArticlesAll
				};
			if(selectExpenseArticleDialog.ShowDialog() == true)
			{
				var selectedContractField = ListViewGroupContractField.SelectedItem as SubGroupContractField;
				selectedContractField.CurrentExpenseArticle = (ExpenseArticle) selectExpenseArticleDialog.SelectedItem;
				SaveContext.Invoke();
				CollectionViewSource.GetDefaultView(ListViewGroupContractField.ItemsSource).Refresh();
			}
		}

		#region DependencyProperties

		public static readonly DependencyProperty CurrentSubGroupProperty =
			DependencyProperty.Register("CurrentSubGroup", typeof(SubGroup), typeof(UcContractFieldTitlesMng),
			                            new PropertyMetadata(default(SubGroup)));

		public SubGroup CurrentSubGroup
		{
			get => (SubGroup) GetValue(CurrentSubGroupProperty);
			set => SetValue(CurrentSubGroupProperty, value);
		}

		public static readonly DependencyProperty ExpenseArticlesAllProperty =
			DependencyProperty.Register("ExpenseArticlesAll", typeof(List<ExpenseArticle>), typeof(UcContractFieldTitlesMng),
			                            new PropertyMetadata(default(List<ExpenseArticle>)));

		public List<ExpenseArticle> ExpenseArticlesAll
		{
			get => (List<ExpenseArticle>) GetValue(ExpenseArticlesAllProperty);
			set => SetValue(ExpenseArticlesAllProperty, value);
		}

		#endregion

	}
}