using System;
using System.Windows;
using System.Windows.Controls;

namespace PaySys.UI.UserControls
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
