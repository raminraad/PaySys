using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using PaySys.Globalization;
using PaySys.ModelAndBindLib.Model;

namespace PaySys.UI.UC
{

	public partial class UcFormStateLabel
	{
		private FormCurrentState _currentState = FormCurrentState.Unknown;
//		public static readonly DependencyProperty IsReadOnlyProperty;
		public UcFormStateLabel()
		{
			InitializeComponent();

			FrameworkPropertyMetadata metadata = new FrameworkPropertyMetadata(
				new Thickness(), FrameworkPropertyMetadataOptions.AffectsMeasure);
		}

		public static readonly DependencyProperty ReadOnlyFieldsProperty = DependencyProperty.Register("ReadOnlyFields", typeof(bool), typeof(UcFormStateLabel), new PropertyMetadata(default(bool)));
		public static readonly DependencyProperty CrudButtonsEnabledProperty = DependencyProperty.Register("CrudButtonsEnabled", typeof(bool), typeof(UcFormStateLabel), new PropertyMetadata(default(bool)));
		public static readonly DependencyProperty SaveCancelButtonsEnabledProperty = DependencyProperty.Register("SaveCancelButtonsEnabled", typeof(bool), typeof(UcFormStateLabel), new PropertyMetadata(default(bool)));

		public bool SaveCancelButtonsEnabled
		{
			get { return (bool) GetValue(SaveCancelButtonsEnabledProperty); }
			set { SetValue(SaveCancelButtonsEnabledProperty, value); }
		}
		public bool CrudButtonsEnabled
		{
			get { return (bool) GetValue(CrudButtonsEnabledProperty); }
			set { SetValue(CrudButtonsEnabledProperty, value); }
		}
		public bool ReadOnlyFields
		{
			get { return (bool)GetValue(ReadOnlyFieldsProperty); }
			set { SetValue(ReadOnlyFieldsProperty, value); }
		}

		public FormCurrentState CurrentState
		{
			get => _currentState;
			set
			{
				switch (value)
				{
					case FormCurrentState.Select:
						LblState.Foreground = (Brush)FindResource("FormStateColorSelect");
						LblState.Content = ResourceAccessor.Labels.GetString("Select");
						break;
					case FormCurrentState.Edit:
						LblState.Foreground = (Brush)FindResource("FormStateColorEdit");
						LblState.Content = ResourceAccessor.Labels.GetString("Edit");
						break;
					case FormCurrentState.Add:
						LblState.Foreground = (Brush)FindResource("FormStateColorAdd");
						LblState.Content = ResourceAccessor.Labels.GetString("Add");
						break;
					case FormCurrentState.Delete:
						LblState.Foreground = (Brush)FindResource("FormStateColorDelete");
						LblState.Content = ResourceAccessor.Labels.GetString("Delete");
						break;
					case FormCurrentState.Unknown:
						LblState.Foreground = (Brush)FindResource("FormStateColorUnknown");
						LblState.Content = ResourceAccessor.Labels.GetString("Unknown");
						break;
					default:
						throw new ArgumentOutOfRangeException(nameof(value), value, null);
				}
				ReadOnlyFields = value != FormCurrentState.Edit && value != FormCurrentState.Add;
				CrudButtonsEnabled = value == FormCurrentState.Select;
				SaveCancelButtonsEnabled = value == FormCurrentState.Edit || value == FormCurrentState.Add;
				_currentState = value;
			}
		}
	}
}