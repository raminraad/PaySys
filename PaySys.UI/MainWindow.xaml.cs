using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Input;
using Arash;
using PaySys.Globalization;
using PaySys.Globalization.Fa;
using PaySys.ModelAndBindLib;
using PaySys.ModelAndBindLib.Engine;
using PaySys.UI.UC;
using PaySys.Windows;
using Stimulsoft.Report;
using Button = System.Windows.Controls.Button;
using Label = System.Windows.Controls.Label;
using MenuItem = System.Windows.Controls.MenuItem;
using MessageBox = System.Windows.MessageBox;

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
            AddOrSelectTab(new UcEmployeeMng(), Labels.tabEmployeeMng);
        }

        private void MnuGroupMng_OnClick(object sender, RoutedEventArgs e)
        {
            AddOrSelectTab(new UcGroupMng(), Labels.tabGroupMng);
        }

        private void mnuRetirementFormField_OnClick(object sender, RoutedEventArgs e)
        {
        }

        private void MnuContractMng_OnClick(object sender, RoutedEventArgs e)
        {
            AddOrSelectTab(new UcContractMng(), Labels.tabContractMng);
        }

        private void MenuItemExpenseArticlesMng_OnClick(object sender, RoutedEventArgs e)
        {
            AddOrSelectTab(new UcExpenseArticleMng(), Labels.tabExpenseArticlesMng);
        }

        private void MnuCityMng_OnClick(object sender, RoutedEventArgs e)
        {
            AddOrSelectTab(new UcCityMng(), Labels.tabCityMng);
        }

        private void MnuJobMng_OnClick(object sender, RoutedEventArgs e)
        {
            AddOrSelectTab(new UcJobMng(), Labels.tabJobMng);
        }

        private void MnuMiscRechargesMng_OnClick(object sender, RoutedEventArgs e)
        {
            AddOrSelectTab(new UcMiscRechargeMng(), Labels.tabMiscRechargesMng);
        }

        private void MnuMissionMng_OnClick(object sender, RoutedEventArgs e)
        {
            AddOrSelectTab(new UcMissionMng(), Labels.tabMissionMng);
        }

        private void MnuMonthlyDataMng_OnClick(object sender, RoutedEventArgs e)
        {
            AddOrSelectTab(new UcMonthlyDataMng(), Labels.tabMonthlyDataMng);
        }

        private void AddOrSelectTab(object tabContent, string tabTitle)
        {
            foreach (var item in TabCntMain.Items)
            {
                var currentContentTypeName = (item as TabItem)?.Content.GetType().Name;
                if (tabContent.GetType().Name != currentContentTypeName) continue;
                TabCntMain.SelectedItem = item;
                return;
            }

            var tabItem = new TabItem
            {
                Content = tabContent
            };
            tabItem.Header = new UcTabHeader
            {
                Text = tabTitle,
                CloseMe = () => TabCntMain.Items.Remove(tabItem)
            };

            TabCntMain.Items.Add(tabItem);
            TabCntMain.SelectedItem = tabItem;
            TabCntMain.Items.Refresh();
        }

        private void SetCurrentYearMonth()
        {
            LabelCurrentYear.Content = PaySysSetting.CurrentYear;
            LabelCurrentMonth.Content = ((PersianMonth) PaySysSetting.CurrentMonth);
        }

        private void ChangeCurrentYearMonth()
        {
            if (!new WinCurrentYearMonthMng().ShowDialog().Value) return;
            SetCurrentYearMonth();
        }

        private void MainWindow_OnLoaded(object sender, RoutedEventArgs e)
        {
            LabelSystemDate.Content = new PersianDate(DateTime.Now).ToLongDateString();
            SetCurrentYearMonth();
        }

        private void MenuItemCurrentYearMonthMng_OnClick(object sender, RoutedEventArgs e)
        {
            ChangeCurrentYearMonth();
        }

        private void CurrentYearMonth_OnMouseUp(object sender, MouseButtonEventArgs e)
        {
            ChangeCurrentYearMonth();
        }

        private void ReportMenuItem_OnClick(object sender, RoutedEventArgs e)
        {
            var report = new StiReport();
            Stimulsoft.Base.StiLicense.Key = "6vJhGtLLLz2GNviWmUTrhSqnOItdDwjBylQzQcAOiHm4lqN+V3/fvJECXRrqdoEPZqYyFXh3g3K9oCDFvgzMl4c9KudQQwMJ6nO6w7rHz59BhwYgDE0QzKtjY2WxEejTNewbNDXY492M2mDsK1Hb4t6MoFYGbSoID0gow3VC5cFhvcOKVoagiHq6/4iqFc3RS7nZQBp95+WB6N1fR//H7OBlvfuBKldvC1pJh6pTW/7HRdOclErt/EGwivx9SpuDNabWWBfIZJBdJsvKjpoIsDQGibWk8Y8F9V1mCLf88FurWwEZ9WzXePbJn+wfabHM+a7pqDAXhsaW33UEW6vQ3kgLrVjbcHWdhtYbp5j6ZeIMLBCW2LXiCZpzy3N3XqjlroV/s7KRZGeZMvRrLWhcM8YJ6sBTMUyQrF/Fk3d5f0WbAf7+opIA5rhxY+qgwWcE42S+HOOcMgb2IWtj5ycC/GXZ4o9qtk2WWJzCC+ygckXK7iI5pzhzScGmLCBrFnjyE5EP65FViVDSCcNl6hUFw7wwRtcQq5pnOu6wHRXsSpwNPufWaRqE4gd7hbrQoPqki4BLOYgGOQjZ3kJdc7MG47nVT62z1ScLsjH73q4yhABt8h1wmJl0LanKnE6EGkvZPCrDBBf0sZp12sJrxK8FGsPUKrBNZ5f4VuYkdzWS1ed2m5MZNHkDDd3LncYkQnLPh/MYAsnc2ud715njwiM62xIWYk9lIrmCWJU3JVcexRDS3/l7Po9UU5gx2xJKUW1tWOXKQ9GqQZYtn/rl804VMC1/tWr4X2XU1otulny5P8YkZ7BTG6zmkcW6raROxv7xkSUwE0LK2FmIpeBr29Zxn0QUYBtC09J+wVD5y00f/1jMU+k1jQmi1n2tpWgy6CxAHPPrQFhk0FsGXEDg82IELCKncbqkfqgT0QyV262YL2uxrcbcJ1Wrzf4llEX90hMa3oZK5wdQLrCBkXIlXverhGL3I09fR6lcXDMj8A3vrQrhr4bwYcWTH0hyJCPVLYIVxAFLwgV8wk700QOo3z32sSWKnyMHVx3brFB8I7rOPaW2Sc8rBxc1E9EvNcqiTwRd9QA3lIdR2Hpc4jyn5dx8PT6GTNGXNKhoTZDLrGMtYqXw4rVOXfkE8+opF4+/H602ockCI/IY7C65+sxooNgSg+9LpzImUt6GdtrvF8HrvJ0rUW6ZiAmGaQmqdrHfwC3K+QKU4UEWuLU1GPG+PKMrYEbtT/Hei7NPA5sOqHqbzaMsW7zj+enlhuEUeMdlxqfQs1QtTERrzX2HXv/JZ43rASH4ycuGftLR2v3AOHhtBCoYOCszhOiHYGmH0I9lwi8HDRTXjkuoRU1Zxmz9rSIQy35/aypV339OS7p4VydO5LibEiwugzaNHXCZalEPShhcWzTAq6+uCb+jA9ob+Za4yICRdIjgot/E5ZQNZl2VEhFmWR7pYSB7MB24z0ysVf3igAc50/3Lr4d7mQ7SgmtKFoejGOjvb75YOq/pEJGxCo1xIS+jhZklyLuiwmtyDe7Xmpn33fip22qx2iF7FYW71AyuT3tTPn2a7Jc2wXlPCAxB6dqx0eeX4fWA3+RIosUTxTyA7a/SM95pwN9FKYFPZ8TcTqDIq0Ntc57IdX5u1To=";

            report.Load("d:\\Report.mrt");
            report.Dictionary.Variables["V1"].ValueObject = 4;

            report.RenderWithWpf();
            report.ShowWithWpf();
        }
    }
}