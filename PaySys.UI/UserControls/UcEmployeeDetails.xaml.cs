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
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Arash.PersianDateControls;
using PaySys.CalcLib.ExtensionMethods;
using PaySys.Globalization;
using PaySys.Globalization.Fa;
using PaySys.ModelAndBindLib.Entities;
using Control = System.Windows.Controls.Control;
using TextBox = System.Windows.Controls.TextBox;
using UserControl = System.Windows.Controls.UserControl;

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
