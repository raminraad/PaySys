﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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
using PaySys.EF;
using PaySys.UI.User_Control;

namespace PaySys
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		PaySysContext context = new PaySysContext();

		public MainWindow()
		{
			InitializeComponent();
			context.Database.Initialize(true);
		}

		private void BtnDo_Click(object sender, RoutedEventArgs e)
		{
			Window owned = new Window();
			owned.Owner = this;
			owned.ShowInTaskbar = false;
			owned.WindowStartupLocation = WindowStartupLocation.CenterOwner;
			owned.Height = owned.Width = 200;
			owned.Show();
		}

		private void MenuItemEmployeeMng_OnClick(object sender, RoutedEventArgs e)
		{
			var userControls = new UcEmployee();
			var tabEmployeeMng = new TabItem
			{
				Content = userControls,
				Header = "اطلاعات پرسنلی"
			};
			TabCntMain.Items.Add(tabEmployeeMng);
			TabCntMain.Items.Refresh();
		}

		private void MenuItemGroupMng_OnClick(object sender, RoutedEventArgs e)
		{
			var userControls = new UcGroupMng();
			var tabGroupMng = new TabItem
			{
				Content = userControls,
				Header = "گروه های استخدامی"
			};
			TabCntMain.Items.Add(tabGroupMng);
			TabCntMain.Items.Refresh();
		}
	}
}