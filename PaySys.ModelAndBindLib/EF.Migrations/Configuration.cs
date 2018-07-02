using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.IO;
using System.Linq;
using Bogus;
using PaySys.ModelAndBindLib.Engine;
using PaySys.ModelAndBindLib.Model;

namespace PaySys.ModelAndBindLib.Migrations
{
	internal sealed class Configuration : DbMigrationsConfiguration<PaySysContext>
	{
		private readonly Faker _faker = new Faker("fa");

		public Configuration()
		{
			AutomaticMigrationsEnabled = false;
		}

		protected override void Seed(PaySysContext context)
		{
			#region MyRegion

			//  This method will be called after migrating to the latest version.

			//  You can use the DbSet<T>.AddOrUpdate() helper extension method 
			//  to avoid creating duplicate seed data. E.g.
			//
			//    context.People.AddOrUpdate(
			//      p => p.FullName,
			//      new Person { FullName = "Andrew Peters" },
			//      new Person { FullName = "Brice Lambson" },
			//      new Person { FullName = "Rowan Miller" }
			//    );
			//

			#endregion

			File.Delete(@"d:\SeedLog.log");
			File.AppendAllText(@"d:\SeedLog.log", "Seed start\n");

			#region Static Arrays

			var mobilePrefix = new[]
			{
				"0912",
				"0935",
				"0919",
				"0933",
				"0918"
			};
			var creditCardNoPrefix = new[]
			{
				"627353",
				"610433",
				"621986",
				"621986",
				"502229",
				"622106",
				"627412",
				"603770",
				"603770",
				"627648",
				"603799"
			};

			#endregion

			#region MainGroups & SubGroups

			var seedMainGroups = new List<MainGroup>
			{
				new MainGroup
				{
					Title = "استخدام کشوری",
					ItemColor = ColorPallet.Goldenrod,
					SubGroups = new List<SubGroup>
					{
						new SubGroup
						{
							Title = "کارمندان ثابت",
							ItemColor = ColorPallet.Goldenrod
						},
						new SubGroup
						{
							Title = "کارمندان رسمی",
							ItemColor = ColorPallet.Goldenrod
						},
						new SubGroup
						{
							Title = "شهردار",
							ItemColor = ColorPallet.Goldenrod
						}
					}
				},
				new MainGroup
				{
					Title = "بازنشستگان",
					ItemColor = ColorPallet.Teal,
					SubGroups = new List<SubGroup>
					{
						new SubGroup
						{
							Title = "بازنشستگان",
							ItemColor = ColorPallet.Teal
						},
						new SubGroup
						{
							Title = "موظفین",
							ItemColor = ColorPallet.Teal
						}
					}
				},
				new MainGroup
				{
					Title = "تأمین اجتماعی",
					ItemColor = ColorPallet.CornflowerBlue,
					SubGroups = new List<SubGroup>
					{
						new SubGroup
						{
							Title = "کارگران خدمات شهری",
							ItemColor = ColorPallet.CornflowerBlue
						},
						new SubGroup
						{
							Title = "کارگران خدمات اداری",
							ItemColor = ColorPallet.CornflowerBlue
						}
					}
				}
			};
			context.MainGroups.AddRange(seedMainGroups);
			foreach (var item in context.MainGroups.ToList())
			{
				File.AppendAllText(@"d:\SeedLog.log", $@"MainGroup: {item.MainGroupId}.{item.Title}" + "\n");
				foreach (var subItem in item.SubGroups)
					File.AppendAllText(@"d:\SeedLog.log", "\t\t\t" + $@"SubGroup: {subItem.SubGroupId}.{subItem.Title}" + "\n");
			}

			#endregion

			#region Employee

			var employeeFaker = new Faker<Employee>("fa").StrictMode(false).Rules((f, e) =>
			{
				e.FName = f.Name.FirstName();
				e.LName = f.Name.LastName();
				e.Address =
					$"{f.Address.City()} - {f.Address.CitySuffix()} - {f.Address.StreetName()} - پلاک  {f.Address.BuildingNumber()}";
				e.BirthPlace = f.Address.City();
				e.IdCardExportPlace = f.Address.City();
				e.CellNo = $"{f.PickRandom(mobilePrefix)}{f.Phone.PhoneNumber("#######")}";
				e.HomeTel = f.Phone.PhoneNumber();
				e.BirthDate = $"13{f.Random.Number(30) + 30:d2}{f.Random.Number(11) + 1:d2}{f.Random.Number(29) + 1:d2}";
				e.IdCardExportDate = $"13{f.Random.Number(30) + 30:d2}{f.Random.Number(11) + 1:d2}{f.Random.Number(29) + 1:d2}";
				e.DossierNo = $"{f.Random.Number(999999):d6}";
				e.PersonnelCode = $"{f.Random.Number(999999):d6}";
				e.FatherName = f.Name.FirstName();
				e.Sex = f.PickRandom(Enum.GetValues(typeof(Sex)).Cast<Sex>().Where(sex => sex != Sex.Unknown));
				e.PostalCode = $"{f.Random.Number(999999999):d10}";
				e.NationalCardNo = $"{f.Random.Number(999999999):d10}";
				e.IdCardNo = $"{f.Random.Number(999999)}";
			});
			var seedEmployees = employeeFaker.Generate(40);
			context.Employees.AddRange(seedEmployees);
			foreach (var item in context.Employees.ToList())
				File.AppendAllText(@"d:\SeedLog.log", $@"Employee: {item.EmployeeId}.{item.FullName}" + "\n");

			#endregion

			#region ExpenseArticle

			var expenseArticleFaker = new Faker<ExpenseArticle>("fa").StrictMode(false).Rules((f, e) =>
			{
				e.Title = f.Finance.AccountName();
				e.Code = $"{f.Random.Number(99999):D5}";
				e.IsActive = true;
			});
			var seedExpenseArticles = expenseArticleFaker.Generate(7);
			seedExpenseArticles.AddRange(seedExpenseArticles);
			foreach (var item in seedExpenseArticles.ToList())
				File.AppendAllText(@"d:\SeedLog.log", $@"ExpenseArticle: {item.ExpenseArticleId}.{item.Title}" + "\n");

			#endregion

			#region Job

			var jobFaker = new Faker<Job>("fa").StrictMode(false).Rules((f, e) =>
			{
				e.Description = f.Name.JobDescriptor();
				e.JobNo = $"{f.Random.Number(999999):d6}";
				e.ItemColor = f.PickRandom(Enum.GetValues(typeof(ColorPallet)).Cast<ColorPallet>()
					.Where(x => x != ColorPallet.Unknown));
				e.Title = f.Name.JobTitle();
			});
			var seedJobs = jobFaker.Generate(10);
			context.Jobs.AddRange(seedJobs);
			foreach (var item in context.Jobs.ToList())
				File.AppendAllText(@"d:\SeedLog.log", $@"Job: {item.JobId}.{item.Title}" + "\n");

			#endregion

			#region ContractField

			var contractFieldTitleFaker = new Faker<ContractField>("fa").StrictMode(false).Rules((f, e) =>
			{
				e.SubGroup = f.PickRandom(f.PickRandom(seedMainGroups).SubGroups);
				e.Title = f.Finance.AccountName().Replace("Account", string.Empty).Trim();
				e.Year = 97;
			});
			var seedContractFieldTitles = contractFieldTitleFaker.Generate(40);
			context.ContractFields.AddRange(seedContractFieldTitles);
			foreach (var item in context.ContractFields.ToList())
				File.AppendAllText(@"d:\SeedLog.log", $@"ContractField: {item.ContractFieldId}.{item.Title}" + "\n");

			#endregion

			#region Misc

			var miscFaker = new Faker<Misc>("fa").StrictMode(false).Rules((f, e) =>
			{
				e.Title = f.Commerce.Department();
				e.Year = 97;
				e.Month = 007;
			});

			var seedMiscs=new List<Misc>();
			foreach (var mainGroup in seedMainGroups)
				foreach (var subGroup in mainGroup.SubGroups)
					for (var i = 0; i < 10; i++)
					{
						miscFaker.RuleFor(misc => misc.SubGroup, subGroup).RuleFor(misc => misc.IsPayment, i < 5);
						seedMiscs.Add(miscFaker.Generate());
					}
			context.Miscs.AddRange(seedMiscs);
			foreach (var item in context.Miscs.ToList())
				File.AppendAllText(@"d:\SeedLog.log",
					$@"(SubGroup.Misc): {item.MiscId}.({item.SubGroup.Title}.{item.Title})" + "\n");

			#endregion

			#region ExpenseArticleOfMiscForSubGroups
			var seedExpenseArticleOfMiscForSubGroups = new List<ExpenseArticleOfMiscForSubGroup>();
			foreach (var mainGroup in seedMainGroups)
				foreach (var subGroup in mainGroup.SubGroups)
					foreach (var misc in subGroup.Miscs)
						seedExpenseArticleOfMiscForSubGroups.Add(new ExpenseArticleOfMiscForSubGroup
						{
							SubGroup = subGroup,
							Month = 007,
							Misc = misc,
							ExpenseArticle = _faker.PickRandom(seedExpenseArticles)
						});
			context.ExpenseArticleOfMiscForSubGroups.AddRange(seedExpenseArticleOfMiscForSubGroups);
			foreach (var item in context.ExpenseArticleOfMiscForSubGroups.ToList())
				File.AppendAllText(@"d:\SeedLog.log",
					$@"ExpenseArticleOfMiscForSubGroup (SubGroup.ExpenseArticle.Misc): {item.ExpenseArticleOfMiscForSubGroupId}.({
							item.SubGroup.Title
						}.{item.ExpenseArticle.Title}.{item.Misc.Title})" + "\n");

