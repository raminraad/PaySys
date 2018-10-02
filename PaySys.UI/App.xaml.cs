using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using PaySys.UI.ExtensionMethods;

namespace PaySys
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

        private void TextBoxLostFocus(object sender, RoutedEventArgs e)
        {
            var currentTextBox = sender as TextBox;
            if (BindingOperations.GetBinding(sender as TextBox, TextBox.TextProperty)?.ValidationRules?.Count > 0)
                currentTextBox.GetBindingExpression(TextBox.TextProperty)?.ValidateWithoutUpdate();
        }
    }
}