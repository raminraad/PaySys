using System.Windows;
using System.Windows.Controls;
using PaySys.Model.Validation.INotifyDataErrorInfo;

namespace PaySys.UI
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void ListItemGotFocus(object sender, RoutedEventArgs e)
        {
            (e.OriginalSource as TextBox)?.SelectAll();
        }

        private void TextBoxTextChanged(object sender, RoutedEventArgs e)
        {
            var currentTextBox = sender as TextBox;
            Validator.ValidateTextBox(currentTextBox);
        }
    }
}