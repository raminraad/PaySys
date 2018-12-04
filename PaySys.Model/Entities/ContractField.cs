using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using PaySys.Model.Entities.Base;
using PaySys.Model.Static;

namespace PaySys.Model.Entities
{
    /// <summary> #13 فیلدهای احکام زیرگروه در سال </summary>
    public class ContractField : EntityBase
    {
        private string _tempCurrentExpenseArticleCode = null;

        public int Year { get; set; }

        public virtual MainGroup MainGroup { get; set; }

        public string Title { get; set; }

        public string Alias { get; set; }

        public bool IsEditable { get; set; }

        public int Index { get; set; }

        public int IndexInRetirementReport { get; set; }

        public virtual List<ParameterInvolvedContractField> ParameterInvolvedContractFields { get; set; }

        public virtual List<ContractDetail> ContractDetails { get; set; }

        public virtual List<MissionFormulaInvolvedContractField> MissionFormulaInvolvedContractFields { get; set; }

        public virtual List<ExpenseArticleOfContractFieldForSubGroup> ExpenseArticleOfContractFieldForSubGroups
        {
            get;
            set;
        }

        [NotMapped]
        public bool IsInUse => InvolversCount > 0;

        [NotMapped]
        public int InvolversCount =>
            ParameterInvolvedContractFields.Count + MissionFormulaInvolvedContractFields.Count + ContractDetails.Count;

        [NotMapped]
        public ExpenseArticle CurrentExpenseArticle
        {
            get =>
                ExpenseArticleOfContractFieldForSubGroups
                    ?.FirstOrDefault(exp => exp.Month == PaySysSetting.CurrentMonth)?.ExpenseArticle;
            set
            {
                if (CurrentExpenseArticle != null)
                {
                    ExpenseArticleOfContractFieldForSubGroups
                        .FirstOrDefault(exp => exp.Month == PaySysSetting.CurrentMonth).ExpenseArticle = value;
                }
                else
                {
                    var newLink = new ExpenseArticleOfContractFieldForSubGroup
                    {
                        ContractField = this,
                        Month = PaySysSetting.CurrentMonth,
                        ExpenseArticle = value
                    };
                    if (ExpenseArticleOfContractFieldForSubGroups == null)
                        ExpenseArticleOfContractFieldForSubGroups =
                            new List<ExpenseArticleOfContractFieldForSubGroup>();
                    ExpenseArticleOfContractFieldForSubGroups.Add(newLink);
                }
            }
        }

        [NotMapped]
        public string TempCurrentExpenseArticleCode
        {
            get => _tempCurrentExpenseArticleCode ?? (_tempCurrentExpenseArticleCode = CurrentExpenseArticle?.Code);
            set => _tempCurrentExpenseArticleCode = value;
        }
    }
}