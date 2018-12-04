using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations.Schema;
using PaySys.Model.Entities.Base;

namespace PaySys.Model.Entities
{
    /// <summary> #15 جدول مالیات </summary>
    public class TaxTable : EntityBase
    {
        public int Year { get; set; }

        public int Month { get; set; }

        public virtual SubGroup SubGroup { get; set; }

        public virtual List<TaxRow> TaxRows { get; set; }

        [NotMapped]
        public virtual ObservableCollection<TaxRow> TaxRowsForGrid
        {
            get
            {
                double previousTo = 0;
                TaxRows.Sort();
                foreach (var taxRow in TaxRows)
                {
                    taxRow.ValueFrom = previousTo;
                    previousTo = taxRow.ValueTo;
                }

                return new ObservableCollection<TaxRow>(TaxRows);
            }
            set => TaxRows = new List<TaxRow>(value);
        }

        public void CommitTempToValues()
        {
            TaxRows.ForEach(row => row.ValueTo = row.TempValueTo.Value);
        }

        public void DiscardTempToValues()
        {
            TaxRows.ForEach(row => row.TempValueTo = row.ValueTo);
        }
    }
}