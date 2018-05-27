using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using PaySys.ModelAndBindLib.Engine;
using PaySys.ModelAndBindLib.Model;

namespace PaySys.UI.User_Control
{
	/// <summary>Interaction logic for UcEmployeeMng.xaml</summary>
	public partial class UcEmployeeMng : UserControl
	{
		private readonly PaySysContext _context = new PaySysContext();
//		private List<Employee> _lstMain;
		private ObservableCollection<Employee> _lstMain;
		public Employee CurrentItem { get; set; }

		public UcEmployeeMng()
		{
			InitializeComponent();
			RefreshDtgMain();
			UcFormState.CurrentState = FormCurrentState.Select;
		}

		private void DtgMain_OnSelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
		{
			GrdDetail.DataContext = CurrentItem = (Employee) DtgMain.SelectedItem;
		}

		private void BtnFilter_OnClick(object sender, RoutedEventArgs e)
		{
			RefreshDtgMain();
		}

		private void RefreshDtgMain()
		{
			var filters = TxtFilter.Text.Split(' ');
			var lists = new List<List<Employee>>();
			foreach (var strFilter in filters)
				lists.Add((from x in _context.Employees
					where x.FName.Contains(strFilter) || x.LName.Contains(strFilter) || x.DossierNo.Contains(strFilter)
					select x).ToList());
			var filteredList = _context.Employees.ToList();
			lists.ForEach(x => filteredList.RemoveAll(employee => filteredList.Except(x).Contains(employee)));
			_lstMain = new ObservableCollection<Employee>(filteredList);
			DtgMain.DataContext = _lstMain;
		}

		private void BtnEmployeeAdd_OnClick(object sender, RoutedEventArgs e)
		{
			GrdDetail.DataContext = new Employee();
			UcFormState.CurrentState = FormCurrentState.Add;
		}

		private void BtnEmployeeEdit_OnClick(object sender, RoutedEventArgs e)
		{
			UcFormState.CurrentState = FormCurrentState.Edit;
		}

		private void BtnEmployeeSave_OnClick(object sender, RoutedEventArgs e)
		{
			foreach (var textBox in GrdDetail.Children.OfType<Control>())
			{
				textBox.GetBindingExpression(TextBox.TextProperty)?.UpdateSource();
				textBox.GetBindingExpression(Selector.SelectedItemProperty)?.UpdateSource();
			}
			_context.SaveChanges();
			UcFormState.CurrentState = FormCurrentState.Select;
		}

		private void BtnEmployeeCancel_OnClick(object sender, RoutedEventArgs e)
		{
			GrdDetail.DataContext = null;
			DtgMain_OnSelectedCellsChanged(null, null);
			UcFormState.CurrentState = FormCurrentState.Select;
		}

		private void BtnEmployeeDelete_OnClick(object sender, RoutedEventArgs e)
		{
			var result = MessageBox.Show("About to delete the current row.\n\nProceed?", "Delete", MessageBoxButton.YesNo,
				MessageBoxImage.Question, MessageBoxResult.No);
			if (result == MessageBoxResult.Yes)
			{
				var index = DtgMain.SelectedIndex;
				_context.Employees.Remove(CurrentItem);
				_lstMain.Remove(CurrentItem);
				if (DtgMain.Items.Count > index)
					DtgMain.SelectedIndex = index;
				_context.SaveChanges();
			}
		}
	}
}