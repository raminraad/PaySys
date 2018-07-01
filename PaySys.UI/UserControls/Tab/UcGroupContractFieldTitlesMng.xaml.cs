using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using PaySys.CalcLib.Delegates;
using PaySys.Globalization;
using PaySys.ModelAndBindLib.Model;
using MessageBox = System.Windows.Forms.MessageBox;
using MessageBoxOptions = System.Windows.Forms.MessageBoxOptions;
using UserControl = System.Windows.Controls.UserControl;

namespace PaySys.UI.UC
{
	/// <summary>
	/// Interaction logic for UcGroupContractFieldTitlesMng.xaml
	/// </summary>
	public partial class UcGroupContractFieldTitlesMng : UserControl
	{
		private string EnterTitleMessage;
		public DelegateSaveContext SaveContext { set; get; }
		public static readonly DependencyProperty CurrentMainGroupProperty = DependencyProperty.Register("CurrentMainGroup",
			typeof(MainGroup), typeof(UcGroupContractFieldTitlesMng), new PropertyMetadata(default(MainGroup)));
		public MainGroup CurrentMainGroup
		{
			get => (MainGroup) GetValue(CurrentMainGroupProperty);
			set => SetValue(CurrentMainGroupProperty, value);
		}

		public UcGroupContractFieldTitlesMng()
		{
			InitializeComponent();
			EnterTitleMessage = ResourceAccessor.Messages.GetString("EnterGroupContractFieldTitle");
		}

		private void BtnAddMainGroup_OnClick(object sender, RoutedEventArgs e)
		{
			string newTitle = string.Empty;
			if (InputBox.Show(EnterTitleMessage, ref newTitle) == DialogResult.OK)
			{
				CurrentMainGroup.GroupContractFieldTitles.Add(new GroupContractFieldTitle
				{
					Title = newTitle,
					Year = 97
				});
				SaveContext.Invoke();
					CollectionViewSource.GetDefaultView(ListViewGroupContractFieldTitle.ItemsSource).Refresh();
			}
		}

		private void BtnEdit_OnClick(object sender, RoutedEventArgs e)
		{
			var selectedTitle = ListViewGroupContractFieldTitle.SelectedItem as GroupContractFieldTitle;
			var newTitle = selectedTitle?.Title;
			if (InputBox.Show(EnterTitleMessage, ref newTitle) == DialogResult.OK)
			{
				selectedTitle.Title = newTitle;
				SaveContext.Invoke();
					CollectionViewSource.GetDefaultView(ListViewGroupContractFieldTitle.ItemsSource).Refresh();
			}
		}

		private void BtnDelete_OnClick(object sender, RoutedEventArgs e)
		{
			if (MessageBox.Show(ResourceAccessor.Messages.GetString("DeleteConfirmationOfItem"), "",MessageBoxButtons.YesNo,MessageBoxIcon.Question,MessageBoxDefaultButton.Button2,MessageBoxOptions.RightAlign) == DialogResult.Yes)
			{
				CurrentMainGroup.GroupContractFieldTitles.Remove((GroupContractFieldTitle) ListViewGroupContractFieldTitle.SelectedItem);
				SaveContext.Invoke();
					CollectionViewSource.GetDefaultView(ListViewGroupContractFieldTitle.ItemsSource).Refresh();
			}
		}
	}
}