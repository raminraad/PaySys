using System;
using PaySys.Model.Attributes;
using PaySys.Model.Entities.Base;
using PaySys.Model.Enums;

namespace PaySys.Model.Entities
{
    /// <summary> #27 مأموریت </summary>
    public class Mission : EntityBase
    {
        [IncludeInLookup]
        public string Title { get; set; }

        [IncludeInLookup]
        public DateTime StartDate { get; set; }

        [IncludeInLookup]
        public DateTime EndDate { get; set; }

        [IncludeInLookup]
        public DateTime StartTime { get; set; }

        [IncludeInLookup]
        public DateTime EndTime { get; set; }

        /// <summary> مدت با بیتوته </summary>
        [IncludeInLookup]
        public int AmountResident { get; set; }

        /// <summary> مدت بدون بیتوته </summary>
        [IncludeInLookup]
        public int AmountNonResident { get; set; }

        [IncludeInLookup]
        public VehicleType VehicleType { set; get; }

        /// <summary> هزینه سفر </summary>
        [IncludeInLookup]
        public double TravelExpense { get; set; }

        [IncludeInLookup]
        public string MissionContractNo { get; set; }

        public virtual ContractMaster ContractMaster { get; set; }

        [IncludeInLookup]
        public virtual City City { get; set; }
    }
}