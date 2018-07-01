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
using PaySys.ModelAndBindLib.Model;

namespace PaySys.UI.UC
{
	/// <summary>
	/// Interaction logic for UcGroupContractFieldTitlesMng.xaml
	/// </summary>
	public partial class UcGroupContractFieldTitlesMng : UserControl
	{
		public static readonly DependencyProperty CurrentMainGroupProperty = DependencyProperty.Register("CurrentMainGroup", typeof(MainGroup), typeof(UcGroupContractFieldTitlesMng), new PropertyMetadata(default(MainGroup)));

		public MainGroup CurrentMainGroup
		{
			get { return (MainGroup) GetValue(CurrentMainGroupProperty); }
			set { SetValue(CurrentMainGroupProperty, value); }
		}
		public UcGroupContractFieldTitlesMng()
		{
			InitializeComponent();
		}
	}
}
