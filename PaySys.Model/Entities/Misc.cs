using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using PaySys.Model.Entities.Base;

namespace PaySys.Model.Entities
{
    /// <summary> #04 کسور و پرداختهای متفرقه زیرگروه در سال </summary>
    public class Misc : EntityBase
    {
        private string _tempExpenseArticleCode = null;

        public int Year { get; set; }

        public int Month { get; set; }

        public virtual List<ParameterInvolvedMisc> ParameterInvolvedMiscs { get; set; }

        public virtual List<MiscValueForEmployee> MiscValueForEmployees { get; set; }

        public virtual List<MiscRecharge> MiscRecharges { get; set; }
        public virtual List<PayslipValueOfMiscForEmployee> PayslipValueOfMiscForEmployees { get; set; }

        public virtual MiscTitle MiscTitle { get; set; }

        public virtual SubGroup SubGroup { set; get; }

        public virtual ExpenseArticle ExpenseArticle { set; get; }

        [NotMapped]
        public string TempExpenseArticleCode
        {
            get => _tempExpenseArticleCode ?? (_tempExpenseArticleCode = ExpenseArticle?.Code);
            set => _tempExpenseArticleCode = value;
        }

        public override bool Equals(object obj)
        {
            if (obj?.GetType() != GetType())
                return false;

            return ((Misc) obj).Id == Id;
        }
    }
}