using System.Windows.Input;

namespace PaySys.UI
{
	public class CommandAdd
	{
		static CommandAdd()
		{
			// Initialize the command.
			InputGestureCollection inputs = new InputGestureCollection
			{
				new KeyGesture(Key.N, ModifierKeys.Control, "Ctrl+N"),
			};
			Add = new RoutedUICommand("Add", "Add", typeof(CommandAdd), inputs);
		}

		public static RoutedUICommand Add { get; }
	}
}