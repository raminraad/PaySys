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

		public static readonly DependencyProperty ReadOnlyFieldsProperty = DependencyProperty.Register("ReadOnlyFields", typeof(bool), typeof(UcShowContractMaster), new PropertyMetadata(default(bool)));
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
		private void UcShowContractMaster_OnDataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
		{
			//			if (e.NewValue == null)
			//				return;
			//			var newContractMaster = (ContractMaster)e.NewValue;
			//			var selectedMainGroup = _mainGroupsAll.First(x => x.Equals(newContractMaster.SubGroup.MainGroup));
			//			CmbMainGroup.SelectedItem = selectedMainGroup;
			//			CmbSubGroup.SelectedItem = selectedMainGroup.SubGroups.FirstOrDefault(x => x.Equals(newContractMaster.SubGroup));
		}

		private void TmpBtnFill_OnClick(object sender, RoutedEventArgs e)
		{
			var f = new Faker();
			TxtAccountNoEmp.Text = $"{f.Random.Number(9999):D4}-{f.Random.Number(9999):D4}-{f.Random.Number(9999):D4}-{f.Random.Number(9999):D4}";
			TxtAccountNoGov.Text =
				$"{f.Random.Number(9999):D4}-{f.Random.Number(9999):D4}-{f.Random.Number(9999):D4}-{f.Random.Number(9999):D4}";
			TxtContractNo.Text = $"{f.Random.Number(99999):D5}";
			TxtDateEmployment.Text = $"13{f.Random.Number(20) + 40:d2}{f.Random.Number(11) + 1:d2}{f.Random.Number(29) + 1:d2}";
			TxtDateExecution.Text = $"13{f.Random.Number(20) + 40:d2}{f.Random.Number(11) + 1:d2}{f.Random.Number(29) + 1:d2}";
			TxtDateExport.Text = $"13{f.Random.Number(20) + 40:d2}{f.Random.Number(11) + 1:d2}{f.Random.Number(29) + 1:d2}";
			TxtHardshipFactor.Text = $"{f.Random.Number(99):D2}";
			TxtInsuranceNo.Text = $"{f.Random.Number(999999):D6}";
			CmbMainGroup.SelectedIndex = f.PickRandom(CmbMainGroup.Items.Count);
			CmbSubGroup.SelectedIndex = f.PickRandom(CmbSubGroup.Items.Count);
			CmbEducationStand.SelectedIndex = f.PickRandom(CmbEducationStand.Items.Count);
			CmbEmploymentType.SelectedIndex = f.PickRandom(CmbEmploymentType.Items.Count);
			CmbJob.SelectedIndex = f.PickRandom(CmbJob.Items.Count); ;
			CmbMaritalStatus.SelectedIndex = f.PickRandom(CmbMaritalStatus.Items.Count);
			CmbSacrificeStand.SelectedIndex = f.PickRandom(CmbSacrificeStand.Items.Count);

		}
	}
}