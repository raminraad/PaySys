using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using PaySys.CalcLib.Converter;

namespace PaySys.ModelAndBindLib.Model
{
	/// <summary>#01 گروه اصلی</summary>
	public class MainGroup
	{
		public int MainGroupId { set; get; }
		public string Title { get; set; }
		public ColorPallet ItemColorPallet { get; set; }
		public virtual List<SubGroup> SubGroups { get; set; }
		public virtual List<GroupMisc> GroupMiscs { get; set; }
		public virtual List<GroupContractFieldTitle> GroupContractFieldTitles { get; set; }
		public virtual List<GroupMonthlyVariable> GroupMonthlyVariables { get; set; }
		public override bool Equals(object obj)
		{
			return obj != null && ((MainGroup)obj).MainGroupId == MainGroupId && string.Equals(Title, ((MainGroup)obj).Title) ;
		}

	}

	/// <summary>#02 زیر گروه</summary>
	public class SubGroup
	{
		public int SubGroupId { get; set; }
		public string Title { get; set; }
		public ColorPallet ItemColorPallet { get; set; }
		[Required]
		public virtual MainGroup MainGroup { set; get; }
		public virtual List<GroupSpecValue> GroupSpecValues { get; set; }
		public virtual List<MissionFormula> MissionFormulas { get; set; }
		public virtual List<TaxTable> TaxTables { get; set; }
		public virtual List<HandselFormula> HandselFormulas { get; set; }
		public virtual List<ContractMaster> ContractMasters { get; set; }
		public override bool Equals(object obj)
		{
			return (obj as SubGroup)?.SubGroupId == SubGroupId;
		}
	}

	/// <summary>#03 عناوین بدهی یا پرداختهای متفرقه</summary>
	public class MiscTitle
	{
		public int MiscTitleId { get; set; }
		public string Title { get; set; }
		public int Index { get; set; }
		public bool IsPayment { get; set; }
		public virtual List<EmployeeMiscRemain> EmployeeMiscRemains { get; set; }
		public virtual List<GroupMisc> GroupMiscs { get; set; }
	}

	/// <summary>#04 بدهی یا پرداخت های متفرقه در سال و ماه</summary>
	public class GroupMisc
	{
		public int GroupMiscId { get; set; }
		public int Year { get; set; }
		public int Month { get; set; }
		public virtual MiscTitle MiscTitle { get; set; }
		public virtual List<SpecMisc> SpecMiscs { get; set; }
		public virtual MainGroup MainGroup { set; get; }
		public virtual ExpenseArticle ExpenseArticle { set; get; }
		public virtual List<EmployeeMisc> EmployeeMiscs { get; set; }
	}

	/// <summary>#05 پرداختهای متفرقه دخیل در محاسبات مؤلفه ها</summary>
	public class SpecMisc
	{
		public int SpecMiscId { get; set; }
		public virtual GroupMisc GroupMisc { get; set; }
		public virtual GroupSpecValue GroupSpecValue { get; set; }
	}

	/// <summary>#06 عناوین مؤلفه های محاسباتی گروهی</summary>
	public class GroupSpecTitle
	{
		public int GroupSpecTitleId { get; set; }
		public string Title { get; set; }
		public string Alias { get; set; }
		public virtual List<GroupSpecValue> GroupSpecValues { get; set; }
	}

	/// <summary>#07 مقادیر مؤلفه های محاسباتی گروهی زیرگروه در سال و ماه</summary>
	public class GroupSpecValue
	{
		public int GroupSpecValueId { get; set; }
		public float Value { get; set; }
		public ContentType ContentType { get; set; }
		public virtual List<SpecContractField> SpecContractFields { get; set; }
		public virtual List<SpecMisc> SpecMiscs { get; set; }
		public virtual GroupSpecTitle GroupSpecTitle { get; set; }
		public virtual SubGroup SubGroup { get; set; }
	}

	/// <summary>#08 فیلدهای احکام دخیل در محاسبات مؤلفه ها</summary>
	public class SpecContractField
	{
		public int SpecContractFieldId { get; set; }
		public virtual GroupContractFieldTitle GroupContractFieldTitle { get; set; }
		public virtual GroupSpecValue GroupSpecValue { get; set; }
	}

	/// <summary>#09 مواد هزینه</summary>
	public class ExpenseArticle
	{
		public int ExpenseArticleId { get; set; }
		public string Title { get; set; }
		public string Code { get; set; }
		public bool IsActive { get; set; }
		public virtual List<GroupMisc> GroupMiscs { get; set; }
		public virtual List<GroupContractFieldTitle> GroupContractFieldTitles { get; set; }
		public virtual List<GroupMonthlyVariable> GroupMonthlyVariables { get; set; }
	}

	/// <summary>#10 مانده بدهی متفرقه اشخاص</summary>
	public class EmployeeMiscRemain
	{
		public int EmployeeMiscRemainId { set; get; }
		public float Value { get; set; }
		public int Year { get; set; }
		public int Month { get; set; }
		public virtual MiscTitle MiscTitle { get; set; }
		public virtual Employee Employee { get; set; }
	}

