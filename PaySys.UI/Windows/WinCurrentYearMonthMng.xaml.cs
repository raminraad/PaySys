using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Runtime.Remoting.Contexts;
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
using PaySys.ModelAndBindLib;
using PaySys.ModelAndBindLib.Engine;

namespace PaySys.Windows
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
    }
}
