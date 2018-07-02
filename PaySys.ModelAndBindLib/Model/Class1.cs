using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using PaySys.CalcLib.Converters;

namespace PaySys.ModelAndBindLib.Model
{
	/// <summary>#01 گروه اصلی</summary>
	public class MainGroup
	{
		public int MainGroupId { set; get; }
		public string Title { get; set; }
		public ColorPallet ItemColor { get; set; }
		public virtual List<SubGroup> SubGroups { get; set; }

		public override bool Equals(object obj)
		{
			return (obj as MainGroup)?.MainGroupId == MainGroupId && string.Equals(Title, ((MainGroup)obj).Title);
		}
	}

	/// <summary>#02 زیرگروه</summary>
	public class SubGroup
	{
		public int SubGroupId { get; set; }
		public string Title { get; set; }
		public ColorPallet ItemColor { get; set; }
		public bool Is31 { set; get; }
		[Required]
		public virtual MainGroup MainGroup { set; get; }
		public virtual List<ParameterValue> ParameterValues { set; get; }
		public virtual List<MissionFormula> MissionFormulas { get; set; }
		public virtual List<TaxTable> TaxTables { get; set; }
		public virtual List<HandselFormula> HandselFormulas { get; set; }
		public virtual List<ContractMaster> ContractMasters { get; set; }
		public virtual List<Misc> Miscs { get; set; }
		public virtual List<ContractField> ContractFields { get; set; }
		public virtual List<ExpenseArticleOfContractFieldForSubGroup> ExpenseArticleOfContractFieldForSubGroups { get; set; }
		public virtual List<ExpenseArticleOfMiscForSubGroup> ExpenseArticleOfMiscForSubGroups { get; set; }
		public virtual List<ExpenseArticleOfOverTimeForSubGroup> ExpenseArticleOfOverTimeForSubGroups { get; set; }

		public override bool Equals(object obj)
		{
			return (obj as SubGroup)?.SubGroupId == SubGroupId && string.Equals(Title, ((SubGroup)obj).Title);
		}
	}

	/// <summary>#04 بدهی یا پرداختهای متفرقه زیرگروه در سال و ماه</summary>
	public class Misc
	{
		public int MiscId { get; set; }
		public int Year { get; set; }
		public int Month { get; set; }
		public string Title { get; set; }
		public bool IsPayment { get; set; }
		public int Index { get; set; }
		public virtual List<ParameterInvolvedMisc> ParameterInvolvedMiscs { get; set; }
		public virtual List<EmployeeMiscRemain> EmployeeMiscRemains { get; set; }
		public virtual SubGroup SubGroup { set; get; }
		public virtual List<PayslipEmployeeMisc> PayslipEmployeeMiscs { get; set; }
		public virtual List<ExpenseArticleOfMiscForSubGroup> ExpenseArticleOfMiscForSubGroups { get; set; }
	}

	/// <summary>#05 پرداختهای متفرقه دخیل در محاسبات مؤلفه ها</summary>
	public class ParameterInvolvedMisc
	{
		public int ParameterInvolvedMiscId { get; set; }
		public virtual Misc Misc { get; set; }
		public virtual ParameterValue ParameterValue { get; set; }
	}

	/// <summary>#07 مقادیر مؤلفه های محاسباتی زیرگروه در سال و ماه</summary>
	public class ParameterValue
	{
		public int ParameterValueId { get; set; }
		public float Value { get; set; }
		public ContentType ContentType { get; set; }
		public string Title { get; set; }
		public string Alias { get; set; }
		public virtual List<ParameterInvolvedMisc> ParameterInvolvedMiscs { set; get; }
		public virtual List<ParameterInvolvedContractField> ParameterInvolvedContractFields { get; set; }
		public virtual SubGroup SubGroup { get; set; }
	}

	/// <summary>#08 فیلدهای احکام دخیل در محاسبات مؤلفه ها</summary>
	public class ParameterInvolvedContractField
	{
		public int ParameterInvolvedContractFieldId { get; set; }
		public virtual ContractField ContractField { get; set; }
		public virtual ParameterValue ParameterValue { get; set; }
	}

	/// <summary>#09 مواد هزینه</summary>
	public class ExpenseArticle
	{
		public int ExpenseArticleId { get; set; }
		public string Title { get; set; }
		public string Code { get; set; }
		public bool IsActive { get; set; }
		public virtual List<ExpenseArticleOfContractFieldForSubGroup> ExpenseArticleOfContractFieldForSubGroups { get; set; }
		public virtual List<ExpenseArticleOfMiscForSubGroup> ExpenseArticleOfMiscForSubGroups { get; set; }
		public virtual List<ExpenseArticleOfOverTimeForSubGroup> ExpenseArticleOfOverTimeForSubGroups { get; set; }
	}

