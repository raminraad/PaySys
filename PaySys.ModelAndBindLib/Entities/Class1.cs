#region
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Data;
using PaySys.CalcLib.Converters;
using PaySys.CalcLib.ExtensionMethods;
using PaySys.Globalization;
using PaySys.ModelAndBindLib.Annotations;
using PaySys.ModelAndBindLib.Attributes;
#endregion

namespace PaySys.ModelAndBindLib.Entities
{
    /// <summary> #01 گروه اصلی </summary>
    public class MainGroup : EntityBase
    {
        public string Title { get; set; }

        public string Alias { get; set; }

        public ColorPallet ItemColor { get; set; }

        public virtual List<SubGroup> SubGroups { get; set; }

        public virtual List<ContractField> ContractFields { get; set; }

        public virtual List<Variable> Variables { get; set; }

        [NotMapped]
        public IEnumerable<ContractField> CurrentContractFields =>
            ContractFields.Where(c => c.Year == PaySysSetting.CurrentYear);

        [NotMapped]
        public IEnumerable<Variable> CurrentVariables => Variables.Where(v => v.IncludesCurrentDate);

        public override bool Equals(object obj)
        {
            return string.Equals((obj as MainGroup)?.Alias, Alias);
        }
    }

    /// <summary> #02 زیرگروه </summary>
    public class SubGroup : EntityBase
    {
        private HandselFormula _currentOrNewHandselFormula = null;
        private MissionFormula _currentOrNewMissionFormula = null;

        public string Alias { get; set; }

        public string WorkshopCode { set; get; }

        public string Title { get; set; }

        public ColorPallet ItemColor { get; set; }

        public bool Is31 { set; get; }
        public bool IsFreeZone { set; get; }

        [Required]
        public virtual MainGroup MainGroup { set; get; }

        public virtual List<Parameter> Parameters { set; get; }

        public virtual List<MissionFormula> MissionFormulas { get; set; }

        public virtual List<TaxTable> TaxTables { get; set; }

        public virtual List<HandselFormula> HandselFormulas { get; set; }

        public virtual List<ContractMaster> ContractMasters { get; set; }

        public virtual List<Misc> Miscs { get; set; }

        public virtual ExpenseArticle ExpenseArticleOfOvertime { set; get; }
        public virtual ExpenseArticle ExpenseArticleOfMission { set; get; }
        public virtual ExpenseArticle ExpenseArticleOfEmployer { set; get; }

        public virtual List<ExpenseArticleOfContractFieldForSubGroup> ExpenseArticleOfContractFieldForSubGroups
        {
            get;
            set;
        }

        [NotMapped]
        public List<Misc> MiscsOfTypePayment => Miscs.Where(misc => misc.MiscTitle.IsPayment).ToList();

        [NotMapped]
        public List<Misc> MiscsOfTypeDebt => Miscs.Where(misc => !misc.MiscTitle.IsPayment).ToList();

        [NotMapped]
        public IEnumerable<ContractField> CurrentContractFields => MainGroup.CurrentContractFields;

        [NotMapped]
        public IEnumerable<Variable> CurrentVariables => MainGroup.CurrentVariables;

        [NotMapped]
        public TaxTable CurrenTaxTable
        {
            get => TaxTables.FirstOrDefault(table => table.Year == PaySysSetting.CurrentYear);
            set
            {
                var taxTable = TaxTables.FirstOrDefault(table => table.Year == PaySysSetting.CurrentYear);
                if (taxTable != null)
                    taxTable = value;
            }
        }

        [NotMapped]
        public HandselFormula CurrentOrNewHandselFormula
        {
            set => _currentOrNewHandselFormula = value;
            get =>
                _currentOrNewHandselFormula ?? (_currentOrNewHandselFormula =
                    HandselFormulas.FirstOrDefault(hf =>
                        hf.Year == PaySysSetting.CurrentYear && hf.Month == PaySysSetting.CurrentMonth) ??
                    new HandselFormula
                    {
                        Year = PaySysSetting.CurrentYear,
                        Month = PaySysSetting.CurrentMonth,
                        SubGroup = this
                    });
        }

