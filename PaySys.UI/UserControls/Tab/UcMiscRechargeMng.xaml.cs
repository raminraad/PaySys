using System;
using System.Data.Entity;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using PaySys.Globalization;
using PaySys.ModelAndBindLib;
using PaySys.ModelAndBindLib.Engine;
using PaySys.ModelAndBindLib.Model;
using PaySys.UI.Commands;

namespace PaySys.UI.UC
{
	/// <summary>
	///     Interaction logic for
	///     UcMiscRechargeMng.xaml
	/// </summary>
	public partial class UcMiscRechargeMng : UserControl
	{
		public UcMiscRechargeMng()
		{
			InitializeComponent();

//			AddHandler( UcFormStateLabel.PreviewFormCurrentStateChangedEvent, new RoutedEventHandler( ChildFormStateChanged ) );
		}

		private PaySysContext Context { set; get; } = new PaySysContext();

		private void Reload( object sender, ExecutedRoutedEventArgs e )
		{
			SmpUcSelectGroupAndSubGroup.SelectedSubGroupChanged -= SmpUcSelectGroupAndSubGroup_OnSelectedSubGroupChanged;
			var currentMgId = SmpUcSelectGroupAndSubGroup.SelectedMainGroup.MainGroupId;
			var currentSgId = SmpUcSelectGroupAndSubGroup.SelectedSubGroup.SubGroupId;
			Context = new PaySysContext();
			Context.MainGroups.Load();
			SmpUcSelectGroupAndSubGroup.DataContext = Context.MainGroups.Local;

			SmpUcSelectGroupAndSubGroup.SelectedMainGroupId = currentMgId;
			SmpUcSelectGroupAndSubGroup.SelectedSubGroupId = currentSgId;
			LeftJoinAndAssignSubGroupMiscRecharges();
			SmpUcSelectGroupAndSubGroup.SelectedSubGroupChanged += SmpUcSelectGroupAndSubGroup_OnSelectedSubGroupChanged;
		}

		private void Save( object sender, ExecutedRoutedEventArgs e )
		{
			//Todo: implement data validation

			if( true )
			{
				foreach( var rec in SmpUcSelectGroupAndSubGroup.SelectedSubGroup.TempMiscRechargesOfEmployees )
				{
					if( rec.MiscRechargeId == 0 && Math.Abs( rec.Value ) > 0 )
						Context.MiscRecharges.Add( rec );
					if( rec.MiscRechargeId != 0 && rec.Value == 0 )
						Context.MiscRecharges.Remove( rec );
				}

				Context.SaveChanges();
				MessageBox.Show( ResourceAccessor.Messages.GetString( "SaveSuccessful" ) );
				SmpUcFormStateLabel.CurrentState = FormCurrentState.Select;
			}
		}

		private void Edit( object sender, ExecutedRoutedEventArgs e )
		{
			SmpUcFormStateLabel.CurrentState = FormCurrentState.Edit;
		}

		private void DiscardChanges( object sender, ExecutedRoutedEventArgs e )
		{
			SmpUcFormStateLabel.CurrentState = FormCurrentState.Select;
			Context.DiscardChanges();
			LeftJoinAndAssignSubGroupMiscRecharges();
			SmpUcMiscRechargesOfOneEmployee.RefreshCvsOfSubGroupMiscRecharges();
			SmpUcMiscRechargesOfOneMisc.RefreshCvsOfSubGroupMiscRecharges();
		}

		private void SmpUcSelectGroupAndSubGroup_OnSelectedSubGroupChanged( object sender, RoutedEventArgs e )
		{
			LeftJoinAndAssignSubGroupMiscRecharges();
		}

		private void LeftJoinAndAssignSubGroupMiscRecharges()
		{
			var sg = SmpUcSelectGroupAndSubGroup.SelectedSubGroup;
			if( sg == null )
				return;

			var sgCnts = sg.ContractMasters.Where( master => master.IsCurrent );
			var sgEmps = sgCnts.Select( c => c.Employee );
			var sgRecs = sgEmps.SelectMany( emp => emp.MiscRecharges );
			var newQuery = sgEmps.GroupJoin( sgRecs, emp => emp, rec => rec.Employee, ( emp, recEnum ) => new
			{
				Employee = emp,
				MiscRecharges = from m in sg.Miscs.Where( misc => misc.Year == PaySysSetting.CurrentYear )
				                join r in recEnum.Where( r => r.Year == PaySysSetting.CurrentYear && r.Month == PaySysSetting.CurrentMonth ) on m equals r.Misc into empRecs
				                from subRec in empRecs.DefaultIfEmpty( new MiscRecharge
				                {
					                Misc = m,
					                Value = 0,
					                Employee = emp,
					                Month = PaySysSetting.CurrentMonth,
					                Year = m.Year,
					                MiscRechargeId = 0
				                } )
				                select subRec
			} );
			sg.TempMiscRechargesOfEmployees = newQuery.SelectMany( arg => arg.MiscRecharges ).ToList();

			SmpUcMiscRechargesOfOneEmployee.DataContext = sg;
			SmpUcMiscRechargesOfOneMisc.DataContext = sg;
		}

		private void UcMiscRechargeMng_OnInitialized( object sender, EventArgs e )
		{
			Context.MainGroups.Load();
			SmpUcSelectGroupAndSubGroup.DataContext = Context.MainGroups.Local;
			SmpUcFormStateLabel.CurrentState = FormCurrentState.Select;
		}

		private void CommandBinding_OnCanExecute( object sender, CanExecuteRoutedEventArgs e )
		{
			if( e.Command as RoutedUICommand == PaySysCommands.Edit )
				e.CanExecute = SmpUcFormStateLabel?.EnabledOfCrudButtons ?? false;
			else if( e.Command as RoutedUICommand == PaySysCommands.Save )
				e.CanExecute = SmpUcFormStateLabel?.EnabledOfSaveCancelButtons ?? false;
			else if( e.Command as RoutedUICommand == PaySysCommands.DiscardChanges )
				e.CanExecute = SmpUcFormStateLabel?.EnabledOfSaveCancelButtons ?? false;
			else if( e.Command as RoutedUICommand == PaySysCommands.Reload )
				e.CanExecute = SmpUcFormStateLabel?.EnabledOfCrudButtons ?? false;
			e.Handled = true;
		}
	}
}