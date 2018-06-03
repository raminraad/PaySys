﻿using System;
using System.Collections.Generic;
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
using PaySys.ModelAndBindLib.Model;

namespace PaySys.UI.UC
{
    /// <summary>
    /// Interaction logic for UcContractMng.xaml
    /// </summary>
    public partial class UcContractMng : UserControl
    {
        public UcContractMng()
        {
            InitializeComponent();
	        UcSelectContOfEmp.SelectedContractChanged += UcSelectContOfEmpOnSelectedContractChanged;
        }

	    private void UcSelectContOfEmpOnSelectedContractChanged(object sender, SelectionChangedEventArgs e)
	    {
		    UcShowContMaster.DataContext = ((Selector) sender).SelectedItem;
			UcShowContractDetails.DataContext= ((ContractMaster)((Selector)sender).SelectedItem).ContractDetails;
		}
    }
}
