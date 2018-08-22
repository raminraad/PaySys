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
		private enum GroupType
		{
			None,
			MainGroup,
			SubGroup
		}

		public PaySysContext Context
		{
			set;
			get;
		} = new PaySysContext();
		public UcGroupMng()
		{
			InitializeComponent();
			Reload();

			SmpUcMiscMng.SaveContext += () => Context.SaveChanges();
			SmpUcParameterMng.SaveContext += () => Context.SaveChanges();
			SmpUcTaxTableMng.SaveContext += () => Context.SaveChanges();
			SmpUcHandselFormula.SaveContext += () => Context.SaveChanges();
			SmpUcMissionFormulaMng.SaveContext += () => Context.SaveChanges();

			Context.ExpenseArticles.ToList();
			SmpUcMiscMng.ExpenseArticlesAll = Context.ExpenseArticles.ToList();
			SmpUcMiscMng.MiscTitlesAll = Context.MiscTitles.ToList();

		}

		private void Reload()
		{
			Context.MainGroups.Load();
			DataContext = Context.MainGroups.Local;
			foreach (var control in GridMain.FindVisualChildren<Control>())
				control.GetBindingExpression(UcTextPair.TextProperty)?.UpdateTarget();
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
			if (InputBox.Show(ResourceAccessor.Messages.GetString("EnterSubGroupName"), ref title) == DialogResult.OK)
			{
				var selectedMainGroup = (MainGroup)ListViewMainGroups.SelectedItem;
				selectedMainGroup.SubGroups.Add(new SubGroup
				{
						Title = title,
						ItemColor = selectedMainGroup.ItemColor
				});
				Context.SaveChanges();
				CollectionViewSource.GetDefaultView(ListViewSubGroups.ItemsSource).Refresh();
			}
		}

		private void Edit_CanExecute( object sender, CanExecuteRoutedEventArgs e )
		{
			e.CanExecute = ( ListViewSubGroups?.SelectedItem as SubGroup ) != null;
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
			foreach (var control in GridMain.FindVisualChildren<Control>())
				control.GetBindingExpression( UcTextPair.TextProperty )?.UpdateSource();

			foreach( var cntEntity in Context.GetChangesOfType<ContractField>().Where( cnt => cnt.State == EntityState.Added || cnt.State == EntityState.Modified ) )
			{
				var cnt = cntEntity.Cast<ContractField>().Entity;
				if( !cnt.TempCurrentExpenseArticleCodeChanged )
					continue;

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

			Context.SaveChanges();
			MessageBox.Show( ResourceAccessor.Messages.GetString( "SaveSuccessful" ) );
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
			e.CanExecute = SmpUcFormStateLabel?.EnabledOfCrudButtons??false;
		}
	}
}