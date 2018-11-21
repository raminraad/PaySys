#region
using System;
using System.Data.Entity;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using PaySys.Globalization;
using PaySys.ModelAndBindLib;
using PaySys.ModelAndBindLib.Engine;
using PaySys.ModelAndBindLib.Entities;
using PaySys.ModelAndBindLib.Enums;
using PaySys.UI.Commands;
using ValueType = PaySys.ModelAndBindLib.Enums.ValueType;
#endregion


namespace PaySys.UI.UC
{
    public partial class UcMonthlyDataMng : UserControl
    {
        public UcMonthlyDataMng()
        {
            InitializeComponent();
        }

        private PaySysContext Context { set; get; } = new PaySysContext();

        private void Reload(object sender, ExecutedRoutedEventArgs e)
        {
            DataGridSubGroups.SelectionChanged -= DataGridSubGroups_OnSelectionChanged;
//            var currentMg = (DataGridMainGroups.SelectedItem as MainGroup);
//            var currentSg = (DataGridSubGroups.SelectedItem as SubGroup);
            Context = new PaySysContext();
            Context.MainGroups.Include(mg=>mg.SubGroups).Load();
            DataGridMainGroups.DataContext = Context.MainGroups.Local;

//            DataGridMainGroups.SelectedItem = currentMg??Context.MainGroups.Local.First();
//            DataGridSubGroups.SelectedItem = currentSg?? (DataGridMainGroups.SelectedItem as MainGroup)?.SubGroups.First();
            LeftJoinAndAssignSubGroupMonthlyData();
            DataGridSubGroups.SelectionChanged +=
                DataGridSubGroups_OnSelectionChanged;
        }

        private void Save(object sender, ExecutedRoutedEventArgs e)
        {
            //Todo: implement data validation

            if (DataGridSubGroups.SelectedItem is SubGroup sg)
            {
                foreach (var v in sg.TempVariableValuesOfEmployees)
                {
                    if (v.Id == 0)
                        switch (v.Variable.ValueType)
                        {
                            case ValueType.Absolute:
                            case ValueType.Percent:
                                if (v.NumericValue.HasValue) Context.VariableValueForEmployees.Add(v);
                                break;
                            case ValueType.Date:
                                if (v.DateValue.HasValue) Context.VariableValueForEmployees.Add(v);
                                break;
                            case ValueType.String:
                                if (!string.IsNullOrEmpty(v.StringValue)) Context.VariableValueForEmployees.Add(v);
                                break;
                        }
                    else if (v.Id != 0)
                        switch (v.Variable.ValueType)
                        {
                            case ValueType.Absolute:
                            case ValueType.Percent:
                                if (!v.NumericValue.HasValue) Context.VariableValueForEmployees.Remove(v);
                                break;
                            case ValueType.Date:
                                if (!v.DateValue.HasValue) Context.VariableValueForEmployees.Remove(v);
                                break;
                            case ValueType.String:
                                if (string.IsNullOrEmpty(v.StringValue)) Context.VariableValueForEmployees.Remove(v);
                                break;
                        }
                }

                foreach (var m in sg.TempMiscValuesOfEmployees)
                {
                    if (m.Id == 0 && (m.Value != 0 || m.ValueSubtraction != 0)) Context.MiscValueForEmployees.Add(m);
                    if (m.Id != 0 && (m.Value != 0 || m.ValueSubtraction != 0)) Context.MiscValueForEmployees.Remove(m);
                }

                Context.SaveChanges();
                MessageBox.Show(ResourceAccessor.Messages.GetString("SaveSuccessful"));
                SmpUcFormStateLabel.CurrentState = FormCurrentState.Select;
            }
        }

        private void Edit(object sender, ExecutedRoutedEventArgs e)
        {
            SmpUcFormStateLabel.CurrentState = FormCurrentState.Edit;
        }

        private void DiscardChanges(object sender, ExecutedRoutedEventArgs e)
        {
            SmpUcFormStateLabel.CurrentState = FormCurrentState.Select;
            Context.DiscardChanges();
            LeftJoinAndAssignSubGroupMonthlyData();
            SmpUcMonthlyDataOfOneEmployee.RefreshCvs();
            SmpUcMonthlyDataOfOneVariable.RefreshCvs();
            SmpUcMonthlyDataOfOneMiscPayment.RefreshCvs();
            SmpUcMonthlyDataOfOneMiscDebt.RefreshCvs();
        }

        private void DataGridSubGroups_OnSelectionChanged(object sender, RoutedEventArgs e)
        {
            LeftJoinAndAssignSubGroupMonthlyData();
        }

