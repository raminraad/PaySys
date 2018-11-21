using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Migrations;
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
using PaySys.CalcLib.ExtensionMethods;
using PaySys.Globalization;
using PaySys.ModelAndBindLib.Engine;
using PaySys.ModelAndBindLib.Entities;
using PaySys.ModelAndBindLib.Enums;
using PaySys.UI.UC;
using Binding = System.Windows.Data.Binding;
using Control = System.Windows.Controls.Control;
using DataGrid = System.Windows.Controls.DataGrid;
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
        public PaySysContext Context { set; get; } 

        public UcGroupMng()
        {
            InitializeComponent();
            Reload();

            SmpUcMiscMng.ExpenseArticlesAll = Context.ExpenseArticles.ToList();
            SmpUcMiscMng.MiscTitlesAll = Context.MiscTitles.ToList();
        }

        private void Reload()
        {
            Context = new PaySysContext();
            Context.MainGroups.Load();
            DataContext = Context.MainGroups.Local;
            foreach (var control in GridMain.FindVisualChildren<UcTextPair>())
                control.UpdateTarget();

            Context.ContractFields.Load();
            SmpUcContractFieldTitlesMng.ContractFieldsAll = Context.ContractFields.Local.ToList();
        }

        private void UcGroupMng_OnInitialized(object sender, EventArgs e)
        {
            SmpUcFormStateLabel.CurrentState = FormCurrentState.Select;
        }

        private void AddSubGroup_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void AddSubGroup_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            SmpUcFormStateLabel.CurrentState = FormCurrentState.Add;
            var newItem = new SubGroup
            {
                Title = ResourceAccessor.Labels.GetString("New")
            };
            (DataGridMainGroups.SelectedItem as MainGroup)?.SubGroups.Add(newItem);
            DataGridSubGroups.SelectedItem = newItem;
            CollectionViewSource.GetDefaultView(DataGridSubGroups.ItemsSource)?.Refresh();
            DataGridSubGroups.ScrollIntoView(newItem);
            TabItemSubGroupBaseInfo.IsSelected = true;
            SmpUcSubGroupEdit.Focus();
        }

        private void Edit_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = DataGridSubGroups?.SelectedItem is SubGroup;
        }

        private void Edit_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            SmpUcFormStateLabel.CurrentState = FormCurrentState.Edit;
        }

        private void Save_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = SmpUcFormStateLabel?.EnabledOfSaveDiscardButtons ?? false;
        }

        private void Save_Executed(object sender, ExecutedRoutedEventArgs e)
        {
//			foreach( var control in GridMain.FindVisualChildren<Control>() )
//				control.GetBindingExpression( UcTextPair.TextOfLabelProperty )?.UpdateSource();
            foreach (var control in GridMain.FindVisualChildren<UcTextPair>())
                control.UpdateSource();

            SmpUcSubGroupEdit.UpdateSource();

            #region Contract Fields
            var expChangedContractFields = Context.ContractFields.Local
                .Where(c => c.CurrentExpenseArticle?.Code != c.TempCurrentExpenseArticleCode).ToList();

            foreach (var cnt in expChangedContractFields)
            {
                var exp = Context.ExpenseArticles.FirstOrDefault(x => x.Code == cnt.TempCurrentExpenseArticleCode) ??
                          Context.ExpenseArticles.Add(new ExpenseArticle
                {
                    Code = cnt.TempCurrentExpenseArticleCode,
                    IsActive = true
                });

                cnt.CurrentExpenseArticle = exp;
//                cnt.TempCurrentExpenseArticleCode = exp.Code;
            }
            #endregion

            #region Miscs
            var expChangedMiscs = Context.Miscs.Local.Where(c => c.ExpenseArticle?.Code != c.TempExpenseArticleCode)
                .ToList();

            foreach (var msc in expChangedMiscs)
            {
                var exp = Context.ExpenseArticles.FirstOrDefault(x => x.Code == msc.TempExpenseArticleCode);
                if (exp == null)
                    msc.ExpenseArticle = Context.ExpenseArticles.Add(new ExpenseArticle
                    {
                        Code = msc.TempExpenseArticleCode,
                        IsActive = true
                    });
                else
                    msc.ExpenseArticle = exp;
            }
            #endregion

            var currentSubGroup = DataGridSubGroups.SelectedItem as SubGroup;


            #region TaxTable
            currentSubGroup?.CurrenTaxTable.CommitTempToValues();
            #endregion


            #region HandselFormula
            Context.HandselFormulas.AddOrUpdate(currentSubGroup?.CurrentOrNewHandselFormula);
            #endregion

            #region MissionFormula
            // Investigating user's checked or not checked ContractFields one by one:
            foreach (var cf in currentSubGroup.CurrentOrNewMissionFormula.TempMissionFormulaInvolvedContractFieldsLeftJoined)
            {
                //User has checked this ContractField but it doesn't exist in involved contract fields list:
                if(cf.Value&&!currentSubGroup.CurrentOrNewMissionFormula.MissionFormulaInvolvedContractFields.Select(f => f.ContractField).Contains(cf.Key))
                    currentSubGroup.CurrentOrNewMissionFormula.MissionFormulaInvolvedContractFields.Add(new MissionFormulaInvolvedContractField
                    {
                        ContractField = cf.Key,
//                        MissionFormula = currentSubGroup.CurrentOrNewMissionFormula,
                    });

                //User has unchecked this ContractField but it exists in involved contract fields list:
                if (!cf.Value && currentSubGroup.CurrentOrNewMissionFormula.MissionFormulaInvolvedContractFields.Select(f => f.ContractField).Contains(cf.Key))
                    currentSubGroup.CurrentOrNewMissionFormula.MissionFormulaInvolvedContractFields.Remove(currentSubGroup.CurrentOrNewMissionFormula.MissionFormulaInvolvedContractFields.First(f => f.ContractField==cf.Key));
            }

            Context.MissionFormulas.AddOrUpdate(currentSubGroup.CurrentOrNewMissionFormula);
            #endregion


            #region TaxTable
            #endregion

            Context.SaveChanges();
            SmpUcFormStateLabel.CurrentState = FormCurrentState.Select;
            SmpUcParameterMng.Refresh();
            SmpUcContractFieldTitlesMng.Refresh();
            SmpUcMiscMng.Refresh();
            CollectionViewSource.GetDefaultView(DataGridSubGroups.ItemsSource)?.Refresh();
        }

        private void DiscardChanges_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = SmpUcFormStateLabel?.EnabledOfSaveDiscardButtons ?? false;
        }

        private void DiscardChanges_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (SmpUcFormStateLabel.CurrentState == FormCurrentState.Add)
            {
                (DataGridMainGroups.SelectedItem as MainGroup)?.SubGroups.Remove((SubGroup)DataGridSubGroups.SelectedItem);
                CollectionViewSource.GetDefaultView(DataGridSubGroups.ItemsSource)?.Refresh();
            }
            else
            {
                Context.DiscardChanges();
                (DataGridSubGroups.SelectedItem as SubGroup)?.CurrenTaxTable.DiscardTempToValues();
                var tmp1 = DataGridMainGroups.SelectedIndex;
                var tmp2 = DataGridSubGroups.SelectedIndex;
                DataGridMainGroups.SelectedIndex = -1;
                DataGridSubGroups.SelectedIndex = -1;
                DataGridMainGroups.SelectedIndex = tmp1;
                DataGridSubGroups.SelectedIndex = tmp2;


//                Reload();
            }
            SmpUcFormStateLabel.CurrentState = FormCurrentState.Select;
        }

        private void Reload_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = SmpUcFormStateLabel?.EnabledOfCrudButtons ?? false;
        }

        private void Reload_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            Reload();
        }
    }
}