        [NotMapped]
        public MissionFormula CurrentOrNewMissionFormula
        {
            set => _currentOrNewMissionFormula = value;
            get =>
                _currentOrNewMissionFormula ?? (_currentOrNewMissionFormula =
                    MissionFormulas.FirstOrDefault(mf =>
                        mf.Year == PaySysSetting.CurrentYear && mf.Month == PaySysSetting.CurrentMonth) ??
                    new MissionFormula
                    {
                        Year = PaySysSetting.CurrentYear,
                        Month = PaySysSetting.CurrentMonth,
                        SubGroup = this
                    });
        }

        [NotMapped]
        public IEnumerable<Employee> CurrentEmployees
        {
            get { return ContractMasters.Where(master => master.IsCurrent).Select(master => master.Employee).ToList(); }
        }

        [NotMapped]
        public IEnumerable<MiscRecharge> CurrentMiscRecharges
        {
            get
            {
                var currentContractsOfSubGroup = ContractMasters.Where(master => master.IsCurrent);
                var currentEmployeesOfSubGroup = currentContractsOfSubGroup.Select(m => m.Employee);
                var miscRechargesOfCurrentEmployees =
                    currentEmployeesOfSubGroup.SelectMany(employee => employee.MiscRecharges);
                return miscRechargesOfCurrentEmployees;
            }
        }

        [NotMapped]
        public IEnumerable<Parameter> CurrentParameters =>
            Parameters.Where(m => m.Year == PaySysSetting.CurrentYear && m.Month == PaySysSetting.CurrentMonth);

        [NotMapped]
        public IEnumerable<Misc> CurrentMiscs => Miscs.Where(m => m.Year == PaySysSetting.CurrentYear);

        [NotMapped]
        public IEnumerable<Misc> CurrentMiscPayments =>
            Miscs.Where(m => m.Year == PaySysSetting.CurrentYear && m.MiscTitle.IsPayment);

        [NotMapped]
        public IEnumerable<Misc> CurrentMiscDebts =>
            Miscs.Where(m => m.Year == PaySysSetting.CurrentYear && !m.MiscTitle.IsPayment);

        [NotMapped]
        public List<MiscRecharge> TempMiscRechargesOfEmployees { set; get; }

        [NotMapped]
        public List<MiscValueForEmployee> TempMiscValuesOfEmployees { set; get; }

        [NotMapped]
        public List<VariableValueForEmployee> TempVariableValuesOfEmployees { set; get; }

        [NotMapped]
        public List<ContractField> ContractFields => MainGroup.ContractFields;

        public override bool Equals(object obj)
        {
            if (obj?.GetType() != GetType())
                return false;

            return ((SubGroup) obj).Alias == Alias;
        }
    }

    /// <summary> #03 عناوین کسور و پرداختهای متفرقه </summary>
    public class MiscTitle : EntityBase
    {
        public string Title { get; set; }

        public int Index { get; set; }

        public string Alias { get; set; }

        public bool IsPayment { get; set; }

        public virtual List<Misc> Miscs { get; set; }
    }

    /// <summary> #04 کسور و پرداختهای متفرقه زیرگروه در سال </summary>
    public class Misc : EntityBase
    {
        private string _tempExpenseArticleCode = null;

        public int Year { get; set; }

        public int Month { get; set; }

        public virtual List<ParameterInvolvedMisc> ParameterInvolvedMiscs { get; set; }

        public virtual List<MiscValueForEmployee> MiscValueForEmployees { get; set; }

        public virtual List<MiscRecharge> MiscRecharges { get; set; }

        public virtual MiscTitle MiscTitle { get; set; }

        public virtual SubGroup SubGroup { set; get; }

        public virtual ExpenseArticle ExpenseArticle { set; get; }

        [NotMapped]
        public string TempExpenseArticleCode
        {
            get => _tempExpenseArticleCode ?? (_tempExpenseArticleCode = ExpenseArticle?.Code);
            set => _tempExpenseArticleCode = value;
        }

        public override bool Equals(object obj)
        {
            if (obj?.GetType() != GetType())
                return false;

            return ((Misc) obj).Id == Id;
        }
    }

    /// <summary> #05 پرداختهای متفرقه دخیل در محاسبات مؤلفه ها </summary>
    public class ParameterInvolvedMisc : EntityBase
    {
        public virtual Misc Misc { get; set; }

        public virtual Parameter Parameter { get; set; }
    }

    /// <summary> #07 مقادیر مؤلفه های محاسباتی زیرگروه در سال و ماه </summary>
    public class Parameter : EntityBase
    {
        private Dictionary<ContractField, bool> _tempParameterInvolvedContractFieldsLeftJoined = null;
        private Dictionary<Misc, bool> _tempParameterInvolvedMiscPaymentsLeftJoined = null;
        private double? _value;

