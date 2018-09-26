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
using PaySys.ModelAndBindLib.Entities;
using PaySys.UI.Annotations;

namespace PaySys.UI.UC
{
	public partial class UcFormStateLabel : INotifyPropertyChanged
	{
		private FormCurrentState _currentState = FormCurrentState.Unknown;
		public Type FormType { set; get; }
		public UcFormStateLabel()
		{
			InitializeComponent();

//			FrameworkPropertyMetadata metadata = new FrameworkPropertyMetadata(
//				new Thickness(), FrameworkPropertyMetadataOptions.AffectsMeasure);
		}

		//#RoutedEvent
		public static readonly RoutedEvent FormCurrentStateChangedEvent = EventManager.RegisterRoutedEvent("FormCurrentStateChanged", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(UcFormStateLabel));

		public event RoutedEventHandler FormCurrentStateChanged
		{
			add => AddHandler(FormCurrentStateChangedEvent, value);
		    remove => RemoveHandler(FormCurrentStateChangedEvent, value);
		}

		public static readonly RoutedEvent PreviewFormCurrentStateChangedEvent = EventManager.RegisterRoutedEvent("PreviewFormCurrentStateChanged", RoutingStrategy.Tunnel, typeof(RoutedEventHandler), typeof(UcFormStateLabel));

		public event RoutedEventHandler PreviewFormCurrentStateChanged
		{
			add => AddHandler(FormCurrentStateChangedEvent, value);
		    remove => RemoveHandler(FormCurrentStateChangedEvent, value);
		}


		/// <summary>
		/// Enabled attribute of Save and DiscardChanges buttons
		/// </summary>
		public bool EnabledOfSaveCancelButtons { set; get; }

		/// <summary>
		/// Enabled attribute of Add,Edit and Delete buttons
		/// </summary>
		public bool EnabledOfCrudButtons { set; get; }

		/// <summary>
		/// ReadOnly attribute of controls that are used to edit fields (like textbox,combo,...) of models in Grid/Textbox forms
		/// </summary>
		public bool ReadOnlyOfEditFields { set; get; }

		/// <summary>
		/// ReadOnly attribute of controls that should be enabled just in Add mode
		/// </summary>
		public bool ReadOnlyOfAddFields { set; get; }

		public FormCurrentState CurrentState
		{
			get => _currentState;
			set
			{
				switch(value)
				{
					case FormCurrentState.Select:
						LblState.Foreground = (Brush) FindResource("FormStateColorSelect");
						LblState.Content = ResourceAccessor.Labels.GetString("Select");
						break;
					case FormCurrentState.Edit:
						LblState.Foreground = (Brush) FindResource("FormStateColorEdit");
						LblState.Content = ResourceAccessor.Labels.GetString("Edit");
						break;
					case FormCurrentState.Add:
						LblState.Foreground = (Brush) FindResource("FormStateColorAdd");
						LblState.Content = ResourceAccessor.Labels.GetString("Add");
						break;
					case FormCurrentState.AddMaster:
						LblState.Foreground = (Brush) FindResource("FormStateColorAdd");
						LblState.Content = ResourceAccessor.Labels.GetString("AddMaster");
						break;
					case FormCurrentState.AddDetails:
						LblState.Foreground = (Brush) FindResource("FormStateColorAdd");
						LblState.Content = ResourceAccessor.Labels.GetString("AddDetails");
						break;
					case FormCurrentState.Delete:
						LblState.Foreground = (Brush) FindResource("FormStateColorDelete");
						LblState.Content = ResourceAccessor.Labels.GetString("Delete");
						break;
					case FormCurrentState.Unknown:
						LblState.Foreground = (Brush) FindResource("FormStateColorUnknown");
						LblState.Content = ResourceAccessor.Labels.GetString("Unknown");
						break;
					default:
						throw new ArgumentOutOfRangeException(nameof(value), value, null);
				}

				EnabledOfSaveCancelButtons = value == FormCurrentState.Edit || value == FormCurrentState.Add || value == FormCurrentState.AddMaster || value == FormCurrentState.AddDetails;
				ReadOnlyOfAddFields = !(value == FormCurrentState.Add || value == FormCurrentState.AddMaster || value == FormCurrentState.AddDetails);
				EnabledOfCrudButtons = ReadOnlyOfEditFields = !EnabledOfSaveCancelButtons;
				OnPropertyChanged(nameof(EnabledOfSaveCancelButtons));
				OnPropertyChanged(nameof(EnabledOfCrudButtons));
				OnPropertyChanged(nameof(ReadOnlyOfEditFields));
				OnPropertyChanged(nameof(ReadOnlyOfAddFields));
				_currentState = value;

				var previewStateChangedArgs = new FormCurrentStateChangedEventArgs(PreviewFormCurrentStateChangedEvent,this){FormCurrentState = value,FormType = FormType};
				RaiseEvent(previewStateChangedArgs);
				if (!previewStateChangedArgs.Handled)
				{
					var stateChangedArgs = new FormCurrentStateChangedEventArgs(FormCurrentStateChangedEvent,this){FormCurrentState = value,FormType = FormType};
					RaiseEvent(stateChangedArgs);
				}
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