	/// <summary>#10 مانده بدهی متفرقه اشخاص</summary>
	public class EmployeeMiscRemain
	{
		public int EmployeeMiscRemainId { set; get; }
		public float Value { get; set; }
		public int Year { get; set; }
		public int Month { get; set; }
		public virtual Misc Misc { get; set; }
		public virtual Employee Employee { get; set; }
	}

	/// <summary>#13 فیلدهای احکام زیرگروه در سال</summary>
	public class ContractField
	{
		public int ContractFieldId { get; set; }
		public string Title { get; set; }
		public int Year { get; set; }
		public virtual SubGroup SubGroup { get; set; }
		public virtual List<ParameterInvolvedContractField> ParameterInvolvedContractFields { get; set; }
		public virtual List<ContractDetail> ContractDetails { get; set; }
		public virtual List<ExpenseArticleOfContractFieldForSubGroup> ExpenseArticleOfContractFieldForSubGroups { get; set; }
		[NotMapped]
		public ExpenseArticle CurrentExpenseArticle
		{
			get => ExpenseArticleOfContractFieldForSubGroups.FirstOrDefault(exp => exp.Month == 007)?.ExpenseArticle;
			set
			{
				if (CurrentExpenseArticle != null)
				{
					ExpenseArticleOfContractFieldForSubGroups.FirstOrDefault(exp => exp.Month == 007).ExpenseArticle = value;
				}
				else
				{
					var newLink = new ExpenseArticleOfContractFieldForSubGroup
					{
						SubGroup = SubGroup,
						ContractField = this,
						Month = 007,
						ExpenseArticle = value
					};
					ExpenseArticleOfContractFieldForSubGroups.Add(newLink);
				}
			}
		}
	}

	/// <summary>#14 فرمول مأموریت زیرگروه در سال و ماه</summary> 
	public class MissionFormula
	{
		//Todo: Add fileds
		public int MissionFormulaId { get; set; }
		public virtual SubGroup SubGroup { get; set; }
	}

	/// <summary>#15 جدول مالیات</summary>
	public class TaxTable
	{
		public int TaxTableId { get; set; }
		public int Year { get; set; }
		public int Month { get; set; }
		public virtual SubGroup SubGroup { get; set; }
		public virtual List<TaxRow> TaxRows { get; set; }
	}

	/// <summary>#16 سطر جدول مالیات</summary>
	public class TaxRow
	{
		public int TaxRowId { get; set; }
		public float ValueTo { get; set; }
		public float Factor { get; set; }
		public virtual TaxTable TaxTable { get; set; }
	}

	/// <summary>#18 مقادیر بدهی یا پرداختهای متفرقه برای اشخاص در سال و ماه</summary>
	public class PayslipEmployeeMisc
	{
		public int PayslipEmployeeMiscId { get; set; }
		public float Value { get; set; }
		public float PayslipValue { get; set; }
		public int Year { get; set; }
		public int Month { get; set; }
		public virtual Misc Misc { get; set; }
		public virtual Employee Employee { get; set; }
	}

	/// <summary>#20 پایه حکم</summary>
	public class ContractMaster
	{
		public int ContractMasterId { get; set; }
		public string ContractNo { get; set; }
		public string DateExport { get; set; }
		public string DateExecution { get; set; }
		public string DateEmployment { get; set; }
		public MaritalStatus MaritalStatus { get; set; }
		public double HardshipFactor { get; set; }
		public string InsuranceNo { get; set; }
		public EducationStand EducationStand { get; set; }
		public EmploymentType EmploymentType { get; set; }
		public SacrificeStand SacrificeStand { get; set; }
		public string AccountNoGov { get; set; }
		public string AccountNoEmp { get; set; }
		public bool IsCurrentContract { set; get; }
		public virtual SubGroup SubGroup { set; get; }
		public virtual Employee Employee { get; set; }
		public virtual Job Job { get; set; }
		public virtual List<ContractDetail> ContractDetails { get; set; }

		public override bool Equals(object obj)
		{
			return obj is ContractMaster && ((ContractMaster)obj).ContractMasterId == ContractMasterId &&
				   string.Equals(ContractNo, ((ContractMaster)obj).ContractNo);
		}
	}

	/// <summary>#21 جزئیات احکام</summary>
	public class ContractDetail
	{
		public int ContractDetailId { get; set; }
		public float Value { get; set; }
		public virtual ContractField ContractField { get; set; }
		public virtual ContractMaster ContractMaster { get; set; }
		public virtual List<PayslipContractDetail> PayslipContractDetails { set; get; }
	}

