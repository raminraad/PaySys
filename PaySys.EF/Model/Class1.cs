using System.Collections;
using System.Collections.Generic;

namespace PaySysModel
{
	public class MainGroup
	{
		public int MainGroupId { set; get; }
		public string Title { get; set; }
		public IList<SubGroup> SubGroups { get; set; }
		public IList<ContractFieldTitle> ContractFieldTitles { get; set; }
	}

	public class SubGroup
	{
		public int SubGroupId { get; set; }
		public virtual MainGroup MainGroup { set; get; }
	}

	public class ContractFieldTitle
	{
		public int ContractFieldTitleId { get; set; }
		public string Title { get; set; }
		public virtual MainGroup MainGroup { set; get; }
		public bool IsCalcAuto { get; set; }
		public virtual RetirementFormField RetirementFormField { get; set; }
	}

	public class ContractFieldValue
	{
		public int ContractFieldValueId { get; set; }

		//Rtodo: Relation
		public virtual ContractFieldTitle ContractFieldTitle { get; set; }

		//Rtodo: Relation
		public virtual Contract Contract { get; set; }

		public double Value { get; set; }
	}

	public enum EducationStand
	{
		//todo: fill enum
	}

	public enum EmploymentType
	{
		//todo: fill enum
	}

	public enum JobCategory
	{
		//todo: fill enum
	}

	public enum Sex
	{
		//todo: fill enum
	}

	public enum ContentType
	{
		//todo: fill enum
	}

	public enum VehicleType
	{
		//todo: fill enum
	}

	public class Contract
	{
		public int ContractId { get; set; }

		//Rtodo: Relation
		public virtual SubGroup SubGroup { set; get; }

		//Rtodo: Relation
		public virtual Employee Employee { get; set; }

		public string ContractNo { get; set; }
		public string DateExport { get; set; }
		public string DateExecution { get; set; }
		public string DateEmployment { get; set; }

		public bool IsMarried { get; set; }

		//Rtodo: Relation
		public virtual Job Job { get; set; }

		public double HardshipFactor { get; set; }
		public string InsuranceNo { get; set; }
		public EducationStand EducSt { get; set; }
		public EmploymentType EmpType { get; set; }
		public JobCategory JobCat { get; set; }
	}

	public class Employee
	{
		public int EmployeeId { get; set; }
		public string FName { get; set; }
		public string LName { get; set; }

		public string NationalCardNo { set; get; }
		public string BirthCertificateNo { get; set; }
		public string DateBirth { get; set; }
		public string BirthCertificatePlace { get; set; }
		public string BirthPlace { get; set; }
		public string FatherName { get; set; }
		public string HomeTel { get; set; }
		public string Address { get; set; }
		public string DossierNo { get; set; }
		public string BirthCertificateDate { set; get; }
		public Sex Sex { set; get; }
		public string CellNo { get; set; }
		public string PostalCode { get; set; }
	}

	public class City
	{
		public int CityId { get; set; }
		public string Title { get; set; }
		public int Distance { get; set; }
		public int Percentage { get; set; }
	}

	public class GroupVariableValue
	{
		public int GroupVariableValueId { get; set; }

		//Rtodo: Relation
		public virtual GroupValueTitle GroupValueTitle { get; set; }

		//Rtodo: Relation
		public virtual MainGroup MainGroup { get; set; }

		public double Value { get; set; }
		public ContentType ContentType { get; set; }
	}

	public class GroupValueTitle
	{
		public int GroupValueTitleId { set; get; }
		public string Title { get; set; }
		public string Alias { get; set; }
	}

	public class GroupParameterTitle
	{
		public int GroupParameterTitleId { get; set; }
		public string Title { get; set; }
		public string Alias { get; set; }
	}

	public class GroupParameterValue
	{
		public int GroupParameterValueId { get; set; }

		//Rtodo: Relation
		public virtual GroupParameterTitle GroupParameterTitle { get; set; }

