using System;
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
using Arash.PersianDateControls;

namespace PaySys.UI.UC
{
    /// <summary>
    /// Interaction logic for UcEmployeeDetail.xaml
    /// </summary>
    public partial class UcEmployeeDetails : UserControl
    {
        public UcEmployeeDetails()
        {
            InitializeComponent();
        }

	    public static readonly DependencyProperty ReadOnlyOfFieldsProperty = DependencyProperty.Register( "ReadOnlyOfFields", typeof(bool), typeof(UcEmployeeDetails), new PropertyMetadata( default(bool) ) );

	    public bool ReadOnlyOfFields
	    {
		    get => (bool) GetValue( ReadOnlyOfFieldsProperty );
	        set => SetValue( ReadOnlyOfFieldsProperty, value );
	    }

	    public void UpdateSource()
	    {
		    foreach (var control in GridDetail.Children.OfType<Control>())
		    {
			    control.GetBindingExpression(TextBox.TextProperty)?.UpdateSource();
			    control.GetBindingExpression(Selector.SelectedItemProperty)?.UpdateSource();
			    control.GetBindingExpression(PersianDatePicker.SelectedDateProperty)?.UpdateSource();
		    }
	    }

	    public void UpdateTarget()
	    {
		    foreach (var control in GridDetail.Children.OfType<Control>())
		    {
			    control.GetBindingExpression(TextBox.TextProperty)?.UpdateTarget();
			    control.GetBindingExpression(Selector.SelectedItemProperty)?.UpdateTarget();
			    control.GetBindingExpression(PersianDatePicker.SelectedDateProperty)?.UpdateTarget();
		    }
	    }
    }
}
