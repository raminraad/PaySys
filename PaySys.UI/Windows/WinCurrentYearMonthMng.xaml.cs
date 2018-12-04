using System;
using System.Data.Entity;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using Arash;

using PaySys.Model.EntityFramework;
using PaySys.Model.Static;

namespace PaySys.UI.Windows
{
    /// <summary>
    /// Interaction logic for WinCurrentYearMonthMng.xaml
    /// </summary>
    public partial class WinCurrentYearMonthMng : Window
    {
        public PaySysContext Context { get; set; }=new PaySysContext();
        public WinCurrentYearMonthMng()
        {
            InitializeComponent();
            Context.ContractFields.Load();
            FillMonthComboBox();
            FillYearComboBox();
        }

        private void FillMonthComboBox()
        {
            foreach (var m in Enum.GetValues(typeof(Arash.PersianMonth)))
            {
                ComboBoxMonth.Items.Add(m);
            }
        }

        private void FillYearComboBox()
        {
            foreach (var m in Context.ContractFields.Local.Select(field => field.Year).Distinct().ToList().OrderBy(i => i))
            {
                ComboBoxYear.Items.Add(m);
            }
        }

        private void Save_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            PaySysSetting.CurrentMonth = (int) ComboBoxMonth.SelectedItem;
            PaySysSetting.CurrentYear = (int) ComboBoxYear.SelectedItem;
            this.DialogResult = true;
            Close();
        }

        private void Save_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = ComboBoxYear.SelectedIndex > -1 && ComboBoxMonth.SelectedIndex > -1;
        }

        private void DiscardChanges_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            Close();
        }

        private void DiscardChanges_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void WinCurrentYearMonthMng_OnLoaded(object sender, RoutedEventArgs e)
        {
            ComboBoxMonth.SelectedItem=(PersianMonth)PaySysSetting.CurrentMonth ;
            ComboBoxYear.SelectedItem=PaySysSetting.CurrentYear ;
        }
    }
}
