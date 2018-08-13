#region

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using PaySys.CalcLib.Converters;
using PaySys.Globalization;

#endregion

namespace PaySys.ModelAndBindLib.Model
{
	/// <summary> #01 گروه اصلی </summary>
	public class MainGroup
	{
		public int MainGroupId { set; get; }

		public string Title { get; set; }

		public ColorPallet ItemColor { get; set; }

		public virtual List<SubGroup> SubGroups { get; set; }

		public override bool Equals( object obj ) { return ( obj as MainGroup )?.MainGroupId == MainGroupId && string.Equals( Title, ( (MainGroup) obj ).Title ); }
	}

	/// <summary> #02 زیرگروه </summary>
	public class SubGroup
	{
		public int SubGroupId { get; set; }

		public string Title { get; set; }

		public ColorPallet ItemColor { get; set; }

		public bool Is31 { set; get; }

		[Required]
		public virtual MainGroup MainGroup { set; get; }

		public virtual List<Parameter> Parameters { set; get; }

		public virtual List<MissionFormula> MissionFormulas { get; set; }

		public virtual List<TaxTable> TaxTables { get; set; }

		public virtual List<HandselFormula> HandselFormulas { get; set; }

		public virtual List<ContractMaster> ContractMasters { get; set; }

		public virtual List<Misc> Miscs { get; set; }

		public virtual List<SubGroupVariable> SubGroupVariables { get; set; }

		public virtual List<ContractField> ContractFields { get; set; }

		[NotMapped]
		public List<Misc> MiscsOfTypePayment => Miscs.Where( misc => misc.MiscTitle.IsPayment ).ToList();

		[NotMapped]
		public List<Misc> MiscsOfTypeDebt => Miscs.Where( misc => !misc.MiscTitle.IsPayment ).ToList();

		[NotMapped]
		public TaxTable CurrenTaxTable
		{
			get => TaxTables.FirstOrDefault( table => table.Year == PaySysSetting.CurrentYear );
			set
			{
				var taxTable = TaxTables.FirstOrDefault( table => table.Year == PaySysSetting.CurrentYear );
				if( taxTable != null )
					taxTable = value;
			}
		}

		[NotMapped]
		public HandselFormula CurrenHandselFormula
		{
			get => HandselFormulas.FirstOrDefault( table => table.Year == PaySysSetting.CurrentYear );
			set
			{
				var handselFormula = HandselFormulas.FirstOrDefault( table => table.Year == PaySysSetting.CurrentYear );
				if( handselFormula != null )
					handselFormula = value;
			}
		}

		[NotMapped]
		public MissionFormula CurrenMissionFormula
		{
			get => MissionFormulas.FirstOrDefault( table => table.Year == PaySysSetting.CurrentYear );
			set
			{
				var MissionFormula = MissionFormulas.FirstOrDefault( table => table.Year == PaySysSetting.CurrentYear );
				if( MissionFormula != null )
					MissionFormula = value;
			}
		}

		[NotMapped]
		public IEnumerable<Employee> CurrentEmployees
		{
			get { return ContractMasters.Where( master => master.IsCurrent ).Select( master => master.Employee ).ToList(); }
		}

		[NotMapped]
		public IEnumerable<MiscRecharge> CurrentMiscRecharges
		{
			get
			{
				var currentContractsOfSubGroup = ContractMasters.Where( master => master.IsCurrent );
				var currentEmployeesOfSubGroup = currentContractsOfSubGroup.Select( m => m.Employee );
				var miscRechargesOfCurrentEmployees = currentEmployeesOfSubGroup.SelectMany( employee => employee.MiscRecharges );
				return miscRechargesOfCurrentEmployees;
			}
		}

		[NotMapped]
		public IEnumerable<Misc> CurrentMiscs=>Miscs.Where( m => m.Year == PaySysSetting.CurrentYear );

		[NotMapped]
		public IEnumerable<Misc> CurrentMiscPayments => Miscs.Where( m => m.Year == PaySysSetting.CurrentYear && m.MiscTitle.IsPayment );

