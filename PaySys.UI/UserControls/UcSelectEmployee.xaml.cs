using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using PaySys.ModelAndBindLib.Engine;
using PaySys.ModelAndBindLib.Entities;

namespace PaySys.UI.UC
{
	public partial class UcSelectEmployee : UserControl
	{
		public event SelectionChangedEventHandler SelectedItemChanged;

		public UcSelectEmployee()
		{
			InitializeComponent();
		}

		public Employee SelectedEmployee
		{
			get => CmbEmployee.SelectedItem as Employee;
			set => CmbEmployee.SelectedItem = value;
		}
		public ObservableCollection<Employee> EmployeesAll
		{
			set
			{
				var selectedId = SelectedEmployee?.Id;
				DataContext = value;
				SelectedEmployee = EmployeesAll.FirstOrDefault(emp => emp.Id == selectedId);
			}
			get => DataContext as ObservableCollection<Employee>;
		}

		private void CmbEmployee_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			SelectedItemChanged?.Invoke(sender, e);
		}
	}
}