using PaySys.Model.Entities.Base;

namespace PaySys.Model.Entities
{
    /// <summary> #34 فیلدهای احکام دخیل در فرمول مأموریت </summary>
    public class MissionFormulaInvolvedContractField : EntityBase
    {
        public virtual ContractField ContractField { get; set; }

        public virtual MissionFormula MissionFormula { get; set; }
    }
}