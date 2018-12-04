using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Forms;
using System.Windows.Input;
using Arash.PersianDateControls;
using PaySys.Globalization.Fa;
using PaySys.Model.Entities;
using PaySys.Model.ExtensionMethods;
using Control = System.Windows.Controls.Control;
using TextBox = System.Windows.Controls.TextBox;
using UserControl = System.Windows.Controls.UserControl;

namespace PaySys.UI.UserControls
{
    /// <summary>
    /// Interaction logic for UcEmployeeDetail.xaml
    /// </summary>
    public partial class UcEmployeeEdit : UserControl
    {
        public UcEmployeeEdit()
        {
            InitializeComponent();
        }

	    public static readonly DependencyProperty ReadOnlyOfFieldsProperty = DependencyProperty.Register( "ReadOnlyOfFields", typeof(bool), typeof(UcEmployeeEdit), new PropertyMetadata( default(bool) ) );

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

        private void FileBrowse_Execute(object sender, ExecutedRoutedEventArgs e)
        {
            var op = new OpenFileDialog
            {
                Title = Messages.SelectPhotoFile,
                Filter = Labels.OpenFileDialogFilterForPhotos
            };
            if (op.ShowDialog() == DialogResult.OK)
            {
                var image = System.Drawing.Image.FromFile(op.FileName);
                ((Employee) DataContext).PhotoStream = image.ToByteArray();
                ImagePhoto.GetBindingExpression(Image.SourceProperty)?.UpdateTarget();
            }
        }

        private void FileBrowse_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = !ReadOnlyOfFields;
        }

        private void FileClear_Execute(object sender, ExecutedRoutedEventArgs e)
        {
            ((Employee)DataContext).PhotoStream = null;
            ImagePhoto.GetBindingExpression(Image.SourceProperty)?.UpdateTarget();

        }

        private void FileClear_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute= !ReadOnlyOfFields&&(DataContext as Employee)?.Photo!=null;
        }
    }
}