	/// <summary>#22 اشخاص</summary>
	public class Employee
	{
		public int EmployeeId { get; set; }
		public string FName { get; set; }
		public string LName { get; set; }
		public string NationalCardNo { set; get; }
		public string BirthDate { get; set; }
		public string BirthPlace { get; set; }
		public string FatherName { get; set; }
		public string HomeTel { get; set; }
		public string Address { get; set; }
		public string DossierNo { get; set; }
		public Sex Sex { set; get; }
		public string CellNo { get; set; }
		public string PostalCode { get; set; }
		public string PersonnelCode { get; set; }
		public string IdCardExportPlace { get; set; }
		public string IdCardExportDate { set; get; }
		public string IdCardNo { get; set; }
		public virtual List<ContractMaster> ContractMasters { get; set; }
		public virtual List<EmployeeMiscRemain> EmployeeMiscRemains { get; set; }
		public virtual List<Mission> Missions { get; set; }
		public virtual List<PayslipEmployeeMisc> PayslipEmployeeMiscs { get; set; }
		public virtual List<PayslipEmployeeOvertime> PayslipEmployeeOvertimes { get; set; }
		[NotMapped]
		public string FullName => $"{FName} {LName}";
		[NotMapped]
		public string LuffName => $"{LName} {FName}";

		public override bool Equals(object obj)
		{
			return obj != null && ((Employee)obj).EmployeeId == EmployeeId;
		}
	}

	/// <summary>#23 فرمول عیدی در سال و ماه</summary>
	public class HandselFormula
	{
		public int HandselFormulaId { get; set; }
		public int DaysCount { get; set; }
		public double Min { get; set; }
		public double Max { get; set; }
		public double TaxFreeValue { get; set; }
		public int TaxRate { get; set; }
		public int Year { get; set; }
		public int Month { get; set; }
		public virtual SubGroup SubGroup { get; set; }
	}

	/// <summary>#24 تفاوت احکام</summary>
	public class ContractDifference
	{
		[Key]
		[ForeignKey("Contract1St")]
		public int ContractMasterId { get; set; }
		public string DateFrom { get; set; }
		public string DateTo { get; set; }
		public double FirstMonth { get; set; }
		public int PayYear { get; set; }
		public int PayMonth { get; set; }
		public virtual ContractMaster Contract1St { get; set; }
		public virtual ContractMaster Contract2Nd { get; set; }
	}

	/// <summary>#25 شغل</summary>
	public class Job
	{
		public int JobId { get; set; }
		public string Title { get; set; }
		public string JobNo { get; set; }
		public string Description { get; set; }
		public ColorPallet ItemColor { get; set; }
		public virtual List<ContractMaster> ContractMasters { get; set; }
	}

	/// <summary>#26 شهر</summary>
	public class City
	{
		public int CityId { get; set; }
		public string Title { get; set; }
		public int Distance { get; set; }
		public int Percentage { get; set; }
		public virtual List<Mission> Missions { get; set; }
	}

	/// <summary>#27 مأموریت</summary>
	public class Mission
	{
		public int MissionId { get; set; }
		public string Title { get; set; }
		public string DateStart { get; set; }
		public string DateEnd { get; set; }
		public string TimeStart { get; set; }
		public string TimeEnd { get; set; }
		/// <summary>مدت با بیتوته</summary>
		public int AmountResident { get; set; }
		/// <summary>مدت بدون بیتوته</summary>
		public int AmountNonResident { get; set; }
		public VehicleType VehicleType { set; get; }
		/// <summary>هزینه سفر</summary>
		public double TravelExpense { get; set; }
		public virtual Employee Employee { get; set; }
		public virtual City City { get; set; }
	}

	/// <summary>#28 مقادیر جزئیات احکام در فیش حقوقی</summary>
	public class PayslipContractDetail
	{
		public int PayslipContractDetailId { get; set; }
		public float PayslipValue { get; set; }
		public int Year { get; set; }
		public int Month { get; set; }
		public virtual ContractDetail ContractDetail { get; set; }
	}

	/// <summary>
	///#30 مواد هزینه عناوین فیلدهای احکام برای زیرگروه در ماه
	/// </summary>
	public class ExpenseArticleOfContractFieldForSubGroup
	{
		public int ExpenseArticleOfContractFieldForSubGroupId { get; set; }
		public int Month { get; set; }
		public virtual ExpenseArticle ExpenseArticle { set; get; }
		public virtual ContractField ContractField { set; get; }
		public virtual SubGroup SubGroup { set; get; }
	}

	/// <summary>
	///#31 مواد هزینه بدهی یا پرداختهای متفرقه برای زیرگروه در ماه
	/// </summary>
	public class ExpenseArticleOfMiscForSubGroup
	{
		public int ExpenseArticleOfMiscForSubGroupId { get; set; }
		public int Month { get; set; }
		public virtual ExpenseArticle ExpenseArticle { set; get; }
		public virtual Misc Misc { set; get; }
		public virtual SubGroup SubGroup { set; get; }
	}