		[NotMapped]
		public IEnumerable<Misc> CurrentMiscDebts => Miscs.Where( m => m.Year == PaySysSetting.CurrentYear && !m.MiscTitle.IsPayment );

		[NotMapped]
		public IEnumerable<SubGroupVariable> CurrentVariables=>SubGroupVariables.Where( v => v.IncludesCurrentDate );

		[NotMapped]
		public List<MiscRecharge> TempMiscRechargesOfEmployees { set; get; }
		[NotMapped]
		public List<MiscValueForEmployee> TempMiscValuesOfEmployees { set; get; }
		[NotMapped]
		public List<VariableValueForEmployee> TempVariableValuesOfEmployees { set; get; }
		public override bool Equals( object obj ) { return ( obj as SubGroup )?.SubGroupId == SubGroupId && string.Equals( Title, ( (SubGroup) obj ).Title ); }
	}

	/// <summary> #03 عناوین کسور و پرداختهای متفرقه </summary>
	public class MiscTitle
	{
		public int MiscTitleId { get; set; }

		public string Title { get; set; }

		public bool IsPayment { get; set; }

		public virtual List<Misc> Miscs { get; set; }
	}

	/// <summary> #04 کسور و پرداختهای متفرقه زیرگروه در سال </summary>
	public class Misc
	{
		public int MiscId { get; set; }

		public int Year { get; set; }

		public int Month { get; set; }

		public int Index { get; set; }

		public virtual List<ParameterInvolvedMisc> ParameterInvolvedMiscs { get; set; }

		public virtual List<MiscValueForEmployee> MiscValueForEmployees { get; set; }

		public virtual List<MiscRecharge> MiscRecharges { get; set; }

		public virtual MiscTitle MiscTitle { get; set; }

		public virtual SubGroup SubGroup { set; get; }

		public virtual ExpenseArticle ExpenseArticle { set; get; }

		public override bool Equals( object obj )
		{
			if( obj?.GetType() != GetType() )
				return false;

			return ( (Misc) obj ).MiscId == MiscId;
		}
	}

	/// <summary> #05 پرداختهای متفرقه دخیل در محاسبات مؤلفه ها </summary>
	public class ParameterInvolvedMisc
	{
		public int ParameterInvolvedMiscId { get; set; }

		public virtual Misc Misc { get; set; }

		public virtual Parameter Parameter { get; set; }
	}

	/// <summary> #07 مقادیر مؤلفه های محاسباتی زیرگروه در سال و ماه </summary>
	public class Parameter
	{
		public int ParameterId { get; set; }

		public double Value { get; set; }

		public ValueType ValueType { get; set; }

		public string Title { get; set; }

		public string Alias { get; set; }

		public int Year { get; set; }

		public int Month { get; set; }

		public virtual List<ParameterInvolvedMisc> ParameterInvolvedMiscs { set; get; }

		public virtual List<ParameterInvolvedContractField> ParameterInvolvedContractFields { get; set; }

		public virtual SubGroup SubGroup { get; set; }
	}

	/// <summary> #08 فیلدهای احکام دخیل در محاسبات مؤلفه ها </summary>
	public class ParameterInvolvedContractField
	{
		public int ParameterInvolvedContractFieldId { get; set; }

		public virtual ContractField ContractField { get; set; }

		public virtual Parameter Parameter { get; set; }
	}

	/// <summary> #09 مواد هزینه </summary>
	public class ExpenseArticle
	{
		public int ExpenseArticleId { get; set; }

		public string Title { get; set; }

		public string Code { get; set; }

		public bool IsActive { get; set; }

		public virtual List<Misc> Miscs { get; set; }

		public virtual List<ExpenseArticleOfContractFieldForSubGroup> ExpenseArticleOfContractFieldForSubGroups { get; set; }

		public virtual List<SubGroupVariable> SubGroupVariables { get; set; }

