using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PaySys.Model.Entities
{
    /// <summary> #24 تفاوت احکام </summary>
    public class ContractDifference
    {
        [Key]
        [ForeignKey("Contract1St")]
        public int ContractMasterId { get; set; }

        public DateTime DateFrom { get; set; }

        public DateTime DateTo { get; set; }

        public double FirstMonth { get; set; }

        public int PayYear { get; set; }

        public int PayMonth { get; set; }

        public virtual ContractMaster Contract1St { get; set; }

        public virtual ContractMaster Contract2Nd { get; set; }
    }
}