#region
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using Arash.PersianDateControls;
using PaySys.Model.Entities;
using Xceed.Wpf.Toolkit;
#endregion

namespace PaySys.UI.UserControls
{
	/// <summary>
	///     Interaction logic for
	///     UcMissionDetail.xaml
	/// </summary>
	public partial class UcMissionDetail : UserControl
	{
		public static readonly DependencyProperty ReadOnlyOfFieldsProperty = DependencyProperty.Register( "ReadOnlyOfFields", typeof(bool), typeof(UcMissionDetail), new PropertyMetadata( default(bool) ) );
	    public ObservableCollection<City> CitiesAll { set => ComboBoxCities.ItemsSource = value; }
		public UcMissionDetail()
		{
			InitializeComponent();
		}

		public bool ReadOnlyOfFields
		{
			get => (bool) GetValue( ReadOnlyOfFieldsProperty );
			set => SetValue( ReadOnlyOfFieldsProperty, value );
		}

		public void UpdateSource()
		{
			foreach( var control in GridDetail.Children.OfType<Control>() )
			{
				control.GetBindingExpression( PersianDatePicker.SelectedDateProperty )?.UpdateSource();
				control.GetBindingExpression( TextBox.TextProperty )?.UpdateSource();
				control.GetBindingExpression( Selector.SelectedItemProperty )?.UpdateSource();
				control.GetBindingExpression( TimePicker.ValueProperty)?.UpdateSource();
			}
		}

		public void UpdateTarget()
		{
			foreach( var control in GridDetail.Children.OfType<Control>() )
			{
				control.GetBindingExpression( TextBox.TextProperty )?.UpdateTarget();
				control.GetBindingExpression( Selector.SelectedItemProperty )?.UpdateTarget();
				control.GetBindingExpression( PersianDatePicker.SelectedDateProperty )?.UpdateTarget();
				control.GetBindingExpression( TimePicker.ValueProperty)?.UpdateTarget();
			}
		}
	}
}