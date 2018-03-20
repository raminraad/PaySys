using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PaySys.UI
{
	public class CommandEdit
	{
		static CommandEdit()
		{
			// Initialize the command.
			InputGestureCollection inputs = new InputGestureCollection
			{
				new KeyGesture(Key.E, ModifierKeys.Control, "Ctrl+E"),
				new KeyGesture(Key.F2, ModifierKeys.None, "F2")
			};
			Edit = new RoutedUICommand("New", "New", typeof(CommandEdit), inputs);
		}

		public static RoutedUICommand Edit { get; }
	}
}