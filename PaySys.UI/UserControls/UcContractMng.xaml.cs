﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using PaySys.Model.Entities;
using PaySys.Model.EntityFramework;
using PaySys.Model.Enums;
using UserControl = System.Windows.Controls.UserControl;

namespace PaySys.UI.UserControls
{
    /// <summary>
    /// Interaction logic for UcContractMng.xaml
    /// </summary>
    public partial class UcContractMng : UserControl
    {
        public PaySysContext Context { set; get; }
        public ObservableCollection<Employee> EmployeesAll { set; get; }

        public UcContractMng()
        {
            InitializeComponent();
            Reload();
            SmpUcFormState.CurrentState = FormCurrentState.Select;
        }

        private void Reload()
        {
            var selectedId = (DataGridEmployees.SelectedItem as Employee)?.Id;
            Context = new PaySysContext();
            Context.MainGroups.Include(mg=>mg.SubGroups).Load();
            Context.SubGroups.Load();
            Context.Jobs.Load();
            Context.Employees.Include(employee => employee.ContractMasters).Load();

            SmpUcContractMasterEdit.MainGroups = Context.MainGroups.Local;
            SmpUcContractMasterEdit.SubGroups = Context.SubGroups.Local;
            SmpUcContractMasterEdit.Jobs = Context.Jobs.Local;

            EmployeesAll = Context.Employees.Local;
            DataGridEmployees.GetBindingExpression(ItemsControl.ItemsSourceProperty)?.UpdateTarget();
            DataGridEmployees.GetBindingExpression(DataContextProperty)?.UpdateTarget();
            if (selectedId.HasValue)
                DataGridEmployees.SelectedItem = EmployeesAll.FirstOrDefault(emp => emp.Id == selectedId.Value);
            SmpUcLookup_OnLookupTextChanged(null, null);
        }

        private void Add_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            SmpUcFormState.CurrentState = FormCurrentState.AddMaster;

            if (!(DataGridEmployees.SelectedItem is Employee selectedEmployee)) return;
            var newItem = new ContractMaster
            {
                Employee = selectedEmployee,
                ContractNo = "0",
                ContractDetails = new List<ContractDetail>(),
                DateEmployment = selectedEmployee.ContractMasters.FirstOrDefault(c => c.IsCurrent)?.DateEmployment??DateTime.Now,
                DateExecution = DateTime.Now,
                DateExport = DateTime.Now
            };
            selectedEmployee.ContractMasters.Add(newItem);
            CollectionViewSource.GetDefaultView(DataGridEmployeeContracts.ItemsSource)?.Refresh();
            DataGridEmployeeContracts.SelectedItem = newItem;
            DataGridEmployeeContracts.ScrollIntoView(newItem);
            SmpUcContractMasterEdit.SelectFirstItem();

//			SmpUcContractDetailsEdit.DataContext = null;
//		    SmpUcContractMasterEdit.CurrentContractMaster = newItem;
        }

        private void Edit_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            SmpUcFormState.CurrentState = FormCurrentState.Edit;
        }

        private void Delete_Executed(object sender, ExecutedRoutedEventArgs e)
        {
        }

        private void Save_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            switch (SmpUcFormState.CurrentState)
            {
                case FormCurrentState.AddMaster:
                    SaveContractMasterAndContinue();
                    SmpUcFormState.CurrentState = FormCurrentState.Edit;
                    break;
                case FormCurrentState.Edit:
                    SmpUcContractDetailsEdit.CommitContext();
                    SmpUcContractMasterEdit.CommitContext();
                    SmpUcContractDetailsEdit.Refresh();
                    SmpUcContractDetailsEdit.GetBindingExpression(DataContextProperty)?.UpdateSource();
                    SmpUcFormState.CurrentState = FormCurrentState.Select;
                    break;
            }

            Context.SaveChanges();
        }

        private void SaveContractMasterAndContinue()
        {
            SmpUcContractMasterEdit.CommitContext();
            if (!(SmpUcContractMasterEdit.DataContext is ContractMaster newItem)) return;
            var contractFields = newItem.MainGroup.CurrentContractFields.ToList();
            contractFields.ForEach(gft => newItem.ContractDetails.Add(new ContractDetail {ContractField = gft}));
            Context.ContractMasters.Add(newItem);
            Context.SaveChanges();
            SmpUcFormState.CurrentState = FormCurrentState.AddDetails;
            DataGridEmployeeContracts.SelectedItem = newItem;
            SmpUcContractDetailsEdit.Refresh();
            SmpUcContractDetailsEdit.Select();
        }

        private void Reload_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            Reload();
        }

        private void CrudCommands_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (SmpUcFormState != null) e.CanExecute = SmpUcFormState.EnabledOfCrudButtons;
        }

        private void EditCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = SmpUcFormState?.EnabledOfCrudButtons == true &&
                           DataGridEmployeeContracts.SelectedItem is ContractMaster;
        }

        private void SaveCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = SmpUcFormState?.EnabledOfSaveDiscardButtons == true;
        }

        private void DiscardChangesCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = SmpUcFormState?.EnabledOfSaveDiscardButtons == true;
        }

        private void DiscardChanges_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            Context.DiscardChanges();
            Reload();
            SmpUcFormState.CurrentState = FormCurrentState.Select;
        }

        private void SmpUcLookup_OnLookupTextChanged(object sender, TextChangedEventArgs e)
        {
            var dtg = DataGridEmployees;
            if (dtg.ItemsSource == null) return;
            if (string.IsNullOrEmpty(SmpUcLookup.LookupText))
                CollectionViewSource.GetDefaultView(dtg.ItemsSource).Filter = null;
            else
                CollectionViewSource.GetDefaultView(dtg.ItemsSource).Filter =
                    o => ((Employee)o).ContainsValue(SmpUcLookup.LookupText);
        }
    }
}