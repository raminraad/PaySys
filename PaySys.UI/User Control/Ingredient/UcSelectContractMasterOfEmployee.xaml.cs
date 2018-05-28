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
using PaySys.ModelAndBindLib.Model;

namespace PaySys.UI.UC
{
    /// <summary>
    /// Interaction logic for UcSelectContractMasterOfContractMaster.xaml
    /// </summary>
    public partial class UcSelectContractMasterOfEmployee : UserControl
    {
        public UcSelectContractMasterOfEmployee()
        {
            InitializeComponent();
			UcSelectEmp.SelectedEmployee
        }

	    public static readonly DependencyProperty SelectedContractMasterProperty = DependencyProperty.Register("SelectedContractMaster", typeof(ContractMaster), typeof(UcSelectContractMaster), new PropertyMetadata(default(ContractMaster)));

	    public ContractMaster SelectedContractMaster
	    {
		    get { return (ContractMaster)GetValue(SelectedContractMasterProperty); }
		    set { SetValue(SelectedContractMasterProperty, value); }
	    }

	    private void CmbContractMaster_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
	    {
		    SelectedContractMaster = (ContractMaster)CmbContractMaster.SelectedItem;
		}
	}
}
