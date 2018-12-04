using System.Collections;
using System.Windows;
using PaySys.Globalization;

namespace PaySys.UI.Modals
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