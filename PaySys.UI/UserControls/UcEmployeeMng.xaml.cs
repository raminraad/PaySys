#region

using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using PaySys.Globalization;
using PaySys.ModelAndBindLib.Engine;
using PaySys.ModelAndBindLib.Entities;

#endregion

namespace PaySys.UI.UC
{
	/// <summary>
	///     Interaction logic for
	///     UcEmployeeMng.xaml
	/// </summary>
	public partial class UcEmployeeMng : UserControl
	{
		private readonly PaySysContext _context = new PaySysContext();
        public UcEmployeeMng()
		{
			InitializeComponent();
			RefreshDtgMain();
			SmpUcFormStateLabel.CurrentState = FormCurrentState.Select;
		}

//		private List<Employee> EmployeesAll;
		public ObservableCollection<Employee> EmployeesAll { set; get; }

		private void RefreshDtgMain()
		{
			var index = DataGridEmployees.SelectedIndex;
			if( SmpUcLookup.LookupText.Trim() == string.Empty )
			{
				EmployeesAll = new ObservableCollection<Employee>( _context.Employees.ToList() );
			}
			else
			{
				var lookups = SmpUcLookup.LookupText.Split( ' ' );
				var lists = lookups.Select( strLookup => ( from x in _context.Employees
				                                           where x.FName.Contains( strLookup ) || x.LName.Contains( strLookup ) || x.PersonnelCode.Contains( strLookup ) || x.DossierNo.Contains( strLookup ) || x.NationalCardNo.Contains( strLookup ) || x.IdCardNo.Contains( strLookup )
				                                           select x ).ToList() ).ToList();

				var lookedUpList = _context.Employees.ToList();
				lists.ForEach( x => lookedUpList.RemoveAll( employee => lookedUpList.Except( x ).Contains( employee ) ) );
				EmployeesAll = new ObservableCollection<Employee>( lookedUpList );
			}

			DataGridEmployees.GetBindingExpression( DataContextProperty )?.UpdateTarget();

			if( DataGridEmployees.Items.Count > index )
				DataGridEmployees.SelectedIndex = index;
		}

		private void ButtonAdd_OnClick( object sender, RoutedEventArgs e )
		{
			SmpUcFormStateLabel.CurrentState = FormCurrentState.Add;
			var newItem = new Employee
			{
				FName = ResourceAccessor.Labels.GetString( "New" )
			};
			EmployeesAll.Add( newItem );
			DataGridEmployees.SelectedItem = newItem;
			DataGridEmployees.ScrollIntoView( newItem );
		}

		private void ButtonEdit_OnClick( object sender, RoutedEventArgs e )
		{
			SmpUcFormStateLabel.CurrentState = FormCurrentState.Edit;
		}

		private void BtnEmployeeDelete_OnClick( object sender, RoutedEventArgs e )
		{
			//Todo
		}

		private void ButtonSave_OnClick( object sender, RoutedEventArgs e )
		{
			SmpUcEmployeeDetail.UpdateSource();

			_context.SaveChanges();
			SmpUcFormStateLabel.CurrentState = FormCurrentState.Select;
			CollectionViewSource.GetDefaultView(DataGridEmployees.DataContext).Refresh();
		}

		private void ButtonDiscardChanges_OnClick( object sender, RoutedEventArgs e )
		{
			if( SmpUcFormStateLabel.CurrentState == FormCurrentState.Add )
				EmployeesAll.Remove( (Employee) DataGridEmployees.SelectedItem );

			SmpUcEmployeeDetail.UpdateTarget();

			SmpUcFormStateLabel.CurrentState = FormCurrentState.Select;
		}

		private void ButtonReload_OnClick( object sender, RoutedEventArgs e )
		{
			RefreshDtgMain();
		}

		private void TextBoxLookup_OnTextChanged( object sender, TextChangedEventArgs e )
		{
			RefreshDtgMain();
		}
	}
}