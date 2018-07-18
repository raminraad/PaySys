using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using PaySys.ModelAndBindLib.Model;

namespace PaySys.UI.Commands
{
	public static class PaySysCommandLibrary
	{
		#region Add/Remove item to/from list

		private static RoutedUICommand _addItem = new RoutedUICommand("Add item to list", "AddItem", typeof(PaySysCommandLibrary));

		private static RoutedUICommand _removeItem = new RoutedUICommand("Remove item from list", "RemoveItem", typeof(PaySysCommandLibrary),new InputGestureCollection{new KeyGesture(Key.Delete,ModifierKeys.Control),new KeyGesture(Key.Delete)});

		public static RoutedUICommand AddItem => _addItem;

		public static RoutedUICommand RemoveItem => _removeItem;

		#endregion
	}
}
