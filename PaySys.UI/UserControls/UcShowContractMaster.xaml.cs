using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using Bogus;
using PaySys.ModelAndBindLib.Model;
using PaySys.UI.ExtensionMethods;

namespace PaySys.UI.UC
{
	public partial class UcShowContractMaster : UserControl
	{
		public UcShowContractMaster()
		{
			InitializeComponent();
		}

		public static readonly DependencyProperty ReadOnlyOfAddFieldsProperty = DependencyProperty.Register("ReadOnlyOfAddFields", typeof(bool), typeof(UcShowContractMaster), new PropertyMetadata(default(bool)));

		public static readonly DependencyProperty ReadOnlyOfEditFieldsProperty = DependencyProperty.Register( "ReadOnlyOfEditFields", typeof(bool), typeof(UcShowContractMaster), new PropertyMetadata( default(bool) ) );

		public bool ReadOnlyOfAddFields
		{
			get { return (bool) GetValue( ReadOnlyOfAddFieldsProperty ); }
			set { SetValue( ReadOnlyOfAddFieldsProperty, value ); }
		}

		public bool ReadOnlyOfEditFields
		{
			get { return (bool) GetValue( ReadOnlyOfEditFieldsProperty ); }
			set { SetValue( ReadOnlyOfEditFieldsProperty, value ); }
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
			get => (ObservableCollection<Job>) CmbJob.ItemsSource;
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
			foreach (var cnt in this.FindVisualChildren<Control>())
			{
				cnt.GetBindingExpression(TextBox.TextProperty)?.UpdateSource();
				cnt.GetBindingExpression(Selector.SelectedItemProperty)?.UpdateSource();
			}
			GetBindingExpression(DataContextProperty)?.UpdateSource();
		}

	}
}