		[NotMapped]
		public string CodeTitle => $"{Code}:{Title}";

		[NotMapped]
		public ObservableCollection<TitledCompositeCollection> InvolversUngrouped
		{
			get
			{
				//todo: Apply current date in following calculations


				var result = new ObservableCollection<TitledCompositeCollection>();
				var ccContractFields = new TitledCompositeCollection
				{
					Title = ResourceAccessor.Labels.GetString( "ContractFields" ),
					CompositeCollection = new CompositeCollection()
				};
				if( ExpenseArticleOfContractFieldForSubGroups != null )
					foreach( var item in ExpenseArticleOfContractFieldForSubGroups.ToList() )
						ccContractFields.CompositeCollection.Add( item.ContractField );

				result.Add( ccContractFields );


				var ccMiscs = new TitledCompositeCollection
				{
					Title = ResourceAccessor.Labels.GetString( "MiscPayments" ),
					CompositeCollection = new CompositeCollection()
				};

				foreach( var item in Miscs )
					ccMiscs.CompositeCollection.Add( item );

				result.Add( ccMiscs );


				var ccVariables = new TitledCompositeCollection
				{
					Title = ResourceAccessor.Labels.GetString( "MonthlyVariables" ),
					CompositeCollection = new CompositeCollection()
				};

				
				foreach( var item in this.SubGroupVariables )
					ccVariables.CompositeCollection.Add( item );

				result.Add( ccVariables );
				return result;
			}
		}

		[NotMapped]
		public int InvolversCount
		{
			get
			{
				//todo: Apply current date in following calculations
				var count = 0;
				if( ExpenseArticleOfContractFieldForSubGroups != null )
					count += ExpenseArticleOfContractFieldForSubGroups.Count;
				if( Miscs != null )
					count += Miscs.Count;
				if( SubGroupVariables != null )
					count += SubGroupVariables.Count;
				return count;
			}
		}
	}

	/// <summary> #10 مانده بدهی متفرقه اشخاص </summary>
	public class MiscRecharge
	{
		public int MiscRechargeId { set; get; }

		public double Value { get; set; }

		public int Year { get; set; }

		public int Month { get; set; }

		public virtual Misc Misc { get; set; }

		public virtual Employee Employee { get; set; }
	}

	/// <summary> #13 فیلدهای احکام زیرگروه در سال </summary>
	public class ContractField
	{
		public int ContractFieldId { get; set; }

		public string Title { get; set; }

		public int Year { get; set; }

		public virtual SubGroup SubGroup { get; set; }

		public virtual List<ParameterInvolvedContractField> ParameterInvolvedContractFields { get; set; }

		public virtual List<ContractDetail> ContractDetails { get; set; }
		public virtual List<MissionFormulaInvolvedContractField> MissionFormulaInvolvedContractFields { get; set; }

		public virtual List<ExpenseArticleOfContractFieldForSubGroup> ExpenseArticleOfContractFieldForSubGroups { get; set; }

		[NotMapped]
		public ExpenseArticle CurrentExpenseArticle
		{
			get => ExpenseArticleOfContractFieldForSubGroups?.FirstOrDefault( exp => exp.Month == PaySysSetting.CurrentMonth )?.ExpenseArticle;
			set
			{
				if( CurrentExpenseArticle != null )
				{
					ExpenseArticleOfContractFieldForSubGroups.FirstOrDefault( exp => exp.Month == PaySysSetting.CurrentMonth ).ExpenseArticle = value;
				}
				else
				{
					var newLink = new ExpenseArticleOfContractFieldForSubGroup
					{
						ContractField = this,
						Month = PaySysSetting.CurrentMonth,
						ExpenseArticle = value
					};
					if( ExpenseArticleOfContractFieldForSubGroups == null )
						ExpenseArticleOfContractFieldForSubGroups = new List<ExpenseArticleOfContractFieldForSubGroup>();
					ExpenseArticleOfContractFieldForSubGroups.Add( newLink );
				}
			}
		}
	}

