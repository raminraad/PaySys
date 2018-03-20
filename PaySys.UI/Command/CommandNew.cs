using System.Windows.Input;

namespace PaySys.UI
{
	public class CommandNew
	{
		static CommandNew()
		{
			// Initialize the command.
			InputGestureCollection inputs = new InputGestureCollection
			{
				new KeyGesture(Key.N, ModifierKeys.Control, "Ctrl+N"),
			};
			New = new RoutedUICommand("New", "New", typeof(CommandNew), inputs);
		}

		public static RoutedUICommand New { get; }
	}
}