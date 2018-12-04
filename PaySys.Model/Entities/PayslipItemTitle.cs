using System.Collections.Generic;
using PaySys.Model.Entities.Base;

namespace PaySys.Model.Entities
{
    /// <summary>
    /// #40 عناوین آیتم های فیش حقوقی
    /// </summary>
    public class PayslipItemTitle : EntityBase
    {
        public string Title { get; set; }

        public string Alias { get; set; }

        public bool IsPayment { get; set; }
        public virtual List<PayslipItemForSubGroup> PayslipItemForSubGroups { get; set; }
        public virtual List<PayslipItemValueForEmployee> PayslipItemValueForEmployees { get; set; }
    }
}