	/// <summary> #14 فرمول مأموریت زیرگروه در سال و ماه </summary>
	public class MissionFormula
	{
		public int MissionFormulaId { get; set; }

		public double DivideFactor { get; set; }

		public double AddFactor { get; set; }

		public double MaxFactor { get; set; }

		public double SubtractFactor { get; set; }

		public double PerKmFactor { get; set; }

		public int Year { get; set; }

		public int Month { get; set; }

		public virtual SubGroup SubGroup { get; set; }

		public virtual List<MissionFormulaInvolvedContractField> MissionFormulaInvolvedContractFields { get; set; }
	}

	/// <summary> #15 جدول مالیات </summary>
	public class TaxTable
	{
		public int TaxTableId { get; set; }

		public int Year { get; set; }

		public int Month { get; set; }

		public virtual SubGroup SubGroup { get; set; }

		public virtual List<TaxRow> TaxRows { get; set; }

		[NotMapped]
		public virtual ObservableCollection<TaxRow> TaxRowsForGrid
		{
			get
			{
				double previousTo = 0;
				TaxRows.Sort();
				foreach( var taxRow in TaxRows )
				{
					taxRow.ValueFrom = previousTo;
					previousTo = taxRow.ValueTo;
				}

				return new ObservableCollection<TaxRow>( TaxRows );
			}
			set => TaxRows = new List<TaxRow>( value );
		}
	}

	/// <summary> #16 سطر جدول مالیات </summary>
	public class TaxRow : IComparable
	{
		public int TaxRowId { get; set; }

		public double ValueTo { get; set; }

		public double Factor { get; set; }

		public virtual TaxTable TaxTable { get; set; }

		[NotMapped]
		public double ValueFrom { get; set; }

		[NotMapped]
		public double TaxValue { get; set; } //Todo: Calculate this field

		#region Implementation of IComparable

		public int CompareTo( object obj ) { return ValueTo.CompareTo( ( (TaxRow) obj ).ValueTo ); }

		#endregion
	}

	/// <summary> #18 مقادیر کسور و پرداختهای متفرقه برای اشخاص در سال و ماه </summary>
	public class MiscValueForEmployee
	{
		public int MiscValueForEmployeeId { get; set; }

		public double Value { get; set; }

		public double ValueSubtraction { get; set; }

		public int Year { get; set; }

		public int Month { get; set; }

		public virtual Misc Misc { get; set; }

		public virtual Employee Employee { get; set; }
	}

	/// <summary> #20 پایه حکم </summary>
	public class ContractMaster
	{
		public int ContractMasterId { get; set; }

		public string ContractNo { get; set; }

		public DateTime DateExport { get; set; }

		public DateTime DateExecution { get; set; }

		public DateTime DateEmployment { get; set; }

		public MaritalStatus MaritalStatus { get; set; }

		public double HardshipFactor { get; set; }

		public string InsuranceNo { get; set; }

		public EducationStand EducationStand { get; set; }

		public EmploymentType EmploymentType { get; set; }

		public SacrificeStand SacrificeStand { get; set; }

		public string AccountNoGov { get; set; }

		public string AccountNoEmp { get; set; }

		public bool IsCurrent { set; get; }

		public virtual SubGroup SubGroup { set; get; }

		public virtual Employee Employee { get; set; }

		public virtual Job Job { get; set; }

		public virtual List<Mission> Missions { get; set; }

		public virtual List<ContractDetail> ContractDetails { get; set; }

		public override bool Equals( object obj ) { return obj is ContractMaster && ( (ContractMaster) obj ).ContractMasterId == ContractMasterId && string.Equals( ContractNo, ( (ContractMaster) obj ).ContractNo ); }
	}

	/// <summary> #21 جزئیات احکام </summary>
	public class ContractDetail
	{
		public int ContractDetailId { get; set; }

		public double Value { get; set; }

		public virtual ContractField ContractField { get; set; }

