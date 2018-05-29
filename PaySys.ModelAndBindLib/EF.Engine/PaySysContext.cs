using System.Data.Entity;
using PaySys.ModelAndBindLib.Model;

namespace PaySys.ModelAndBindLib.Engine
{
	public class PaySysContext : DbContext
	{
		public PaySysContext() : base("PaySys")
		{
			Database.SetInitializer(new PaySysDbInitializer());
			Database.Initialize(true);
		}

		public DbSet<MainGroup> MainGroups { set; get; }
		public DbSet<SubGroup> SubGroups { set; get; }
		public DbSet<MiscTitle> MiscTitles { set; get; }
		public DbSet<GroupMisc> GroupMiscs { set; get; }
		public DbSet<SpecMisc> SpecMiscs { set; get; }
		public DbSet<GroupSpecTitle> GroupSpecTitles { set; get; }
		public DbSet<GroupSpecValue> GroupSpecValues { set; get; }
		public DbSet<SpecContractField> SpecContractFields { set; get; }
		public DbSet<ExpenseArticle> ExpenseArticles { set; get; }
		public DbSet<EmployeeMiscRemain> EmployeeMiscRemains { set; get; }
		public DbSet<MonthlyVariableTitle> MonthlyVariableTitles { set; get; }
		public DbSet<ContractFieldTitle> ContractFieldTitles { set; get; }
		public DbSet<GroupContractFieldTitle> GroupContractFieldTitles { set; get; }
		public DbSet<MissionFormula> MissionFormulas { set; get; }
		public DbSet<TaxTable> TaxTables { set; get; }
		public DbSet<TaxRow> TaxRows { set; get; }
		public DbSet<GroupMonthlyVariable> GroupMonthlyVariables { set; get; }
		public DbSet<EmployeeMisc> EmployeeMiscs { set; get; }
		public DbSet<EmployeeMonthlyVariable> EmployeeMonthlyVariables { set; get; }
		public DbSet<ContractMaster> ContractMasters { set; get; }
		public DbSet<ContractDetail> ContractDetails { set; get; }
		public DbSet<Employee> Employees { set; get; }
		public DbSet<HandselFormula> HandselFormulas { set; get; }
		public DbSet<ContractDifference> ContractDifferences { set; get; }
		public DbSet<Job> Jobs { set; get; }
		public DbSet<City> Cities { set; get; }
		public DbSet<Mission> Missions { set; get; }
	}
}
