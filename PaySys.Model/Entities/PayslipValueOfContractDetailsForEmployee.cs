using PaySys.Model.Entities.Base;

namespace PaySys.Model.Entities
{
    /// <summary>
    /// #44 مقادیر جزئیات احکام در فیش حقوقی در سال و ماه
    /// </summary>
    public class PayslipValueOfContractDetailsForEmployee : EntityBase
    {
        public ContractDetail ContractDetail { get; set; }
        public double Value { get; set; }
        public int Year { get; set; }

        public int Month { get; set; }
    }
}