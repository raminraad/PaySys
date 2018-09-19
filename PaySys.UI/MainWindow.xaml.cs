using System;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using PaySys.Globalization;
using PaySys.ModelAndBindLib;
using PaySys.ModelAndBindLib.Engine;
using PaySys.UI.UC;
using PaySys.UI.UC.Tab;

namespace PaySys
{
    /// <summary>Interaction logic for MainWindow.xaml</summary>
    public partial class MainWindow : Window
    {
        public PaySysContext Context { get; set; } = new PaySysContext();

        public MainWindow()
        {
            InitializeComponent();
            PaySysSetting.CurrentYear = 1397;
            PaySysSetting.CurrentMonth = 7;
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
            switch (((MenuItem) sender).Name)
            {
                case nameof(MnuGroupMng):
                    var uc = new UcGroupMng();
                    var tabItem = new TabItem {Content = uc, Header = ResourceAccessor.Labels.GetString("tabGroupMng")};
                    TabCntMain.Items.Add(tabItem);
                    TabCntMain.SelectedItem = tabItem;
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
            var tabItem = new TabItem
            {
                Content = userControls,
                Header = ResourceAccessor.Labels.GetString("tabContractMng")
            };
            TabCntMain.Items.Add(tabItem);
            TabCntMain.SelectedItem = tabItem;
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
            TabCntMain.SelectedItem = tabItem;
            TabCntMain.Items.Refresh();
        }

        private void MnuCityMng_OnClick(object sender, RoutedEventArgs e)
        {
            var uc = new UcCityMng();
            var tabItem = new TabItem {Content = uc, Header = ResourceAccessor.Labels.GetString("tabCityMng")};
            TabCntMain.Items.Add(tabItem);
            TabCntMain.SelectedItem = tabItem;
            TabCntMain.Items.Refresh();
        }

        private void MnuJobMng_OnClick(object sender, RoutedEventArgs e)
        {
            var uc = new UcJobMng();
            var tabItem = new TabItem {Content = uc, Header = ResourceAccessor.Labels.GetString("tabJobMng")};
            TabCntMain.Items.Add(tabItem);
            TabCntMain.SelectedItem = tabItem;
            TabCntMain.Items.Refresh();
        }

        private void MnuMiscRechargesMng_OnClick(object sender, RoutedEventArgs e)
        {
            var uc = new UcMiscRechargeMng();
            var tabItem = new TabItem {Content = uc, Header = ResourceAccessor.Labels.GetString("tabMiscRechargesMng")};
            TabCntMain.Items.Add(tabItem);
            TabCntMain.SelectedItem = tabItem;
            TabCntMain.Items.Refresh();
        }

        private void MnuMissionMng_OnClick(object sender, RoutedEventArgs e)
        {
            var uc = new UcMissionMng();
            var tabItem = new TabItem {Content = uc, Header = ResourceAccessor.Labels.GetString("tabMissionMng")};
            TabCntMain.Items.Add(tabItem);
            TabCntMain.SelectedItem = tabItem;
            TabCntMain.Items.Refresh();
        }

        private void MnuMonthlyDataMng_OnClick(object sender, RoutedEventArgs e)
        {
            var uc = new UcMonthlyDataMng();
            var tabItem = new TabItem {Content = uc, Header = ResourceAccessor.Labels.GetString("tabMonthlyDataMng")};
            TabCntMain.Items.Add(tabItem);
            TabCntMain.SelectedItem = tabItem;
            TabCntMain.Items.Refresh();
        }
    }
}