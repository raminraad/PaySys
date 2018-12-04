using System.Collections.Generic;
using PaySys.Model.Entities.Base;

namespace PaySys.Model.Entities
{
    /// <summary>
    /// #41 عناوین آیتم های فیش حقوقی برای زیرگروه
    /// </summary>
    public class PayslipItemForSubGroup : EntityBase
    {
        public PayslipItemTitle PayslipItemTitle { get; set; }

        public SubGroup SubGroup { get; set; }


        public virtual List<ParameterTitle> ParameterTitles { get; set; }
    }
}