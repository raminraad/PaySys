using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        public UcSelectEmployee()
        {
            InitializeComponent();
			DataContext=EmployeeList;
        }

	    public static readonly DependencyProperty SelectedEmployeeProperty = DependencyProperty.Register("SelectedEmployee", typeof(Employee), typeof(UcSelectEmployee), new PropertyMetadata(default(Employee)));

	    public Employee SelectedEmployee
	    {
		    get { return (Employee) GetValue(SelectedEmployeeProperty); }
		    set { SetValue(SelectedEmployeeProperty, value); }
	    }
	    private void CmbSelector_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
	    {
		    SelectedEmployee = (Employee)CmbEmployee.SelectedItem;
	    }
    }
}
