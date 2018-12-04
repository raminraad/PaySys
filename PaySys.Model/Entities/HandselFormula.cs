using PaySys.Model.Entities.Base;

namespace PaySys.Model.Entities
{
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
}