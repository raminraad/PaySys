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

namespace PaySys.UI.UC
{
    /// <summary>
    /// Interaction logic for UcTextPair.xaml
    /// </summary>
    public partial class UcTextPair : UserControl
    {
        public UcTextPair()
        {
            InitializeComponent();
        }

	    public static readonly DependencyProperty TextProperty = DependencyProperty.Register( "Text", typeof(string), typeof(UcTextPair), new PropertyMetadata( default(string) ) );

	    public string Text { get { return (string) GetValue( TextProperty ); } set { SetValue( TextProperty, value ); } }

	    public static readonly DependencyProperty ReadOnlyProperty = DependencyProperty.Register( "ReadOnly", typeof(bool), typeof(UcTextPair), new PropertyMetadata( default(bool) ) );

	    public bool ReadOnly { get { return (bool) GetValue( ReadOnlyProperty ); } set { SetValue( ReadOnlyProperty, value ); } }


	    public void UpdateSource()
	    {
			    TextBoxData.GetBindingExpression(TextBox.TextProperty)?.UpdateSource();
			    TextBlockData.GetBindingExpression(TextBlock.TextProperty)?.UpdateSource();
	    }

	    public void UpdateTarget()
	    {
		    TextBoxData.GetBindingExpression(TextBox.TextProperty).UpdateTarget();
		    TextBlockData.GetBindingExpression(TextBlock.TextProperty).UpdateTarget();
	    }
    }
}
