using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
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
using PaySys.UI.Annotations;

namespace PaySys.UI.UC
{

	public partial class UcFormStateLabel:INotifyPropertyChanged
	{
		private FormCurrentState _currentState = FormCurrentState.Unknown;
//		public static readonly DependencyProperty IsReadOnlyProperty;
		public UcFormStateLabel()
		{
			InitializeComponent();

			FrameworkPropertyMetadata metadata = new FrameworkPropertyMetadata(
				new Thickness(), FrameworkPropertyMetadataOptions.AffectsMeasure);
		}

		public static readonly DependencyProperty ReadOnlyOfEditControlsProperty = DependencyProperty.Register("ReadOnlyOfEditControls", typeof(bool), typeof(UcFormStateLabel), new PropertyMetadata(default(bool)));
		public static readonly DependencyProperty EnabledOfCrudButtonsProperty = DependencyProperty.Register("EnabledOfCrudButtons", typeof(bool), typeof(UcFormStateLabel), new PropertyMetadata(default(bool)));
		public static readonly DependencyProperty EnabledOfSaveCancelButtonsProperty = DependencyProperty.Register("EnabledOfSaveCancelButtons", typeof(bool), typeof(UcFormStateLabel), new PropertyMetadata(default(bool)));
		//		public static readonly DependencyProperty ReadOnlyAddFieldsProperty = DependencyProperty.Register("ReadOnlyAddFields", typeof(bool), typeof(UcFormStateLabel), new PropertyMetadata(default(bool)));

		//		public bool ReadOnlyAddFields
		//		{
		//			get => (bool) GetValue(ReadOnlyAddFieldsProperty);
		//			set => SetValue(ReadOnlyAddFieldsProperty, value);
		//		}
		/// <summary>
		/// Enabled attribute of Save and Cancel buttons
		/// </summary>
		public bool EnabledOfSaveCancelButtons
		{
			get => (bool) GetValue(EnabledOfSaveCancelButtonsProperty);
			set => SetValue(EnabledOfSaveCancelButtonsProperty, value);
		}
		/// <summary>
		/// Enabled attribute of Add,Edit and Delete buttons
		/// </summary>
		public bool EnabledOfCrudButtons
		{
			get => (bool) GetValue(EnabledOfCrudButtonsProperty);
			set => SetValue(EnabledOfCrudButtonsProperty, value);
		}
		/// <summary>
		/// ReadOnly attribute of controls that are used to edit fields (like textbox,combo,...) of models in Grid/Textbox forms
		/// </summary>
		public bool ReadOnlyOfEditControls
		{
			get => (bool)GetValue(ReadOnlyOfEditControlsProperty);
			set => SetValue(ReadOnlyOfEditControlsProperty, value);
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
					case FormCurrentState.AddMaster:
						LblState.Foreground = (Brush)FindResource("FormStateColorAdd");
						LblState.Content = ResourceAccessor.Labels.GetString("AddMaster");
						break;
					case FormCurrentState.AddDetails:
						LblState.Foreground = (Brush)FindResource("FormStateColorAdd");
						LblState.Content = ResourceAccessor.Labels.GetString("AddDetails");
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
				ReadOnlyOfEditControls = value != FormCurrentState.Edit && value != FormCurrentState.Add && value != FormCurrentState.AddMaster && value != FormCurrentState.AddDetails;
//				ReadOnlyAddFields = value != FormCurrentState.Add && value != FormCurrentState.AddMaster &&
//				                    value != FormCurrentState.AddDetails;
				EnabledOfCrudButtons = value == FormCurrentState.Select;
				EnabledOfSaveCancelButtons = value == FormCurrentState.Edit || value == FormCurrentState.Add || value == FormCurrentState.AddMaster || value == FormCurrentState.AddDetails;
				OnPropertyChanged(nameof(ReadOnlyOfEditControls));
//				OnPropertyChanged(nameof(ReadOnlyAddFields));
				OnPropertyChanged(nameof(EnabledOfCrudButtons));
				OnPropertyChanged(nameof(EnabledOfSaveCancelButtons));

				_currentState = value;
			}
		}
		public event PropertyChangedEventHandler PropertyChanged;

		[NotifyPropertyChangedInvocator]
		protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}
	}
}