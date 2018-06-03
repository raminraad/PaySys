using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using PaySys.Globalization;
using PaySys.ModelAndBindLib.Engine;
using PaySys.ModelAndBindLib.Model;


namespace PaySys.UI.UC
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
			GridDetail.DataContext = CurrentItem = (Employee) DtgMain.SelectedItem;
		}

		private void BtnFilter_OnClick(object sender, RoutedEventArgs e)
		{
			RefreshDtgMain();
		}

		private void RefreshDtgMain()
		{
			var index = DtgMain.SelectedIndex;
			if(TxtFilter.Text.Trim()==string.Empty)
				_lstMain = new ObservableCollection<Employee>(_context.Employees.ToList());
			else
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
			}
			DtgMain.DataContext = _lstMain;
			if (DtgMain.Items.Count > index)
				DtgMain.SelectedIndex = index;
		}

		private void BtnEmployeeAdd_OnClick(object sender, RoutedEventArgs e)
		{
			UcFormState.CurrentState = FormCurrentState.Add;
			GridDetail.DataContext = new Employee();
		}

		private void BtnEmployeeEdit_OnClick(object sender, RoutedEventArgs e)
		{
			UcFormState.CurrentState = FormCurrentState.Edit;
		}

		private void BtnEmployeeDelete_OnClick(object sender, RoutedEventArgs e)
		{
			String strmsg = ResourceAccessor.Messages.GetString("DeleteRecord", CultureInfo.CurrentCulture);
			var result = MessageBox.Show(strmsg, "Delete", MessageBoxButton.YesNo,
				MessageBoxImage.Question, MessageBoxResult.No);
			if (result != MessageBoxResult.Yes)
				return;
			var index = DtgMain.SelectedIndex;
			_context.Employees.Remove(CurrentItem);
			_lstMain.Remove(CurrentItem);
			if (DtgMain.Items.Count > index)
				DtgMain.SelectedIndex = index;
			_context.SaveChanges();
		}

		private void BtnEmployeeSave_OnClick(object sender, RoutedEventArgs e)
		{
			foreach (var textBox in GridDetail.Children.OfType<Control>())
			{
				textBox.GetBindingExpression(TextBox.TextProperty)?.UpdateSource();
				textBox.GetBindingExpression(Selector.SelectedItemProperty)?.UpdateSource();
			}
			if (UcFormState.CurrentState==FormCurrentState.Add)
				_context.Employees.Add((Employee)GridDetail.DataContext);
			_context.SaveChanges();
			RefreshDtgMain();
			UcFormState.CurrentState = FormCurrentState.Select;
		}

		private void BtnEmployeeCancel_OnClick(object sender, RoutedEventArgs e)
		{
			GridDetail.DataContext = null;
			DtgMain_OnSelectedCellsChanged(null, null);
			UcFormState.CurrentState = FormCurrentState.Select;
		}

		private void BtnEmployeeRefresh_OnClick(object sender, RoutedEventArgs e)
		{
			RefreshDtgMain();
		}
	}
}