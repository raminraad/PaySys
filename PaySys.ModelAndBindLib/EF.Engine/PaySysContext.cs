﻿#region

using System.Data.Entity;
using System.Linq;
using System.Windows;
using PaySys.ModelAndBindLib.Model;

#endregion

namespace PaySys.ModelAndBindLib.Engine
{
	public class PaySysContext : DbContext
	{
		public PaySysContext() : base( "PaySys" )
		{
			Database.SetInitializer( new PaySysDbInitializer() );

			//			Database.Initialize(true);
		}

		public DbSet<MainGroup> MainGroups { set; get; }

		public DbSet<SubGroup> SubGroups { set; get; }

		public DbSet<MiscTitle> MiscTitles { set; get; }

		public DbSet<Misc> Miscs { set; get; }

		public DbSet<ParameterInvolvedMisc> ParameterInvolvedMiscs { set; get; }

		public DbSet<Parameter> Parameters { set; get; }

		public DbSet<ParameterInvolvedContractField> ParameterInvolvedContractFields { set; get; }

		public DbSet<ExpenseArticle> ExpenseArticles { set; get; }

		public DbSet<MiscRecharge> MiscRecharges { set; get; }

		public DbSet<SubGroupContractField> SubGroupContractFields { set; get; }

		public DbSet<MissionFormula> MissionFormulas { set; get; }

		public DbSet<TaxTable> TaxTables { set; get; }

		public DbSet<TaxRow> TaxRows { set; get; }

		public DbSet<MiscValueForEmployee> MiscValueForEmployees { set; get; }

		public DbSet<ContractMaster> ContractMasters { set; get; }

		public DbSet<ContractDetail> ContractDetails { set; get; }

		public DbSet<Employee> Employees { set; get; }

		public DbSet<HandselFormula> HandselFormulas { set; get; }

		public DbSet<ContractDifference> ContractDifferences { set; get; }

		public DbSet<Job> Jobs { set; get; }

		public DbSet<City> Cities { set; get; }

		public DbSet<Mission> Missions { set; get; }

		public DbSet<ExpenseArticleOfContractFieldForSubGroup> ExpenseArticleOfContractFieldForSubGroups { set; get; }

		public DbSet<MissionFormulaInvolvedContractField> MissionFormulaInvolvedContractFields { set; get; }

		public DbSet<VariableTitle> VariableTitles { set; get; }

		public DbSet<SubGroupVariable> SubGroupVariables { set; get; }

		public DbSet<VariableValueForEmployee> VariableValueForEmployees { set; get; }
		public DbSet<ParameterTitle> ParameterTitles { set; get; }
		public DbSet<ContractFieldTitle> ContractFieldTitles { set; get; }

		public void ShowChanges()
		{
			var changes = ChangeTracker.Entries().Where( entry => entry.State != EntityState.Unchanged );
			var changesCount = changes.Count();
			var i = 0;
			foreach( var entry in changes )
			{
				var msg = $"Change {++i}/{changesCount}";
				msg += $"\n\t{entry.Entity.GetType().Name} : {entry.State}";
				MessageBox.Show( msg );
			}
		}

		public int DiscardChanges()
		{
			var changes = ChangeTracker.Entries().Where( entry => entry.State != EntityState.Unchanged );
			var changesCount = changes.Count();
			foreach( var entry in changes )
				switch( entry.State )
				{
					case EntityState.Modified:
						entry.State = EntityState.Unchanged;
						break;
					case EntityState.Added:
						entry.State = EntityState.Detached;
						break;
					case EntityState.Deleted:
						entry.Reload();
						break;
				}

			return changesCount;
		}
	}
}