        public double? Value
        {
            get => _value;
            set
            {
                _value = value;
                OnPropertyChanged(nameof(Value));
            }
        }

        public int Year { get; set; }

        public int Month { get; set; }
        public ValueType ValueType => Value <= 100 ? ValueType.Percent : ValueType.Rial;

        public virtual SubGroup SubGroup { set; get; }

        public virtual ParameterTitle ParameterTitle { get; set; }

        public virtual List<ParameterInvolvedMisc> ParameterInvolvedMiscs { set; get; }

        public virtual List<ParameterInvolvedContractField> ParameterInvolvedContractFields { get; set; }

        [NotMapped]
        public Dictionary<ContractField, bool> TempParameterInvolvedContractFieldsLeftJoined
        {
            get
            {
                if (_tempParameterInvolvedContractFieldsLeftJoined != null)
                    return _tempParameterInvolvedContractFieldsLeftJoined;
                var cfs = ParameterInvolvedContractFields.Select(p => p.ContractField).ToList();
                _tempParameterInvolvedContractFieldsLeftJoined = new Dictionary<ContractField, bool>();
                cfs.ForEach(p => _tempParameterInvolvedContractFieldsLeftJoined.Add(p, true));
                SubGroup.MainGroup.CurrentContractFields.Except(cfs).ToList().ForEach(p =>
                    _tempParameterInvolvedContractFieldsLeftJoined.Add(p, false));
                return _tempParameterInvolvedContractFieldsLeftJoined;
            }
            set => _tempParameterInvolvedContractFieldsLeftJoined = value;
        }

        [NotMapped]
        public Dictionary<Misc, bool> TempParameterInvolvedMiscPaymentsLeftJoined
        {
            get
            {
                if (_tempParameterInvolvedMiscPaymentsLeftJoined != null)
                    return _tempParameterInvolvedMiscPaymentsLeftJoined;
                var mp = ParameterInvolvedMiscs.Select(m => m.Misc).ToList();
                _tempParameterInvolvedMiscPaymentsLeftJoined = new Dictionary<Misc, bool>();
                mp.ForEach(m => _tempParameterInvolvedMiscPaymentsLeftJoined.Add(m, true));
                SubGroup.CurrentMiscPayments.Except(mp).ToList()
                    .ForEach(m => _tempParameterInvolvedMiscPaymentsLeftJoined.Add(m, false));
                return _tempParameterInvolvedMiscPaymentsLeftJoined;
            }
            set => _tempParameterInvolvedMiscPaymentsLeftJoined = value;
        }

        [NotMapped]
        public string ValueAndValueType => $"{Value:N0} {ValueType.GetDescription()}";

        public override void ValidateProperty(string propertyName)
        {
            switch (propertyName)
            {
                case nameof(Value):
                {
                    ValidateMandatory(Value, nameof(Value));
                    ValidateNonNegative(Value, nameof(Value));
                }
                    break;
            }
        }

    }

    /// <summary> #08 فیلدهای احکام دخیل در محاسبات مؤلفه ها </summary>
    public class ParameterInvolvedContractField : EntityBase
    {
        public virtual ContractField ContractField { get; set; }

        public virtual Parameter Parameter { get; set; }
    }

    /// <summary> #09 مواد هزینه </summary>
    public class ExpenseArticle : EntityBase
    {
        public string Title { get; set; }

        public string Code { get; set; }

        public bool IsActive { get; set; } = true;

        public virtual List<Misc> Miscs { get; set; }

        public virtual List<ExpenseArticleOfContractFieldForSubGroup> ExpenseArticleOfContractFieldForSubGroups
        {
            get;
            set;
        }

