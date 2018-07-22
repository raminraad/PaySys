using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.EnterpriseServices;
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
	/// Interaction logic for UcSelectContractMasterOfContractMaster.xaml
	/// </summary>
	public partial class UcSelectContractMasterOfEmployee : UserControl
	{
		public event SelectionChangedEventHandler SelectedContractChanged;
//		private PaySysContext _context = new PaySysContext();

		public UcSelectContractMasterOfEmployee()
		{
			InitializeComponent();
			UcSelectEmp.SelectedItemChanged += OnSelectedEmployeeChanged;
		}

		public ObservableCollection<ContractMaster> ContractMastersAll
		{
			set => CmbContractMaster.DataContext = value;
			get => CmbContractMaster.DataContext as ObservableCollection<ContractMaster>;
		}
		public ObservableCollection<Employee> EmployeesAll
		{
			set => UcSelectEmp.EmployeesAll = value;
			get => UcSelectEmp.EmployeesAll;
		}
		private void OnSelectedEmployeeChanged(object sender, SelectionChangedEventArgs e)
		{
			/*//			if (e.AddedItems.Count != 1)
			//				return;

						var lstContract = (from c in ContractMastersAll
										   where c.Employee.Equals(SelectedEmployee)
										   orderby c.DateExport
										   select c).ToList();
						_contractMastersOfCurrentEmployee = new ObservableCollection<ContractMaster>(lstContract);*/
			CmbContractMaster.Items.Filter=o => ((ContractMaster) o).Employee.Equals(SelectedEmployee);

			//	CmbContractMaster.DataContext = _contractMastersOfCurrentEmployee;

			//			if (_contractMastersOfCurrentEmployee.Count > 0)
			//				CmbContractMaster.SelectedItem = _contractMastersOfCurrentEmployee.FirstOrDefault(c => c.IsCurrentContract);
			if (CmbContractMaster.Items.Count > 0)
				CmbContractMaster.SelectedItem = ContractMastersAll.Where(c => c.Employee.Equals(SelectedEmployee))
					.FirstOrDefault(c => c.IsCurrentContract);
		}

		//		public static readonly DependencyProperty SelectedContractMasterProperty = DependencyProperty.Register("SelectedContractMaster", typeof(ContractMaster), typeof(UcSelectContractMasterOfEmployee), new PropertyMetadata(default(ContractMaster)));
		//
		//		public ContractMaster SelectedContractMaster
		//		{
		//			get { return (ContractMaster) GetValue(SelectedContractMasterProperty); }
		//			set { SetValue(SelectedContractMasterProperty, value); }
		//		}
		public Employee SelectedEmployee
		{
			get => UcSelectEmp.SelectedEmployee;
			set => UcSelectEmp.SelectedEmployee = value;
		}
		public ContractMaster SelectedContractMaster
		{
			get => CmbContractMaster.SelectedItem as ContractMaster;
			set => CmbContractMaster.SelectedItem = value;
		}

		private void CmbContractMaster_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
		{
//			if (e.AddedItems.Count > 0)
//			{
//				SelectedContractMaster = (ContractMaster) ((Selector) sender).SelectedItem;
				SelectedContractChanged?.Invoke(sender, e);
//			}
		}
		
	}
}