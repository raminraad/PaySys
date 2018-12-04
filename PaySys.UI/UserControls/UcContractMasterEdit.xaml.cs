using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using PaySys.Model.Entities;
using PaySys.Model.ExtensionMethods;

namespace PaySys.UI.UserControls
{
	public partial class UcContractMasterEdit : UserControl
	{
		public UcContractMasterEdit()
		{
			InitializeComponent();
		}

		public static readonly DependencyProperty ReadOnlyOfAddFieldsProperty = DependencyProperty.Register("ReadOnlyOfAddFields", typeof(bool), typeof(UcContractMasterEdit), new PropertyMetadata(default(bool)));

		public static readonly DependencyProperty ReadOnlyOfEditFieldsProperty = DependencyProperty.Register( "ReadOnlyOfEditFields", typeof(bool), typeof(UcContractMasterEdit), new PropertyMetadata( default(bool) ) );

		public bool ReadOnlyOfAddFields
		{
			get => (bool) GetValue( ReadOnlyOfAddFieldsProperty );
		    set => SetValue( ReadOnlyOfAddFieldsProperty, value );
		}

		public bool ReadOnlyOfEditFields
		{
			get => (bool) GetValue( ReadOnlyOfEditFieldsProperty );
		    set => SetValue( ReadOnlyOfEditFieldsProperty, value );
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

	    public void SelectFirstItem()
	    {
	        TxtContractNo.Focus();
            TxtContractNo.SelectAll();
        }

	}
}