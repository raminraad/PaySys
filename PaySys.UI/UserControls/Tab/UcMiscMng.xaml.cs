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
using PaySys.UI.Commands;
using PaySys.UI.Dialogs;
using PaySys.UI.Modals;
using DialogResult = System.Windows.Forms.DialogResult;
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
		private readonly string _enterTitleMessage = ResourceAccessor.Messages.GetString("EnterMiscTitle");
		private MiscType _selectedList = MiscType.None;
		private Misc _selectedMisc;

		private ListView SelectedListView => _selectedList == MiscType.Payment ? ListViewMiscPayment : ListViewMiscDebt;

		public UcMiscMng()
		{
			InitializeComponent();
			ListViewMiscDebt.Items.Filter = o => ((Misc) o).Year == 97;
			ListViewMiscPayment.Items.Filter = o => ((Misc) o).Year == 97;
		}

		public DelegateSaveContext SaveContext { set; get; }

		private void BtnEditMiscTitle_OnClick(object sender, RoutedEventArgs e)
		{
			//Todo
		}

		private void BtnChangeExpenseArticle_OnClick(object sender, RoutedEventArgs e)
		{
			var selectExpenseArticleDialog = new WinSelectItem(ResourceAccessor.Messages.GetString("SelectExpenseArticle"));
			selectExpenseArticleDialog.ListViewItemsSource = ExpenseArticlesAll;
			if(selectExpenseArticleDialog.ShowDialog() == true)
			{
				_selectedMisc.CurrentExpenseArticle = (ExpenseArticle) selectExpenseArticleDialog.SelectedItem;
				SaveContext.Invoke();
				SelectedListView.GetBindingExpression(ItemsControl.ItemsSourceProperty)?.UpdateTarget();
			}
		}

		#region Properties

		public static readonly DependencyProperty CurrentSubGroupProperty = DependencyProperty.Register("CurrentSubGroup", typeof(SubGroup), typeof(UcMiscMng), new PropertyMetadata(default(SubGroup)));

		public SubGroup CurrentSubGroup
		{
			get => (SubGroup) GetValue(CurrentSubGroupProperty);
			set => SetValue(CurrentSubGroupProperty, value);
		}

		public List<ExpenseArticle> ExpenseArticlesAll { get; set; }

		public List<MiscTitle> MiscTitlesAll { get; set; }

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

		private void UcMiscMng_OnLoaded(object sender, RoutedEventArgs e)
		{
			SmpUcSuggesterTextBoxMiscTitlePayments.ItemsSource = MiscTitlesAll.Where(title => title.IsPayment).Select(title => title.Title);
			SmpUcSuggesterTextBoxMiscTitleDebts.ItemsSource = MiscTitlesAll.Where(title => !title.IsPayment).Select(title => title.Title);
		}

		#region Add/Remove misc commands

		private void AddMisc_Execute(object target, ExecutedRoutedEventArgs e)
		{
			var newItemIsPayment = (target as Control).Name.Equals("ListViewMiscPayment");
			var param = e.Parameter.ToString().Trim();
			var newMiscTitle = MiscTitlesAll.FirstOrDefault(title => string.Equals(title.Title, param)) ?? new MiscTitle
			{
				Title = param,
				IsPayment = newItemIsPayment
			};
			CurrentSubGroup.Miscs.Add(new Misc
			{
				MiscTitle = newMiscTitle,
				Year = 97
			});
			if(newMiscTitle.MiscTitleId == 0)
				MiscTitlesAll.Add(newMiscTitle);
			SaveContext.Invoke();
			if(newItemIsPayment)
				ListViewMiscPayment.GetBindingExpression(ItemsControl.ItemsSourceProperty)?.UpdateTarget();
			else
				ListViewMiscDebt.GetBindingExpression(ItemsControl.ItemsSourceProperty)?.UpdateTarget();
		}

		private void AddMisc_CanExecute(object target, CanExecuteRoutedEventArgs e)
		{
			//Todo: Optimize performance by using Misc Title list of sub group one-time loaded not every time.
			var newItemIsPayment = (target as Control).Name.Equals("ListViewMiscPayment");
			if(newItemIsPayment)
				e.CanExecute = !String.IsNullOrEmpty(SmpUcSuggesterTextBoxMiscTitlePayments.SelectedValue) && !CurrentSubGroup.MiscsOfTypePayment.Select(misc => misc.MiscTitle.Title).Contains(SmpUcSuggesterTextBoxMiscTitlePayments.SelectedValue);
			else
				e.CanExecute = !String.IsNullOrEmpty(SmpUcSuggesterTextBoxMiscTitleDebts.SelectedValue) && !CurrentSubGroup.MiscsOfTypeDebt.Select(misc => misc.MiscTitle.Title).Contains(SmpUcSuggesterTextBoxMiscTitleDebts.SelectedValue);
			e.Handled = true;
		}

		#endregion

		private void RemoveMisc_Execute(object target, ExecutedRoutedEventArgs e)
		{
			var item = e.Parameter as Misc;
			var itemIsPayment = item.MiscTitle.IsPayment;
			var listTitle = ResourceAccessor.Labels.GetString(itemIsPayment ? "MiscPayments" : "MiscDebts");
			if(PaySysMessage.GetDeleteSubGroupMiscConfirmation(item.MiscTitle.Title, listTitle) != MessageBoxResult.Yes)
				return;

			CurrentSubGroup.Miscs.Remove(item);
			SaveContext.Invoke();
			if(itemIsPayment)
				ListViewMiscPayment.GetBindingExpression(ItemsControl.ItemsSourceProperty)?.UpdateTarget();
			else
				ListViewMiscDebt.GetBindingExpression(ItemsControl.ItemsSourceProperty)?.UpdateTarget();
		}
	}
}