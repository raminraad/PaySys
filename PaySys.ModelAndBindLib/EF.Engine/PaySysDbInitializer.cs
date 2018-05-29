﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Bogus;
using PaySys.ModelAndBindLib.Model;

namespace PaySys.ModelAndBindLib.Engine
{
	class PaySysDbInitializer : DropCreateDatabaseIfModelChanges<PaySysContext>
	{
		public PaySysDbInitializer()
		{
		}

		protected override void Seed(PaySysContext context)
		{
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
			var seedMainGroups = new List<MainGroup>
			{
				new MainGroup
				{
					Title = "استخدام کشوری",
					SubGroups = new List<SubGroup>
					{
						new SubGroup
						{
							Title = "کارمندان ثابت"
						},
						new SubGroup
						{
							Title = "کارمندان رسمی"
						},
						new SubGroup
						{
							Title = "شهردار"
						}
					}
				},
				new MainGroup
				{
					Title = "بازنشستگان",
					SubGroups = new List<SubGroup>
					{
						new SubGroup
						{
							Title = "بازنشستگان"
						},
						new SubGroup
						{
							Title = "موظفین"
						},
					}
				},
				new MainGroup
				{
					Title = "تأمین اجتماعی",
					SubGroups = new List<SubGroup>
					{
						new SubGroup
						{
							Title = "کارگران خدمات شهری"
						},
						new SubGroup
						{
							Title = "کارگران خدمات اداری"
						}
					}
				},
			};

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



			var jobFaker = new Faker<Job>("fa").StrictMode(false).Rules((f, e) =>
			{
				e.Description = f.Lorem.Text();
				e.JobNo = $"{f.Random.Number(999999):d6}";
				e.Title = f.Company.CompanySuffix();
			});
			var seedJobs = jobFaker.Generate(10);


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
				e.EducSt = f.PickRandom(Enum.GetValues(typeof(EducationStand)).Cast<EducationStand>()
					.Where(sex => sex != EducationStand.Unknown));
				e.EmpType = f.PickRandom(Enum.GetValues(typeof(EmploymentType)).Cast<EmploymentType>()
					.Where(sex => sex != EmploymentType.Unknown));
				e.HardshipFactor = f.Random.Number(100);
				e.InsuranceNo = $"{f.Random.Number(99999999):d8}";
				e.IsMarried = f.PickRandomParam(true, false);
				e.Job = f.PickRandom<Job>(seedJobs);
				e.SacrificeStand = f.PickRandom(Enum.GetValues(typeof(SacrificeStand)).Cast<SacrificeStand>()
					.Where(sex => sex != SacrificeStand.Unknown));
				e.SubGroup = f.PickRandom(f.PickRandom(seedMainGroups).SubGroups);
			});
			var seedContractMasters = contractMasterFaker.Generate(30);

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

			seedMainGroups.ForEach(e => context.MainGroups.Add(e));
			seedJobs.ForEach(e => context.Jobs.Add(e));
			seedEmployees.ForEach(e => context.Employees.Add(e));
			seedContractMasters.ForEach(e => context.ContractMasters.Add(e));

			base.Seed(context);
		}
	}
}