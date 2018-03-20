using System.Data.Entity;
using PaySys.EF;

namespace PaySys.EF
{
	public class PaySysContext : DbContext
	{
		public PaySysContext() : base("PaySysContext")
		{
			Database.SetInitializer(new PaySysDbInitializer());
		}

		public DbSet<MainGroup> MainGroups { set; get; }
		public DbSet<SubGroup> SubGroups { set; get; }
		public DbSet<SysMenu> SysMenus { set; get; }
		public DbSet<SysForm> SysForms { set; get; }
		public DbSet<AccessMenu> AccessMenus { set; get; }
		public DbSet<AccessForm> AccessForms { set; get; }
		public DbSet<Rule> Rules { set; get; }
		public DbSet<User> Users { set; get; }
		public DbSet<ExpArtAddDedBen> ExpArtAddDedBens { set; get; }
		public DbSet<ExpArtCntFieldTitle> ExpArtCntFieldTitles { set; get; }
		public DbSet<HandselAdvance> HandselAdvances { set; get; }
		public DbSet<HandselInclusion> HandselInclusions { set; get; }
		public DbSet<HandselFormula> HandselFormulas { set; get; }
		public DbSet<ContractDifference> ContractDifferences { set; get; }
		public DbSet<MonthlyDataContract> MonthlyDataContracts { set; get; }
		public DbSet<MonthlyDataMainGroup> MonthlyDataMainGroups { set; get; }
		public DbSet<MonthlyData> MonthlyDatas { set; get; }
		public DbSet<AddDedBenGroupParameterTitle> AddDedBenGroupParameterTitles { set; get; }
		public DbSet<AddDedBenEmployee> AddDedBenEmployees { set; get; }
		public DbSet<AddDedBenMainGroup> AddDedBenMainGroups { set; get; }
		public DbSet<AdditionalDeductionOrBenefit> AdditionalDeductionOrBenefits { set; get; }
		public DbSet<RetirementFormField> RetirementFormFields { set; get; }
		public DbSet<MainGroupExpenseArticle> MainGroupExpenseArticles { set; get; }
		public DbSet<ExpenseArticle> ExpenseArticles { set; get; }
		public DbSet<TaxItem> TaxItems { set; get; }
		public DbSet<Tax> Taxs { set; get; }
		public DbSet<Department> Departments { set; get; }
		public DbSet<Job> Jobs { set; get; }
		public DbSet<Mission> Missions { set; get; }
		public DbSet<GroupParameterValue> GroupParameterValues { set; get; }
		public DbSet<GroupParameterTitle> GroupParameterTitles { set; get; }
		public DbSet<GroupValueTitle> GroupValueTitles { set; get; }
		public DbSet<GroupVariableValue> GroupVariableValues { set; get; }
		public DbSet<City> Citys { set; get; }
		public DbSet<Employee> Employees { set; get; }
		public DbSet<Contract> Contracts { set; get; }
		public DbSet<ContractFieldValue> ContractFieldValues { set; get; }
		public DbSet<ContractFieldTitle> ContractFieldTitles { set; get; }
	}
}