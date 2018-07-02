using System.Data.Entity;
using System.Data.Entity.Migrations;
using PaySys.ModelAndBindLib.Model;

namespace PaySys.ModelAndBindLib.Engine
{
	public class PaySysContext : DbContext
	{
		public PaySysContext() : base("PaySys")
		{
			Database.SetInitializer(new PaySysDbInitializer());
			//			Database.Initialize(true);
		}

		public DbSet<MainGroup> MainGroups { set; get; }
		public DbSet<SubGroup> SubGroups { set; get; }
		public DbSet<Misc> Miscs { set; get; }
		public DbSet<ParameterInvolvedMisc> ParameterInvolvedMiscs { set; get; }
		public DbSet<ParameterValue> ParameterValues { set; get; }
		public DbSet<ParameterInvolvedContractField> ParameterInvolvedContractFields { set; get; }
		public DbSet<ExpenseArticle> ExpenseArticles { set; get; }
		public DbSet<EmployeeMiscRemain> EmployeeMiscRemains { set; get; }
		public DbSet<ContractField> ContractFields { set; get; }
		public DbSet<MissionFormula> MissionFormulas { set; get; }
		public DbSet<TaxTable> TaxTables { set; get; }
		public DbSet<TaxRow> TaxRows { set; get; }
		public DbSet<PayslipEmployeeMisc> PayslipEmployeeMiscs { set; get; }
		public DbSet<ContractMaster> ContractMasters { set; get; }
		public DbSet<ContractDetail> ContractDetails { set; get; }
		public DbSet<Employee> Employees { set; get; }
		public DbSet<HandselFormula> HandselFormulas { set; get; }
		public DbSet<ContractDifference> ContractDifferences { set; get; }
		public DbSet<Job> Jobs { set; get; }
		public DbSet<City> Cities { set; get; }
		public DbSet<Mission> Missions { set; get; }
		public DbSet<PayslipContractDetail> PayslipContractDetails { set; get; }
		public DbSet<ExpenseArticleOfContractFieldForSubGroup> ExpenseArticleOfContractFieldForSubGroups { set; get; }
		public DbSet<ExpenseArticleOfMiscForSubGroup> ExpenseArticleOfMiscForSubGroups { set; get; }
		public DbSet<ExpenseArticleOfOverTimeForSubGroup> ExpenseArticleOfOverTimeForSubGroups { set; get; }
		public DbSet<PayslipEmployeeOvertime> PayslipEmployeeOvertimes { set; get; }
	}
}
