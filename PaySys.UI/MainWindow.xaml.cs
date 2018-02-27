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

namespace PaySys
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

		private void BtnDo_Click(object sender, RoutedEventArgs e)
		{
			Window owned = new Window();
			owned.Owner = this;
			owned.ShowInTaskbar = false;
			owned.WindowStartupLocation=WindowStartupLocation.CenterOwner;
			owned.Height = owned.Width = 200;
			owned.Show();
		}

	    private void MenuItem_OnClick(object sender, RoutedEventArgs e)
	    {
		    MessageBox.Show("Event");

	    }
    }
}
