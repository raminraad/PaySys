using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Windows;
using PaySys.Model.Entities.Base;
using ValueType = PaySys.Model.Enums.ValueType;

namespace PaySys.Model.Entities
{
    /// <summary> #37 متغیرهای ماهانه برای اشخاص </summary>
    public class VariableValueForEmployee : EntityBase
    {
        public double? NumericValue { get; set; }

        public string StringValue { get; set; }

        public int Year { get; set; }

        public int Month { get; set; }

        public DateTime? DateValue { get; set; }

        public virtual Employee Employee { get; set; }

        public virtual Variable Variable { get; set; }

        [NotMapped]
        public object Value
        {
            get
            {
                switch (Variable.ValueType)
                {
                    case ValueType.Unknown:
                        return null;
                    case ValueType.Absolute:
                    case ValueType.Rial:
                    case ValueType.Percent:
                        return NumericValue;
                    case ValueType.Date:
                        return DateValue;
                    case ValueType.String:
                        return StringValue;
                    default:
                        return null;
                }
            }
            set
            {
                switch (Variable.ValueType)
                {
                    case ValueType.Unknown:
                        break;
                    case ValueType.Absolute:
                    case ValueType.Rial:
                    case ValueType.Percent:
                        if (value != null)
                            NumericValue = Convert.ToDouble(value);
                        else
                            NumericValue = null;
                        break;
                    case ValueType.Date:
                        if (value != null)
                            DateValue = Convert.ToDateTime(value);
                        else
                            DateValue = null;
                        break;
                    case ValueType.String:
                        StringValue = value?.ToString();
                        break;
                }
            }
        }

        [NotMapped]
        public Visibility ValueIsNumeric =>
            (Variable.ValueType == ValueType.Absolute || Variable.ValueType == ValueType.Percent ||
             Variable.ValueType == ValueType.Rial)
                ? Visibility.Visible
                : Visibility.Collapsed;

        [NotMapped]
        public Visibility ValueIsString =>
            Variable.ValueType == ValueType.String ? Visibility.Visible : Visibility.Collapsed;

        [NotMapped]
        public Visibility ValueIsDate =>
            Variable.ValueType == ValueType.Date ? Visibility.Visible : Visibility.Collapsed;
    }
}