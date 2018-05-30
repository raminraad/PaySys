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
using PaySys.ModelAndBindLib.Model;

namespace PaySys.UI.UC
{
    /// <summary>
    /// Interaction logic for UcSelectEmployee.xaml
    /// </summary>
    public partial class UcSelectEmployee : UserControl
    {
		ObservableCollection<Employee> EmployeeList = new ObservableCollection<Employee>(new PaySysContext().Employees);
	    public event SelectionChangedEventHandler SelectedItemChanged;
        public UcSelectEmployee()
        {
            InitializeComponent();
			DataContext=EmployeeList;
        }

	    public Employee SelectedEmployee
	    {
		    get => CmbEmployee.SelectedItem as Employee;
		    set => CmbEmployee.SelectedItem=value;
	    }
	    private void CmbEmployee_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
	    {
		    if (e.AddedItems.Count > 0)
		    {
			    SelectedEmployee = (Employee) ((Selector) sender).SelectedItem;
			    SelectedItemChanged?.Invoke(sender, e);
		    }
	    }
    }
}
