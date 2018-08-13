#region

using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using PaySys.Globalization;
using PaySys.ModelAndBindLib.Engine;
using PaySys.ModelAndBindLib.Model;

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

		private void BtnFilter_OnClick( object sender, RoutedEventArgs e )
		{
			RefreshDtgMain();
		}

		private void RefreshDtgMain()
		{
			var index = DataGridEmployees.SelectedIndex;
			if( TxtFilter.Text.Trim() == string.Empty )
			{
				EmployeesAll = new ObservableCollection<Employee>( _context.Employees.ToList() );
			}
			else
			{
				var filters = TxtFilter.Text.Split( ' ' );
				var lists = new List<List<Employee>>();
				foreach( var strFilter in filters )
					lists.Add( ( from x in _context.Employees
					             where x.FName.Contains( strFilter ) || x.LName.Contains( strFilter ) || x.DossierNo.Contains( strFilter )
					             select x ).ToList() );

				var filteredList = _context.Employees.ToList();
				lists.ForEach( x => filteredList.RemoveAll( employee => filteredList.Except( x ).Contains( employee ) ) );
				EmployeesAll = new ObservableCollection<Employee>( filteredList );
			}

			DataGridEmployees.GetBindingExpression( DataContextProperty )?.UpdateTarget();

			if( DataGridEmployees.Items.Count > index )
				DataGridEmployees.SelectedIndex = index;
		}

		private void BtnEmployeeAdd_OnClick( object sender, RoutedEventArgs e )
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

		private void BtnEmployeeEdit_OnClick( object sender, RoutedEventArgs e )
		{
			SmpUcFormStateLabel.CurrentState = FormCurrentState.Edit;
		}

		private void BtnEmployeeDelete_OnClick( object sender, RoutedEventArgs e )
		{
			//Todo
		}

		private void BtnEmployeeSave_OnClick( object sender, RoutedEventArgs e )
		{
			SmpUcEmployeeDetail.UpdateSource();

			_context.SaveChanges();
			SmpUcFormStateLabel.CurrentState = FormCurrentState.Select;
		}

		private void BtnEmployeeCancel_OnClick( object sender, RoutedEventArgs e )
		{
			if( SmpUcFormStateLabel.CurrentState == FormCurrentState.Add )
				EmployeesAll.Remove( (Employee) DataGridEmployees.SelectedItem );

			SmpUcEmployeeDetail.UpdateTarget();

			SmpUcFormStateLabel.CurrentState = FormCurrentState.Select;
		}

		private void BtnEmployeeRefresh_OnClick( object sender, RoutedEventArgs e )
		{
			RefreshDtgMain();
		}
	}
}