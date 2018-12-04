using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Media.Imaging;
using PaySys.Model.Enums;
using PaySys.UI.EventArgs;
using PaySys.UI.Properties;

namespace PaySys.UI.UserControls
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
		public bool EnabledOfSaveDiscardButtons { set; get; }

		/// <summary>
		/// Enabled attribute of Add,Edit and Delete buttons
		/// </summary>
		public bool EnabledOfCrudButtons { set; get; }

        /// <summary>
        /// Enabled attribute of items that are used to change selected item(s) of user control like: LookupBox, DataGrid, ...
        /// </summary>
	    public bool EnabledOfSelectionElements { set; get; }

        /// <summary>
        /// ReadOnly attribute of controls that are used to edit fields (like textbox,combo,...) of models in Grid/Textbox forms
        /// </summary>
        public bool ReadOnlyOfEditFields { set; get; }
	    public bool EnabledOfEditFields => !ReadOnlyOfEditFields;

		/// <summary>
		/// ReadOnly attribute of controls that should be enabled just in Add mode
		/// </summary>
		public bool ReadOnlyOfAddFields { set; get; }

	    public bool EnabledOfAddFields => !ReadOnlyOfAddFields;

		public FormCurrentState CurrentState
		{
			get => _currentState;
			set
			{
				switch(value)
                {
					case FormCurrentState.Select:
					    Icon.Source = (BitmapImage)FindResource("icon_form_state_select");
                        break;
					case FormCurrentState.Edit:
					    Icon.Source = (BitmapImage)FindResource("icon_form_state_edit");
						break;
					case FormCurrentState.Add:
					    Icon.Source = (BitmapImage)FindResource("icon_form_state_add");
						break;
					case FormCurrentState.AddMaster:
					    Icon.Source = (BitmapImage)FindResource("icon_form_state_add");
						break;
					case FormCurrentState.AddDetails:
					    Icon.Source = (BitmapImage)FindResource("icon_form_state_add");
						break;
					case FormCurrentState.Delete:
					    Icon.Source = (BitmapImage)FindResource("icon_form_state_delete");
						break;
					case FormCurrentState.Unknown:
					    Icon.Source = (BitmapImage)FindResource("icon_form_state_unknown");
						break;
					default:
						throw new ArgumentOutOfRangeException(nameof(value), value, null);
				}

				EnabledOfSaveDiscardButtons = value == FormCurrentState.Edit || value == FormCurrentState.Add || value == FormCurrentState.AddMaster || value == FormCurrentState.AddDetails;
				ReadOnlyOfAddFields = !(value == FormCurrentState.Add || value == FormCurrentState.AddMaster || value == FormCurrentState.AddDetails);
				EnabledOfCrudButtons = ReadOnlyOfEditFields = !EnabledOfSaveDiscardButtons;
			    EnabledOfSelectionElements = value == FormCurrentState.Select;
				OnPropertyChanged(nameof(EnabledOfSaveDiscardButtons));
				OnPropertyChanged(nameof(EnabledOfCrudButtons));
				OnPropertyChanged(nameof(EnabledOfSelectionElements));
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