        [NotMapped]
        public ObservableCollection<TitledCompositeCollection> InvolversUngrouped
        {
            get
            {
                //todo: Apply current date in following calculations

                var result = new ObservableCollection<TitledCompositeCollection>();
                /*var ccContractFields = new TitledCompositeCollection
                {
                        Title = ResourceAccessor.Labels.GetString( "ContractFields" ),
                        CompositeCollection = new CompositeCollection()
                };
                if( ExpenseArticleOfContractFieldForSubGroups != null )
                    foreach( var item in ExpenseArticleOfContractFieldForSubGroups.ToList() )
                        ccContractFields.CompositeCollection.Add( item.ContractField );

                result.Add( ccContractFields );*/

                var ccMiscs = new TitledCompositeCollection
                {
                    Title = ResourceAccessor.Labels.GetString("MiscPayments"),
                    CompositeCollection = new CompositeCollection()
                };

                foreach (var item in Miscs)
                    ccMiscs.CompositeCollection.Add(item);

                result.Add(ccMiscs);

                var ccVariables = new TitledCompositeCollection
                {
                    Title = ResourceAccessor.Labels.GetString("MonthlyVariables"),
                    CompositeCollection = new CompositeCollection()
                };

                result.Add(ccVariables);
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
                if (ExpenseArticleOfContractFieldForSubGroups != null)
                    count += ExpenseArticleOfContractFieldForSubGroups.Count;
                if (Miscs != null)
                    count += Miscs.Count;

                return count;
            }
        }

        public override string ToString()
        {
            return Code;
        }
    }

    /// <summary> #10 مانده بدهی متفرقه اشخاص </summary>
    public class MiscRecharge : EntityBase
    {
        public double Value { get; set; }

        public int Year { get; set; }

        public int Month { get; set; }

        public virtual Misc Misc { get; set; }

        public virtual Employee Employee { get; set; }
    }

    /// <summary> #13 فیلدهای احکام زیرگروه در سال </summary>
    public class ContractField : EntityBase
    {
        private string _tempCurrentExpenseArticleCode = null;

        public int Year { get; set; }

        public virtual MainGroup MainGroup { get; set; }

        public string Title { get; set; }

        public string Alias { get; set; }

        public bool IsEditable { get; set; }

        public int Index { get; set; }

        public int IndexInRetirementReport { get; set; }

        public virtual List<ParameterInvolvedContractField> ParameterInvolvedContractFields { get; set; }

        public virtual List<ContractDetail> ContractDetails { get; set; }

        public virtual List<MissionFormulaInvolvedContractField> MissionFormulaInvolvedContractFields { get; set; }

        public virtual List<ExpenseArticleOfContractFieldForSubGroup> ExpenseArticleOfContractFieldForSubGroups
        {
            get;
            set;
        }

        [NotMapped]
        public bool IsInUse => InvolversCount > 0;

        [NotMapped]
        public int InvolversCount =>
            ParameterInvolvedContractFields.Count + MissionFormulaInvolvedContractFields.Count + ContractDetails.Count;

        [NotMapped]
        public ExpenseArticle CurrentExpenseArticle
        {
            get =>
                ExpenseArticleOfContractFieldForSubGroups
                    ?.FirstOrDefault(exp => exp.Month == PaySysSetting.CurrentMonth)?.ExpenseArticle;
            set
            {
                if (CurrentExpenseArticle != null)
                {
                    ExpenseArticleOfContractFieldForSubGroups
                        .FirstOrDefault(exp => exp.Month == PaySysSetting.CurrentMonth).ExpenseArticle = value;
                }
                else
                {
                    var newLink = new ExpenseArticleOfContractFieldForSubGroup
                    {
                        ContractField = this,
                        Month = PaySysSetting.CurrentMonth,
                        ExpenseArticle = value
                    };
                    if (ExpenseArticleOfContractFieldForSubGroups == null)
                        ExpenseArticleOfContractFieldForSubGroups =
                            new List<ExpenseArticleOfContractFieldForSubGroup>();
                    ExpenseArticleOfContractFieldForSubGroups.Add(newLink);
                }
            }
        }

        [NotMapped]
        public string TempCurrentExpenseArticleCode
        {
            get => _tempCurrentExpenseArticleCode ?? (_tempCurrentExpenseArticleCode = CurrentExpenseArticle?.Code);
            set => _tempCurrentExpenseArticleCode = value;
        }
    }

    /// <summary> #14 فرمول مأموریت زیرگروه در سال و ماه </summary>
    public class MissionFormula : EntityBase
    {
        private Dictionary<ContractField, bool> _tempMissionFormulaInvolvedContractFieldsLeftJoined = null;

        public double DivideFactor { get; set; }

        public double AddFactor { get; set; }

        public double MaxFactor { get; set; }

        public double SubtractFactor { get; set; }

        public double PerKmFactor { get; set; }

        public int Year { get; set; }

        public int Month { get; set; }

        public virtual SubGroup SubGroup { get; set; }

