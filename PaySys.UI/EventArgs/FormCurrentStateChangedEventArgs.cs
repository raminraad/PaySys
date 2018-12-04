using System;
using System.Windows;
using PaySys.Model.Enums;

namespace PaySys.UI.EventArgs
{
	public class FormCurrentStateChangedEventArgs:RoutedEventArgs
	{
		public FormCurrentState FormCurrentState { get; set; }
		public Type FormType { set; get; }

		public FormCurrentStateChangedEventArgs():base()
		{
			
		}
		public FormCurrentStateChangedEventArgs(RoutedEvent routedEvent):base(routedEvent)
		{
			
		}

		public FormCurrentStateChangedEventArgs(RoutedEvent routedEvent, object source):base(routedEvent,source)
		{
			
		}
	}
}
