using PaySys.Model.Entities.Base;

namespace PaySys.Model.Entities
{
    /// <summary> #18 مقادیر کسور و پرداختهای متفرقه برای اشخاص در سال و ماه </summary>
    public class MiscValueForEmployee : EntityBase
    {
        public double Value { get; set; }

        public double ValueSubtraction { get; set; }

        public virtual Misc Misc { get; set; }

        public virtual Employee Employee { get; set; }
    }
}