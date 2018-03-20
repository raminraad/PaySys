using System.Windows.Input;

namespace PaySys.UI
{
	public class CommandCloseTab
	{
		static CommandCloseTab()
		{
			// Initialize the command.
			InputGestureCollection inputs = new InputGestureCollection
			{
				new KeyGesture(Key.W, ModifierKeys.Control, "Ctrl+W"),
				new KeyGesture(Key.F4, ModifierKeys.Control, "Ctrl+F4"),
			};
			CloseTab = new RoutedUICommand("CloseTab", "CloseTab", typeof(CommandCloseTab), inputs);
		}

		public static RoutedUICommand CloseTab { get; }
	}
}