		//Rtodo: Relation
		public virtual ContractFieldTitle ContractFieldTitle { get; set; }

		public bool Value { get; set; }
	}

	public class Mission
	{
		public int MissionId { get; set; }

		//Rtodo: Relation
		public virtual Contract Contract { get; set; }

		//Rtodo: Relation
		public virtual City City { get; set; }

		public string Title { get; set; }
		public string DateStart { get; set; }
		public string DateEnd { get; set; }
		public string TimeStart { get; set; }
		public string TimeEnd { get; set; }
		public int AmountResident { get; set; }
		public int AmountNonResident { get; set; }

		public VehicleType VehicleType { set; get; }
		public double TravelerExpense { get; set; }
	}

	public class Job
	{
		public int JobId { get; set; }
		public string Title { get; set; }
		public string JobNo { get; set; }

		public string Alias { get; set; }

		//Rtodo: Relation
		public virtual int Parentjob { get; set; }

		public int MaxOccupy { get; set; }

		//Rtodo: Relation
		public virtual Department Department { get; set; }

		public string Description { get; set; }
	}

	public class Department
	{
		public int DepartmentId { get; set; }

		//Rtodo: Relation
		public virtual Department ParentDepartment { get; set; }

		public string Title { get; set; }
		public string DepartmentNo { get; set; }
		public string Alias { get; set; }
	}

	public class Tax
	{
		public int TaxId { get; set; }

		public string Title { get; set; }

		//Rtodo: Relation
		public virtual MainGroup MainGroup { get; set; }

		public string Alias { get; set; }
		public string DateAppliance { get; set; }
	}

	public class TaxItem
	{
		public int TaxItemId { get; set; }
		public double ValueTo { get; set; }
		public double Factor { get; set; }
	}

	public class ExpenseArticle
	{
		public int ExpenseArticleId { get; set; }
		public string Title { get; set; }
		public string Code { get; set; }
	}

	public class MainGroupExpenseArticle
	{
		public int MainGroupExpenseArticleId { get; set; }

		//Rtodo: Relation
		public virtual ExpenseArticle ExpenseArticle { get; set; }

		//Rtodo: Relation
		public virtual MainGroup MainGroup { get; set; }
	}

	public class RetirementFormField
	{
		public int RetirementFormFieldId { get; set; }
		public string Title { get; set; }
		public IList<ContractFieldTitle> ContractFieldTitles { get; set; }
	}

	public class AdditionalDeductionOrBenefit
	{
		public int AdditionalDeductionOrBenefitId { get; set; }
		public string Title { get; set; }
		public int Index { get; set; }
		public bool IsBenefit { get; set; }
		public string Alias { get; set; }
		public bool IsActive { get; set; }
	}

	public class AddDedBenMainGroup
	{
		public int AddDedBenMainGroupId { get; set; }

		//Rtodo: Relation
		public virtual AdditionalDeductionOrBenefit AdditionalDeductionOrBenefit { get; set; }

		//Rtodo: Relation
		public virtual MainGroup MainGroup { get; set; }

		public int ExecYear { get; set; }
		public int ExecMonth { get; set; }
	}

	public class AddDedBenEmployee
	{
		public int AddDedBenEmployeeId { get; set; }

		//Rtodo: Relation
		public virtual AdditionalDeductionOrBenefit AdditionalDeductionOrBenefit { get; set; }

		//Rtodo: Relation
		public virtual Employee Employee { get; set; }

		public double ValueCurrent { get; set; }
		public double ValueRemain { get; set; }
		public int ExecYear { get; set; }
		public int ExecMonth { get; set; }
	}

	public class AddDedBenGroupParameterTitle
	{
		public int AddDedBenGroupParameterTitleId { get; set; }

		//Rtodo: Relation
		public virtual GroupParameterTitle GroupParameterTitle { get; set; }

		//Rtodo: Relation
		public virtual AdditionalDeductionOrBenefit AdditionalDeductionOrBenefit { get; set; }

		public bool Value { get; set; }
	}

