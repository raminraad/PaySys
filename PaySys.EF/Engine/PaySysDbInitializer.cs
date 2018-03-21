using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bogus;
using PaySys.EF;
using Bogus.DataSets;

namespace PaySys.EF
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
				e.DateBirth = $"13{f.Random.Number(30) + 30}{f.Random.Number(11) + 1}{f.Random.Number(29) + 1}";
				e.IdCardExportDate = $"13{f.Random.Number(30) + 30}{f.Random.Number(11) + 1}{f.Random.Number(29) + 1}";
				e.DossierNo = $"{f.Random.Number(999999):d6}";
				e.PersonnelCode = $"{f.Random.Number(999999):d6}";
				e.FatherName = f.Name.FirstName();
				e.Sex = f.PickRandom(Enum.GetValues(typeof(Sex)).Cast<Sex>().Where(sex => sex != Sex.Unknown));
				e.PostalCode = $"{f.Random.Number(999999999):d10}";
				e.NationalNo = $"{f.Random.Number(999999999):d10}";
				e.IdCardNo = $"{f.Random.Number(999999)}";
			});

			var retirementFormFieldFaker = new Faker<RetirementFormField>("fa").StrictMode(false).Rules((f, e) =>
			{
				e.Title = f.Commerce.ProductAdjective();
			});

			var mainGroups = new List<MainGroup>
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

			retirementFormFieldFaker.Generate(11).ForEach(rff=>context.RetirementFormFields.Add(rff));
			employeeFaker.Generate(80).ForEach(e => context.Employees.Add(e));
			mainGroups.ForEach(e => context.MainGroups.Add(e));
			base.Seed(context);
		}
	}
}