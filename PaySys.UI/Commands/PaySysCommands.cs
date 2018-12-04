using System.Windows.Input;

namespace PaySys.UI.Commands
{
    public static class PaySysCommands
    {
        #region Add/Delete item to/from list
        public static RoutedUICommand AddItem { get; } =
            new RoutedUICommand("Add item to list", "AddItem", typeof(PaySysCommands),new InputGestureCollection(){new KeyGesture(Key.I,ModifierKeys.Control)});

        public static RoutedUICommand DeleteItem { get; } =
            new RoutedUICommand("Delete item from list", "DeleteItem", typeof(PaySysCommands), new InputGestureCollection { new KeyGesture(Key.Delete) });
        #endregion


        #region Lookup & Navigation
        public static RoutedUICommand NavigateNext { get; } = new RoutedUICommand("Navigate to next item",
            nameof(NavigateNext), typeof(PaySysCommands));

        public static RoutedUICommand NavigatePrevious { get; } = new RoutedUICommand("Navigate to previous item",
            nameof(NavigatePrevious), typeof(PaySysCommands));

        public static RoutedUICommand NavigateFirst { get; } = new RoutedUICommand("Navigate to first item",
            nameof(NavigateFirst), typeof(PaySysCommands));

        public static RoutedUICommand NavigateLast { get; } = new RoutedUICommand("Navigate to last item",
            nameof(NavigateLast), typeof(PaySysCommands));

        public static RoutedUICommand Lookup { get; } = new RoutedUICommand("Lookup for an item in list",
            nameof(Lookup), typeof(PaySysCommands),
            new InputGestureCollection {new KeyGesture(Key.F, ModifierKeys.Control), new KeyGesture(Key.F3)});
        #endregion


        #region CRUD
        public static RoutedUICommand Save { get; } = new RoutedUICommand("Save changes to DB-Context", nameof(Save),
            typeof(PaySysCommands), new InputGestureCollection {new KeyGesture(Key.S, ModifierKeys.Control)});

        public static RoutedUICommand DiscardChanges { get; } =
            new RoutedUICommand("Discard changes", nameof(DiscardChanges), typeof(PaySysCommands), new InputGestureCollection { new KeyGesture(Key.Escape) });

        public static RoutedUICommand Reload { get; } = new RoutedUICommand("Reload data from DB-Context",
            nameof(Reload), typeof(PaySysCommands), new InputGestureCollection {new KeyGesture(Key.F5)});

        public static RoutedUICommand Add { get; } = new RoutedUICommand("Add new item", nameof(Add),
            typeof(PaySysCommands), new InputGestureCollection {new KeyGesture(Key.N, ModifierKeys.Control)});

        public static RoutedUICommand Edit { get; } = new RoutedUICommand("Edit item", nameof(Edit),
            typeof(PaySysCommands),
            new InputGestureCollection {new KeyGesture(Key.E, ModifierKeys.Control), new KeyGesture(Key.F2)});

        public static RoutedUICommand Delete { get; } = new RoutedUICommand("Delete item", nameof(Delete),
            typeof(PaySysCommands), new InputGestureCollection {new KeyGesture(Key.Delete, ModifierKeys.Control)});
        #endregion


        #region File Operations

        public static RoutedUICommand FileBrowse { get; } = new RoutedUICommand("Browse for file", nameof(FileBrowse),
            typeof(PaySysCommands), null);
        public static RoutedUICommand FileClear { get; } = new RoutedUICommand("Clear selected file", nameof(FileClear),
            typeof(PaySysCommands), null);
        #endregion


        #region Application Operations
        public static RoutedUICommand CalculatePayslip { get; } = new RoutedUICommand("Perform calculations related to payslip", nameof(CalculatePayslip),
            typeof(PaySysCommands), new InputGestureCollection { new KeyGesture(Key.F12) });
        #endregion


    }
}