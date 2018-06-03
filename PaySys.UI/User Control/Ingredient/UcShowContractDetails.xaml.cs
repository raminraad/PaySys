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
    /// Interaction logic for UcShowContractDetails.xaml
    /// </summary>
    public partial class UcShowContractDetails : UserControl
    {
	    public ContractMaster CurrentContractMaster { get; set; }
        public UcShowContractDetails()
        {
            InitializeComponent();
        }

	    private void UcShowContractDetails_OnDataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
	    {
		    
	    }
    }
}