        public virtual List<MissionFormulaInvolvedContractField> MissionFormulaInvolvedContractFields { get; set; }

        [NotMapped]
        public Dictionary<ContractField, bool> TempMissionFormulaInvolvedContractFieldsLeftJoined
        {
            get
            {
                if (_tempMissionFormulaInvolvedContractFieldsLeftJoined != null)
                    return _tempMissionFormulaInvolvedContractFieldsLeftJoined;
                var cfs = MissionFormulaInvolvedContractFields?.Select(p => p.ContractField).ToList();
                if (cfs == null) return new Dictionary<ContractField, bool>();
                _tempMissionFormulaInvolvedContractFieldsLeftJoined = new Dictionary<ContractField, bool>();
                cfs.ForEach(p => _tempMissionFormulaInvolvedContractFieldsLeftJoined.Add(p, true));
                SubGroup.MainGroup.CurrentContractFields.Except(cfs).ToList().ForEach(p =>
                    _tempMissionFormulaInvolvedContractFieldsLeftJoined.Add(p, false));
                return _tempMissionFormulaInvolvedContractFieldsLeftJoined;
            }
            set => _tempMissionFormulaInvolvedContractFieldsLeftJoined = value;
        }
    }

    /// <summary> #15 جدول مالیات </summary>
    public class TaxTable : EntityBase
    {
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
                foreach (var taxRow in TaxRows)
                {
                    taxRow.ValueFrom = previousTo;
                    previousTo = taxRow.ValueTo;
                }

