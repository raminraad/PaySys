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
using PaySys.EF;

namespace PaySys.UI.User_Control
{
	/// <summary>
	/// Interaction logic for UcGroupMng.xaml
	/// </summary>
	public partial class UcGroupMng : UserControl
	{
		private readonly string _typeOfLevel1 = "MainGroup";
		private readonly string _typeOfLevel2 = "SubGroup";

		private List<MainGroup> _lstMaster;
		private PaySysContext _context = new PaySysContext();
		SolidColorBrush mainGroupItemBrush = new SolidColorBrush();
		SolidColorBrush subGroupItemBrush = new SolidColorBrush();

		public TabControl ParentTabControl { get; set; }
		public UcGroupMng()
		{
			InitializeComponent();
			mainGroupItemBrush.Color = Colors.Maroon;
			subGroupItemBrush.Color = Colors.DarkSlateBlue;
			_lstMaster = _context.MainGroups.Include(x => x.SubGroups).ToList();
			TreeViewMain.ItemsSource = _lstMaster;
			GridDetail.Visibility = Visibility.Hidden;
		}

		private void TreeViewMain_OnSelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
		{
			GridDetail.DataContext = TreeViewMain.SelectedItem;
		}

		private void CommandEdit_OnExecuted(object sender, ExecutedRoutedEventArgs e)
		{
			GridDetail.Visibility = Visibility.Visible;
			TreeViewMain.IsEnabled = false;
		}

		private void CommandEdit_OnCanExecute(object sender, CanExecuteRoutedEventArgs e)
		{
			if (TreeViewMain != null)
				e.CanExecute = TreeViewMain.SelectedItem != null;
			else
			{
				e.CanExecute = false;
			}
		}

		private void ButtonSave_Click(object sender, RoutedEventArgs e)
		{
			BindingExpression binding = TextBoxItemValue.GetBindingExpression(TextBox.TextProperty);
			binding.UpdateSource();
			_context.SaveChanges();
			TreeViewMain.IsEnabled = true;
			GridDetail.Visibility = Visibility.Hidden;
			if (TreeViewMain.SelectedItem.GetType().Name.StartsWith(_typeOfLevel1))
			{
				//				MessageBox.Show("Level 1");
			}
			else if (TreeViewMain.SelectedItem.GetType().Name.StartsWith(_typeOfLevel2))
			{
				//				MessageBox.Show("Level 2");
			}
		}

		private void ButtonCancel_Click(object sender, RoutedEventArgs e)
		{
			GridDetail.Visibility = Visibility.Hidden;
			TreeViewMain.IsEnabled = true;
		}

		private void CommandNew_OnExecuted(object sender, ExecutedRoutedEventArgs e)
		{
			var msg = string.Empty;
			msg += $"Cout: {_context.ChangeTracker.Entries().Count()}\n";
			foreach (var entityEntry in _context.ChangeTracker.Entries())
			{
				msg += $"{entityEntry.State} : {entityEntry.OriginalValues.PropertyNames} >> {entityEntry.CurrentValues}\n";
			}
			MessageBox.Show(msg);
		}

		private void CommandNew_OnCanExecute(object sender, CanExecuteRoutedEventArgs e)
		{
			e.CanExecute = true;
		}

		private void CommandSave_OnCanExecute(object sender, CanExecuteRoutedEventArgs e)
		{
			MessageBox.Show("CommandSave_OnCanExecute");
			e.CanExecute = GridDetail.Visibility == Visibility.Visible;
		}

		private void CommandCancel_OnCanExecute(object sender, CanExecuteRoutedEventArgs e)
		{
			MessageBox.Show("CommandCancel_OnCanExecute");
			e.CanExecute = GridDetail.Visibility == Visibility.Visible;
		}

		private void CommandCloseTab_OnExecuted(object sender, ExecutedRoutedEventArgs e)
		{
			ParentTabControl.Items.Remove(this.Parent);
		}

		private void CommandCloseTab_OnCanExecute(object sender, CanExecuteRoutedEventArgs e)
		{
			MessageBox.Show("CommandCloseTab_OnCanExecute");
		}
	}
}