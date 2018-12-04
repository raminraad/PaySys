using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using PaySys.Model.Attributes;
using PaySys.Model.Entities.Base;
using PaySys.Model.Enums;
using PaySys.Model.Static;

namespace PaySys.Model.Entities
{
    /// <summary> #02 زیرگروه </summary>
    public class SubGroup : EntityBase
    {
        private HandselFormula _currentOrNewHandselFormula = null;
        private MissionFormula _currentOrNewMissionFormula = null;

        public string Alias { get; set; }

        [IncludeInLookup]
        public string WorkshopCode { set; get; }

        [IncludeInLookup]
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
        public virtual List<PayslipItemForSubGroup> PayslipItemForSubGroups { get; set; }

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
}