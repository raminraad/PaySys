using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using System.Windows.Forms;
using System.Windows.Shell;
using PaySys.CalcLib.Delegates;
using PaySys.Globalization;
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
		private readonly string EnterTitleMessage;

		public UcContractFieldTitlesMng()
		{
			InitializeComponent();
			EnterTitleMessage = ResourceAccessor.Messages.GetString("EnterContractFieldTitle");
			ListViewGroupContractFieldTitle.Items.Filter = o => ((ContractField) o).Year == 97;
		}

		public DelegateSaveContext SaveContext { set; get; }

		private void BtnAddMainGroup_OnClick(object sender, RoutedEventArgs e)
		{
			var newTitle = string.Empty;
			if (InputBox.Show(EnterTitleMessage, ref newTitle) == DialogResult.OK)
			{
				CurrentSubGroup.ContractFields.Add(new ContractField
				{
					Title = newTitle,
					Year = 97
				});
				SaveContext.Invoke();
				CollectionViewSource.GetDefaultView(ListViewGroupContractFieldTitle.ItemsSource).Refresh();
			}
		}

		private void BtnEdit_OnClick(object sender, RoutedEventArgs e)
		{
			var selectedTitle = ListViewGroupContractFieldTitle.SelectedItem as ContractField;
			var newTitle = selectedTitle?.Title;
			if (InputBox.Show(EnterTitleMessage, ref newTitle) == DialogResult.OK)
			{
				selectedTitle.Title = newTitle;
				SaveContext.Invoke();
				CollectionViewSource.GetDefaultView(ListViewGroupContractFieldTitle.ItemsSource).Refresh();
			}
		}

		private void BtnDelete_OnClick(object sender, RoutedEventArgs e)
		{
			if (MessageBox.Show(ResourceAccessor.Messages.GetString("DeleteConfirmationOfItem"), "", MessageBoxButtons.YesNo,
				    MessageBoxIcon.Question, MessageBoxDefaultButton.Button2, MessageBoxOptions.RightAlign) == DialogResult.Yes)
			{
				CurrentSubGroup.ContractFields.Remove(
					(ContractField) ListViewGroupContractFieldTitle.SelectedItem);
				SaveContext.Invoke();
				CollectionViewSource.GetDefaultView(ListViewGroupContractFieldTitle.ItemsSource).Refresh();
			}
		}

		private void BtnChangeExpenseArticle_OnClick(object sender, RoutedEventArgs e)
		{
			WinSelectExpenseArticle selectExpenseArticleDialog = new WinSelectExpenseArticle();
			selectExpenseArticleDialog.ListItemsSource=ExpenseArticlesAll;
			if (selectExpenseArticleDialog.ShowDialog() == true)
			{
				var selectedContractField = ListViewGroupContractFieldTitle.SelectedItem as ContractField;
				selectedContractField.CurrentExpenseArticle=selectExpenseArticleDialog.SelectedExpenseArticle;
				SaveContext.Invoke();
				CollectionViewSource.GetDefaultView(ListViewGroupContractFieldTitle.ItemsSource).Refresh();
			}
		}

		#region DependencyProperties

		public static readonly DependencyProperty CurrentSubGroupProperty = DependencyProperty.Register("CurrentSubGroup", typeof(SubGroup), typeof(UcContractFieldTitlesMng), new PropertyMetadata(default(SubGroup)));

		public SubGroup CurrentSubGroup
		{
			get => (SubGroup) GetValue(CurrentSubGroupProperty);
			set => SetValue(CurrentSubGroupProperty, value);
		}

		public static readonly DependencyProperty ExpenseArticlesAllProperty = DependencyProperty.Register("ExpenseArticlesAll", typeof(List<ExpenseArticle>), typeof(UcContractFieldTitlesMng), new PropertyMetadata(default(List<ExpenseArticle>)));

		public List<ExpenseArticle> ExpenseArticlesAll
		{
			get => (List<ExpenseArticle>) GetValue(ExpenseArticlesAllProperty);
			set => SetValue(ExpenseArticlesAllProperty, value);
		}
		#endregion
	}
}