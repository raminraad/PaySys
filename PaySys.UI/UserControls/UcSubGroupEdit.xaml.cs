using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace PaySys.UI.UserControls
{
    /// <summary>
    /// Interaction logic for UcSubGroupEdit.xaml
    /// </summary>
    public partial class UcSubGroupEdit : UserControl
    {
        public UcSubGroupEdit()
        {
            InitializeComponent();
        }
        public static readonly DependencyProperty ReadOnlyOfFieldsProperty = DependencyProperty.Register("ReadOnlyOfFields", typeof(bool), typeof(UcSubGroupEdit), new PropertyMetadata(default(bool)));

        public bool ReadOnlyOfFields
        {
            get => (bool)GetValue(ReadOnlyOfFieldsProperty);
            set => SetValue(ReadOnlyOfFieldsProperty, value);
        }

        public void UpdateSource()
        {
            foreach (var control in GridDetail.Children.OfType<Control>())
            {
                control.GetBindingExpression(TextBox.TextProperty)?.UpdateSource();
                control.GetBindingExpression(ToggleButton.IsCheckedProperty)?.UpdateSource();
            }
        }

        public void UpdateTarget()
        {
            foreach (var control in GridDetail.Children.OfType<Control>())
            {
                control.GetBindingExpression(TextBox.TextProperty)?.UpdateTarget();
                control.GetBindingExpression(ToggleButton.IsCheckedProperty)?.UpdateTarget();
            }
        }

        
    }
}
