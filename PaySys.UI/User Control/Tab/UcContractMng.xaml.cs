using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.Remoting.Contexts;
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
	/// Interaction logic for UcContractMng.xaml
	/// </summary>
	public partial class UcContractMng : UserControl
	{
		private PaySysContext _context = new PaySysContext();
		private ObservableCollection<ContractMaster> _contractMastersAll;
		private ObservableCollection<Employee> _employeesAll;
		public UcContractMng()
		{
			InitializeComponent();
			RefreshContracsAll();
			_employeesAll = SmpUcSelectContOfEmp.EmployeesAll = new ObservableCollection<Employee>(_context.Employees);
			SmpUcShowContractMaster.MainGroups = new ObservableCollection<MainGroup>(_context.MainGroups);
			SmpUcShowContractMaster.SubGroups = new ObservableCollection<SubGroup>(_context.SubGroups);
			SmpUcShowContractMaster.Jobs = new ObservableCollection<Job>(_context.Jobs);

			SmpUcFormState.CurrentState = FormCurrentState.Select;
			SmpUcSelectContOfEmp.SelectedContractChanged += OnSelectedContractChanged;
		}

		private void RefreshContracsAll()
		{
			_contractMastersAll = SmpUcSelectContOfEmp.ContractMastersAll =
				new ObservableCollection<ContractMaster>(_context.ContractMasters);
		}

		private void OnSelectedContractChanged(object sender, SelectionChangedEventArgs e)
		{
			SmpUcShowContractDetails.CurrentContractMaster = SmpUcShowContractMaster.CurrentContractMaster = SmpUcSelectContOfEmp.SelectedContractMaster;
		}

		private void BtnContractMasterAdd_OnClick(object sender, RoutedEventArgs e)
		{
			SmpUcFormState.CurrentState = FormCurrentState.AddMaster;
			SmpUcShowContractDetails.CurrentContractMaster = null;
			SmpUcShowContractMaster.CurrentContractMaster =
				new ContractMaster {Employee = SmpUcSelectContOfEmp.SelectedEmployee,ContractDetails = new List<ContractDetail>()};
		}

		private void BtnContractMasterEdit_OnClick(object sender, RoutedEventArgs e)
		{
			SmpUcFormState.CurrentState = FormCurrentState.Edit;
		}

		private void BtnContractMasterDelete_OnClick(object sender, RoutedEventArgs e)
		{

		}

		private void BtnContractMasterSave_OnClick(object sender, RoutedEventArgs e)
		{
			switch (SmpUcFormState.CurrentState)
			{
				case FormCurrentState.AddMaster:
					
					break;
				case FormCurrentState.Edit:
					SmpUcShowContractDetails.CommitContext();
					SmpUcShowContractMaster.CommitContext();
					break;
			}
			_context.SaveChanges();

			SmpUcFormState.CurrentState = FormCurrentState.Select;

			//		    SmpUcShowContractMaster.ReadOnlyFields = true;
		}

		private void BtnSaveContractMasterAndContinueToContractDetails_Click(object sender, RoutedEventArgs e)
		{
			SmpUcShowContractMaster.CommitContext();
			var newContract = SmpUcShowContractMaster.CurrentContractMaster;
			var groupContractFieldTitles = _context.GroupContractFieldTitles.Where(gft => gft.MainGroup.MainGroupId==newContract.SubGroup.MainGroup.MainGroupId && gft.Year == 97).ToList();
			groupContractFieldTitles.ForEach(gft => newContract.ContractDetails.Add(new ContractDetail { GroupContractFieldTitle = gft }));
			_context.ContractMasters.Add(newContract);
			_context.SaveChanges();
			SmpUcFormState.CurrentState = FormCurrentState.AddDetails;
			RefreshContracsAll();
			SmpUcSelectContOfEmp.SelectedContractMaster = newContract;

			SmpUcShowContractDetails.Focus();

		}

		private void BtnContractMasterRefresh_OnClick(object sender, RoutedEventArgs e)
		{
			RefreshContracsAll();
		}
	}
}
