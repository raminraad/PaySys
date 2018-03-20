using System.Windows.Input;

namespace PaySys.UI
{
	public class CommandCancel
	{
		static CommandCancel()
		{
			// Initialize the command.
			InputGestureCollection inputs = new InputGestureCollection
			{
				new KeyGesture(Key.Escape, ModifierKeys.None, "Enter"),
			};
			Cancel = new RoutedUICommand("Cancel", "Cancel", typeof(CommandCancel), inputs);
		}

		public static RoutedUICommand Cancel { get; }
	}
}