		public virtual ContractMaster ContractMaster { get; set; }
	}

	/// <summary> #22 اشخاص </summary>
	public class Employee
	{
		public int EmployeeId { get; set; }

		public string FName { get; set; }

		public string LName { get; set; }

		public string NationalCardNo { set; get; }

		public DateTime BirthDate { get; set; }

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

		public DateTime IdCardExportDate { set; get; }

		public string IdCardNo { get; set; }

		public virtual List<ContractMaster> ContractMasters { get; set; }

		public virtual List<MiscRecharge> MiscRecharges { get; set; }

		public virtual List<MiscValueForEmployee> MiscValueForEmployees { get; set; }

		public virtual List<VariableValueForEmployee> VariableValueForEmployees { get; set; }

		[NotMapped]
		public IEnumerable<Mission> MissionsOfCurrentYear => ContractMasters.SelectMany( c => c.Missions.Where( m => m.StartDate >= PaySysSetting.CurrentYearStartGreg && m.StartDate <= PaySysSetting.CurrentYearEndGreg ) );

		[NotMapped]
		public string FullName => $"{EmployeeId} : {FName} {LName}"; //Todo: Remove EmployeeId from result

		[NotMapped]
		public string LuffName => $"{LName} {FName}";

		public override bool Equals( object obj )
		{
			if( obj?.GetType() != GetType() )
				return false;

			return ( (Employee) obj ).EmployeeId == EmployeeId;
		}
	}

	/// <summary> #23 فرمول عیدی در سال و ماه </summary>
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

	/// <summary> #24 تفاوت احکام </summary>
	public class ContractDifference
	{
		[Key]
		[ForeignKey( "Contract1St" )]
		public int ContractMasterId { get; set; }

		public DateTime DateFrom { get; set; }

		public DateTime DateTo { get; set; }

		public double FirstMonth { get; set; }

		public int PayYear { get; set; }

		public int PayMonth { get; set; }

		public virtual ContractMaster Contract1St { get; set; }

		public virtual ContractMaster Contract2Nd { get; set; }
	}

	/// <summary> #25 شغل </summary>
	public class Job
	{
		public int JobId { get; set; }

		public string Title { get; set; }

		public string JobNo { get; set; }

		public string Description { get; set; }

		public ColorPallet ItemColor { get; set; }

		public virtual List<ContractMaster> ContractMasters { get; set; }
	}

	/// <summary> #26 شهر </summary>
	public class City
	{
		public int CityId { get; set; }

		public string Title { get; set; }

		public int Distance { get; set; }

		public double Percentage { get; set; }

		public virtual List<Mission> Missions { get; set; }
	}

	/// <summary> #27 مأموریت </summary>
	public class Mission
	{
		public int MissionId { get; set; }

		public string Title { get; set; }

		public DateTime StartDate { get; set; }

		public DateTime EndDate { get; set; }

		public DateTime StartTime { get; set; }

		public DateTime EndTime { get; set; }

		/// <summary> مدت با بیتوته </summary>
		public int AmountResident { get; set; }

		/// <summary> مدت بدون بیتوته </summary>
		public int AmountNonResident { get; set; }

		public VehicleType VehicleType { set; get; }

		/// <summary> هزینه سفر </summary>
		public double TravelExpense { get; set; }

		public string MissionContractNo { get; set; }

		public virtual ContractMaster ContractMaster { get; set; }

		public virtual City City { get; set; }
	}

	/// <summary> #30 مواد هزینه عناوین فیلدهای احکام برای زیرگروه در ماه </summary>
	public class ExpenseArticleOfContractFieldForSubGroup
	{
		public int ExpenseArticleOfContractFieldForSubGroupId { get; set; }

		public int Month { get; set; }

		public virtual ExpenseArticle ExpenseArticle { set; get; }

		public virtual ContractField ContractField { set; get; }

	}

