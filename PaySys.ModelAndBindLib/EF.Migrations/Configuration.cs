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
			        ItemColorPallet = ColorPallet.Goldenrod,
			        SubGroups = new List<SubGroup>
			        {
				        new SubGroup
				        {
					        Title = "کارمندان ثابت",
					        ItemColorPallet = ColorPallet.Goldenrod
				        },
				        new SubGroup
				        {
					        Title = "کارمندان رسمی",
					        ItemColorPallet = ColorPallet.Goldenrod
				        },
				        new SubGroup
				        {
					        Title = "شهردار",
					        ItemColorPallet = ColorPallet.Goldenrod
				        }
			        }
		        },
		        new MainGroup
		        {
			        Title = "بازنشستگان",
			        ItemColorPallet = ColorPallet.Teal,
			        SubGroups = new List<SubGroup>
			        {
				        new SubGroup
				        {
					        Title = "بازنشستگان",
					        ItemColorPallet = ColorPallet.Teal,
				        },
				        new SubGroup
				        {
					        Title = "موظفین",
					        ItemColorPallet = ColorPallet.Teal,
				        },
			        }
		        },
		        new MainGroup
		        {
			        Title = "تأمین اجتماعی",
			        ItemColorPallet = ColorPallet.CornflowerBlue,
			        SubGroups = new List<SubGroup>
			        {
				        new SubGroup
				        {
					        Title = "کارگران خدمات شهری",
					        ItemColorPallet = ColorPallet.CornflowerBlue,
				        },
				        new SubGroup
				        {
					        Title = "کارگران خدمات اداری",
					        ItemColorPallet = ColorPallet.CornflowerBlue,
				        }
			        }
		        },
	        };
	        context.MainGroups.AddRange(seedMainGroups);

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
	        var seedEmployees = employeeFaker.Generate(20);
	        context.Employees.AddRange(seedEmployees);
	        

	        #endregion

	        #region Job

	        var jobFaker = new Faker<Job>("fa").StrictMode(false).Rules((f, e) =>
	        {
		        e.Description = f.Name.JobDescriptor();
		        e.JobNo = $"{f.Random.Number(999999):d6}";
		        e.ItemColorPallet = f.PickRandom(Enum.GetValues(typeof(ColorPallet)).Cast<ColorPallet>()
			        .Where(x => x != ColorPallet.Unknown));
		        e.Title = f.Name.JobTitle();
	        });
	        var seedJobs = jobFaker.Generate(10);
				        context.Jobs.AddRange(seedJobs);


			#endregion

			#region ContractFieldTitle & GroupContractFieldTitle

			var contractFieldTitleFaker = new Faker<ContractFieldTitle>("fa").StrictMode(false).Rules((f, e) =>
				{
					e.Title = f.Finance.AccountName().Replace("Account", string.Empty).Trim();
				});

	        var groupContractFieldTitleFaker = new Faker<GroupContractFieldTitle>("fa").StrictMode(false).Rules((f, e) =>
	        {
		        e.MainGroup = f.PickRandom(seedMainGroups);
		        e.ContractFieldTitle = contractFieldTitleFaker.Generate();
		        e.Year = 97;
	        });
	        var seedGroupContractFieldTitle = groupContractFieldTitleFaker.Generate(40);
	        context.GroupContractFieldTitles.AddRange(seedGroupContractFieldTitle);
	        

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
		        e.Job = f.PickRandom<Job>(seedJobs);
		        e.SacrificeStand = f.PickRandom(Enum.GetValues(typeof(SacrificeStand)).Cast<SacrificeStand>()
			        .Where(sex => sex != SacrificeStand.Unknown));
		        e.SubGroup = f.PickRandom(f.PickRandom(seedMainGroups).SubGroups);
	        });
	        var seedContractMasters = contractMasterFaker.Generate(30);
	        context.ContractMasters.AddRange(seedContractMasters);
	        

	        #endregion

	        #region ContractDetail

	        var contractDetailFaker = new Faker<ContractDetail>("fa").StrictMode(false).Rules((f, e) =>
	        {
		        e.Value = f.Random.Number(100)*10000;
	        });
	        var seedContractDetails = new List<ContractDetail>();


	        int indexContMast = 0;
	        int indexGrpContField = 0;
	        foreach (var contMast in seedContractMasters)
	        {
		        File.AppendAllText(@"d:\SeedLog.log", @"ContractMaster no." + (indexContMast++) + "\n");
		        foreach (var grpCntField in seedGroupContractFieldTitle.Where(c=>c.MainGroup.Equals(contMast.SubGroup.MainGroup)))
//		        foreach (var grpCntField in seedGroupContractFieldTitle.Where(c => c.MainGroup.EqualsByTitle(contMast.SubGroup.MainGroup)).ToList())
		        {
			        contractDetailFaker.RuleFor(detail => detail.ContractMaster, contMast);
			        contractDetailFaker.RuleFor(detail => detail.GroupContractFieldTitle, grpCntField);
			        var contractDetail = contractDetailFaker.Generate();
			        seedContractDetails.Add(contractDetail);
			        File.AppendAllText(@"d:\SeedLog.log", "\tDetail " + (indexGrpContField++)+$@" ({grpCntField.ContractFieldTitle.Title} : {contractDetail.Value}) " + "\n");
		        }
	        }
	        context.ContractDetails.AddRange(seedContractDetails);
	        

	        #endregion


			#region Set CurrentContract for each Employee
			var queryEmpContracts =
		        from cont in seedContractMasters
		        group cont by cont.Employee into newGroup
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
