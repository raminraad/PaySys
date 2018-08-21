using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using PaySys.Globalization;
using PaySys.ModelAndBindLib.Engine;
using PaySys.ModelAndBindLib.Model;
using MessageBox = System.Windows.MessageBox;
using UserControl = System.Windows.Controls.UserControl;

namespace PaySys.UI.UC
{
	/// <summary>
	/// Interaction logic for UcExpenseArticleMng.xaml
	/// </summary>
	public partial class UcExpenseArticleMng : UserControl
	{
		PaySysContext _context = new PaySysContext();

		public UcExpenseArticleMng()
		{
			InitializeComponent();
			TreeViewExpenseArticles.ItemsSource = _context.ExpenseArticles.ToList();
		}

		private void BtnAdd_OnClick(object sender, RoutedEventArgs e)
		{
			var messageTitle = ResourceAccessor.Messages.GetString("EnterExpenseArticleTitle");
			var messageCode = ResourceAccessor.Messages.GetString("EnterExpenseArticleCode");
			var title = string.Empty;
			var code = string.Empty;
				if(InputBox.Show(messageCode, ref code) == DialogResult.OK)
				{
					_context.ExpenseArticles.Add(new ExpenseArticle()
					{
						Code = code,
					});
					_context.SaveChanges();
					TreeViewExpenseArticles.ItemsSource = _context.ExpenseArticles.ToList();
				}
		}

		private void BtnEdit_OnClick(object sender, RoutedEventArgs e)
		{
			var selectedItem = TreeViewExpenseArticles.SelectedItem as ExpenseArticle;
			var messageTitle = ResourceAccessor.Messages.GetString("EnterExpenseArticleTitle");
			var messageCode = ResourceAccessor.Messages.GetString("EnterExpenseArticleCode");
			var code = selectedItem?.Code;
				if(InputBox.Show(messageCode, ref code) == DialogResult.OK)
				{
					selectedItem.Code = code;
					_context.SaveChanges();
					TreeViewExpenseArticles.ItemsSource = _context.ExpenseArticles.ToList();
				}
		}

		private void TreeViewExpenseArticles_OnSelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
		{
			BtnEdit.IsEnabled = BtnDelete.IsEnabled = TreeViewExpenseArticles.SelectedItem is ExpenseArticle;
		}

		private void BtnRefresh_OnClick(object sender, RoutedEventArgs e)
		{
			_context = new PaySysContext();
			TreeViewExpenseArticles.ItemsSource = _context.ExpenseArticles.ToList();
		}
	}
}