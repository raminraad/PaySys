using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using PaySys.Globalization;
using PaySys.Model.Entities;
using PaySys.Model.Static;
using PaySys.UI.Modals;
using UserControl = System.Windows.Controls.UserControl;

namespace PaySys.UI.UserControls
{
    /// <summary>
    /// Interaction logic for UcMiscMng.xaml
    /// </summary>
    public partial class UcMiscMng : UserControl
    {
        public UcMiscMng()
        {
            InitializeComponent();
            ListViewMiscDebt.Items.Filter = o => ((Misc) o).Year == PaySysSetting.CurrentYear;
            ListViewMiscPayment.Items.Filter = o => ((Misc) o).Year == PaySysSetting.CurrentYear;
        }

        public static readonly DependencyProperty ReadOnlyOfFieldsProperty =
            DependencyProperty.Register("ReadOnlyOfFields", typeof(bool), typeof(UcMiscMng),
                new PropertyMetadata(default(bool)));

        public bool ReadOnlyOfFields
        {
            get => (bool) GetValue(ReadOnlyOfFieldsProperty);
            set => SetValue(ReadOnlyOfFieldsProperty, value);
        }


        #region Properties
        public static readonly DependencyProperty CurrentSubGroupProperty =
            DependencyProperty.Register("CurrentSubGroup", typeof(SubGroup), typeof(UcMiscMng),
                new PropertyMetadata(default(SubGroup)));

        public SubGroup CurrentSubGroup
        {
            get => (SubGroup) GetValue(CurrentSubGroupProperty);
            set => SetValue(CurrentSubGroupProperty, value);
        }

        public List<ExpenseArticle> ExpenseArticlesAll { get; set; }

        public List<MiscTitle> MiscTitlesAll { get; set; }
        #endregion


        private void UcMiscMng_OnLoaded(object sender, RoutedEventArgs e)
        {
            SmpUcSuggesterTextBoxMiscTitlePayments.ItemsSource =
                MiscTitlesAll?.Where(title => title.IsPayment).Select(title => title.Title);
            SmpUcSuggesterTextBoxMiscTitleDebts.ItemsSource =
                MiscTitlesAll?.Where(title => !title.IsPayment).Select(title => title.Title);
        }


        #region Add/Remove misc commands
        private void AddMisc_CanExecute(object target, CanExecuteRoutedEventArgs e)
        {
            //ToDo: Optimize performance by using Misc Title list of sub group one-time loaded not every time.
            if (CurrentSubGroup == null)
            {
                e.CanExecute = false;
                return;
            }

//            var newItemIsPayment = (target as Control).Name.Equals("ListViewMiscPayment");
//            if (newItemIsPayment)
                var val = (target as Control).Name.Equals("ListViewMiscPayment")
                    ? SmpUcSuggesterTextBoxMiscTitlePayments.SelectedValue
                    : SmpUcSuggesterTextBoxMiscTitleDebts.SelectedValue;

            e.CanExecute = !string.IsNullOrEmpty(val) &&
                           !CurrentSubGroup.MiscsOfTypePayment.Select(misc => misc.MiscTitle.Title)
                               .Contains(val) && !CurrentSubGroup
                               .MiscsOfTypeDebt.Select(misc => misc.MiscTitle.Title)
                               .Contains(val);

            //            else
            //                e.CanExecute = !string.IsNullOrEmpty(SmpUcSuggesterTextBoxMiscTitleDebts.SelectedValue) &&
            //                               !CurrentSubGroup.MiscsOfTypeDebt.Select(misc => misc.MiscTitle.Title)
            //                                   .Contains(SmpUcSuggesterTextBoxMiscTitleDebts.SelectedValue);
            e.Handled = true;
        }

        private void AddMisc_Execute(object target, ExecutedRoutedEventArgs e)
        {
            var newItemIsPayment = (target as Control).Name.Equals("ListViewMiscPayment");
            var param = e.Parameter.ToString().Trim();
            var newMiscTitle = MiscTitlesAll.FirstOrDefault(title => string.Equals(title.Title, param)) ??
                               new MiscTitle
                               {
                                   Title = param,
                                   IsPayment = newItemIsPayment
                               };
            CurrentSubGroup.Miscs.Add(new Misc
            {
                MiscTitle = newMiscTitle,
                Year = PaySysSetting.CurrentYear
            });
            if (newMiscTitle.Id == 0) MiscTitlesAll.Add(newMiscTitle);

            if (newItemIsPayment)
                ListViewMiscPayment.GetBindingExpression(ItemsControl.ItemsSourceProperty)?.UpdateTarget();
            else
                ListViewMiscDebt.GetBindingExpression(ItemsControl.ItemsSourceProperty)?.UpdateTarget();
            if(newItemIsPayment) SmpUcSuggesterTextBoxMiscTitlePayments.CollapseList();
            else SmpUcSuggesterTextBoxMiscTitleDebts.CollapseList();
        }
        #endregion


        private void RemoveMisc_Execute(object target, ExecutedRoutedEventArgs e)
        {
            if (!(e.Parameter is Misc item)) return;
            var itemIsPayment = item.MiscTitle.IsPayment;
            var listTitle = ResourceAccessor.Labels.GetString(itemIsPayment ? "MiscPayments" : "MiscDebts");
            if (PaySysMessage.GetDeleteSubGroupMiscConfirmation(item.MiscTitle.Title, listTitle) !=
                MessageBoxResult.Yes)
                return;

            CurrentSubGroup.Miscs.Remove(item);

            if (itemIsPayment)
                ListViewMiscPayment.GetBindingExpression(ItemsControl.ItemsSourceProperty)?.UpdateTarget();
            else
                ListViewMiscDebt.GetBindingExpression(ItemsControl.ItemsSourceProperty)?.UpdateTarget();
        }

        public void Refresh()
        {
            CollectionViewSource.GetDefaultView(ListViewMiscPayment.ItemsSource).Refresh();
            CollectionViewSource.GetDefaultView(ListViewMiscDebt.ItemsSource).Refresh();
        }
    }
}