	/// <summary>#11 عناوین متغیرهای ماهانه</summary>
	public class MonthlyVariableTitle
	{
		public int MonthlyVariableTitleId { get; set; }
		public string Title { get; set; }
		public string Alias { get; set; }
		public ValueType ValueType { set; get; }
		public virtual List<GroupMonthlyVariable> GroupMonthlyVariables { get; set; }
	}

	/// <summary>#12 عناوین فیلدهای احکام</summary>
	public class ContractFieldTitle
	{
		public int ContractFieldTitleId { get; set; }
		public string Title { get; set; }
		public virtual List<GroupContractFieldTitle> GroupContractFieldTitles { get; set; }
	}

	/// <summary>#13 عناوین فیلدهای احکام گروه اصلی در سال</summary>
	public class GroupContractFieldTitle
	{
		public int GroupContractFieldTitleId { get; set; }
		public int Year { get; set; }
		public virtual ExpenseArticle ExpenseArticle { get; set; }
		public virtual List<SpecContractField> SpecContractFields { get; set; }
		public virtual MainGroup MainGroup { get; set; }
		public virtual ContractFieldTitle ContractFieldTitle { get; set; }
		public virtual List<ContractDetail> ContractDetails { get; set; }
	}

	/// <summary>#14 فرمول مأموریت زیرگروه در سال و ماه</summary> 
	public class MissionFormula
	{
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

	/// <summary>#17 عناوین متغیرهای ماهانه گروه اصلی</summary>
	public class GroupMonthlyVariable
	{
		public int GroupMonthlyVariableId { get; set; }
		public bool IsActive { get; set; }
		public virtual MonthlyVariableTitle MonthlyVariableTitle { get; set; }
		public virtual ContractFieldTitle ContractFieldTitle { get; set; }
		public virtual MainGroup MainGroup { get; set; }
		public virtual List<EmployeeMonthlyVariable> EmployeeMonthlyVariables { get; set; }
	}

	/// <summary>#18 مقادیر بدهی یا پرداختهای متفرقه برای اشخاص در سال و ماه</summary>
	public class EmployeeMisc
	{
		public int EmployeeMiscId { get; set; }
		public float Value { get; set; }
		public virtual GroupMisc GroupMisc { get; set; }
		public virtual Employee Employee { get; set; }
	}

	/// <summary>#19 مقادیر متغیرهای ماهانه برای اشخاص در سال و ماه</summary>
	public class EmployeeMonthlyVariable
	{
		public int EmployeeMonthlyVariableId { get; set; }
		public int Year { get; set; }
		public int Month { get; set; }
		public float ValueNum { get; set; }
		public float ValueStr { get; set; }
		public virtual GroupMonthlyVariable GroupMonthlyVariable { get; set; }
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
		public bool IsCurrentContract { set; get; }
		public EducationStand EducationStand { get; set; }
		public EmploymentType EmploymentType { get; set; }
		//		public JobCategory JobCat { get; set; }
		public SacrificeStand SacrificeStand { get; set; }
		public string AccountNoGov { get; set; }
		public string AccountNoEmp { get; set; }
		public virtual List<ContractDetail> ContractDetails { get; set; }
		public virtual Job Job { get; set; }
		public virtual SubGroup SubGroup { set; get; }
		public virtual Employee Employee { get; set; }
	}

	/// <summary>#21 جزئیات احکام</summary>
	public class ContractDetail
	{
		public int ContractDetailId { get; set; }
		public float Value { get; set; }
		public virtual ContractMaster ContractMaster { get; set; }
		public virtual GroupContractFieldTitle GroupContractFieldTitle { get; set; }
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
		public string IdCardNo { get; set; }
		public string IdCardExportPlace { get; set; }
		public string IdCardExportDate { set; get; }
		public virtual List<ContractMaster> ContractMaster { get; set; }
		public virtual List<EmployeeMiscRemain> EmployeeMiscRemain { get; set; }
		public virtual List<Mission> Mission { get; set; }
		public virtual List<EmployeeMisc> EmployeeMisc { get; set; }
		public virtual List<EmployeeMonthlyVariable> EmployeeMonthlyVariable { get; set; }
		//		public virtual ContractMaster CurrentContract { get; set; }

		public override bool Equals(object obj)
		{
			return obj != null && ((Employee)obj).EmployeeId == EmployeeId;
		}
	}

	/// <summary>#23 فرمول عیدی</summary>
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
		//		public int ContractDifferenceId { get; set; }
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
		public ColorPallet ItemColorPallet { get; set; }
		public string Description { get; set; }
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
		public virtual City City { get; set; }
		public virtual Employee Employee { get; set; }
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

	//	public enum JobCategory
	//	{
	//		//todo: fill enum
	//	}

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

	public enum ValueType
	{
		//todo: fill enum
	}

	public enum FormCurrentState
	{
		Unknown = 0,
		Select,
		Edit,
		Add,
		Delete
	}
}