using PaySys.Model.Entities.Base;

namespace PaySys.Model.Entities
{
    /// <summary>
    /// #42 مقادیر آیتم های فیش حقوقی برای اشخاص در سال و ماه
    /// </summary>
    public class PayslipItemValueForEmployee : EntityBase
    {
        public PayslipItemTitle PayslipItemTitle { get; set; }

        public Employee Employee { get; set; }

        public double Value { get; set; }

        public int Year { get; set; }

        public int Month { get; set; }
    }
}