			#endregion

			#region ExpenseArticleOfContractFieldForSubGroups

			var seedExpenseArticleOfContractFieldForSubGroups = new List<ExpenseArticleOfContractFieldForSubGroup>();
			foreach (var mainGroup in seedMainGroups)
				foreach (var subGroup in mainGroup.SubGroups)
					foreach (var contractField in subGroup.ContractFields)
						seedExpenseArticleOfContractFieldForSubGroups.Add(new ExpenseArticleOfContractFieldForSubGroup
						{
							SubGroup = subGroup,
							Month = 007,
							ContractField = contractField,
							ExpenseArticle = _faker.PickRandom(seedExpenseArticles)
						});
			context.ExpenseArticleOfContractFieldForSubGroups.AddRange(seedExpenseArticleOfContractFieldForSubGroups);
			foreach (var item in context.ExpenseArticleOfContractFieldForSubGroups.ToList())
				File.AppendAllText(@"d:\SeedLog.log",
					$@"ExpenseArticleOfContractFieldForSubGroup (SubGroup.ExpenseArticle.ContractField): {
							item.ExpenseArticleOfContractFieldForSubGroupId
						}.({item.SubGroup.Title}.{item.ExpenseArticle.Title}.{item.ContractField.Title})" + "\n");

			#endregion

