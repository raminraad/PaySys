using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PaySys.UI.UC
{
    /// <summary>
    /// Interaction logic for UcTabHeader.xaml
    /// </summary>
    public partial class UcTabHeader : UserControl
    {
        public UcTabHeader()
        {
            InitializeComponent();
        }

        public string Text
        {
            get => TextBlockHeader.Text;
            set => TextBlockHeader.Text = value;
        }

        public Action CloseMe;

        private void CloseButton_OnClick(object sender, RoutedEventArgs e)
        {
            CloseMe?.Invoke();
        }
    }
}
