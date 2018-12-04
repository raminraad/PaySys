using System.Collections.Generic;
using PaySys.Model.Entities.Base;

namespace PaySys.Model.Entities
{
    /// <summary> #03 عناوین کسور و پرداختهای متفرقه </summary>
    public class MiscTitle : EntityBase
    {
        public string Title { get; set; }

        public int Index { get; set; }

        public string Alias { get; set; }

        public bool IsPayment { get; set; }

        public virtual List<Misc> Miscs { get; set; }
    }
}