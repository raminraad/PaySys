using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Markup;
using PaySys.ModelAndBindLib.Model;
using Binding = System.Windows.Data.Binding;
using Label = System.Windows.Controls.Label;
using MessageBox = System.Windows.Forms.MessageBox;
using UserControl = System.Windows.Controls.UserControl;

namespace PaySys.UI.UC
{
	/// <summary>Interaction logic for UcRibbonSelector.xaml</summary>
	public partial class UcRibbonSelector : UserControl, IComponentConnector
	{
		public static readonly DependencyProperty SelectedItemProperty = DependencyProperty.Register( "SelectedItem", typeof(object), typeof(UcRibbonSelector), new PropertyMetadata( default(object) ) );
		public static readonly RoutedEvent SelectedItemChangedEvent = EventManager.RegisterRoutedEvent( "SelectedSubGroupChanged", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(UcRibbonSelector) );

		public static readonly RoutedEvent ListDataContextChangedEvent = EventManager.RegisterRoutedEvent( "ListDataContextChanged", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(UcRibbonSelector) );
		private string _displayMember;

		public UcRibbonSelector()
		{
			InitializeComponent();
		}

		public Label TitleLabel => LabelTitle;

		public string SortDescription
		{
			set
			{
				var cvs = CollectionViewSource.GetDefaultView( ListViewHolder.DataContext );
				if( cvs == null )
					return;

				cvs.SortDescriptions.Clear();
				cvs.SortDescriptions.Add( new SortDescription( value, ListSortDirection.Ascending ) );
				cvs.MoveCurrentToFirst();

//				cvs.Refresh();
			}
		}

		public string TitleDisplayMember { set; get; }
		public object PreviousSelectedItem { get; private set; }

		public bool SelectPreviousSelectedItem()
		{
			if( PreviousSelectedItem == null || DataContext == null )
				return false;

			if( !( DataContext as IEnumerable<object> ).Contains( PreviousSelectedItem ) )
				return false;

			SelectedItem = PreviousSelectedItem;
			return true;
		}
		
		public object SelectedItem
		{
			get => GetValue( SelectedItemProperty );
			set => SetValue( SelectedItemProperty, value );
		}

		public event RoutedEventHandler SelectedItemChanged
		{
			add => AddHandler( SelectedItemChangedEvent, value );
			remove => RemoveHandler( SelectedItemChangedEvent, value );
		}

		public event RoutedEventHandler ListDataContextChanged
		{
			add => AddHandler( ListDataContextChangedEvent, value );
			remove => RemoveHandler( ListDataContextChangedEvent, value );
		}

		private void Navigate_OnExecuted( object sender, ExecutedRoutedEventArgs e )
		{
			switch( (NavigationType) e.Parameter )
			{
				case NavigationType.First:
					CollectionViewSource.GetDefaultView( ListViewHolder.ItemsSource ).MoveCurrentToFirst();
					break;
				case NavigationType.Previous:
					CollectionViewSource.GetDefaultView( ListViewHolder.ItemsSource ).MoveCurrentToPrevious();
					break;
				case NavigationType.Next:
					CollectionViewSource.GetDefaultView( ListViewHolder.ItemsSource ).MoveCurrentToNext();
					break;
				case NavigationType.Last:
					CollectionViewSource.GetDefaultView( ListViewHolder.ItemsSource ).MoveCurrentToLast();
					break;
			}
		}

		private void Navigate_OnCanExecute( object sender, CanExecuteRoutedEventArgs e )
		{
			if( e.Parameter == null )
				return;

			switch( (NavigationType) e.Parameter )
			{
				case NavigationType.First:
				case NavigationType.Previous:
					e.CanExecute = ListViewHolder?.SelectedIndex > 0;
					break;
				case NavigationType.Next:
				case NavigationType.Last:
					e.CanExecute = ListViewHolder?.SelectedIndex < ( DataContext as IEnumerable<object> )?.Count() - 1;
					break;
			}
		}

		public bool Move( NavigationType direction = NavigationType.None )
		{
			if( direction == NavigationType.None )
				return false;

			switch( direction )
			{
				case NavigationType.First:
					if( ButtonFirst.IsEnabled )
						ButtonFirst.Command.Execute( direction );
					return true;
				case NavigationType.Previous:
					if( ButtonPrevious.IsEnabled )
						ButtonPrevious.Command.Execute( direction );
					return true;
				case NavigationType.Next:
					if( ButtonNext.IsEnabled )
						ButtonNext.Command.Execute( direction );
					return true;
				case NavigationType.Last:
					if( ButtonLast.IsEnabled )
						ButtonLast.Command.Execute( direction );
					return true;
				default:
					throw new ArgumentOutOfRangeException( nameof(direction), direction, null );
			}
		}

		private void UcRibbonSelector_OnDataContextChanged( object sender, DependencyPropertyChangedEventArgs e )
		{
			if( e.NewValue != null )
			{
				var itemsCount = ( e.NewValue as IEnumerable<object> ).Count();
				ProgressBarOfItems.Maximum = itemsCount > 0 ? itemsCount - 1 : 0;
			}
			var binding = new Binding( TitleDisplayMember );
			binding.Source = DataContext;
			LabelTitle.SetBinding( System.Windows.Controls.Label.ContentProperty, binding );
		}

		private void ListViewHolder_OnSelectionChanged( object sender, SelectionChangedEventArgs e )
		{
			if( e.RemovedItems.Count > 0 )
				PreviousSelectedItem = e.RemovedItems[0];
			var newEventArgs = new RoutedEventArgs( SelectedItemChangedEvent, sender );
			RaiseEvent( newEventArgs );
		}

		private void ListViewHolder_OnDataContextChanged( object sender, DependencyPropertyChangedEventArgs e )
		{
			var newEventArgs = new RoutedEventArgs( ListDataContextChangedEvent, sender );
			RaiseEvent( newEventArgs );
		}
	}
}