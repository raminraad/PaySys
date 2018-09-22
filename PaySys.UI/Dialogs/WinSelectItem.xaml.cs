using System;
using System.Collections;
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
using System.Windows.Shapes;
using PaySys.Globalization;
using PaySys.ModelAndBindLib.Model;

namespace PaySys.UI.Dialogs
{
	/// <summary>
	/// Interaction logic for WinSelectItem.xaml
	/// </summary>
	public partial class WinSelectItem : Window
	{

		public WinSelectItem(string SelectItemRequest, string ListViewDisplayMemberPath = "Title")
		{
			InitializeComponent();
			if(string.IsNullOrEmpty(SelectItemRequest)) SelectItemRequest = ResourceAccessor.Messages.GetString("SelectItem");
		    TextBlockSelectItemRequest.Text=SelectItemRequest;
			ListViewItems.DisplayMemberPath = ListViewDisplayMemberPath;
		}

		public object SelectedItem
		{
			get => ListViewItems.SelectedItem;
			set => ListViewItems.SelectedItem = value;
		}

		public IEnumerable ListViewItemsSource
		{
			get => ListViewItems.ItemsSource;
			set => ListViewItems.ItemsSource = value;
		}

		private void BtnOk_OnClick(object sender, RoutedEventArgs e)
		{
			DialogResult = true;
		}

		private void BtnCancel_Click(object sender, RoutedEventArgs e)
		{
			DialogResult = false;
		}

	}
}