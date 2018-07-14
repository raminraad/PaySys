using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using Bogus;
using PaySys.ModelAndBindLib.Engine;
using PaySys.ModelAndBindLib.Model;
using PaySys.UI.ExtensionMethods;

namespace PaySys.UI.UC
{
	/// <summary>Interaction logic for UcShowContractMaster.xaml</summary>
	public partial class UcShowContractMaster : UserControl
	{
		private readonly ObservableCollection<Job> _jobsAll;
		private readonly ObservableCollection<MainGroup> _mainGroupsAll;
		private PaySysContext _context = new PaySysContext();

		public UcShowContractMaster()
		{
			InitializeComponent();
			//			CmbMainGroup.DataContext = _mainGroupsAll = new ObservableCollection<MainGroup>(_context.MainGroups);
			//			CmbJob.DataContext = _jobsAll = new ObservableCollection<Job>(_context.Jobs);
		}

		public static readonly DependencyProperty ReadOnlyFieldsProperty = DependencyProperty.Register("ReadOnlyOfEditControls", typeof(bool), typeof(UcShowContractMaster), new PropertyMetadata(default(bool)));
		public static readonly DependencyProperty ReadOnlyAddFieldsProperty = DependencyProperty.Register("ReadOnlyAddFields", typeof(bool), typeof(UcShowContractMaster), new PropertyMetadata(default(bool)));

		/// <summary>
		/// ReadOnly property of fields that are editable only in Add state
		/// </summary>
		public bool ReadOnlyAddFields
		{
			get { return (bool)GetValue(ReadOnlyAddFieldsProperty); }
			set { SetValue(ReadOnlyAddFieldsProperty, value); }
		}

		/// <summary>
		/// ReadOnly property of normal fields
		/// </summary>
		public bool ReadOnlyFields

		{
			get { return (bool)GetValue(ReadOnlyFieldsProperty); }
			set { SetValue(ReadOnlyFieldsProperty, value); }
		}
		public ObservableCollection<MainGroup> MainGroups
		{
			set => CmbMainGroup.ItemsSource = value;
		}
		public ObservableCollection<SubGroup> SubGroups
		{
			set => CmbSubGroup.ItemsSource = value;
		}
		public ObservableCollection<Job> Jobs
		{
			set => CmbJob.ItemsSource = value;
		}
		public ContractMaster CurrentContractMaster
		{
			get => (ContractMaster)DataContext;
			set => DataContext = value;
		}

		private void CmbMainGroup_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			if (e.AddedItems.Count > 0)
				CmbSubGroup.Items.Filter = s => (s as SubGroup)?.MainGroup == (MainGroup)CmbMainGroup.SelectedItem;
		}
		public void CommitContext()
		{
			//			foreach (var cnt in GridFields.Children.OfType<Control>())
			foreach (var cnt in this.FindVisualChildren<Control>())
			{
				cnt.GetBindingExpression(TextBox.TextProperty)?.UpdateSource();
				cnt.GetBindingExpression(Selector.SelectedItemProperty)?.UpdateSource();
			}
			GetBindingExpression(DataContextProperty)?.UpdateSource();

		}

	}
}