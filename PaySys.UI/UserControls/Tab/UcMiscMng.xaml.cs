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
using Bogus;
using PaySys.CalcLib.Delegates;
using PaySys.Globalization;
using PaySys.ModelAndBindLib.Model;
using PaySys.UI.Dialogs;
using DialogResult= System.Windows.Forms.DialogResult;
using MessageBoxDefaultButton = System.Windows.Forms.MessageBoxDefaultButton;
using MessageBoxButtons = System.Windows.Forms.MessageBoxButtons;
using MessageBoxIcon = System.Windows.Forms.MessageBoxIcon;
using MessageBox = System.Windows.MessageBox;
using MessageBoxOptions = System.Windows.MessageBoxOptions;
using UserControl = System.Windows.Controls.UserControl;

namespace PaySys.UI.UC
{
	/// <summary>
	/// Interaction logic for UcMiscMng.xaml
	/// </summary>
	public partial class UcMiscMng : UserControl
	{
		private enum MiscType
		{
			None,
			Payment,
			Debt
		}

		private readonly string _enterTitleMessage;
		private MiscType _selectedList = MiscType.None;
		private Misc _selectedMisc;
		private ListView SelectedListView => _selectedList == MiscType.Payment ? ListViewMiscPayment : ListViewMiscDebt;
		public UcMiscMng()
		{
			InitializeComponent();
			_enterTitleMessage = ResourceAccessor.Messages.GetString("EnterMiscTitle");
		}

		public DelegateSaveContext SaveContext { set; get; }

		private void BtnAdd_OnClick(object sender, RoutedEventArgs e)
		{
			var newTitle = string.Empty;
			if (InputBox.Show(_enterTitleMessage, ref newTitle) == DialogResult.OK)
			{
				CurrentSubGroup.Miscs.Add(new Misc
				{
					Title = newTitle,
					Year = 97,
					Month = 007,
					IsPayment = _selectedList == MiscType.Payment
				});
				SaveContext.Invoke();
				SelectedListView.GetBindingExpression(ItemsControl.ItemsSourceProperty)?.UpdateTarget();
			}
		}

		private void BtnEditMiscTitle_OnClick(object sender, RoutedEventArgs e)
		{
			var newTitle = _selectedMisc?.Title;
			if (InputBox.Show(_enterTitleMessage, ref newTitle) == DialogResult.OK)
			{
				_selectedMisc.Title = newTitle;
				SaveContext.Invoke();
//				CollectionViewSource.GetDefaultView(SelectedListView.ItemsSource).Refresh();
				SelectedListView.GetBindingExpression(ItemsControl.ItemsSourceProperty)?.UpdateTarget();
			}
		}

		private void BtnDelete_OnClick(object sender, RoutedEventArgs e)
		{
			if (System.Windows.Forms.MessageBox.Show(ResourceAccessor.Messages.GetString("DeleteConfirmationOfItem"), "",MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2,
				    System.Windows.Forms.MessageBoxOptions.RightAlign) == DialogResult.Yes)
			{
				CurrentSubGroup.Miscs.Remove(_selectedMisc);
				SaveContext.Invoke();
				SelectedListView.GetBindingExpression(ItemsControl.ItemsSourceProperty)?.UpdateTarget();
			}
		}

		private void BtnChangeExpenseArticle_OnClick(object sender, RoutedEventArgs e)
		{
			var selectExpenseArticleDialog = new WinSelectItem(ResourceAccessor.Messages.GetString("SelectExpenseArticle"));
			selectExpenseArticleDialog.ListViewItemsSource = ExpenseArticlesAll;
			if (selectExpenseArticleDialog.ShowDialog() == true)
			{
				_selectedMisc.CurrentExpenseArticle = (ExpenseArticle) selectExpenseArticleDialog.SelectedItem;
				SaveContext.Invoke();
				SelectedListView.GetBindingExpression(ItemsControl.ItemsSourceProperty)?.UpdateTarget();
			}
		}

		#region DependencyProperties

		public static readonly DependencyProperty CurrentSubGroupProperty = DependencyProperty.Register("CurrentSubGroup",
			typeof(SubGroup), typeof(UcMiscMng), new PropertyMetadata(default(SubGroup)));
		public SubGroup CurrentSubGroup
		{
			get => (SubGroup) GetValue(CurrentSubGroupProperty);
			set => SetValue(CurrentSubGroupProperty, value);
		}
		public static readonly DependencyProperty ExpenseArticlesAllProperty =
			DependencyProperty.Register("ExpenseArticlesAll", typeof(List<ExpenseArticle>), typeof(UcMiscMng),
				new PropertyMetadata(default(List<ExpenseArticle>)));
		public List<ExpenseArticle> ExpenseArticlesAll
		{
			get => (List<ExpenseArticle>) GetValue(ExpenseArticlesAllProperty);
			set => SetValue(ExpenseArticlesAllProperty, value);
		}

		#endregion

		private void ListViewMiscPayment_OnGotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
		{
			_selectedList = MiscType.Payment;
			_selectedMisc = ListViewMiscPayment.SelectedItem as Misc;
		}

		private void ListViewMiscDebt_OnGotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
		{
			_selectedList = MiscType.Debt;
			_selectedMisc = ListViewMiscDebt.SelectedItem as Misc;
		}
	}
}