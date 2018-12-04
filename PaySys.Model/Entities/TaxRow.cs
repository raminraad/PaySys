using System;
using System.ComponentModel.DataAnnotations.Schema;
using PaySys.Model.Entities.Base;

namespace PaySys.Model.Entities
{
    /// <summary> #16 سطر جدول مالیات </summary>
    public class TaxRow : EntityBase, IComparable
    {
        private double? _tempValueTo = null;

        public double ValueTo { get; set; }

        public double Factor { get; set; }

        public virtual TaxTable TaxTable { get; set; }

        [NotMapped]
        public double? TempValueTo
        {
            get => _tempValueTo ?? (_tempValueTo = ValueTo);
            set
            {
                _tempValueTo = value;
                OnPropertyChanged(nameof(TempValueTo));
            }
        }

        [NotMapped]
        public double ValueFrom { get; set; }

        [NotMapped]
        public double TaxValue { get; set; } //Todo: Calculate this field

        #region Implementation of IComparable
        public int CompareTo(object obj)
        {
            return ValueTo.CompareTo(((TaxRow) obj).ValueTo);
        }
        #endregion

//        public event PropertyChangedEventHandler PropertyChanged;

//        [NotifyPropertyChangedInvocator]
//        protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
//        {
//            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
//        }
    }
}