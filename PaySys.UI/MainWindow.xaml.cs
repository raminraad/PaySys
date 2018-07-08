using System.Windows;
using System.Windows.Controls;
using PaySys.Globalization;
using PaySys.ModelAndBindLib.Engine;
using PaySys.UI.UC;
using PaySys.UI.UC.Tab;

namespace PaySys
{
	/// <summary>Interaction logic for MainWindow.xaml</summary>
	public partial class MainWindow : Window
	{
		private readonly PaySysContext context = new PaySysContext();

		public MainWindow()
		{
			InitializeComponent();
//			context.Database.Initialize(true);
			WindowStartupLocation = WindowStartupLocation.CenterScreen;
			WindowState = WindowState.Maximized;
		}

		private void MnuEmployeeMng_OnClick(object sender, RoutedEventArgs e)
		{
			var userControls = new UcEmployeeMng();
			var tabEmployeeMng = new TabItem
			{
				Content = userControls,
				Header = ResourceAccessor.Labels.GetString("tabEmployeeMng")
			};
			TabCntMain.Items.Add(tabEmployeeMng);
			TabCntMain.Items.Refresh();
		}

		private void MenuItem_OnClick(object sender, RoutedEventArgs e)
		{
			switch (((MenuItem)sender).Name)
			{
				case nameof(MnuGroupMng):
					var uc = new UcGroupMng();
					var tab = new TabItem
					{
						Content = uc,
						Header = ResourceAccessor.Labels.GetString("tabGroupMng")
					};
					TabCntMain.Items.Add(tab);
					break;
			}
			TabCntMain.Items.Refresh();
		}

		private void mnuRetirementFormField_OnClick(object sender, RoutedEventArgs e)
		{
		}

		private void MnuContractMng_OnClick(object sender, RoutedEventArgs e)
		{
			var userControls = new UcContractMng();
			var tabContractMng = new TabItem
			{
				Content = userControls,
				Header = ResourceAccessor.Labels.GetString("tabContractMng")
			};
			TabCntMain.Items.Add(tabContractMng);
			TabCntMain.Items.Refresh();
		}

		private void MenuItemExpenseArticlesMng_OnClick(object sender, RoutedEventArgs e)
		{
			var uc = new UcExpenseArticleMng();
			var tabItem = new TabItem
			{
				Content = uc,
				Header = ResourceAccessor.Labels.GetString("tabExpenseArticlesMng")
			};
			TabCntMain.Items.Add(tabItem);
			TabCntMain.Items.Refresh();
		}

		private void MnuCityMng_OnClick(object sender, RoutedEventArgs e)
		{
			var uc = new UcCityMng();
			var tabItem = new TabItem
			{
				Content = uc,
				Header = ResourceAccessor.Labels.GetString("tabCityMng")
			};
			TabCntMain.Items.Add(tabItem);
			TabCntMain.Items.Refresh();
		}
	}
}