	/// <summary>
	///#32 مواد هزینه اضافه کار برای زیرگروه در سال و ماه
	/// </summary>
	public class ExpenseArticleOfOverTimeForSubGroup
	{
		public int ExpenseArticleOfOverTimeForSubGroupId { get; set; }
		public int Year { get; set; }
		public int Month { get; set; }
		public virtual ExpenseArticle ExpenseArticle { set; get; }
		public virtual SubGroup SubGroup { set; get; }
	}

	/// <summary>
	///#33 مقادیر اضافه کار اشخاص در سال و ماه و فیش حقوقی
	/// </summary>
	public class PayslipEmployeeOvertime
	{
		public int PayslipEmployeeOvertimeId { get; set; }
		public float Value { get; set; }
		public float PayslipValue { get; set; }
		public int Year { get; set; }
		public int Month { get; set; }
		public Employee Employee { get; set; }
	}

	[TypeConverter(typeof(EnumDescriptionTypeConverter))]
	public enum SacrificeStand
	{
		[Description("وارد نشده")]
		Unknown = 0,
		[Description("رزمنده")]
		Razmande,
		[Description("آزاده")]
		Azade,
		[Description("خانواده آزاده")]
		KhanevadeAzade,
		[Description("جانباز")]
		Janbaz,
		[Description("خانواده جانباز")]
		KhanevadeJanbaz,
		[Description("خانواده شهید")]
		KhanevadeShahid,
		[Description("ایثارگر")]
		Isargar,
		[Description("سایر موارد")]
		Other
	}

	[TypeConverter(typeof(EnumDescriptionTypeConverter))]
	public enum ColorPallet
	{
		[Description("وارد نشده")]
		Unknown = 0,
		YellowGreen,
		White,
		SeaGreen,
		Red,
		Purple,
		Peru,
		PaleGoldenrod,
		Navy,
		MediumOrchid,
		Maroon,
		Lime,
		LightPink,
		LightCoral,
		Goldenrod,
		Gold,
		GhostWhite,
		ForestGreen,
		DodgerBlue,
		DarkSlateBlue,
		DarkRed,
		Teal,
		CornflowerBlue
	}

	[TypeConverter(typeof(EnumDescriptionTypeConverter))]
	public enum EducationStand
	{
		[Description("وارد نشده")]
		Unknown = 0,
		[Description("بی سواد")]
		Bisavad,
		[Description("سیکل")]
		Sikl,
		[Description("زیر دیپلم")]
		Zildiplom,
		[Description("دیپلم")]
		Diplom,
		[Description("کاردانی")]
		Kardani,
		[Description("کارشناسی")]
		Karshenasi,
		[Description("فوق لیسانس")]
		Foqlisans,
		[Description("دکترا")]
		Doctora,
		[Description("فوق دکترا")]
		Foqoctora,
		[Description("سایر موارد")]
		Other
	}

	[TypeConverter(typeof(EnumDescriptionTypeConverter))]
	public enum EmploymentType
	{
		[Description("وارد نشده")]
		Unknown = 0,
		[Description("رسمی آزمایشی")]
		Rasmiazmayeshi,
		[Description("رسمی قطعی")]
		Rasmiqatei,
		[Description("شرکتی")]
		Sherkati,
		[Description("پیمانی")]
		Peymani,
		[Description("قراردادی")]
		Qarardadi,
		[Description("تعاونی")]
		Taavoni,
		[Description("فصلی")]
		Fasli,
		[Description("خرید خدمتی")]
		Kharidkhedmati,
		[Description("روز مزد")]
		Ruzmozd,
		[Description("سایر موارد")]
		Other
	}

	[TypeConverter(typeof(EnumDescriptionTypeConverter))]
	public enum MaritalStatus
	{
		[Description("وارد نشده")]
		Unknown = 0,
		[Description("مجرد")]
		Single,
		[Description("متأهل")]
		Married,
	}

	[TypeConverter(typeof(EnumDescriptionTypeConverter))]
	public enum Sex
	{
		[Description("وارد نشده")]
		Unknown = 0,
		[Description("مذکر")]
		Male,
		[Description("مؤنث")]
		Female
	}

	[TypeConverter(typeof(EnumDescriptionTypeConverter))]
	public enum ContentType
	{
		//todo: fill enum
	}

	[TypeConverter(typeof(EnumDescriptionTypeConverter))]
	public enum VehicleType
	{
		//todo: fill enum
		[Description("وارد نشده")]
		Unknown = 0,
	}

	[TypeConverter(typeof(EnumDescriptionTypeConverter))]
	public enum ValueType
	{
		//todo: fill enum
	}

	[TypeConverter(typeof(EnumDescriptionTypeConverter))]
	public enum FormCurrentState
	{
		Unknown = 0,
		Select,
		Edit,
		Add,
		AddMaster,
		AddDetails,
		Delete
	}
}