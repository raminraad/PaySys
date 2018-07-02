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
using System.Windows.Shapes;
using PaySys.ModelAndBindLib.Model;

namespace PaySys.UI.Dialogs
{
	/// <summary>
	/// Interaction logic for WinSelectExpenseArticle.xaml
	/// </summary>
	public partial class WinSelectExpenseArticle : Window
	{
		public WinSelectExpenseArticle()
		{
			InitializeComponent();
		}

		public ExpenseArticle SelectedExpenseArticle
		{
			get => (ExpenseArticle)ListViewExpenseArticles.SelectedItem;
			set => ListViewExpenseArticles.SelectedItem = value;
		}

		public List<ExpenseArticle> ListItemsSource
		{
			get => (List<ExpenseArticle>)ListViewExpenseArticles.ItemsSource;
			set => ListViewExpenseArticles.ItemsSource = value;
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
