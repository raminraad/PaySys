using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Controls;
using PaySys.Model.Entities;

namespace PaySys.UI.UserControls
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