	public class MonthlyData
	{
		public int MonthlyDataId { get; set; }
		public string Title { get; set; }
		public string Alias { get; set; }
		public bool IsActive { get; set; }
	}

	public class MonthlyDataMainGroup
	{
		public int MonthlyDataMainGroupId { get; set; }

		//Rtodo: Relation
		public virtual MonthlyData MonthlyData { get; set; }

		//Rtodo: Relation
		public virtual MainGroup MainGroup { get; set; }

		public int ExecYear { get; set; }
		public int ExecMonth { get; set; }
	}

	public class MonthlyDataContract
	{
		public int MonthlyDataContractId { get; set; }

		//Rtodo: Relation
		public virtual MonthlyData MonthlyData { get; set; }

		//Rtodo: Relation
		public virtual Contract Contract { get; set; }

		public int ExecYear { get; set; }
		public int ExecMonth { get; set; }
		public double Value { get; set; }
	}

	public class ContractDifference
	{
		public int ContractDifferenceId { get; set; }

		//Rtodo: Relation
		public virtual Contract Contract { get; set; }

		public string DateFrom { get; set; }
		public string DateTo { get; set; }
		public double FirstMonth { get; set; }
		public int PayYear { get; set; }
		public int PayMonth { get; set; }
	}

	public class HandselFormula
	{
		public int HandselFormulaId { get; set; }

		//Rtodo: Relation
		public virtual MainGroup MainGroup { get; set; }

		public int ExecYear { get; set; }
		public int DaysCount { get; set; }
		public double Min { get; set; }
		public double Max { get; set; }
		public double TaxFreeValue { get; set; }
		public int TaxRate { get; set; }
	}

	public class HandselInclusion
	{
		public int HandselInclusionId { get; set; }

		//Rtodo: Relation
		public virtual Employee Employee { get; set; }

		public int ExecYear { get; set; }
		public int InclusionValue { get; set; }
	}

	public class HandselAdvance
	{
		public int HandselAdvanceId { get; set; }

		//Rtodo: Relation
		public virtual Employee Employee { get; set; }

		public double Value { get; set; }
		public int ExecYear { get; set; }
	}

	public class ExpArtCntFieldTitle
	{
		public int ExpArtCntFieldTitleId { get; set; }

		//Rtodo: Relation
		public virtual ContractFieldTitle ContractFieldTitle { get; set; }

		//Rtodo: Relation
		public virtual ExpenseArticle ExpenseArticle { get; set; }
	}

	public class ExpArtAddDedBen
	{
		public int ExpArtAddDedBenId { get; set; }

		//Rtodo: Relation
		public virtual AdditionalDeductionOrBenefit AdditionalDeductionOrBenefit { get; set; }

		//Rtodo: Relation
		public virtual ExpenseArticle ExpenseArticle { get; set; }
	}

	public class User
	{
		public int UserId { get; set; }
		public string Username { get; set; }

		public string Password { get; set; }

		//Rtodo: Relation
		public virtual Rule Rule { get; set; }
	}

	public class Rule
	{
		public int RuleId { get; set; }
		public string Title { get; set; }
	}

	public class AccessForm
	{
		public int AccessFormId { get; set; }

		//Rtodo: Relation
		public virtual Rule Rule { get; set; }

		//Rtodo: Relation
		public virtual SysForm SysForm { get; set; }

		public bool SelectAccess { get; set; }
		public bool UpdateAccess { get; set; }
		public bool InsertAccess { get; set; }
		public bool DeleteAccess { get; set; }
	}

	public class AccessMenu
	{
		public int AccessMenuId { get; set; }

		//Rtodo: Relation
		public virtual Rule Rule { get; set; }

		public bool ViewAccess { get; set; }
		public bool ExecuteAccess { get; set; }
	}

	public class SysForm
	{
		public int SysFormId { get; set; }
		public string Title { get; set; }
	}

	public class SysMenu
	{
		public int SysMenuId { get; set; }
		public string Title { get; set; }
	}
}