using System.Collections.Generic;
using PaySys.Model.Attributes;
using PaySys.Model.Entities.Base;
using PaySys.Model.Enums;

namespace PaySys.Model.Entities
{
    /// <summary> #25 شغل </summary>
    public class Job : EntityBase
    {
        [IncludeInLookup]
        public string Title { get; set; }

        [IncludeInLookup]
        public string JobNo { get; set; }

        [IncludeInLookup]
        public string Description { get; set; }

        public ColorPallet ItemColor { get; set; }

        public virtual List<ContractMaster> ContractMasters { get; set; }
    }
}