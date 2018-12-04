using System;
using System.Windows.Controls;
using System.Windows.Media;

namespace PaySys.UI.UserControls
{
    /// <summary>
    /// Interaction logic for UcTextImagePair.xaml
    /// </summary>
    public partial class UcTextImagePair : UserControl
    {
        public UcTextImagePair()
        {
            InitializeComponent();
        }

        public ImageSource Icon
        {
            get => Image.Source;
            set => Image.Source = value;
        }

        public Double IconWidth
        {
            get => Image.Width;
            set => Image.Width = value;
        }
        public Double IconHeight
        {
            get => Image.Height;
            set => Image.Height = value;
        }

        public string Text
        {
            get => Label.Text;
            set => Label.Text = value;
        }

        public FontFamily TextFontFamily
        {
            set => Label.FontFamily=value;
            get => Label.FontFamily;
        }

        public double TextFontSize
        {
            set => Label.FontSize = value;
            get => Label.FontSize;
        }
    }
}
