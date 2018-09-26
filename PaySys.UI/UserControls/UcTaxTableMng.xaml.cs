using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using PaySys.CalcLib.Delegates;
using PaySys.ModelAndBindLib.Entities;
using UserControl = System.Windows.Controls.UserControl;

namespace PaySys.UI.UC
{
	/// <summary>
	/// Interaction logic for UcTaxTableMng.xaml
	/// </summary>
	public partial class UcTaxTableMng : UserControl
	{
	    private CollectionViewSource CvsTaxRows => Resources["CvsTaxRows"] as CollectionViewSource;
        public UcTaxTableMng()
		{
			InitializeComponent();
            CvsTaxRows.SortDescriptions.Add(new SortDescription("TempValueTo",ListSortDirection.Ascending));
//			ListViewTaxItem.Items.Filter = o => ((Misc) o).Year == PaySysSetting.CurrentYear && ((Misc) o).Month == PaySysSetting.CurrentMonth;
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