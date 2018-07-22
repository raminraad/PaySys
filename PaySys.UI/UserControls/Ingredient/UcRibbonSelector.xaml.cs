using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PaySys.UI.UC
{
	/// <summary>
	/// Interaction logic for UcRibbonSelector.xaml
	/// </summary>
	public partial class UcRibbonSelector : UserControl, System.Windows.Markup.IComponentConnector
	{
		private string _displayMember;

		public UcRibbonSelector()
		{
			InitializeComponent();
		}

		public TextBlock TitleTextBlock=>TextBlockTitle;

		public string TitleDisplayMember { set; get; }

		public static readonly DependencyProperty SelectedItemProperty = DependencyProperty.Register("SelectedItem", typeof(object), typeof(UcRibbonSelector), new PropertyMetadata(default(object)));

		public object SelectedItem
		{
			get
			{
				return (object) GetValue(SelectedItemProperty);
			}
			set
			{
				SetValue(SelectedItemProperty, value);
			}
		}


		public static readonly RoutedEvent SelectedItemChangedEvent = EventManager.RegisterRoutedEvent(
		                                                                               "SelectedItemChanged", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(UcRibbonSelector));

		// Provide CLR accessors for the event
		public event RoutedEventHandler SelectedItemChanged
		{
			add { AddHandler(SelectedItemChangedEvent, value); }
			remove { RemoveHandler(SelectedItemChangedEvent, value); }
		}


//		public SelectionChangedEventHandler SelectedItemChanged { set; get; }

		private void Navigate_OnExecuted(object sender, ExecutedRoutedEventArgs e)
		{
			switch ((int)e.Parameter)
			{
				case 2:
					CollectionViewSource.GetDefaultView(ListViewHolder.ItemsSource).MoveCurrentToLast();
					break;
				case 1:
					CollectionViewSource.GetDefaultView(ListViewHolder.ItemsSource).MoveCurrentToNext();
					break;
				case -1:
					CollectionViewSource.GetDefaultView(ListViewHolder.ItemsSource).MoveCurrentToPrevious();
					break;
				case -2:
					CollectionViewSource.GetDefaultView(ListViewHolder.ItemsSource).MoveCurrentToFirst();
					break;
			}
		}

		private void Navigate_OnCanExecute(object sender, CanExecuteRoutedEventArgs e)
		{
			if(e.Parameter == null)
				return;
			switch ((int)e.Parameter)
			{
				case 2:
				case 1:
					e.CanExecute= this.ListViewHolder?.SelectedIndex < (DataContext as IEnumerable<object>)?.Count() - 1;
					break;
				case -1:
				case -2:
					e.CanExecute = this.ListViewHolder?.SelectedIndex > 0;
					break;
			}
		}

		private void UcRibbonSelector_OnDataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
		{
			if(e.NewValue!=null)
			{
				var itemsCount = (e.NewValue as IEnumerable<object>).Count();
				ProgressBarOfItems.Maximum = itemsCount>0? itemsCount - 1:0;
			}

				Binding binding = new Binding(TitleDisplayMember);
				binding.Source = DataContext;
				TextBlockTitle.SetBinding(TextBlock.TextProperty, binding);
		}

		private void ListViewHolder_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			RoutedEventArgs newEventArgs = new RoutedEventArgs(SelectedItemChangedEvent,sender);
			RaiseEvent(newEventArgs);
		}
	}
}