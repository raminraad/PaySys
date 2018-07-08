using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Diagnostics;
using System.IO;
using System.Linq;
using Bogus;
using PaySys.ModelAndBindLib.Engine;
using PaySys.ModelAndBindLib.Model;
using ValueType = PaySys.ModelAndBindLib.Model.ValueType;

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

			#region Debug

//						if (Debugger.IsAttached == false)
//						{
//							Debugger.Launch();
//						}

			#endregion

			#region Static Arrays

			var contractFields = new[]
			{
				"بیمه تکمیلی سهم دولت",
				"بیمه تبعی یک",
				"بیمه تبعی دو",
				"بیمه مازاد",
				"درصد معاف مالیات",
				"شماره حساب سهم دولت",
				"شماره حساب سهم پرسنل",
				"مبلغ کل بیمه تکمیلی",
				"اضافه کار عادی",
				"اضافه کار جمعه",
				"بازنشستگی گذشته",
				"بیمه عمر",
				"کسور متفرقه",
				"مقرری ماه اول",
				"بدهی اضافه کار",
				"معافیت وام مسکن",
				"مازاد سی سال",
				"پرداختهای متفرقه",
				"مدت پرداخت بروز",
				"سهم بیمه تکمیلی",
				"روزهای کارکرد",
				"ساعات شیفت کاری",
				"بدهی اضافه کار",
				"مدت مرخصی ذخیره",
				"ضریب پایان کار",
				"حق جذب",
				"عائله مندی",
				"سختی کار",
				"خوار و بار",
				"ایاب و ذهاب",
				"فوق العاده اشتغال خارج از كشور",
				"فوق العاده شغل",
				"فوق العاده مخصوص",
				"بدي آب و هوا",
				"تفاوت تطبيق",
				"حق سرپرستي",
				"همطرازي",
				"هزينه سفر"
			};
			var miscsPayment = new[]
			{
				"پاداش",
				"عیدی",
				"حق الزحمه",
				"تشویقی",
				"مساعده",
				"حق العمل",
				"حسن انجام کار",
				"هزینه تحصیل",
				"بورسیه"
			};
			var miscDebt = new[]
			{
				"جریمه",
				"توبیخی",
				"بدهی قبلی",
				"وام مسکن",
				"وام ضروری",
				"کسر کار",
				"اضافه مرخصی",
				"خسارت اموال",
				"هزینه های متفرقه"
			};
			var expenseArticles = new[]
			{
				"دیون",
				"اداری",
				"پاداش",
				"پرسنلی",
				"سرمایه ای",
				"تفاوت تطبيق",
				"مزایا و کمکها",
				"حقوق و دستمزد",
				"هزینه های سری",
				"پرداختهای انتقالی"
			};
			var parameterTitles = new[]
			{
				"درصد مأموریت با بیتوته",
				"درصد مأموریت بدون بیتوته",
				"ضریب نرخ کارکرد",
				"نرخ بیمه حق العمل",
				"ضریب فوق العاده",
				"نرخ مزایای متفرقه",
				"میزان بیمه سنواتی",
				"درصد تعدیل مازاد",
				"نرخ افزایش درآمد",
				"میزان کسور متفرقه",
				"کسر هزار ریال",
				"نرخ کارکرد",
				"ضریب دریافت علی الحساب",
				"درصد معافیت",
				"نرخ تورم",
				"میزان ده درصد",
				"ضریب افزایشی",
				"کاهش بیست درصد",
			};
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

			#endregion

			#region Employees

			var employeeFaker = new Faker<Employee>("fa").StrictMode(false).Rules((f, e) =>
			{
				e.FName = f.Name.FirstName();
				e.LName = f.Name.LastName();
				e.Address = $"{f.Address.City()} - {f.Address.CitySuffix()} - {f.Address.StreetName()} - پلاک  {f.Address.BuildingNumber()}";
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

			#endregion

			#region ExpenseArticles

			var expenseArticleFaker = new Faker<ExpenseArticle>("fa").StrictMode(false).Rules((f, e) =>
			{
				e.Title = f.PickRandom(expenseArticles);
				e.Code = $"{f.Random.Number(99999):D5}";
				e.IsActive = true;
			});
			var seedExpenseArticles = expenseArticleFaker.Generate(7);
			seedExpenseArticles.AddRange(seedExpenseArticles);

			#endregion

			#region Jobs

			var jobFaker = new Faker<Job>("fa").StrictMode(false).Rules((f, e) =>
			{
				e.Description = f.Name.JobDescriptor();
				e.JobNo = $"{f.Random.Number(999999):d6}";
				e.ItemColor = f.PickRandom(Enum.GetValues(typeof(ColorPallet)).Cast<ColorPallet>().Where(x => x != ColorPallet.Unknown));
				e.Title = f.Name.JobTitle();
			});
			var seedJobs = jobFaker.Generate(10);
			context.Jobs.AddRange(seedJobs);

			#endregion

			#region ContractFields

			var seedContractFields = new List<ContractField>();
			var contractFieldTitleFaker = new Faker<ContractField>("fa").StrictMode(false).Rules((f, e) =>
			{
				e.SubGroup = f.PickRandom(f.PickRandom(seedMainGroups).SubGroups);
				e.Year = 97;
			});
			foreach(var mainGroup in seedMainGroups)
				foreach(var subGroup in mainGroup.SubGroups)
					for(int i = 0; i < 10; i++)
					{
						contractFieldTitleFaker.RuleFor(field => field.Title, _faker.PickRandom(contractFields));
						var newContractField = contractFieldTitleFaker.Generate();
						if(!seedContractFields.Where(field=>field.SubGroup.Equals(subGroup)).Select(field => field.Title).Contains(newContractField.Title))
							seedContractFields.Add(newContractField);
					}
			
			context.ContractFields.AddRange(seedContractFields);

			#endregion

			#region Miscs

			var miscFaker = new Faker<Misc>("fa").StrictMode(false).Rules((f, e) =>
			{
				e.Year = 97;
				e.Month = 007;
			});
			var seedMiscs = new List<Misc>();
			foreach (var mainGroup in seedMainGroups)
				foreach (var subGroup in mainGroup.SubGroups)
					for (var i = 0; i < 10; i++)
					{
						miscFaker.RuleFor(misc => misc.SubGroup, subGroup).RuleFor(misc => misc.IsPayment, i < 5);
						if (i < 5)
							miscFaker.RuleFor(misc => misc.Title, _faker.PickRandom(miscsPayment));
						else
							miscFaker.RuleFor(misc => misc.Title, _faker.PickRandom(miscDebt));
						seedMiscs.Add(miscFaker.Generate());
					}

			context.Miscs.AddRange(seedMiscs);

			#endregion

			#region TaxTables

			var TaxTableFaker = new Faker<TaxTable>("fa").StrictMode(false).Rules((f, e) => { e.Month = 007; });
			var seedTaxTables = new List<TaxTable>();
			foreach (var mainGroup in seedMainGroups)
				foreach (var subGroup in mainGroup.SubGroups)
					for (var i = 95; i <= 97; i++)
					{
						TaxTableFaker.RuleFor(TaxTable => TaxTable.SubGroup, subGroup).RuleFor(TaxTable => TaxTable.Year, i);
						seedTaxTables.Add(TaxTableFaker.Generate());
					}

			context.TaxTables.AddRange(seedTaxTables);
			var TaxRowFaker = new Faker<TaxRow>("fa").StrictMode(false).Rules((f, e) =>
			{
				e.ValueTo = f.Random.Number(20000) * 1000;
				e.Factor = f.Random.Number(10);
			});
			var seedTaxRows = new List<TaxRow>();
			foreach (var taxTable in seedTaxTables)
				for (int i = 0; i < 10; i++)
				{
					TaxRowFaker.RuleFor(row => row.TaxTable, taxTable);
					seedTaxRows.Add(TaxRowFaker.Generate());
				}

			context.TaxRows.AddRange(seedTaxRows);

			#endregion

			#region MissionFormulas

			var MissionFormulaFaker = new Faker<MissionFormula>("fa").StrictMode(false).Rules((f, e) =>
			{
				e.Month = 007;
				e.DivideFactor = f.Random.Number(30);
				e.AddFactor = f.Random.Number(250000);
				e.MaxFactor = f.Random.Number(1000000);
				e.MissionFormulaInvolvedContractFields = new List<MissionFormulaInvolvedContractField>();
			});
			var seedMissionFormulas = new List<MissionFormula>();
			foreach (var mainGroup in seedMainGroups)
				foreach (var subGroup in mainGroup.SubGroups)
					for (var i = 95; i <= 97; i++)
					{
						MissionFormulaFaker.RuleFor(missionFormula => missionFormula.SubGroup, subGroup).RuleFor(missionFormula => missionFormula.Year, i);
						seedMissionFormulas.Add(MissionFormulaFaker.Generate());
					}
			foreach (var missionFormula in seedMissionFormulas)
				for (int i = 0; i < 10; i++)
				{
					if (_faker.Random.Bool())
					{
						var item = new MissionFormulaInvolvedContractField();
						var validContractFields = missionFormula.SubGroup.ContractFields.Where(field => !missionFormula.MissionFormulaInvolvedContractFields.Select(invFld => invFld.ContractField).Contains(field));
						if (validContractFields.Any())
						{
							item.ContractField = _faker.PickRandom(validContractFields);
							item.MissionFormula = missionFormula;
							missionFormula.MissionFormulaInvolvedContractFields.Add(item);
						}
					}
				}

			context.MissionFormulas.AddRange(seedMissionFormulas);

			#endregion

			#region HandselFormulaFaker

			var HandselFormulaFaker = new Faker<HandselFormula>("fa").StrictMode(false).Rules((f, e) =>
			{
				e.Month = 007;
				e.DaysCount = f.Random.Number(60);
				e.TaxRate = f.Random.Number(10);
				e.Max = f.Random.Number(5000) * 1000;
				e.Min = f.Random.Number(5000) * 1000;
				e.TaxFreeValue = f.Random.Number(5000) * 1000;
			});
			var seedHandselFormulas = new List<HandselFormula>();
			foreach (var mainGroup in seedMainGroups)
				foreach (var subGroup in mainGroup.SubGroups)
					for (var i = 95; i <= 97; i++)
					{
						HandselFormulaFaker.RuleFor(HandselFormula => HandselFormula.SubGroup, subGroup).RuleFor(HandselFormula => HandselFormula.Year, i);
						seedHandselFormulas.Add(HandselFormulaFaker.Generate());
					}

			context.HandselFormulas.AddRange(seedHandselFormulas);

			#endregion

			#region Parameters

			var ParameterFaker = new Faker<Parameter>("fa").StrictMode(false).Rules((f, e) =>
			{
				e.Year = 97;
				e.Month = 007;
				e.Alias = f.Finance.AccountName().Replace(" ", string.Empty);
				e.Title = f.PickRandom(parameterTitles);
				e.ValueType = f.PickRandom(Enum.GetValues(typeof(ValueType)).Cast<ValueType>().Where(valueType => valueType != ValueType.Unknown));
				e.Value = f.Random.Number(999999);
				e.ParameterInvolvedContractFields = new List<ParameterInvolvedContractField>();
				e.ParameterInvolvedMiscs = new List<ParameterInvolvedMisc>();
			});
			var seedParameters = new List<Parameter>();
			foreach (var mainGroup in seedMainGroups)
				foreach (var subGroup in mainGroup.SubGroups)
					for (var i = 0; i < 10; i++)
					{
						ParameterFaker.RuleFor(parameter => parameter.SubGroup, subGroup);
						seedParameters.Add(ParameterFaker.Generate());
					}

			#region ParameterInvolvedContractFields

			foreach (var parameter in seedParameters)
				foreach (var contractField in parameter.SubGroup.ContractFields)
					if (_faker.Random.Bool() && !parameter.ParameterInvolvedContractFields.Select(inv => inv.ContractField).Contains(contractField))
						parameter.ParameterInvolvedContractFields.Add(new ParameterInvolvedContractField
						{
							ContractField = contractField
						});

			#endregion

			#region ParameterInvolvedMiscs

			foreach (var parameter in seedParameters)
				foreach (var misc in parameter.SubGroup.Miscs)
					if (_faker.Random.Bool() && !parameter.ParameterInvolvedMiscs.Select(inv => inv.Misc).Contains(misc))
						parameter.ParameterInvolvedMiscs.Add(new ParameterInvolvedMisc
						{
							Misc = misc
						});

			#endregion

			context.Parameters.AddRange(seedParameters);

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
						ExpenseArticle = _faker.PickRandom(seedExpenseArticles)
					});

			context.ExpenseArticleOfOverTimeForSubGroups.AddRange(seedExpenseArticleOfOverTimeForSubGroups);

			#endregion

			#region ContractMaster

			var contractMasterFaker = new Faker<ContractMaster>("fa").StrictMode(false).Rules((f, e) =>
			{
				e.Employee = f.PickRandom(seedEmployees);
				e.AccountNoEmp = $"{f.PickRandom(creditCardNoPrefix)}-{f.Random.Number(9999):d4}-{f.Random.Number(9999):d4}-{f.Random.Number(9999):d4}";
				e.AccountNoGov = $"{f.PickRandom(creditCardNoPrefix)}-{f.Random.Number(9999):d4}-{f.Random.Number(9999):d4}-{f.Random.Number(9999):d4}";
				e.ContractNo = $"{f.Random.Number(99999):d5}";
				e.DateEmployment = $"13{f.Random.Number(20) + 40:d2}{f.Random.Number(11) + 1:d2}{f.Random.Number(29) + 1:d2}";
				e.DateExecution = $"13{f.Random.Number(20) + 40:d2}{f.Random.Number(11) + 1:d2}{f.Random.Number(29) + 1:d2}";
				e.DateExport = $"13{f.Random.Number(20) + 40:d2}{f.Random.Number(11) + 1:d2}{f.Random.Number(29) + 1:d2}";
				e.EducationStand = f.PickRandom(Enum.GetValues(typeof(EducationStand)).Cast<EducationStand>().Where(sex => sex != EducationStand.Unknown));
				e.EmploymentType = f.PickRandom(Enum.GetValues(typeof(EmploymentType)).Cast<EmploymentType>().Where(sex => sex != EmploymentType.Unknown));
				e.HardshipFactor = f.Random.Number(100);
				e.InsuranceNo = $"{f.Random.Number(99999999):d8}";
				e.MaritalStatus = f.PickRandom(Enum.GetValues(typeof(MaritalStatus)).Cast<MaritalStatus>().Where(sex => sex != MaritalStatus.Unknown));
				e.Job = f.PickRandom(seedJobs);
				e.SacrificeStand = f.PickRandom(Enum.GetValues(typeof(SacrificeStand)).Cast<SacrificeStand>().Where(sex => sex != SacrificeStand.Unknown));
				e.SubGroup = f.PickRandom(f.PickRandom(seedMainGroups).SubGroups);
			});
			var seedContractMasters = contractMasterFaker.Generate(30);
			context.ContractMasters.AddRange(seedContractMasters);

			#endregion

			#region ContractDetail

			var contractDetailFaker = new Faker<ContractDetail>("fa").StrictMode(false).Rules((f, e) => { e.Value = f.Random.Number(100) * 10000; });
			var seedContractDetails = new List<ContractDetail>();
			foreach (var contMast in seedContractMasters)
				foreach (var grpCntField in seedContractFields.Where(c => c.SubGroup.Equals(contMast.SubGroup)))
				{
					contractDetailFaker.RuleFor(detail => detail.ContractMaster, contMast);
					contractDetailFaker.RuleFor(detail => detail.ContractField, grpCntField);
					var contractDetail = contractDetailFaker.Generate();
					seedContractDetails.Add(contractDetail);
				}

			context.ContractDetails.AddRange(seedContractDetails);

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