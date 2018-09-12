using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
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
using PaySys.UI.ExtensionMethods;
using PaySys.UI.UC;
using Binding = System.Windows.Data.Binding;
using Control = System.Windows.Controls.Control;
using ListView = System.Windows.Controls.ListView;
using MessageBox = System.Windows.Forms.MessageBox;
using TextBox = System.Windows.Controls.TextBox;
using UserControl = System.Windows.Controls.UserControl;

namespace PaySys.UI.UC
{
	/// <summary>
	/// Interaction logic for UcGroupMng.xaml
	/// </summary>
	public partial class UcGroupMng : UserControl
	{
		public PaySysContext Context { set; get; } = new PaySysContext();

		public UcGroupMng()
		{
			InitializeComponent();
			Reload();

			SmpUcMiscMng.SaveContext += () => Context.SaveChanges();
			SmpUcTaxTableMng.SaveContext += () => Context.SaveChanges();
			SmpUcHandselFormula.SaveContext += () => Context.SaveChanges();
			SmpUcMissionFormulaMng.SaveContext += () => Context.SaveChanges();

			SmpUcMiscMng.ExpenseArticlesAll = Context.ExpenseArticles.ToList();
			SmpUcMiscMng.MiscTitlesAll = Context.MiscTitles.ToList();
		}

		private void Reload()
		{
			Context.MainGroups.Load();
			DataContext = Context.MainGroups.Local;
			foreach( var control in GridMain.FindVisualChildren<UcTextPair>() )
				control.UpdateTarget();
//				control.GetBindingExpression( UcTextPair.TextOfTextBoxProperty )?.UpdateTarget();

			Context.ContractFields.Load();
			SmpUcContractFieldTitlesMng.ContractFieldsAll = Context.ContractFields.Local.ToList();
		}

		private void UcGroupMng_OnInitialized( object sender, EventArgs e )
		{
			SmpUcFormStateLabel.CurrentState = FormCurrentState.Select;
		}

		private void AddSubGroup_CanExecute( object sender, CanExecuteRoutedEventArgs e )
		{
			e.CanExecute = true;
		}

		private void AddSubGroup_Executed( object sender, ExecutedRoutedEventArgs e )
		{
			var title = string.Empty;
			if( InputBox.Show( ResourceAccessor.Messages.GetString( "EnterSubGroupName" ), ref title ) == DialogResult.OK )
			{
				var selectedMainGroup = (MainGroup) ListViewMainGroups.SelectedItem;
				selectedMainGroup.SubGroups.Add( new SubGroup
				{
						Title = title,
						ItemColor = selectedMainGroup.ItemColor
				} );
				Context.SaveChanges();
				CollectionViewSource.GetDefaultView( ListViewSubGroups.ItemsSource ).Refresh();
			}
		}

		private void Edit_CanExecute( object sender, CanExecuteRoutedEventArgs e )
		{
			e.CanExecute = ListViewSubGroups?.SelectedItem as SubGroup != null;
		}

		private void Edit_Executed( object sender, ExecutedRoutedEventArgs e )
		{
			SmpUcFormStateLabel.CurrentState = FormCurrentState.Edit;
		}

		private void Save_CanExecute( object sender, CanExecuteRoutedEventArgs e )
		{
			e.CanExecute = SmpUcFormStateLabel?.EnabledOfSaveCancelButtons ?? false;
		}

		private void Save_Executed( object sender, ExecutedRoutedEventArgs e )
		{
//			foreach( var control in GridMain.FindVisualChildren<Control>() )
//				control.GetBindingExpression( UcTextPair.TextOfLabelProperty )?.UpdateSource();
			foreach( var control in GridMain.FindVisualChildren<UcTextPair>() )
				control.UpdateSource();

			var expChangedContractFields = Context.ContractFields.Local.Where( c => c.CurrentExpenseArticle?.Code != c.TempCurrentExpenseArticleCode ).ToList();

			foreach( var cnt in expChangedContractFields )
			{
				var exp = Context.ExpenseArticles.FirstOrDefault( x => x.Code == cnt.TempCurrentExpenseArticleCode );
				if( exp == null )
					cnt.CurrentExpenseArticle = Context.ExpenseArticles.Add( new ExpenseArticle
					{
							Code = cnt.TempCurrentExpenseArticleCode,
							IsActive = true
					} );
				else
					cnt.CurrentExpenseArticle = exp;
			}

			var expChangedMiscs = Context.Miscs.Local.Where( c => c.ExpenseArticle?.Code != c.TempExpenseArticleCode ).ToList();

			foreach( var msc in expChangedMiscs )
			{
				var exp = Context.ExpenseArticles.FirstOrDefault( x => x.Code == msc.TempExpenseArticleCode );
				if( exp == null )
					msc.ExpenseArticle = Context.ExpenseArticles.Add( new ExpenseArticle
					{
							Code = msc.TempExpenseArticleCode,
							IsActive = true
					} );
				else
					msc.ExpenseArticle = exp;
			}

			Context.SaveChanges();
			SmpUcFormStateLabel.CurrentState = FormCurrentState.Select;
		}

		

		private void DiscardChanges_CanExecute( object sender, CanExecuteRoutedEventArgs e )
		{
			e.CanExecute = SmpUcFormStateLabel?.EnabledOfSaveCancelButtons ?? false;
		}

		private void DiscardChanges_Executed( object sender, ExecutedRoutedEventArgs e )
		{
			Context.DiscardChanges();
			Reload();
			SmpUcFormStateLabel.CurrentState = FormCurrentState.Select;
		}

		private void Reload_CanExecute( object sender, CanExecuteRoutedEventArgs e )
		{
			e.CanExecute = SmpUcFormStateLabel?.EnabledOfCrudButtons ?? false;
		}
	}
}