using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using Bogus;
using PaySys.Globalization;
using PaySys.ModelAndBindLib.Entities;

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
            ValidationRules = new ObservableCollection<ValidationRule>();
        }

        public ValidateOn ValidateOn { get; set; } = ValidateOn.None;

        public static readonly DependencyProperty TextOfLabelProperty = DependencyProperty.Register("TextOfLabel",
            typeof(string), typeof(UcTextPair), new PropertyMetadata(default(string)));

        public string TextOfLabel
        {
            get => (string) GetValue(TextOfLabelProperty);
            set => SetValue(TextOfLabelProperty, value);
        }

        public static readonly DependencyProperty TextOfTextBoxProperty = DependencyProperty.Register("TextOfTextBox",
            typeof(string), typeof(UcTextPair), new PropertyMetadata(default(string)));

        public string TextOfTextBox
        {
            get => (string) GetValue(TextOfTextBoxProperty);
            set => SetValue(TextOfTextBoxProperty, value);
        }

        public static readonly DependencyProperty ReadOnlyProperty = DependencyProperty.Register("ReadOnly",
            typeof(bool), typeof(UcTextPair), new PropertyMetadata(default(bool)));

        public bool ReadOnly
        {
            get => (bool) GetValue(ReadOnlyProperty);
            set => SetValue(ReadOnlyProperty, value);
        }

        private ObservableCollection<ValidationRule> _validationRules;

        public ObservableCollection<ValidationRule> ValidationRules
        {
            get { return _validationRules; }
            set { _validationRules = value; }
        }

        public void UpdateSource()
        {
            TextBoxData.GetBindingExpression(TextBox.TextProperty)?.UpdateSource();
        }

        public void UpdateTarget()
        {
            TextBoxData.GetBindingExpression(TextBox.TextProperty)?.UpdateTarget();
            LabelData.GetBindingExpression(ContentProperty)?.UpdateTarget();
        }

        private void UcTextPair_OnLoaded(object sender, RoutedEventArgs e)
        {
            if (ValidationRules?.Count > 0)
            {
                if (ValidateOn == ValidateOn.None)
                    throw new ValidationException(
                        ResourceAccessor.Messages.GetString("DevNullValidateOnPropertyException"));
                foreach (var rule in ValidationRules)
                {
                    (TextBoxData.GetBindingExpression(TextBox.TextProperty)?.ParentBinding).ValidationRules.Add(rule);
                }

                if (ValidateOn == ValidateOn.TextChanged)
                    TextBoxData.TextChanged += (o, args) => Validator.ValidateTextBox(TextBoxData);
                else if (ValidateOn == ValidateOn.LostFocus)
                    TextBoxData.LostFocus += (o, args) => Validator.ValidateTextBox(TextBoxData);
            }
        }
    }
}