	/// <summary> #34 فیلدهای احکام دخیل در فرمول مأموریت </summary>
	public class MissionFormulaInvolvedContractField
	{
		public int MissionFormulaInvolvedContractFieldId { get; set; }

		public virtual ContractField ContractField { get; set; }

		public virtual MissionFormula MissionFormula { get; set; }
	}

	/// <summary> #35 عناوین متغیرهای ماهانه </summary>
	public class VariableTitle
	{
		public int VariableTitleId { get; set; }

		public string Title { get; set; }

		public string Alias { get; set; }

		public ValueType ValueType { get; set; }

		public virtual List<SubGroupVariable> SubGroupVariables { get; set; }

	}

	/// <summary> #36 متغیرهای ماهانه زیرگروه در بازه زمانی </summary>
	public class SubGroupVariable
	{
		public int SubGroupVariableId { get; set; }

		public int FromYear { get; set; }

		public int FromMonth { get; set; }

		public int ToYear { get; set; }

		public int ToMonth { get; set; }
		public virtual ExpenseArticle ExpenseArticle { set; get; }

		public virtual SubGroup SubGroup { get; set; }

		public virtual VariableTitle VariableTitle { get; set; }
		public virtual List<VariableValueForEmployee> VariableValueForEmployees { get; set; }

		public bool IncludesDate( int year, int month )
		{
			var fromDate = ( FromYear * 100 ) + FromMonth;
			var toDate = ( ToYear * 100 ) + ToMonth;
			var currentDate = (year * 100) + month;
			return currentDate >= fromDate && currentDate <= toDate;
		}

		[NotMapped]
		public bool IncludesCurrentDate => IncludesDate( PaySysSetting.CurrentYear, PaySysSetting.CurrentMonth );
	}

	/// <summary> #37 متغیرهای ماهانه برای اشخاص </summary>
	public class VariableValueForEmployee
	{
		public int VariableValueForEmployeeId { get; set; }

		public double? NumericValue { get; set; }

		public string StringValue { get; set; }

		public int Year { get; set; }

		public int Month { get; set; }
		
		public DateTime? DateValue { get; set; }

		public virtual Employee Employee { get; set; }

		public virtual SubGroupVariable SubGroupVariable { get; set; }

		[NotMapped]
		public object Value
		{
			get
			{
				switch (SubGroupVariable.VariableTitle.ValueType)
				{
					case ValueType.Unknown:
						return null;
					case ValueType.Absolute:
					case ValueType.Percent:
						return NumericValue;
					case ValueType.Date:
						return DateValue;
					case ValueType.String:
						return StringValue;
					default:
						return null;
				}
			}
			set
			{
				switch (SubGroupVariable.VariableTitle.ValueType)
				{
					case ValueType.Unknown:
						break;
					case ValueType.Absolute:
					case ValueType.Percent:
						if( value != null )
							NumericValue = Convert.ToDouble( value );
						else
							NumericValue = null;
						break;
					case ValueType.Date:
						if( value != null )
							DateValue = Convert.ToDateTime( value );
						else
							DateValue = null;
						break;
					case ValueType.String:
						StringValue = value?.ToString();
						break;
				}
			}
		}

		[NotMapped]
		public Visibility ValueIsNumeric => (SubGroupVariable.VariableTitle.ValueType == ValueType.Absolute || SubGroupVariable.VariableTitle.ValueType == ValueType.Percent)?Visibility.Visible : Visibility.Collapsed;

		[NotMapped]
		public Visibility ValueIsString => SubGroupVariable.VariableTitle.ValueType == ValueType.String?Visibility.Visible : Visibility.Collapsed;
		[NotMapped]
		public Visibility ValueIsDate => SubGroupVariable.VariableTitle.ValueType == ValueType.Date?Visibility.Visible : Visibility.Collapsed;

	}

