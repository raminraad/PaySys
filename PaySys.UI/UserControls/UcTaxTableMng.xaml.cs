using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using PaySys.Model.Entities;
using PaySys.Model.ExtensionMethods;
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

	    public void UpdateSource()
	    {
	        foreach (var control in GridMain.FindVisualChildren<TextBox>())
	        {
	            control.GetBindingExpression(TextBox.TextProperty)?.UpdateSource();
	        }
	    }

	    public void UpdateTarget()
	    {
	        foreach (var control in GridMain.FindVisualChildren<TextBox>())
            {
	            control.GetBindingExpression(TextBox.TextProperty)?.UpdateTarget();
	        }
	    }
    }
}