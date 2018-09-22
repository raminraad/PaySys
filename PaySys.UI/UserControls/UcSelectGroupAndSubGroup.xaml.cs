using System;
using System.Collections;
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
using PaySys.ModelAndBindLib.Model;
using PaySys.UI.Annotations;

namespace PaySys.UI.UC
{
	/// <summary>
	/// Interaction logic for UcSelectGroupAndSubGroup.xaml
	/// </summary>
	public partial class UcSelectGroupAndSubGroup : UserControl,INotifyPropertyChanged
	{
		public UcSelectGroupAndSubGroup()
		{
			InitializeComponent();
		}
		public int SelectedMainGroupId
		{
			get => (ComboBoxMainGroup.SelectedItem as MainGroup).MainGroupId;
			set
			{
				var mainGroups = ComboBoxMainGroup.ItemsSource as IEnumerable<MainGroup>;
				ComboBoxMainGroup.SelectedItem = mainGroups.FirstOrDefault( mg => mg.MainGroupId.Equals( value ) ) ?? mainGroups.FirstOrDefault();
			}
		}

		public int SelectedSubGroupId
		{
			get => (ComboBoxSubGroup.SelectedItem as SubGroup).SubGroupId;
			set
			{
				var SubGroups = ComboBoxSubGroup.ItemsSource as IEnumerable<SubGroup>;
				ComboBoxSubGroup.SelectedItem = SubGroups.FirstOrDefault( mg => mg.SubGroupId.Equals( value ) ) ?? SubGroups.FirstOrDefault();
			}
		}

		public MainGroup SelectedMainGroup
		{
			get => ComboBoxMainGroup.SelectedItem as MainGroup;
			set
			{
				if ((ComboBoxMainGroup.ItemsSource as IEnumerable<object>).Contains( value )) ComboBoxMainGroup.SelectedItem = value ;
			}
		}

		public SubGroup SelectedSubGroup
		{
			get => ComboBoxSubGroup.SelectedItem as SubGroup;
			set
			{
				if ((ComboBoxSubGroup.ItemsSource as IEnumerable<object>).Contains( value )) ComboBoxSubGroup.SelectedItem = value ;
			}
		}

		public event PropertyChangedEventHandler PropertyChanged;

		[NotifyPropertyChangedInvocator]
		protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}

		private void ComboBoxSubGroup_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			var newEventArgs = new RoutedEventArgs(SelectedSubGroupChangedEvent, sender);
			RaiseEvent(newEventArgs);
			OnPropertyChanged(nameof(SelectedSubGroup));
		}

		private void ComboBoxMainGroup_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			var newEventArgs = new RoutedEventArgs(SelectedMainGroupChangedEvent, sender);
			RaiseEvent(newEventArgs);
			OnPropertyChanged(nameof(SelectedMainGroup));
		}

		private void ComboBoxSubGroup_OnDataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
		{
			if ((e.NewValue as IList)?.Count > 0)
				ComboBoxSubGroup.SelectedItem = (e.NewValue as IEnumerable<object>).FirstOrDefault();
		}

		public static readonly RoutedEvent SelectedSubGroupChangedEvent = EventManager.RegisterRoutedEvent("SelectedSubGroupChanged", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(UcSelectGroupAndSubGroup));

		public event RoutedEventHandler SelectedSubGroupChanged
		{
			add => AddHandler(SelectedSubGroupChangedEvent, value);
			remove => RemoveHandler(SelectedSubGroupChangedEvent, value);
		}

		public static readonly RoutedEvent SelectedMainGroupChangedEvent = EventManager.RegisterRoutedEvent("SelectedMainGroupChanged", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(UcSelectGroupAndSubGroup));

		public event RoutedEventHandler SelectedMainGroupChanged
		{
			add => AddHandler(SelectedMainGroupChangedEvent, value);
			remove => RemoveHandler(SelectedMainGroupChangedEvent, value);
		}
	}
}