	[TypeConverter( typeof(EnumDescriptionTypeConverter) )]
	public enum SacrificeStand
	{
		[Description( "وارد نشده" )]
		Unknown = 0,
		[Description( "رزمنده" )]
		Razmande,
		[Description( "آزاده" )]
		Azade,
		[Description( "خانواده آزاده" )]
		KhanevadeAzade,
		[Description( "جانباز" )]
		Janbaz,
		[Description( "خانواده جانباز" )]
		KhanevadeJanbaz,
		[Description( "خانواده شهید" )]
		KhanevadeShahid,
		[Description( "ایثارگر" )]
		Isargar,
		[Description( "سایر موارد" )]
		Other
	}

	[TypeConverter( typeof(EnumDescriptionTypeConverter) )]
	public enum ColorPallet
	{
		[Description( "وارد نشده" )]
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

	[TypeConverter( typeof(EnumDescriptionTypeConverter) )]
	public enum EducationStand
	{
		[Description( "وارد نشده" )]
		Unknown = 0,
		[Description( "بی سواد" )]
		Bisavad,
		[Description( "سیکل" )]
		Sikl,
		[Description( "زیر دیپلم" )]
		Zildiplom,
		[Description( "دیپلم" )]
		Diplom,
		[Description( "کاردانی" )]
		Kardani,
		[Description( "کارشناسی" )]
		Karshenasi,
		[Description( "فوق لیسانس" )]
		Foqlisans,
		[Description( "دکترا" )]
		Doctora,
		[Description( "فوق دکترا" )]
		Foqoctora,
		[Description( "سایر موارد" )]
		Other
	}

	[TypeConverter( typeof(EnumDescriptionTypeConverter) )]
	public enum EmploymentType
	{
		[Description( "وارد نشده" )]
		Unknown = 0,
		[Description( "رسمی آزمایشی" )]
		Rasmiazmayeshi,
		[Description( "رسمی قطعی" )]
		Rasmiqatei,
		[Description( "شرکتی" )]
		Sherkati,
		[Description( "پیمانی" )]
		Peymani,
		[Description( "قراردادی" )]
		Qarardadi,
		[Description( "تعاونی" )]
		Taavoni,
		[Description( "فصلی" )]
		Fasli,
		[Description( "خرید خدمتی" )]
		Kharidkhedmati,
		[Description( "روز مزد" )]
		Ruzmozd,
		[Description( "سایر موارد" )]
		Other
	}

	[TypeConverter( typeof(EnumDescriptionTypeConverter) )]
	public enum MaritalStatus
	{
		[Description( "وارد نشده" )]
		Unknown = 0,
		[Description( "مجرد" )]
		Single,
		[Description( "متأهل" )]
		Married
	}

	[TypeConverter( typeof(EnumDescriptionTypeConverter) )]
	public enum Sex
	{
		[Description( "وارد نشده" )]
		Unknown = 0,
		[Description( "مذکر" )]
		Male,
		[Description( "مؤنث" )]
		Female
	}

	[TypeConverter( typeof(EnumDescriptionTypeConverter) )]
	public enum ValueType
	{
		[Description( "وارد نشده" )]
		Unknown = 0,
		[Description( "مطلق" )]
		Absolute,
		[Description( "درصد" )]
		Percent,
		[Description( "تاریخ" )]
		Date,
		[Description( "رشته" )]
		String,
	}

	[TypeConverter( typeof(EnumDescriptionTypeConverter) )]
	public enum VehicleType
	{
		//todo: fill enum
		[Description( "وارد نشده" )]
		Unknown = 0,
		[Description( "تاکسی" )]
		Cap,
		[Description( "هواپیما" )]
		Plane,
		[Description( "قطار" )]
		Train,
		[Description( "خودرو شخصی" )]
		Personal,
		[Description( "نقلیه عمومی" )]
		PublicTransport
	}

	[TypeConverter( typeof(EnumDescriptionTypeConverter) )]
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

	public enum MiscType
	{
		None,
		Payment,
		Debt
	}

	public enum NavigationType
	{
		First = -2,
		Previous = -1,
		None = 0,
		Next = 1,
		Last = 2
	}
}