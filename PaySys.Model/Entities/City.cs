using System.Collections.Generic;
using PaySys.Model.Attributes;
using PaySys.Model.Entities.Base;

namespace PaySys.Model.Entities
{
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

        public override string ToString()
        {
            return Title;
        }
    }
}