        private void LeftJoin()
        {
            if (!(DataGridSubGroups.SelectedItem is SubGroup sg)) return;

            var sgCnts = sg.ContractMasters.Where(master => master.IsCurrent);
            var sgEmps = sgCnts.Select(c => c.Employee);
            var sgRecs = sgEmps.SelectMany(emp => emp.MiscRecharges);
            var newQuery = sgEmps.GroupJoin(sgRecs, emp => emp, rec => rec.Employee, (emp, recEnum) => new
            {
                Employee = emp,
                MiscRecharges =
                    from m in sg.Miscs.Where(misc =>
                        misc.Year == PaySysSetting.CurrentYear && misc.Month == PaySysSetting.CurrentMonth)
                    join r in recEnum.Where(r =>
                            r.Year == PaySysSetting.CurrentYear && r.Month == PaySysSetting.CurrentMonth) on m equals r
                            .Misc
                        into empRecs
                    from subRec in empRecs.DefaultIfEmpty(new MiscRecharge
                    {
                        Misc = m,
                        Value = 0,
                        Employee = emp,
                        Month = PaySysSetting.CurrentMonth,
                        Year = m.Year,
                        Id = 0
                    })
                    select subRec
            });

            sg.TempMiscRechargesOfEmployees = newQuery.SelectMany(arg => arg.MiscRecharges).ToList();
        }

        private void LeftJoinAndAssignSubGroupMonthlyData()
        {
            if (!(DataGridSubGroups.SelectedItem is SubGroup sg)) return;

            var sgCnts = sg.ContractMasters.Where(master => master.IsCurrent);
            var sgEmps = sgCnts.Select(c => c.Employee).ToList();
            var sgVars = sgEmps.SelectMany(emp => emp.VariableValueForEmployees);
            var sgMiscs = sgEmps.SelectMany(emp => emp.MiscValueForEmployees);
            var empVarsJoin = sgEmps.GroupJoin(sgVars, emp => emp, var => var.Employee, (emp, vars) => new
            {
                Employee = emp,
                VariableValues = from sgV in sg.CurrentVariables
                    join v in vars.Where(r =>
                            r.Year == PaySysSetting.CurrentYear && r.Month == PaySysSetting.CurrentMonth)
                        on sgV equals v.Variable into empVars
                    from subRec in empVars.DefaultIfEmpty(new VariableValueForEmployee
                    {
                        Variable = sgV,
                        
//					                Value = null,
                        NumericValue = null,
                        StringValue = null,
                        DateValue = null,
                        Employee = emp,
                        Month = PaySysSetting.CurrentMonth,
                        Year = PaySysSetting.CurrentYear,
                        Id = 0
                    })
                    select subRec
            });
            var empMiscsJoin = sgEmps.GroupJoin(sgMiscs, emp => emp, var => var.Employee, (emp, miscs) => new
            {
                Employee = emp,
                MiscValues = from sgM in sg.CurrentMiscs
                    join m in miscs.Where(r =>
                            r.Misc.Year == PaySysSetting.CurrentYear && r.Misc.Month == PaySysSetting.CurrentMonth)
                        on sgM equals m.Misc into empMiscs
                    from subVar in empMiscs.DefaultIfEmpty(new MiscValueForEmployee
                    {
                        Misc = sgM,
                        Value = 0,
                        ValueSubtraction = 0,
                        Employee = emp,
                        Id = 0
                    })
                    select subVar
            });

            sg.TempVariableValuesOfEmployees = empVarsJoin.SelectMany(arg => arg.VariableValues).ToList();
            sg.TempMiscValuesOfEmployees = empMiscsJoin.SelectMany(arg => arg.MiscValues).ToList();

            SmpUcMonthlyDataOfOneEmployee.DataContext = sg;
            SmpUcMonthlyDataOfOneVariable.DataContext = sg;
            SmpUcMonthlyDataOfOneMiscPayment.DataContext = sg;
            SmpUcMonthlyDataOfOneMiscDebt.DataContext = sg;
        }

        private void UcMonthlyDataMng_OnInitialized(object sender, EventArgs e)
        {
            SmpUcFormStateLabel.CurrentState = FormCurrentState.Select;
            Reload(null,null);
        }

        private void CommandBinding_OnCanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (e.Command as RoutedUICommand == PaySysCommands.Edit)
                e.CanExecute = SmpUcFormStateLabel?.EnabledOfCrudButtons ?? false;
            else if (e.Command as RoutedUICommand == PaySysCommands.Save)
                e.CanExecute = SmpUcFormStateLabel?.EnabledOfSaveDiscardButtons ?? false;
            else if (e.Command as RoutedUICommand == PaySysCommands.DiscardChanges)
                e.CanExecute = SmpUcFormStateLabel?.EnabledOfSaveDiscardButtons ?? false;
            else if (e.Command as RoutedUICommand == PaySysCommands.Reload)
                e.CanExecute = SmpUcFormStateLabel?.EnabledOfCrudButtons ?? false;
            e.Handled = true;
        }
    }
}