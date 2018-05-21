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
	/// Interaction logic for UcEmployeeMng.xaml
	/// </summary>
	public partial class UcEmployeeMng : UserControl
	{
		private List<Employee> _lstMain;
		private PaySysContext _context = new PaySysContext();

		public UcEmployeeMng()
		{
			InitializeComponent();
			_lstMain = _context.Employees.ToList();
			GridMain.ItemsSource = _lstMain;
		}

		private void GridMain_OnSelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
		{
			GrdDetail.DataContext = (Employee)GridMain.SelectedItem;
			//			GridMain.ScrollIntoView(GridMain.SelectedItem);
		}



		private void BtnFilter_OnClick(object sender, RoutedEventArgs e)
		{
			var filters = TxtFilter.Text.Split(' ');
			List<List<Employee>> lists = new List<List<Employee>>();
			foreach (var strFilter in filters)
			{
				lists.Add((from x in _context.Employees
						   where x.FName.Contains(strFilter) || x.LName.Contains(strFilter) || x.DossierNo.Contains(strFilter)
						   select x).ToList());
			}
			var filteredList = _context.Employees.ToList();
			lists.ForEach(x => filteredList.RemoveAll(employee => filteredList.Except(x).Contains(employee)));
			_lstMain = filteredList;
			GridMain.ItemsSource = _lstMain;
		}
	}
}
