using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using PaySys.Model.Entities.Base;
using PaySys.Model.Enums;

namespace PaySys.Model.Entities
{
    /// <summary> #20 پایه حکم </summary>
    public class ContractMaster : EntityBase
    {
        public string ContractNo { get; set; }

        public DateTime DateExport { get; set; }

        public DateTime DateExecution { get; set; }

        public DateTime DateEmployment { get; set; }

        public MaritalStatus MaritalStatus { get; set; }

        public double HardshipFactor { get; set; }

        public string InsuranceNo { get; set; }

        public EducationStand EducationStand { get; set; }

        public EmploymentType EmploymentType { get; set; }

        public SacrificeStand SacrificeStand { get; set; }
        public ServiceStand ServiceStand { get; set; }

        public string AccountNoGov { get; set; }

        public string AccountNoEmp { get; set; }

        public bool IsCurrent { set; get; }

        public virtual SubGroup SubGroup { set; get; }

        public virtual Employee Employee { get; set; }

        public virtual Job Job { get; set; }

        public virtual List<Mission> Missions { get; set; }

        public virtual List<ContractDetail> ContractDetails { get; set; }

        [NotMapped]
        public MainGroup MainGroup => SubGroup.MainGroup;

        public override bool Equals(object obj)
        {
            return obj is ContractMaster && ((ContractMaster) obj).Id == Id &&
                   string.Equals(ContractNo, ((ContractMaster) obj).ContractNo);
        }
    }
}