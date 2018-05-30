using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using PaySys.ModelAndBindLib.Engine;
using PaySys.ModelAndBindLib.Model;

namespace PaySys.UI.UC
{
	/// <summary>
	/// Interaction logic for UcShowContractMaster.xaml
	/// </summary>
	public partial class UcShowContractMaster : UserControl
	{
		private readonly ObservableCollection<MainGroup> _mainGroupsAll;
		private readonly ObservableCollection<Job> _jobsAll;

		private PaySysContext _context = new PaySysContext();
		public UcShowContractMaster()
		{
			InitializeComponent();
			_mainGroupsAll = new ObservableCollection<MainGroup>(_context.MainGroups);
			_jobsAll = new ObservableCollection<Job>(_context.Jobs);
			CmbMainGroup.DataContext = _mainGroupsAll;
			CmbJob.DataContext = _jobsAll;
		}

		private void CmbMainGroup_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			if (e.AddedItems.Count > 0)
				CmbSubGroup.DataContext = ((MainGroup)CmbMainGroup.SelectedItem).SubGroups;
		}

		private void UcShowContractMaster_OnDataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
		{
			var newContractMaster = (ContractMaster)e.NewValue;
			var selectedMainGroup = _mainGroupsAll.First(x => x.Equals(newContractMaster.SubGroup.MainGroup));
			CmbMainGroup.SelectedItem = selectedMainGroup;
			CmbSubGroup.SelectedItem = selectedMainGroup.SubGroups.FirstOrDefault(x => x.Equals(newContractMaster.SubGroup));
		}
	}
}
