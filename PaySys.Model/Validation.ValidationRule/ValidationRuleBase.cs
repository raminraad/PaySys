using System.Windows.Controls;
using PaySys.Globalization;
using PaySys.Model.Properties;

namespace PaySys.Model.Validation.ValidationRule
{
    public abstract class ValidationRuleBase : System.Windows.Controls.ValidationRule
    {
        [CanBeNull]
        public string FieldName { get; set; }
        public string FurbishedFieldName => string.IsNullOrEmpty(FieldName)
            ? ResourceAccessor.Labels.GetString("Field")
            : $"{ResourceAccessor.Labels.GetString("SurrounderOfFieldNameStart")}{FieldName}{ResourceAccessor.Labels.GetString("SurrounderOfFieldNameEnd")}";


        public override ValidationResult Validate(object value, System.Globalization.CultureInfo cultureInfo)
        {
            return ValidationResult.ValidResult;
        }
    }
}