			#region ExpenseArticleOfOverTimeForSubGroups
			var seedExpenseArticleOfOverTimeForSubGroups = new List<ExpenseArticleOfOverTimeForSubGroup>();

			foreach (var mainGroup in seedMainGroups)
				foreach (var subGroup in mainGroup.SubGroups)
					seedExpenseArticleOfOverTimeForSubGroups.Add(new ExpenseArticleOfOverTimeForSubGroup
					{
						SubGroup = subGroup,
						Year = 97,
						Month = 007,
						ExpenseArticle = _faker.PickRandom<ExpenseArticle>(seedExpenseArticles)
					});
			context.ExpenseArticleOfOverTimeForSubGroups.AddRange(seedExpenseArticleOfOverTimeForSubGroups);

			foreach (var item in context.ExpenseArticleOfOverTimeForSubGroups.ToList())
				File.AppendAllText(@"d:\SeedLog.log",
					$@"ExpenseArticleOfOverTimeForSubGroup (SubGroup.ExpenseArticle): {item.ExpenseArticleOfOverTimeForSubGroupId}.({
							item.SubGroup.Title
						}.{item.ExpenseArticle.Title})" + "\n");

			#endregion

			#region ContractMaster

			var contractMasterFaker = new Faker<ContractMaster>("fa").StrictMode(false).Rules((f, e) =>
			{
				e.Employee = f.PickRandom(seedEmployees);
				e.AccountNoEmp =
					$"{f.PickRandom(creditCardNoPrefix)}-{f.Random.Number(9999):d4}-{f.Random.Number(9999):d4}-{f.Random.Number(9999):d4}";
				e.AccountNoGov =
					$"{f.PickRandom(creditCardNoPrefix)}-{f.Random.Number(9999):d4}-{f.Random.Number(9999):d4}-{f.Random.Number(9999):d4}";
				e.ContractNo = $"{f.Random.Number(99999):d5}";
				e.DateEmployment = $"13{f.Random.Number(20) + 40:d2}{f.Random.Number(11) + 1:d2}{f.Random.Number(29) + 1:d2}";
				e.DateExecution = $"13{f.Random.Number(20) + 40:d2}{f.Random.Number(11) + 1:d2}{f.Random.Number(29) + 1:d2}";
				e.DateExport = $"13{f.Random.Number(20) + 40:d2}{f.Random.Number(11) + 1:d2}{f.Random.Number(29) + 1:d2}";
				e.EducationStand = f.PickRandom(Enum.GetValues(typeof(EducationStand)).Cast<EducationStand>()
					.Where(sex => sex != EducationStand.Unknown));
				e.EmploymentType = f.PickRandom(Enum.GetValues(typeof(EmploymentType)).Cast<EmploymentType>()
					.Where(sex => sex != EmploymentType.Unknown));
				e.HardshipFactor = f.Random.Number(100);
				e.InsuranceNo = $"{f.Random.Number(99999999):d8}";
				e.MaritalStatus = f.PickRandom(Enum.GetValues(typeof(MaritalStatus)).Cast<MaritalStatus>()
					.Where(sex => sex != MaritalStatus.Unknown));
				e.Job = f.PickRandom(seedJobs);
				e.SacrificeStand = f.PickRandom(Enum.GetValues(typeof(SacrificeStand)).Cast<SacrificeStand>()
					.Where(sex => sex != SacrificeStand.Unknown));
				e.SubGroup = f.PickRandom(f.PickRandom(seedMainGroups).SubGroups);
			});
			var seedContractMasters = contractMasterFaker.Generate(30);
			context.ContractMasters.AddRange(seedContractMasters);

