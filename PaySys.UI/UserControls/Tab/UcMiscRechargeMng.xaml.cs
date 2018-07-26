using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Drawing.Imaging;
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
using PaySys.Globalization;
using PaySys.ModelAndBindLib.Engine;
using PaySys.ModelAndBindLib.Model;

namespace PaySys.UI.UC
{
	/// <summary>
	/// Interaction logic for UcMiscRechargeMng.xaml
	/// </summary>
	public partial class UcMiscRechargeMng : UserControl
	{
		private PaySysContext Context
		{
			get;
		} = new PaySysContext();

		public UcMiscRechargeMng()
		{
			InitializeComponent();
		}

		private void CommandBinding_OnExecuted(object sender, ExecutedRoutedEventArgs e)
		{
			foreach(var rec in SmpUcSelectGroupAndSubGroup.SelectedSubGroup.TempMiscRechargesOfEmployees)
			{
				if(rec.MiscRechargeId == 0 && Math.Abs(rec.Value) > 0)
					Context.MiscRecharges.Add(rec);
				if(rec.MiscRechargeId != 0 && rec.Value==0)
					Context.MiscRecharges.Remove(rec);
			}
			Context.SaveChanges();
			MessageBox.Show(ResourceAccessor.Messages.GetString("SaveSuccessful"));
		}

		private void UcMiscRechargeMng_OnLoaded(object sender, RoutedEventArgs e)
		{
			SmpUcSelectGroupAndSubGroup.DataContext = Context.MainGroups.ToList();
		}

		private void SmpUcSelectGroupAndSubGroup_OnSelectedSubGroupChanged(object sender, RoutedEventArgs e)
		{
			var sg = SmpUcSelectGroupAndSubGroup.SelectedSubGroup;
			var sgCnts = sg.ContractMasters.Where(master => master.IsCurrent);
			var sgEmps = sgCnts.Select(c => c.Employee);
			var sgRecs = sgEmps.SelectMany(emp => emp.MiscRecharges);

			var newQuery = sgEmps.GroupJoin(sgRecs, emp => emp, rec => rec.Employee, (emp, recEnum) => new
			{
				Employee = emp,
				MiscRecharges = 
				from m in sg.Miscs.Where(misc => misc.Year == 97)
				join r in recEnum.Where(r => r.Year == 97 && r.Month == 007) on m equals r.Misc into empRecs
				from subRec in empRecs.DefaultIfEmpty(new MiscRecharge{Misc = m,Value = 0,Employee = emp,Month = 007,Year = m.Year,MiscRechargeId = 0})
				select subRec
			});
			
			sg.TempMiscRechargesOfEmployees = newQuery.SelectMany(arg => arg.MiscRecharges).ToList();
		}
	}
}