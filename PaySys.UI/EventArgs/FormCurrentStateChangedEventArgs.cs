using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using PaySys.ModelAndBindLib.Model;

namespace PaySys.UI.UC
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
