using System.Windows.Input;

namespace PaySys.UI
{
	public class CommandSave
	{
		static CommandSave()
		{
			// Initialize the command.
			InputGestureCollection inputs = new InputGestureCollection
			{
				new KeyGesture(Key.S, ModifierKeys.Control, "Ctrl+S"),
				new KeyGesture(Key.Enter, ModifierKeys.None, "Enter"),
			};
			Save = new RoutedUICommand("Save", "Save", typeof(CommandSave), inputs);
		}

		public static RoutedUICommand Save { get; }
	}
}