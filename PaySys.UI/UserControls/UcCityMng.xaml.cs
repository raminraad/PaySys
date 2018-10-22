using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
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
using PaySys.CalcLib.ExtensionMethods;
using PaySys.Globalization;
using PaySys.ModelAndBindLib.Engine;
using PaySys.ModelAndBindLib.Entities;
using MessageBox = System.Windows.Forms.MessageBox;
using UserControl = System.Windows.Controls.UserControl;

namespace PaySys.UI.UC
{
    /// <summary>
    /// Interaction logic for UcCityMng.xaml
    /// </summary>
    public partial class UcCityMng : UserControl
    {
        public PaySysContext Context { get; set; }

        public ObservableCollection<City> Cities { get; set; }

        public UcCityMng()
        {
            InitializeComponent();
            Reload_Executed(null, null);
        }

        private void Add_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            SmpUcFormStateLabel.CurrentState = FormCurrentState.Add;
            var newItem = new City
            {
                Title = ResourceAccessor.Labels.GetString("New")
            };
            Cities.Add(newItem);
            CollectionViewSource.GetDefaultView(DataGridCities.ItemsSource)?.Refresh();
            DataGridCities.ScrollIntoView(newItem);
            TextBoxTitle.Focus();
        }

        private void Edit_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            SmpUcFormStateLabel.CurrentState = FormCurrentState.Edit;
            TextBoxTitle.Focus();
        }

        private void Delete_Executed(object sender, ExecutedRoutedEventArgs e)
        {
        }
            
        private void Save_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            foreach (var textBox in this.FindVisualChildren<TextBox>())
                textBox.GetBindingExpression(TextBox.TextProperty)?.UpdateSource();

            Context.SaveChanges();
            SmpUcFormStateLabel.CurrentState = FormCurrentState.Select;
            CollectionViewSource.GetDefaultView(DataGridCities.ItemsSource)?.Refresh();
        }

        private void DiscardChanges_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (SmpUcFormStateLabel.CurrentState == FormCurrentState.Add)
                Cities.Remove((City) DataGridCities.SelectedItem);
            foreach (var textBox in this.FindVisualChildren<TextBox>())
                textBox.GetBindingExpression(TextBox.TextProperty)?.UpdateTarget();

            SmpUcFormStateLabel.CurrentState = FormCurrentState.Select;
        }

        private void Reload_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            var selectedId = (DataGridCities.SelectedItem as City)?.Id;
            Context = new PaySysContext();
            Context.Cities.Load();
            Cities = Context.Cities.Local;
            DataGridCities.GetBindingExpression(ItemsControl.ItemsSourceProperty)?.UpdateTarget();
            DataGridCities.GetBindingExpression(DataContextProperty)?.UpdateTarget();
            if (selectedId.HasValue)
                DataGridCities.SelectedItem = Cities.FirstOrDefault(city => city.Id == selectedId.Value);
            SmpUcLookup_OnLookupTextChanged(null, null);
        }

        private void UcCityMng_OnLoaded(object sender, RoutedEventArgs e)
        {
            SmpUcFormStateLabel.CurrentState = FormCurrentState.Select;
        }

        private void Lookup_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            SmpUcLookup.Select();
        }

        private void SmpUcLookup_OnLookupTextChanged(object sender, TextChangedEventArgs e)
        {
            var dtg = DataGridCities;
            if (dtg.ItemsSource == null) return;
            if (string.IsNullOrEmpty(SmpUcLookup.LookupText))
                CollectionViewSource.GetDefaultView(dtg.ItemsSource).Filter = null;
            else
                CollectionViewSource.GetDefaultView(dtg.ItemsSource).Filter =
                    o => ((City) o).ContainsValue(SmpUcLookup.LookupText);
        }

        private void SaveCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = SmpUcFormStateLabel?.EnabledOfSaveDiscardButtons == true && Validator.ChildrenAreValid<TextBox>(this);
        }

        private void EditCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = SmpUcFormStateLabel?.EnabledOfCrudButtons == true && DataGridCities.SelectedItems.Count>0;
        }

        private void CrudCommands_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = SmpUcFormStateLabel?.EnabledOfCrudButtons == true;
        }

        private void DiscardChangesCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = SmpUcFormStateLabel?.EnabledOfSaveDiscardButtons == true;
        }
    }
}