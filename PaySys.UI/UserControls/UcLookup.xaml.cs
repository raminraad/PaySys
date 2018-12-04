using System.Windows;
using System.Windows.Controls;

namespace PaySys.UI.UserControls
{
    /// <summary>
    /// Interaction logic for UcLookup.xaml
    /// </summary>
    public partial class UcLookup : UserControl
    {
        public event TextChangedEventHandler LookupTextChanged;

        public UcLookup()
        {
            InitializeComponent();
        }

        private void TextBoxLookup_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            LookupTextChanged?.Invoke(sender, e);
        }

        public string LookupText => TextBoxLookup.Text.Trim();

        public void Select()
        {
            TextBoxLookup.Focus();
            TextBoxLookup.SelectAll();
        }

        private void ButtonClear_OnClick(object sender, RoutedEventArgs e)
        {
            TextBoxLookup.Clear();
        }
    }
}
