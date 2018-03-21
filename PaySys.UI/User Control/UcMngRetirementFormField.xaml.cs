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
using PaySys.EF;

namespace PaySys.UI.User_Control
{
	/// <summary>
	/// Interaction logic for UcMngRetirementFormField.xaml
	/// </summary>
	public partial class UcMngRetirementFormField : UserControl
	{
		private readonly List<RetirementFormField> _lstMain;
		private readonly PaySysContext _context = new PaySysContext();

		#region Prop

		public TabState State { get; set; }
		public TabControl ParentTabControl { get; set; }

		#endregion

		#region Ctor

		public UcMngRetirementFormField()
		{
			InitializeComponent();
			_lstMain = _context.RetirementFormFields.ToList();
			LstMain.ItemsSource = _lstMain;
		}

		#endregion

		#region Edit

		private void CommandEdit_OnCanExecute(object sender, CanExecuteRoutedEventArgs e)
		{
			bool canEdit;
			if (LstMain != null)
				canEdit = LstMain.SelectedItem != null;
			else
			{
				canEdit = false;
			}
			e.CanExecute = canEdit & State == TabState.View;
		}

		private void CommandEdit_OnExecuted(object sender, ExecutedRoutedEventArgs e)
		{
			State = TabState.Edit;
			TxtTitle.Focus();
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
			BindingExpression binding = TxtTitle.GetBindingExpression(TextBox.TextProperty);
			binding.UpdateSource();
			_context.SaveChanges();
			State = TabState.View;
			LstMain.Focus();
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
			LstMain.Focus();
		}

		private void CommandCancel_OnCanExecute(object sender, CanExecuteRoutedEventArgs e)
		{
			e.CanExecute = State == TabState.Add || State == TabState.Edit;
		}

		#endregion

		private void LstMain_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			GridDetail.DataContext = LstMain.SelectedItem;
		}
	}
}