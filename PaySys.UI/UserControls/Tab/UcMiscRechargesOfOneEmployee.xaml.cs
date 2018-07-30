using System;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using PaySys.ModelAndBindLib.Model;
using PaySys.UI.Commands;
using PaySys.UI.ExtensionMethods;

namespace PaySys.UI.UC
{
	/// <summary>
	///     Interaction logic for
	///     UcMiscRechargesOfOneEmployee.xaml
	/// </summary>
	public partial class UcMiscRechargesOfOneEmployee
	{
		public UcMiscRechargesOfOneEmployee()
		{
			InitializeComponent();
		}

		private void SmpUcRibbonSelector_OnListDataContextChanged( object sender, RoutedEventArgs e )
		{
			SmpUcRibbonSelector.SortDescription = "DspLuffName";
		}

		#region Events

		private void CvsFilterMiscRechargesOfCurrentEmployee( object sender, FilterEventArgs e )
		{
			var rec = e.Item as MiscRecharge;
			if( rec == null )
				return;

			e.Accepted = rec.Employee.Equals( SmpUcRibbonSelector.SelectedItem );
		}

		private void SmpUcRibbonSelector_OnSelectedItemChanged( object sender, RoutedEventArgs e )
		{
			CollectionViewSource.GetDefaultView( ListViewMiscRecharges.ItemsSource )?.Refresh();
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

		private void CommandBinding_OnExecuted( object sender, ExecutedRoutedEventArgs e )
		{
			if( e.Command as RoutedUICommand == PaySysCommands.Edit )
			{
				SmpUcFormStateLabel.CurrentState = FormCurrentState.Edit;
			}
			else if( e.Command as RoutedUICommand == PaySysCommands.DiscardChanges )
			{
				var discardArgs = new RoutedEventArgs( PreviewDiscardChangesEvent );
				RaiseEvent( discardArgs );
				SmpUcFormStateLabel.CurrentState = FormCurrentState.Select;
				if( !discardArgs.Handled )
					RaiseEvent( new RoutedEventArgs( DiscardChangesEvent ) );
			}
			else if( e.Command as RoutedUICommand == PaySysCommands.Save )
			{
				//Todo: implement data validation

				if( true )
				{
					SmpUcFormStateLabel.CurrentState = FormCurrentState.Select;
					var saveArgs = new RoutedEventArgs( PreviewSaveEvent );
					RaiseEvent( saveArgs );
					if( !saveArgs.Handled )
						RaiseEvent( new RoutedEventArgs( SaveEvent ) );
				}
			}
			else if( e.Command as RoutedUICommand == PaySysCommands.Reload )
			{
				//Toreview : Some calculations like left join of misc recharges of sub groups are done multiple times now. increase performance by removing redundancy.

				var selectedEmployeeId = ( SmpUcRibbonSelector.SelectedItem as Employee ).EmployeeId;
				var reloadArgs = new RoutedEventArgs( PreviewReloadEvent );
				RaiseEvent( reloadArgs );
				SmpUcRibbonSelector.SelectedItem = ( DataContext as SubGroup )?.CurrentEmployees.FirstOrDefault( emp => emp.EmployeeId == selectedEmployeeId ) ?? ( DataContext as SubGroup )?.CurrentEmployees.FirstOrDefault();
				foreach( var textBox in ListViewMiscRecharges.FindVisualChildren<TextBox>() )
					textBox.GetBindingExpression( TextBox.TextProperty )?.UpdateTarget();

				if( !reloadArgs.Handled )
					RaiseEvent( new RoutedEventArgs( ReloadEvent ) );
			}
		}

		private void UcMiscRechargesOfOneEmployee_OnInitialized( object sender, EventArgs e )
		{
			var cvs = Resources["CvsRechargesOfEmployee"] as CollectionViewSource;
			cvs.SortDescriptions.Add( new SortDescription( "Misc.MiscTitle.Title", ListSortDirection.Ascending ) );
			cvs.Filter += CvsFilterMiscRechargesOfCurrentEmployee;

			SmpUcFormStateLabel.CurrentState = FormCurrentState.Select;
		}

		#endregion

		#region Routed Events

		public static readonly RoutedEvent PreviewSaveEvent = EventManager.RegisterRoutedEvent( "PreviewSave", RoutingStrategy.Tunnel, typeof(RoutedEventHandler), typeof(UcMiscRechargesOfOneEmployee) );

		public event RoutedEventHandler PreviewSave
		{
			add => AddHandler( SaveEvent, value );
			remove => RemoveHandler( SaveEvent, value );
		}

		public static readonly RoutedEvent SaveEvent = EventManager.RegisterRoutedEvent( "Save", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(UcMiscRechargesOfOneEmployee) );

		public event RoutedEventHandler Save
		{
			add => AddHandler( SaveEvent, value );
			remove => RemoveHandler( SaveEvent, value );
		}

		public static readonly RoutedEvent PreviewReloadEvent = EventManager.RegisterRoutedEvent( "PreviewReload", RoutingStrategy.Tunnel, typeof(RoutedEventHandler), typeof(UcMiscRechargesOfOneEmployee) );

		public event RoutedEventHandler PreviewReload
		{
			add => AddHandler( SaveEvent, value );
			remove => RemoveHandler( SaveEvent, value );
		}

		public static readonly RoutedEvent ReloadEvent = EventManager.RegisterRoutedEvent( "Reload", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(UcMiscRechargesOfOneEmployee) );

		public event RoutedEventHandler Reload
		{
			add => AddHandler( SaveEvent, value );
			remove => RemoveHandler( SaveEvent, value );
		}

		public static readonly RoutedEvent PreviewDiscardChangesEvent = EventManager.RegisterRoutedEvent( "PreviewDiscardChanges", RoutingStrategy.Tunnel, typeof(RoutedEventHandler), typeof(UcMiscRechargesOfOneEmployee) );

		public event RoutedEventHandler PreviewDiscardChanges
		{
			add => AddHandler( SaveEvent, value );
			remove => RemoveHandler( SaveEvent, value );
		}

		public static readonly RoutedEvent DiscardChangesEvent = EventManager.RegisterRoutedEvent( "DiscardChanges", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(UcMiscRechargesOfOneEmployee) );

		public event RoutedEventHandler DiscardChanges
		{
			add => AddHandler( SaveEvent, value );
			remove => RemoveHandler( SaveEvent, value );
		}

		#endregion
	}
}