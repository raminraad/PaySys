using System.ComponentModel;
using System.Windows;
using PaySys.Model.Entities;
using UserControl = System.Windows.Controls.UserControl;

namespace PaySys.UI.UserControls
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