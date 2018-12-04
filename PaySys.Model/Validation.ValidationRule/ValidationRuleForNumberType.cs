using System.Windows.Controls;
using PaySys.Globalization;

namespace PaySys.Model.Validation.ValidationRule
{
    public class ValidationRuleForNumberType : ValidationRuleBase
    {
        public string Message =>
            ResourceAccessor.Validation.GetString("NumberTypeRequired").Replace("name", FurbishedFieldName);
        public override ValidationResult Validate(object value, System.Globalization.CultureInfo cultureInfo)
        {
            if (string.IsNullOrEmpty((string)value)||double.TryParse(value.ToString(), out var val)) return ValidationResult.ValidResult;
            return new ValidationResult(false, Message);
        }
    }
}