﻿#region
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using PaySys.Globalization;

using PaySys.Model.Entities;
using PaySys.Model.EntityFramework;
using PaySys.Model.Enums;
#endregion

namespace PaySys.UI.UserControls
{
	/// <summary>
	///     Interaction logic for
	///     UcMissionMng.xaml
	/// </summary>
	public partial class UcMissionMng : UserControl
	{
		public PaySysContext Context { get; set; }

		public UcMissionMng()
		{
			InitializeComponent();
			RefreshListView();
			SmpUcFormStateLabel.CurrentState = FormCurrentState.Select;
		}

		public ObservableCollection<Mission> MissionsAll { set; get; }

		private void RefreshListView()
		{
			var selectedId = (DataGridMissions.SelectedItem as Mission)?.Id;
			Context = new PaySysContext();
			Context.Missions.Load();
            Context.Cities.Load();
			MissionsAll = Context.Missions.Local;
		    SmpUcMissionDetail.CitiesAll = Context.Cities.Local;

            GetBindingExpression( DataContextProperty )?.UpdateTarget();

			if(selectedId.HasValue)
				DataGridMissions.SelectedItem = MissionsAll.FirstOrDefault(m => m.Id == selectedId.Value);
		}

		private void Add_CanExecute( object sender, CanExecuteRoutedEventArgs e )
		{
			if( SmpUcFormStateLabel != null )
				e.CanExecute = SmpUcFormStateLabel.EnabledOfCrudButtons;
		}

		private void Edit_CanExecute( object sender, CanExecuteRoutedEventArgs e )
		{
			if( SmpUcFormStateLabel != null )
			e.CanExecute = SmpUcFormStateLabel.EnabledOfCrudButtons;
		}

		private void Delete_CanExecute( object sender, CanExecuteRoutedEventArgs e )
		{
			if( SmpUcFormStateLabel != null )
			e.CanExecute = SmpUcFormStateLabel.EnabledOfCrudButtons;
		}

		private void Save_CanExecute( object sender, CanExecuteRoutedEventArgs e )
		{
			if( SmpUcFormStateLabel != null )
			e.CanExecute = SmpUcFormStateLabel.EnabledOfSaveDiscardButtons;
		}

		private void DiscardChanges_CanExecute( object sender, CanExecuteRoutedEventArgs e )
		{
			if( SmpUcFormStateLabel != null )
			e.CanExecute = SmpUcFormStateLabel.EnabledOfSaveDiscardButtons;
		}

		private void Reload_CanExecute( object sender, CanExecuteRoutedEventArgs e )
		{
			if( SmpUcFormStateLabel != null )
			e.CanExecute = SmpUcFormStateLabel.EnabledOfCrudButtons;
		}

		private void Add_Executed( object sender, ExecutedRoutedEventArgs e )
		{
			SmpUcFormStateLabel.CurrentState = FormCurrentState.Add;
			var newItem = new Mission
			{
				Title = ResourceAccessor.Labels.GetString( "New" )
			};
			MissionsAll.Add( newItem );
			DataGridMissions.SelectedItem = newItem;
			DataGridMissions.ScrollIntoView( newItem );
		}

		private void Edit_Executed( object sender, ExecutedRoutedEventArgs e )
		{
			SmpUcFormStateLabel.CurrentState = FormCurrentState.Edit;
		}

		private void Delete_Executed( object sender, ExecutedRoutedEventArgs e )
		{
			//Todo
		}

		private void Save_Executed( object sender, ExecutedRoutedEventArgs e )
		{
			SmpUcMissionDetail.UpdateSource();
			Context.SaveChanges();
			SmpUcFormStateLabel.CurrentState = FormCurrentState.Select;
            CollectionViewSource.GetDefaultView(DataGridMissions.ItemsSource)?.Refresh();
		}

		private void DiscardChanges_Executed( object sender, ExecutedRoutedEventArgs e )
		{
			if( SmpUcFormStateLabel.CurrentState == FormCurrentState.Add )
				MissionsAll.Remove( (Mission) DataGridMissions.SelectedItem );

			SmpUcMissionDetail.UpdateTarget();

			SmpUcFormStateLabel.CurrentState = FormCurrentState.Select;
		}

		private void Reload_Executed( object sender, ExecutedRoutedEventArgs e )
		{
			RefreshListView();
		}

	    private void SmpUcLookup_OnLookupTextChanged(object sender, TextChangedEventArgs e)
	    {
	        var dtg = DataGridMissions;
	        if (dtg.ItemsSource == null) return;
	        if (string.IsNullOrEmpty(SmpUcLookup.LookupText))
	            CollectionViewSource.GetDefaultView(dtg.ItemsSource).Filter = null;
	        else
	            CollectionViewSource.GetDefaultView(dtg.ItemsSource).Filter =
	                o => ((Mission)o).ContainsValue(SmpUcLookup.LookupText);
        }
	}
}