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
using PaySys.CalcLib.Delegates;
using PaySys.ModelAndBindLib.Model;
using PaySys.UI.ExtensionMethods;

namespace PaySys.UI.UC
{
	/// <summary>
	/// Interaction logic for UcHandselFormulaMng.xaml
	/// </summary>
	public partial class UcHandselFormulaMng : UserControl
	{
		public DelegateSaveContext SaveContext { set; get; }

		public UcHandselFormulaMng()
		{
			InitializeComponent();
		}

		public static readonly DependencyProperty CurrentSubGroupProperty = DependencyProperty.Register("CurrentSubGroup", typeof(SubGroup), typeof(UcHandselFormulaMng), new PropertyMetadata(default(SubGroup)));

		public SubGroup CurrentSubGroup
		{
			get => (SubGroup) GetValue(CurrentSubGroupProperty);
			set => SetValue(CurrentSubGroupProperty, value);
		}

		private void BtnEdit_OnClick(object sender, RoutedEventArgs e)
		{
		}

		private void BtnSave_Click(object sender, RoutedEventArgs e)
		{
			foreach(var textbox in GridMain.FindVisualChildren<TextBox>())
				textbox.GetBindingExpression(TextBox.TextProperty)?.UpdateSource();

			GridMain.GetBindingExpression(DataContextProperty)?.UpdateSource();
			SaveContext.Invoke();
		}

		private void BtnCancel_Click(object sender, RoutedEventArgs e)
		{
			//			GridMain.GetBindingExpression(DataContextProperty)?.UpdateTarget();
			foreach (var textbox in GridMain.FindVisualChildren<TextBox>())
				textbox.GetBindingExpression(TextBox.TextProperty)?.UpdateTarget();
		}
	}
}