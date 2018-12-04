using PaySys.Model.Entities.Base;

namespace PaySys.Model.Entities
{
    /// <summary>
    /// #43 مقادیر کسور یا پرداختهای متفرقه اشخاص در فیش حقوقی
    /// </summary>
    public class PayslipValueOfMiscForEmployee : EntityBase
    {
        public Employee Employee { get; set; }
        public Misc Misc { get; set; }
        public double Value { get; set; }

    }
}