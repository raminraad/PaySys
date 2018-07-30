using System;
using System.Data.Entity;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using PaySys.Globalization;
using PaySys.ModelAndBindLib;
using PaySys.ModelAndBindLib.Engine;
using PaySys.ModelAndBindLib.Model;

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
			AddHandler( UcFormStateLabel.PreviewFormCurrentStateChangedEvent, new RoutedEventHandler( ChildFormStateChanged ) );

			AddHandler( UcMiscRechargesOfOneEmployee.SaveEvent, new RoutedEventHandler( Save ) );
			AddHandler( UcMiscRechargesOfOneEmployee.PreviewDiscardChangesEvent, new RoutedEventHandler( DiscardChanges ) );
			AddHandler( UcMiscRechargesOfOneEmployee.PreviewReloadEvent, new RoutedEventHandler( Reload ) );

			AddHandler( UcMiscRechargesOfOneMisc.SaveEvent, new RoutedEventHandler( Save ) );
			AddHandler( UcMiscRechargesOfOneMisc.PreviewDiscardChangesEvent, new RoutedEventHandler( DiscardChanges ) );
			AddHandler( UcMiscRechargesOfOneMisc.PreviewReloadEvent, new RoutedEventHandler( Reload ) );
		}

		private PaySysContext Context { set; get; } = new PaySysContext();

		private void Reload( object sender, RoutedEventArgs routedEventArgs )
		{
			var currentMgId = SmpUcSelectGroupAndSubGroup.SelectedMainGroup.MainGroupId;
			var currentSgId = SmpUcSelectGroupAndSubGroup.SelectedSubGroup.SubGroupId;
			Context = new PaySysContext();
			Context.MainGroups.Load();
			SmpUcSelectGroupAndSubGroup.DataContext = Context.MainGroups.Local;
			SmpUcSelectGroupAndSubGroup.SelectedMainGroupId = currentMgId;
			SmpUcSelectGroupAndSubGroup.SelectedSubGroupId = currentSgId;
		}

		private void DiscardChanges( object sender, RoutedEventArgs routedEventArgs )
		{
			Context.DiscardChanges();
		}

		private void Save( object sender, RoutedEventArgs routedEventArgs )
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
		}

		private void ChildFormStateChanged( object sender, RoutedEventArgs e )
		{
			var e2 = (FormCurrentStateChangedEventArgs) e;
			switch( e2.FormCurrentState )
			{
				case FormCurrentState.Unknown:
					break;
				case FormCurrentState.Select:
					SmpUcSelectGroupAndSubGroup.IsEnabled = true;
					TabItemGroupByMiscTitle.IsEnabled = true;
					break;
				case FormCurrentState.Edit:
					if( e2.FormType == SmpUcMiscRechargesOfOneEmployee.GetType() )
					{
						SmpUcSelectGroupAndSubGroup.IsEnabled = false;
						TabItemGroupByMiscTitle.IsEnabled = false;
					}
					break;
				case FormCurrentState.Add:
					break;
				case FormCurrentState.AddMaster:
					break;
				case FormCurrentState.AddDetails:
					break;
				case FormCurrentState.Delete:
					break;
				default:
					throw new ArgumentOutOfRangeException();
			}
		}

		private void SmpUcSelectGroupAndSubGroup_OnSelectedSubGroupChanged( object sender, RoutedEventArgs e )
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
		}

		private void UcMiscRechargeMng_OnInitialized( object sender, EventArgs e )
		{
			Context.MainGroups.Load();
			SmpUcSelectGroupAndSubGroup.DataContext = Context.MainGroups.Local;
		}
	}
}