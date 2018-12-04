using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using PaySys.Model.Entities.Base;
using PaySys.Model.Enums;
using PaySys.Model.ExtensionMethods;

namespace PaySys.Model.Entities
{
    /// <summary> #07 مقادیر مؤلفه های محاسباتی زیرگروه در سال و ماه </summary>
    public class Parameter : EntityBase
    {
        private Dictionary<ContractField, bool> _tempParameterInvolvedContractFieldsLeftJoined = null;
        private Dictionary<Misc, bool> _tempParameterInvolvedMiscPaymentsLeftJoined = null;
        private double? _value;

        public double? Value
        {
            get => _value;
            set
            {
                _value = value;
                OnPropertyChanged(nameof(Value));
            }
        }

        public int Year { get; set; }

        public int Month { get; set; }
        public ValueType ValueType => Value <= 100 ? ValueType.Percent : ValueType.Rial;

        public virtual SubGroup SubGroup { set; get; }

        public virtual ParameterTitle ParameterTitle { get; set; }

        public virtual List<ParameterInvolvedMisc> ParameterInvolvedMiscs { set; get; }

        public virtual List<ParameterInvolvedContractField> ParameterInvolvedContractFields { get; set; }

        [NotMapped]
        public Dictionary<ContractField, bool> TempParameterInvolvedContractFieldsLeftJoined
        {
            get
            {
                if (_tempParameterInvolvedContractFieldsLeftJoined != null)
                    return _tempParameterInvolvedContractFieldsLeftJoined;
                var cfs = ParameterInvolvedContractFields.Select(p => p.ContractField).ToList();
                _tempParameterInvolvedContractFieldsLeftJoined = new Dictionary<ContractField, bool>();
                cfs.ForEach(p => _tempParameterInvolvedContractFieldsLeftJoined.Add(p, true));
                SubGroup.MainGroup.CurrentContractFields.Except(cfs).ToList().ForEach(p =>
                    _tempParameterInvolvedContractFieldsLeftJoined.Add(p, false));
                return _tempParameterInvolvedContractFieldsLeftJoined;
            }
            set => _tempParameterInvolvedContractFieldsLeftJoined = value;
        }

        [NotMapped]
        public Dictionary<Misc, bool> TempParameterInvolvedMiscPaymentsLeftJoined
        {
            get
            {
                if (_tempParameterInvolvedMiscPaymentsLeftJoined != null)
                    return _tempParameterInvolvedMiscPaymentsLeftJoined;
                var mp = ParameterInvolvedMiscs.Select(m => m.Misc).ToList();
                _tempParameterInvolvedMiscPaymentsLeftJoined = new Dictionary<Misc, bool>();
                mp.ForEach(m => _tempParameterInvolvedMiscPaymentsLeftJoined.Add(m, true));
                SubGroup.CurrentMiscPayments.Except(mp).ToList()
                    .ForEach(m => _tempParameterInvolvedMiscPaymentsLeftJoined.Add(m, false));
                return _tempParameterInvolvedMiscPaymentsLeftJoined;
            }
            set => _tempParameterInvolvedMiscPaymentsLeftJoined = value;
        }

        [NotMapped]
        public string ValueAndValueType => $"{Value:N0} {ValueType.GetDescription()}";

        public override void ValidateProperty(string propertyName)
        {
            switch (propertyName)
            {
                case nameof(Value):
                {
                    ValidateMandatory(Value, nameof(Value));
                    ValidateNonNegative(Value, nameof(Value));
                }
                    break;
            }
        }

    }
}