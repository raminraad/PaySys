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
    }
}
