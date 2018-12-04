using System.Collections.Generic;
using PaySys.Model.Entities.Base;

namespace PaySys.Model.Entities
{
    /// <summary> #21 جزئیات احکام </summary>
    public class ContractDetail : EntityBase
    {
        public double Value { get; set; }

        public virtual ContractField ContractField { get; set; }

        public virtual ContractMaster ContractMaster { get; set; }

        public virtual List<PayslipValueOfContractDetailsForEmployee> PayslipValueOfContractDetailsForEmployees { set;
            get;
        }
    }
}