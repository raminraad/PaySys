using System;
using System.Collections.Generic;
using System.Data.Entity;
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
using System.Windows.Threading;
using PaySys.EF;

namespace PaySys.UI.User_Control
{
	/// <summary>
	/// Interaction logic for UcGroupMng.xaml
	/// </summary>
	public partial class UcGroupMng : UserControl
	{
		#region Field

		private readonly List<MainGroup> _lstMaster;
		private readonly PaySysContext _context = new PaySysContext();
		readonly SolidColorBrush _mainGroupItemBrush = new SolidColorBrush();
		readonly SolidColorBrush _subGroupItemBrush = new SolidColorBrush();
		string _treeLevel1 = "MainGroup";
		string _treeLevel2 = "SubGroup";
		private TabState _tabState;

		#endregion

		#region Prop

		public TabControl ParentTabControl { get; set; }

		public TabState State
		{
			get => _tabState;

			set
			{
				switch (value)
				{
					case TabState.Edit:
					case TabState.Add:
						GridDetail.Visibility = Visibility.Visible;
						TreeViewMain.IsEnabled = false;
						break;
					case TabState.View:
						GridDetail.Visibility = Visibility.Hidden;
						TreeViewMain.IsEnabled = true;
						break;
				}
				_tabState=value;
			}
		}

		#endregion

		#region Ctor

		public UcGroupMng()
		{
			InitializeComponent();
			_mainGroupItemBrush.Color = Colors.Maroon;
			_subGroupItemBrush.Color = Colors.DarkSlateBlue;
			_lstMaster = _context.MainGroups.Include(x => x.SubGroups).ToList();
			TreeViewMain.ItemsSource = _lstMaster;
			State = TabState.View;
			TreeViewMain.Focus();
		}

		#endregion

		private void TreeViewMain_OnSelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
		{
			GridDetail.DataContext = TreeViewMain.SelectedItem;
		}

		#region Edit

		private void CommandEdit_OnCanExecute(object sender, CanExecuteRoutedEventArgs e)
		{
			bool canEdit;
			if (TreeViewMain != null)
				canEdit = TreeViewMain.SelectedItem != null;
			else
			{
				canEdit = false;
			}
			e.CanExecute = canEdit & State == TabState.View;
		}

		private void CommandEdit_OnExecuted(object sender, ExecutedRoutedEventArgs e)
		{
			State = TabState.Edit;
			TextBoxItemValue.Focus();
		}

		#endregion

		#region Add

		private void CommandAdd_OnCanExecute(object sender, CanExecuteRoutedEventArgs e)
		{
			e.CanExecute = State == TabState.View;
		}

		private void CommandAdd_OnExecuted(object sender, ExecutedRoutedEventArgs e)
		{
			State = TabState.Add;

			if (TreeViewMain.SelectedItem.GetType().Name.StartsWith(_treeLevel1))
			{
			}
			else
			{
				if (TreeViewMain.SelectedItem.GetType().Name.StartsWith(_treeLevel2))
				{
				}
			}
		}

		#endregion

		#region CloseTab

		private void CommandCloseTab_OnExecuted(object sender, ExecutedRoutedEventArgs e)
		{
			ParentTabControl.Items.Remove(this.Parent);
		}

		private void CommandCloseTab_OnCanExecute(object sender, CanExecuteRoutedEventArgs e)
		{
			e.CanExecute = State == TabState.View;
		}

		#endregion

		#region Save

		private void ButtonSave_Click(object sender, RoutedEventArgs e)
		{
			BindingExpression binding = TextBoxItemValue.GetBindingExpression(TextBox.TextProperty);
			binding.UpdateSource();
			_context.SaveChanges();
			State = TabState.View;
			TreeViewMain.Focus();
		}

		private void CommandSave_OnCanExecute(object sender, CanExecuteRoutedEventArgs e)
		{
			e.CanExecute = State == TabState.Add || State == TabState.Edit;
		}

		#endregion

		#region Cancel

		private void ButtonCancel_Click(object sender, RoutedEventArgs e)
		{
			State = TabState.View;
			TreeViewMain.Focus();
		}

		private void CommandCancel_OnCanExecute(object sender, CanExecuteRoutedEventArgs e)
		{
			e.CanExecute = State == TabState.Add || State == TabState.Edit;
		}

		#endregion
		
	}
}