			#endregion

			#region ContractDetail

			var contractDetailFaker = new Faker<ContractDetail>("fa").StrictMode(false)
				.Rules((f, e) => { e.Value = f.Random.Number(100) * 10000; });
			var seedContractDetails = new List<ContractDetail>();
			foreach (var contMast in seedContractMasters)
				foreach (var grpCntField in seedContractFieldTitles.Where(c => c.SubGroup.Equals(contMast.SubGroup)))
				{
					contractDetailFaker.RuleFor(detail => detail.ContractMaster, contMast);
					contractDetailFaker.RuleFor(detail => detail.ContractField, grpCntField);
					var contractDetail = contractDetailFaker.Generate();
					seedContractDetails.Add(contractDetail);
				}
			context.ContractDetails.AddRange(seedContractDetails);
			foreach (var item in context.ContractMasters)
			{
				File.AppendAllText(@"d:\SeedLog.log", $@"ContractMaster: {item.ContractMasterId}.{item.ContractNo}" + "\n");
				foreach (var subItem in item.ContractDetails)
					File.AppendAllText(@"d:\SeedLog.log",
						"\t\t\t" + $@"{subItem.ContractDetailId}.{subItem.ContractField.Title}:{subItem.Value}" + "\n");
			}

			#endregion

			#region Set CurrentContract for each Employee

			var queryEmpContracts = from cont in seedContractMasters
									group cont by cont.Employee
				into newGroup
									select newGroup;
			foreach (var empContract in queryEmpContracts)
			{
				var lastCnt = empContract.LastOrDefault();
				if (lastCnt != null)
					lastCnt.IsCurrentContract = true;
			}

			#endregion

			base.Seed(context);
		}
	}
}