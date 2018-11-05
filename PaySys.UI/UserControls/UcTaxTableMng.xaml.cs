using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
using PaySys.CalcLib.Delegates;
using PaySys.CalcLib.ExtensionMethods;
using PaySys.ModelAndBindLib.Entities;
using UserControl = System.Windows.Controls.UserControl;

namespace PaySys.UI.UC
{
	/// <summary>
	/// Interaction logic for UcTaxTableMng.xaml
	/// </summary>
	public partial class UcTaxTableMng : UserControl
	{
	    
        public UcTaxTableMng()
		{
			InitializeComponent();
            ListViewTaxItem.Items.SortDescriptions.Add(new SortDescription("TempValueTo",ListSortDirection.Ascending));

		}
	    
        public static readonly DependencyProperty CurrentSubGroupProperty = DependencyProperty.Register("CurrentSubGroup", typeof(SubGroup), typeof(UcTaxTableMng), new PropertyMetadata(default(SubGroup)));
	    public static readonly DependencyProperty ReadOnlyOfFieldsProperty = DependencyProperty.Register("ReadOnlyOfFields", typeof(bool), typeof(UcTaxTableMng), new PropertyMetadata(default(bool)));

	    public SubGroup CurrentSubGroup
		{
			get => (SubGroup) GetValue(CurrentSubGroupProperty);
	        set => SetValue(CurrentSubGroupProperty, value);
	    }

	    public bool ReadOnlyOfFields
	    {
	        get => (bool) GetValue(ReadOnlyOfFieldsProperty);
	        set => SetValue(ReadOnlyOfFieldsProperty, value);
	    }

	    
    }
}