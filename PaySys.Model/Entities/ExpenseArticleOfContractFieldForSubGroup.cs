using PaySys.Model.Entities.Base;

namespace PaySys.Model.Entities
{
    /// <summary> #30 مواد هزینه عناوین فیلدهای احکام برای زیرگروه در ماه </summary>
    public class ExpenseArticleOfContractFieldForSubGroup : EntityBase
    {
        public int Month { get; set; }

        public virtual ExpenseArticle ExpenseArticle { set; get; }

        public virtual ContractField ContractField { set; get; }

        public virtual SubGroup SubGroup { set; get; }
    }
}