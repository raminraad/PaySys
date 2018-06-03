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
        public UcContractMng()
		{
            InitializeComponent();
			_contractMastersAll=new ObservableCollection<ContractMaster>(_context.ContractMasters);
			UcFormState.CurrentState=FormCurrentState.Select;
	        UcSelectContOfEmp.SelectedContractChanged += UcSelectContOfEmpOnSelectedContractChanged;
        }

	    private void UcSelectContOfEmpOnSelectedContractChanged(object sender, SelectionChangedEventArgs e)
	    {
		    var selectedContractMaster = ((ContractMaster) ((Selector) sender).SelectedItem);
		    UcShowContMaster.DataContext= UcShowContractDetails.DataContext = _contractMastersAll.FirstOrDefault(master => master.Equals(selectedContractMaster));
	    }

	    private void BtnContractMasterAdd_OnClick(object sender, RoutedEventArgs e)
	    {
			UcFormState.CurrentState = FormCurrentState.Add;
		}

	    private void BtnContractMasterEdit_OnClick(object sender, RoutedEventArgs e)
	    {
			UcFormState.CurrentState = FormCurrentState.Edit;
		}

		private void BtnContractMasterDelete_OnClick(object sender, RoutedEventArgs e)
	    {

	    }

	    private void BtnContractMasterSave_OnClick(object sender, RoutedEventArgs e)
	    {
			UcShowContractDetails.UpdateDataSources();
			
		    UcShowContractDetails.GetBindingExpression(DataContextProperty)?.UpdateSource();
			if (UcFormState.CurrentState == FormCurrentState.Add)
			    _context.ContractMasters.Add((ContractMaster)UcShowContMaster.DataContext);
		    _context.SaveChanges();
		    
		    UcFormState.CurrentState = FormCurrentState.Select;
		}
    }
}
