﻿using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using PaySys.Model.ExtensionMethods;

namespace PaySys.UI.UserControls
{
    /// <summary>
    /// Interaction logic for SmpUcShowContractDetails.xaml
    /// </summary>
    public partial class UcContractDetailsEdit : UserControl
    {
        public UcContractDetailsEdit()
        {
            InitializeComponent();
        }

        public static readonly DependencyProperty ReadOnlyOfEditFieldsProperty =
            DependencyProperty.Register("ReadOnlyOfEditFields", typeof(bool), typeof(UcContractDetailsEdit),
                new PropertyMetadata(default(bool)));

        public bool ReadOnlyOfEditFields

        {
            get => (bool) GetValue(ReadOnlyOfEditFieldsProperty);
            set => SetValue(ReadOnlyOfEditFieldsProperty, value);
        }

        public void CommitContext()
        {
            foreach (var control in this.FindVisualChildren<UcTextPair>())
                control.UpdateSource();
//            foreach (var cnt in this.FindVisualChildren<Control>())
//            {
//                cnt.GetBindingExpression(TextBox.TextProperty)?.UpdateSource();
//                cnt.GetBindingExpression(Selector.SelectedItemProperty)?.UpdateSource();
//            }

            GetBindingExpression(DataContextProperty)?.UpdateSource();
        }

        public void Select()
        {
            ListViewContractDetails.SelectedIndex = 0;
            ListViewContractDetails.Focus();
        }
public void Refresh()
        {
            CollectionViewSource.GetDefaultView(ListViewContractDetails.ItemsSource)?.Refresh();
        }
    }
}