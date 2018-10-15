using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
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
using PaySys.CalcLib.ExtensionMethods;
using PaySys.Globalization;
using PaySys.ModelAndBindLib.Engine;
using PaySys.ModelAndBindLib.Entities;
using MessageBox = System.Windows.Forms.MessageBox;
using UserControl = System.Windows.Controls.UserControl;

namespace PaySys.UI.UC
{
	/// <summary>
	/// Interaction logic for UcContractMng.xaml
	/// </summary>
	public partial class UcContractMng : UserControl
	{
		public  PaySysContext Context { set; get; } 
		private ObservableCollection<ContractMaster> _contractMastersAll;
		public ObservableCollection<Employee> EmployeesAll { set; get; }
        public UcContractMng()
		{
			InitializeComponent();
            Reload();
//			RefreshContracsAll();
//		    EmployeesAll = SmpUcSelectContOfEmp.EmployeesAll = new ObservableCollection<Employee>(Context.Employees);
			SmpUcShowContractMaster.MainGroups = new ObservableCollection<MainGroup>(Context.MainGroups);
			SmpUcShowContractMaster.SubGroups = new ObservableCollection<SubGroup>(Context.SubGroups);
			SmpUcShowContractMaster.Jobs = new ObservableCollection<Job>(Context.Jobs);

			SmpUcFormState.CurrentState = FormCurrentState.Select;
			SmpUcSelectContOfEmp.SelectedContractChanged += OnSelectedContractChanged;
		}

	    private void Reload()
	    {
	        var selectedId = (DataGridEmployees.SelectedItem as Employee)?.Id;
	        Context = new PaySysContext();
	        Context.Employees.Include(employee => employee.ContractMasters).Load();
	        EmployeesAll = Context.Employees.Local;
	        DataGridEmployees.GetBindingExpression(ItemsControl.ItemsSourceProperty)?.UpdateTarget();
	        DataGridEmployees.GetBindingExpression(DataContextProperty)?.UpdateTarget();
	        if (selectedId.HasValue)
	            DataGridEmployees.SelectedItem = EmployeesAll.FirstOrDefault(emp => emp.Id == selectedId.Value);
	        SmpUcLookup_OnLookupTextChanged(null, null);
        }

	    private void RefreshContracsAll()
		{
			_contractMastersAll = SmpUcSelectContOfEmp.ContractMastersAll =
				new ObservableCollection<ContractMaster>(Context.ContractMasters);
		}

		private void OnSelectedContractChanged(object sender, SelectionChangedEventArgs e)
		{
			SmpUcShowContractDetails.CurrentContractMaster = SmpUcShowContractMaster.CurrentContractMaster = SmpUcSelectContOfEmp.SelectedContractMaster;
		}

		private void Add_Executed(object sender, ExecutedRoutedEventArgs e)
		{
			SmpUcFormState.CurrentState = FormCurrentState.AddMaster;
			SmpUcShowContractDetails.CurrentContractMaster = null;
			SmpUcShowContractMaster.CurrentContractMaster =
				new ContractMaster {Employee = SmpUcSelectContOfEmp.SelectedEmployee,ContractDetails = new List<ContractDetail>()};
		}

		private void Edit_Executed(object sender, ExecutedRoutedEventArgs e)
		{
			SmpUcFormState.CurrentState = FormCurrentState.Edit;
		}

		private void Delete_Executed(object sender, ExecutedRoutedEventArgs e)
		{

		}

		private void Save_Executed(object sender, ExecutedRoutedEventArgs e)
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
			Context.SaveChanges();

			SmpUcFormState.CurrentState = FormCurrentState.Select;

			//		    SmpUcShowContractMaster.ReadOnlyOfEditControls = true;
		}

		private void BtnSaveContractMasterAndContinueToContractDetails_Click(object sender, RoutedEventArgs e)
		{
			SmpUcShowContractMaster.CommitContext();
			var newContract = SmpUcShowContractMaster.CurrentContractMaster;
//			var ContractFields = _context.ContractFields.Where(gft => gft.MainGroup.Equals( newContract.MainGroup ) && gft.Year == PaySysSetting.CurrentYear).ToList();
			var contractFields = newContract.MainGroup.CurrentContractFields.ToList();
			contractFields.ForEach(gft => newContract.ContractDetails.Add(new ContractDetail { ContractField = gft }));
			Context.ContractMasters.Add(newContract);
			Context.SaveChanges();
			SmpUcFormState.CurrentState = FormCurrentState.AddDetails;
			RefreshContracsAll();
			SmpUcSelectContOfEmp.SelectedContractMaster = newContract;

			SmpUcShowContractDetails.Focus();

		}

		private void Reload_Executed(object sender, ExecutedRoutedEventArgs e)
		{
			RefreshContracsAll();
		}

	    private void CrudCommands_CanExecute(object sender, CanExecuteRoutedEventArgs e)
	    {
	        if (SmpUcFormState != null) e.CanExecute = SmpUcFormState.EnabledOfCrudButtons;
	    }

	    private void EditCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
	    {
	        e.CanExecute = SmpUcFormState?.EnabledOfCrudButtons == true && SmpUcSelectContOfEmp.SelectedContractMaster!=null;
	    }

	    private void SaveCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
	    {
	        e.CanExecute = SmpUcFormState?.EnabledOfSaveDiscardButtons == true;

	    }

	    private void DiscardChangesCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
	    {
	        e.CanExecute = SmpUcFormState?.EnabledOfSaveDiscardButtons == true;
        }

	    private void DiscardChanges_Executed(object sender, ExecutedRoutedEventArgs e)
	    {
	        throw new NotImplementedException();
	    }

        private void SmpUcLookup_OnLookupTextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
