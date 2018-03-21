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
    /// Interaction logic for UcMngEmployee.xaml
    /// </summary>
    public partial class UcMngEmployee : UserControl
    {
	    private List<Employee> _lstMain;
	    private PaySysContext _context = new PaySysContext();
		public UcMngEmployee()
        {
            InitializeComponent();
			_lstMain = _context.Employees.ToList();
		    GridMain.ItemsSource = _lstMain;
        }

	    private void GridMain_OnSelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
	    {
		    GrdDetail.DataContext = (Employee) GridMain.SelectedItem;
	    }
    }
}
