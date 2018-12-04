using System;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using PaySys.Globalization;

using PaySys.Model.Entities;
using PaySys.Model.EntityFramework;
using PaySys.Model.Enums;
using PaySys.Model.ExtensionMethods;
using PaySys.Model.Validation.INotifyDataErrorInfo;

namespace PaySys.UI.UserControls
{
    /// <summary>
    /// Interaction logic for UcJobMng.xaml
    /// </summary>
    public partial class UcJobMng : UserControl
    {
        public PaySysContext Context { get; set; }

        public ObservableCollection<Job> Jobs { get; set; }

        public UcJobMng()
        {
            InitializeComponent();
            Reload_Executed(null, null);
            SmpUcFormStateLabel.CurrentState = FormCurrentState.Select;
        }

        private void Add_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            SmpUcFormStateLabel.CurrentState = FormCurrentState.Add;
            var newItem = new Job
            {
                Title = ResourceAccessor.Labels.GetString("New")
            };
            Jobs.Add(newItem);
            DataGridJobs.SelectedItem = newItem;
            DataGridJobs.ScrollIntoView(newItem);
        }

        private void Edit_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            SmpUcFormStateLabel.CurrentState = FormCurrentState.Edit;
        }

        private void Reload_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            var selectedId = (DataGridJobs.SelectedItem as Job)?.Id;
            Context = new PaySysContext();
            Context.Jobs.Load();
            Jobs = Context.Jobs.Local;
            DataGridJobs.GetBindingExpression(ItemsControl.ItemsSourceProperty).UpdateTarget();
            if (selectedId.HasValue) DataGridJobs.SelectedItem = Jobs.FirstOrDefault(Job => Job.Id == selectedId.Value);
        }

        private void Save_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            foreach (var textBox in this.FindVisualChildren<TextBox>())
                textBox.GetBindingExpression(TextBox.TextProperty)?.UpdateSource();

            Context.SaveChanges();
            SmpUcFormStateLabel.CurrentState = FormCurrentState.Select;
            CollectionViewSource.GetDefaultView(DataGridJobs.ItemsSource)?.Refresh();
        }

        private void DiscardChanges_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            Jobs.Remove((Job) DataGridJobs.SelectedItem);
            foreach (var textBox in this.FindVisualChildren<TextBox>())
                textBox.GetBindingExpression(TextBox.TextProperty).UpdateTarget();

            SmpUcFormStateLabel.CurrentState = FormCurrentState.Select;
        }

        private void SaveCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = SmpUcFormStateLabel?.EnabledOfSaveDiscardButtons == true &&
                           Validator.ChildrenAreValid<TextBox>(this);
        }

        private void EditCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = SmpUcFormStateLabel?.EnabledOfCrudButtons == true && DataGridJobs.SelectedItems.Count > 0;
        }

        private void CrudCommands_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = SmpUcFormStateLabel?.EnabledOfCrudButtons == true;
        }

        private void DiscardChangesCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = SmpUcFormStateLabel?.EnabledOfSaveDiscardButtons == true;
        }

        private void Delete_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void SmpUcLookup_OnLookupTextChanged(object sender, TextChangedEventArgs e)
        {
            var dtg = DataGridJobs;
            if (dtg.ItemsSource == null) return;
            if (string.IsNullOrEmpty(SmpUcLookup.LookupText))
                CollectionViewSource.GetDefaultView(dtg.ItemsSource).Filter = null;
            else 
                CollectionViewSource.GetDefaultView(dtg.ItemsSource).Filter =
                    o => ((Job) o).ContainsValue(SmpUcLookup.LookupText);
        }

        private void Lookup_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            SmpUcLookup.Select();
        }
    }
}