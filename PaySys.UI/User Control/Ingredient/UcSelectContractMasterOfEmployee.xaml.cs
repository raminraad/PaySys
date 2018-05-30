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
		private ObservableCollection<ContractMaster> _contListForEmp;
		private readonly ObservableCollection<ContractMaster> _contListAll = new ObservableCollection<ContractMaster>(new PaySysContext().ContractMasters);
		public event SelectionChangedEventHandler SelectedContractChanged;
		private PaySysContext _context = new PaySysContext();

		public UcSelectContractMasterOfEmployee()
		{
			InitializeComponent();
			UcSelectEmp.SelectedItemChanged += UcSelectEmpOnSelectedItemChanged;
		}

		private void UcSelectEmpOnSelectedItemChanged(object sender, SelectionChangedEventArgs e)
		{
			if (e.AddedItems.Count != 1)
				return;
			var lstContract = (from c in _contListAll
							   where c.Employee.Equals(e.AddedItems[0])
							   orderby c.DateExport
							   select c).ToList();
			_contListForEmp = new ObservableCollection<ContractMaster>(lstContract);
			CmbContractMaster.DataContext = _contListForEmp;
			if (_contListForEmp.Count > 0)
				CmbContractMaster.SelectedItem = _contListForEmp.First(c => c.IsCurrentContract);
		}

		public ContractMaster SelectedContractMaster
		{
			get => CmbContractMaster.SelectedItem as ContractMaster;
			set => CmbContractMaster.SelectedItem = value;
		}

		private void CmbContractMaster_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			if (e.AddedItems.Count > 0)
			{
				SelectedContractMaster = (ContractMaster) ((Selector) sender).SelectedItem;
				SelectedContractChanged?.Invoke(sender, e);
			}
		}
	}
}