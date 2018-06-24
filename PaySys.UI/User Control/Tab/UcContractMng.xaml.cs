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
    /// Interaction logic for UcContractMng.xaml
    /// </summary>
    public partial class UcContractMng : UserControl
    {
		private readonly PaySysContext _context = new PaySysContext();
	    private ObservableCollection<ContractMaster> _contractMastersAll;
	    private ObservableCollection<Employee> _employeesAll;
        public UcContractMng()
		{
            InitializeComponent();
			_contractMastersAll= UcSelectContOfEmp.ContractMastersAll = new ObservableCollection<ContractMaster>(_context.ContractMasters);
			_employeesAll= UcSelectContOfEmp.EmployeesAll = new ObservableCollection<Employee>(_context.Employees);
			SmpUcShowContractMaster.MainGroups = new ObservableCollection<MainGroup>(_context.MainGroups);
			SmpUcShowContractMaster.SubGroups = new ObservableCollection<SubGroup>(_context.SubGroups);
			SmpUcShowContractMaster.Jobs = new ObservableCollection<Job>(_context.Jobs);


//			Binding bindingReadonlyFields = new Binding("ReadOnlyFields")
//			{
//				Source = SmpUcFormState
//			};
//			SmpUcShowContractMaster.SetBinding(UcShowContractMaster.ReadOnlyFieldsProperty, bindingReadonlyFields);
//			SmpUcShowContractDetails.SetBinding(UcShowContractDetails.ReadOnlyFieldsProperty, bindingReadonlyFields);


			SmpUcFormState.CurrentState=FormCurrentState.Select;
	        UcSelectContOfEmp.SelectedContractChanged += OnSelectedContractChanged;
        }

	    private void OnSelectedContractChanged(object sender, SelectionChangedEventArgs e)
	    {
		    SmpUcShowContractDetails.CurrentContractMaster= SmpUcShowContractMaster.CurrentContractMaster = UcSelectContOfEmp.SelectedContractMaster;
		}

	    private void BtnContractMasterAdd_OnClick(object sender, RoutedEventArgs e)
	    {
			SmpUcFormState.CurrentState = FormCurrentState.Add;
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
			SmpUcShowContractDetails.CommitContext();
			SmpUcShowContractMaster.CommitContext();

			if (SmpUcFormState.CurrentState == FormCurrentState.Add)
			    _context.ContractMasters.Add((ContractMaster)SmpUcShowContractMaster.DataContext);
		    _context.SaveChanges();

		    SmpUcFormState.CurrentState = FormCurrentState.Select;
		    
		    SmpUcShowContractMaster.ReadOnlyFields = true;
		    RaisePropertyChanged



		}
    }
}
