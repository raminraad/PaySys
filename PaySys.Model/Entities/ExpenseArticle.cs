using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Windows.Data;
using PaySys.Globalization;
using PaySys.Model.Entities.Base;
using PaySys.Model.Helper;

namespace PaySys.Model.Entities
{
    /// <summary> #09 مواد هزینه </summary>
    public class ExpenseArticle : EntityBase
    {
        public string Title { get; set; }

        public string Code { get; set; }

        public bool IsActive { get; set; } = true;

        public virtual List<Misc> Miscs { get; set; }

        public virtual List<ExpenseArticleOfContractFieldForSubGroup> ExpenseArticleOfContractFieldForSubGroups
        {
            get;
            set;
        }

        [NotMapped]
        public ObservableCollection<TitledCompositeCollection> InvolversUngrouped
        {
            get
            {
                //todo: Apply current date in following calculations

                var result = new ObservableCollection<TitledCompositeCollection>();
                var ccContractFields = new TitledCompositeCollection
                {
                    Title = ResourceAccessor.Labels.GetString( "ContractFields" ),
                    CompositeCollection = new CompositeCollection()
                };
                if( ExpenseArticleOfContractFieldForSubGroups != null )
                    foreach( var item in ExpenseArticleOfContractFieldForSubGroups.ToList() )
                        ccContractFields.CompositeCollection.Add( item.ContractField );

                result.Add( ccContractFields );

                var ccMiscs = new TitledCompositeCollection
                {
                    Title = ResourceAccessor.Labels.GetString("MiscPayments"),
                    CompositeCollection = new CompositeCollection()
                };

                foreach (var item in Miscs)
                    ccMiscs.CompositeCollection.Add(item);

                result.Add(ccMiscs);

                /*var ccVariables = new TitledCompositeCollection
                {
                    Title = ResourceAccessor.Labels.GetString("MonthlyVariables"),
                    CompositeCollection = new CompositeCollection()
                };

                result.Add(ccVariables);*/
                return result;
            }
        }

        [NotMapped]
        public int InvolversCount
        {
            get
            {
                //todo: Apply current date in following calculations
                var count = 0;
                if (ExpenseArticleOfContractFieldForSubGroups != null)
                    count += ExpenseArticleOfContractFieldForSubGroups.Count;
                if (Miscs != null)
                    count += Miscs.Count;

                return count;
            }
        }

        public override string ToString()
        {
            return Code;
        }
    }
}