﻿using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
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
using PaySys.UI.ExtensionMethods;

namespace PaySys.UI.UC
{
    /// <summary>
    /// Interaction logic for SmpUcShowContractDetails.xaml
    /// </summary>
    public partial class UcShowContractDetails : UserControl
    {
	    
        public UcShowContractDetails()
        {
            InitializeComponent();
        }

		public static readonly DependencyProperty ReadOnlyOfEditFieldsProperty = DependencyProperty.Register("ReadOnlyOfEditFields", typeof(bool), typeof(UcShowContractDetails), new PropertyMetadata(default(bool)));

	    public bool ReadOnlyOfEditFields

	    {
		    get { return (bool)GetValue(ReadOnlyOfEditFieldsProperty); }
		    set { SetValue(ReadOnlyOfEditFieldsProperty, value); }
	    }

		public ContractMaster CurrentContractMaster
	    {
		    get => (ContractMaster)DataContext;
		    set => DataContext = value;
	    }

		public void CommitContext()
	    {
			foreach (var cnt in this.FindVisualChildren<Control>())
			{
				cnt.GetBindingExpression(TextBox.TextProperty)?.UpdateSource();
			    cnt.GetBindingExpression(Selector.SelectedItemProperty)?.UpdateSource();
		    }
		    GetBindingExpression(DataContextProperty)?.UpdateSource();

		}
	}
}