                return new ObservableCollection<TaxRow>(TaxRows);
            }
            set => TaxRows = new List<TaxRow>(value);
        }
    }

    /// <summary> #16 سطر جدول مالیات </summary>
    public class TaxRow : EntityBase, IComparable
    {
        private double? _tempValueTo = null;

        public double ValueTo { get; set; }

        public double Factor { get; set; }

        public virtual TaxTable TaxTable { get; set; }

        [NotMapped]
        public double? TempValueTo
        {
            get => _tempValueTo ?? (_tempValueTo = ValueTo);
            set
            {
                _tempValueTo = value;
                OnPropertyChanged(nameof(TempValueTo));
            }
        }

        [NotMapped]
        public double ValueFrom { get; set; }

        [NotMapped]
        public double TaxValue { get; set; } //Todo: Calculate this field

        #region Implementation of IComparable
        public int CompareTo(object obj)
        {
            return ValueTo.CompareTo(((TaxRow) obj).ValueTo);
        }
        #endregion

//        public event PropertyChangedEventHandler PropertyChanged;

//        [NotifyPropertyChangedInvocator]
//        protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
//        {
//            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
//        }
    }

    /// <summary> #18 مقادیر کسور و پرداختهای متفرقه برای اشخاص در سال و ماه </summary>
    public class MiscValueForEmployee : EntityBase
    {
        public double Value { get; set; }

        public double ValueSubtraction { get; set; }

        public int Year { get; set; }

        public int Month { get; set; }

        public virtual Misc Misc { get; set; }

        public virtual Employee Employee { get; set; }
    }

    /// <summary> #20 پایه حکم </summary>
    public class ContractMaster : EntityBase
    {
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
        public ServiceStand ServiceStand { get; set; }

        public string AccountNoGov { get; set; }

        public string AccountNoEmp { get; set; }

        public bool IsCurrent { set; get; }

        public virtual SubGroup SubGroup { set; get; }

        public virtual Employee Employee { get; set; }

        public virtual Job Job { get; set; }

        public virtual List<Mission> Missions { get; set; }

        public virtual List<ContractDetail> ContractDetails { get; set; }

        [NotMapped]
        public MainGroup MainGroup => SubGroup.MainGroup;

        public override bool Equals(object obj)
        {
            return obj is ContractMaster && ((ContractMaster) obj).Id == Id &&
                   string.Equals(ContractNo, ((ContractMaster) obj).ContractNo);
        }
    }

    /// <summary> #21 جزئیات احکام </summary>
    public class ContractDetail : EntityBase
    {
        public double Value { get; set; }

        public virtual ContractField ContractField { get; set; }

        public virtual ContractMaster ContractMaster { get; set; }
    }

    /// <summary> #22 اشخاص </summary>
    public class Employee : EntityBase
    {
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
        public IEnumerable<Mission> MissionsOfCurrentYear =>
            ContractMasters.SelectMany(c => c.Missions.Where(m =>
                m.StartDate >= PaySysSetting.CurrentYearStartGreg && m.StartDate <= PaySysSetting.CurrentYearEndGreg));

        [NotMapped]
        public string FullName => $"{Id} : {FName} {LName}"; //Todo: Remove EmployeeId from result

        [NotMapped]
        public string LuffName => $"{LName} {FName}";

        public override bool Equals(object obj)
        {
            if (obj?.GetType() != GetType())
                return false;

            return ((Employee) obj).Id == Id;
        }
    }

    /// <summary> #23 فرمول عیدی در سال و ماه </summary>
    public class HandselFormula : EntityBase
    {
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
        [ForeignKey("Contract1St")]
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
    public class Job : EntityBase
    {
        public string Title { get; set; }

        public string JobNo { get; set; }

        public string Description { get; set; }

        public ColorPallet ItemColor { get; set; }

        public virtual List<ContractMaster> ContractMasters { get; set; }
    }

    /// <summary> #26 شهر </summary>
    public class City : EntityBase
    {
        [IncludeInLookup]
        public string Title { get; set; }

        [IncludeInLookup]
        public int Distance { get; set; }

        [IncludeInLookup]
        public double Percentage { get; set; }

        public virtual List<Mission> Missions { get; set; }
    }

    /// <summary> #27 مأموریت </summary>
    public class Mission : EntityBase
    {
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
    public class ExpenseArticleOfContractFieldForSubGroup : EntityBase
    {
        public int Month { get; set; }

        public virtual ExpenseArticle ExpenseArticle { set; get; }

        public virtual ContractField ContractField { set; get; }

        public virtual SubGroup SubGroup { set; get; }
    }

    /// <summary> #34 فیلدهای احکام دخیل در فرمول مأموریت </summary>
    public class MissionFormulaInvolvedContractField : EntityBase
    {
        public virtual ContractField ContractField { get; set; }

        public virtual MissionFormula MissionFormula { get; set; }
    }

    /// <summary> #36 متغیرهای ماهانه در بازه زمانی </summary>
    public class Variable : EntityBase
    {
        public int Index { get; set; }
        public int FromYear { get; set; }

        public int FromMonth { get; set; }

        public int ToYear { get; set; }

        public int ToMonth { get; set; }

        public virtual MainGroup MainGroup { get; set; }

        public string Title { get; set; }

        public string Alias { get; set; }

        public ValueType ValueType { get; set; }

        public virtual List<VariableValueForEmployee> VariableValueForEmployees { get; set; }

        public bool IncludesDate(int year, int month)
        {
            var fromDate = (FromYear * 100) + FromMonth;
            var toDate = (ToYear * 100) + ToMonth;
            var currentDate = (year * 100) + month;
            return currentDate >= fromDate && currentDate <= toDate;
        }

        [NotMapped]
        public bool IncludesCurrentDate => IncludesDate(PaySysSetting.CurrentYear, PaySysSetting.CurrentMonth);
    }

    /// <summary> #37 متغیرهای ماهانه برای اشخاص </summary>
    public class VariableValueForEmployee : EntityBase
    {
        public double? NumericValue { get; set; }

        public string StringValue { get; set; }

        public int Year { get; set; }

        public int Month { get; set; }

        public DateTime? DateValue { get; set; }

        public virtual Employee Employee { get; set; }

        public virtual Variable Variable { get; set; }

        [NotMapped]
        public object Value
        {
            get
            {
                switch (Variable.ValueType)
                {
                    case ValueType.Unknown:
                        return null;
                    case ValueType.Absolute:
                    case ValueType.Rial:
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
                switch (Variable.ValueType)
                {
                    case ValueType.Unknown:
                        break;
                    case ValueType.Absolute:
                    case ValueType.Rial:
                    case ValueType.Percent:
                        if (value != null)
                            NumericValue = Convert.ToDouble(value);
                        else
                            NumericValue = null;
                        break;
                    case ValueType.Date:
                        if (value != null)
                            DateValue = Convert.ToDateTime(value);
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
        public Visibility ValueIsNumeric =>
            (Variable.ValueType == ValueType.Absolute || Variable.ValueType == ValueType.Percent ||
             Variable.ValueType == ValueType.Rial)
                ? Visibility.Visible
                : Visibility.Collapsed;

        [NotMapped]
        public Visibility ValueIsString =>
            Variable.ValueType == ValueType.String ? Visibility.Visible : Visibility.Collapsed;

        [NotMapped]
        public Visibility ValueIsDate =>
            Variable.ValueType == ValueType.Date ? Visibility.Visible : Visibility.Collapsed;
    }

    /// <summary>
    /// #38 عناوین مؤلفه های گروهی
    /// </summary>
    public class ParameterTitle : EntityBase
    {
        public string Title { get; set; }

        public string Alias { get; set; }

        public virtual List<ParameterTitle> ParameterTitles { get; set; }
    }

    #region Enums
    [TypeConverter(typeof(EnumDescriptionTypeConverter))]
    public enum ServiceStand
    {
        [Description("منطقه عادی")] RegularZone = 0,

        [Description("منطقه آزاد")] FreeZone,

        [Description("منطقه محروم")] DeprivedZone,
    }

    [TypeConverter(typeof(EnumDescriptionTypeConverter))]
    public enum SacrificeStand
    {
        [Description("وارد نشده")] Unknown = 0,

        [Description("رزمنده")] Razmande,

        [Description("آزاده")] Azade,

        [Description("خانواده آزاده")] KhanevadeAzade,

        [Description("جانباز")] Janbaz,

        [Description("خانواده جانباز")] KhanevadeJanbaz,

        [Description("خانواده شهید")] KhanevadeShahid,

        [Description("ایثارگر")] Isargar,

        [Description("سایر موارد")] Other
    }

    [TypeConverter(typeof(EnumDescriptionTypeConverter))]
    public enum ColorPallet
    {
        [Description("وارد نشده")] Unknown = 0,
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
        [Description("وارد نشده")] Unknown = 0,

        [Description("بی سواد")] Bisavad,

        [Description("سیکل")] Sikl,

        [Description("زیر دیپلم")] Zildiplom,

        [Description("دیپلم")] Diplom,

        [Description("کاردانی")] Kardani,

        [Description("کارشناسی")] Karshenasi,

        [Description("فوق لیسانس")] Foqlisans,

        [Description("دکترا")] Doctora,

        [Description("فوق دکترا")] Foqoctora,

        [Description("سایر موارد")] Other
    }

    [TypeConverter(typeof(EnumDescriptionTypeConverter))]
    public enum EmploymentType
    {
        [Description("وارد نشده")] Unknown = 0,

        [Description("رسمی آزمایشی")] Rasmiazmayeshi,

        [Description("رسمی قطعی")] Rasmiqatei,

        [Description("شرکتی")] Sherkati,

        [Description("پیمانی")] Peymani,

        [Description("قراردادی")] Qarardadi,

        [Description("تعاونی")] Taavoni,

        [Description("فصلی")] Fasli,

        [Description("خرید خدمتی")] Kharidkhedmati,

        [Description("روز مزد")] Ruzmozd,

        [Description("سایر موارد")] Other
    }

    [TypeConverter(typeof(EnumDescriptionTypeConverter))]
    public enum MaritalStatus
    {
        [Description("وارد نشده")] Unknown = 0,

        [Description("مجرد")] Single,

        [Description("متأهل")] Married
    }

    [TypeConverter(typeof(EnumDescriptionTypeConverter))]
    public enum Sex
    {
        [Description("وارد نشده")] Unknown = 0,

        [Description("مذکر")] Male,

        [Description("مؤنث")] Female
    }

    [TypeConverter(typeof(EnumDescriptionTypeConverter))]
    public enum ValueType
    {
        [Description("وارد نشده")] Unknown = 0,

        [Description("مطلق")] Absolute,

        [Description("درصد")] Percent,

        [Description("ریـال")] Rial,

        [Description("تاریخ")] Date,

        [Description("رشته")] String,
    }

    [TypeConverter(typeof(EnumDescriptionTypeConverter))]
    public enum VehicleType
    {
        //todo: fill enum
        [Description("وارد نشده")] Unknown = 0,

        [Description("تاکسی")] Cap,

        [Description("هواپیما")] Plane,

        [Description("قطار")] Train,

        [Description("خودرو شخصی")] Personal,

        [Description("نقلیه عمومی")] PublicTransport
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

    public enum MiscType
    {
        None,
        Payment,
        Debt
    }

    public enum ValidateOn
    {
        None,
        TextChanged,
        LostFocus
    }

    public enum NavigationType
    {
        First = -2,
        Previous = -1,
        None = 0,
        Next = 1,
        Last = 2
    }
    #endregion
}