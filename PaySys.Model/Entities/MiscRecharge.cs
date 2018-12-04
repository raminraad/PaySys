using PaySys.Model.Entities.Base;

namespace PaySys.Model.Entities
{
    /// <summary> #10 مانده بدهی متفرقه اشخاص </summary>
    public class MiscRecharge : EntityBase
    {
        public double Value { get; set; }

        public int Year { get; set; }

        public int Month { get; set; }

        public virtual Misc Misc { get; set; }

        public virtual Employee Employee { get; set; }
    }
}