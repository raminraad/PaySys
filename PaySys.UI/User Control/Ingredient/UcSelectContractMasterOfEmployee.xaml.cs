using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.EnterpriseServices;
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
	/// Interaction logic for UcSelectContractMasterOfContractMaster.xaml
	/// </summary>
	public partial class UcSelectContractMasterOfEmployee : UserControl
	{
		ObservableCollection<ContractMaster> ContractMasterList;
		ObservableCollection<ContractMaster> ContList = new ObservableCollection<ContractMaster>(new PaySysContext().ContractMasters);

		private PaySysContext _context = new PaySysContext();

		public UcSelectContractMasterOfEmployee()
		{
			InitializeComponent();
			UcSelectEmp.SelectedItemChanged += UcSelectEmpOnSelectedItemChanged;
		}

		private void UcSelectEmpOnSelectedItemChanged(object sender, SelectionChangedEventArgs selectionChangedEventArgs)
		{
			var lstContract = (from c in ContList
				where c.Employee.Equals(UcSelectEmp.SelectedEmployee)
				select c).ToList();
			ContractMasterList = new ObservableCollection<ContractMaster>(lstContract);
			CmbContractMaster.DataContext = ContractMasterList;
			;
		}

		public static readonly DependencyProperty SelectedContractMasterProperty = DependencyProperty.Register(
			"SelectedContractMaster", typeof(ContractMaster), typeof(UcSelectContractMasterOfEmployee),
			new PropertyMetadata(default(ContractMaster)));
		public ContractMaster SelectedContractMaster
		{
			get { return (ContractMaster) GetValue(SelectedContractMasterProperty); }
			set { SetValue(SelectedContractMasterProperty, value); }
		}

		private void CmbContractMaster_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			SelectedContractMaster = (ContractMaster) CmbContractMaster.SelectedItem;
		}
	}
}