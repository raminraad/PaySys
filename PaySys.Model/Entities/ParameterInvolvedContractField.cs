using PaySys.Model.Entities.Base;

namespace PaySys.Model.Entities
{
    /// <summary> #08 فیلدهای احکام دخیل در محاسبات مؤلفه ها </summary>
    public class ParameterInvolvedContractField : EntityBase
    {
        public virtual ContractField ContractField { get; set; }

        public virtual Parameter Parameter { get; set; }
    }
}