using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
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
using PaySys.CalcLib.ExtensionMethods;
using PaySys.Globalization;
using PaySys.ModelAndBindLib.Engine;
using PaySys.ModelAndBindLib.Entities;
using MessageBox = System.Windows.Forms.MessageBox;
using TextBox = System.Windows.Controls.TextBox;
using UserControl = System.Windows.Controls.UserControl;

namespace PaySys.UI.UC
{
	/// <summary>
	/// Interaction logic for UcExpenseArticleMng.xaml
	/// </summary>
	public partial class UcExpenseArticleMng : UserControl
	{
	    private PaySysContext Context = new PaySysContext();

		public UcExpenseArticleMng()
		{
			InitializeComponent();
			TreeViewExpenseArticles.ItemsSource = Context.ExpenseArticles.ToList();
		}

		private void Add_Executed(object sender, ExecutedRoutedEventArgs e)
		{
		    SmpUcFormStateLabel.CurrentState = FormCurrentState.Add;
            var messageTitle = ResourceAccessor.Messages.GetString("EnterExpenseArticleTitle");
			var messageCode = ResourceAccessor.Messages.GetString("EnterExpenseArticleCode");
			var title = string.Empty;
			var code = string.Empty;
				if(InputBox.Show(messageCode, ref code) == DialogResult.OK)
				{
					Context.ExpenseArticles.Add(new ExpenseArticle()
					{
						Code = code,
					});
					Context.SaveChanges();
					TreeViewExpenseArticles.ItemsSource = Context.ExpenseArticles.ToList();
				}
		}

		private void Edit_Executed(object sender, ExecutedRoutedEventArgs e)
		{
		    SmpUcFormStateLabel.CurrentState = FormCurrentState.Edit;
            var selectedItem = TreeViewExpenseArticles.SelectedItem as ExpenseArticle;
			var messageTitle = ResourceAccessor.Messages.GetString("EnterExpenseArticleTitle");
			var messageCode = ResourceAccessor.Messages.GetString("EnterExpenseArticleCode");
			var code = selectedItem?.Code;
				if(InputBox.Show(messageCode, ref code) == DialogResult.OK)
				{
					selectedItem.Code = code;
					Context.SaveChanges();
					TreeViewExpenseArticles.ItemsSource = Context.ExpenseArticles.ToList();
				}
		}

	    private void Reload_Executed(object sender, ExecutedRoutedEventArgs e)
		{
			Context = new PaySysContext();
			TreeViewExpenseArticles.ItemsSource = Context.ExpenseArticles.ToList();
		}

	    private void Delete_Executed(object sender, ExecutedRoutedEventArgs e)
	    {
	        throw new NotImplementedException();
	    }

        private void Save_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            foreach (var textBox in this.FindVisualChildren<TextBox>())
                textBox.GetBindingExpression(TextBox.TextProperty)?.UpdateSource();

            Context.SaveChanges();
            SmpUcFormStateLabel.CurrentState = FormCurrentState.Select;
            CollectionViewSource.GetDefaultView(TreeViewExpenseArticles.ItemsSource)?.Refresh();
        }

        private void DiscardChanges_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (SmpUcFormStateLabel.CurrentState == FormCurrentState.Add)
                TreeViewExpenseArticles.Items.Remove((ExpenseArticle) TreeViewExpenseArticles.SelectedItem);
            foreach (var textBox in this.FindVisualChildren<TextBox>())
                textBox.GetBindingExpression(TextBox.TextProperty)?.UpdateTarget();

            SmpUcFormStateLabel.CurrentState = FormCurrentState.Select;
        }

	    private void EditAndDeleteCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
	    {
	        e.CanExecute = SmpUcFormStateLabel?.EnabledOfCrudButtons==true && TreeViewExpenseArticles.SelectedItem is ExpenseArticle;
	    }
	    private void SaveCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
	    {
	        e.CanExecute = SmpUcFormStateLabel?.EnabledOfSaveDiscardButtons == true && Validator.ChildrenAreValid<TextBox>(this);
	    }

	    private void CrudCommands_CanExecute(object sender, CanExecuteRoutedEventArgs e)
	    {
	        e.CanExecute = SmpUcFormStateLabel?.EnabledOfCrudButtons == true;
	    }

	    private void DiscardChangesCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
	    {
	        e.CanExecute = SmpUcFormStateLabel?.EnabledOfSaveDiscardButtons == true;
	    }

	    private void UcExpenseArticleMng_OnLoaded(object sender, RoutedEventArgs e)
	    {
	        SmpUcFormStateLabel.CurrentState=FormCurrentState.Select;
	    }
	}
}