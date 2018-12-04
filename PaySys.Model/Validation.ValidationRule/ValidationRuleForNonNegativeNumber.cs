using System.Windows.Controls;
using PaySys.Globalization;

namespace PaySys.Model.Validation.ValidationRule
{
    public class ValidationRuleForNonNegativeNumber : ValidationRuleBase
    {

        public string Message=> ResourceAccessor.Validation.GetString("NegativeValueNotSupported").Replace("name",FurbishedFieldName);
        

        public override ValidationResult Validate(object value, System.Globalization.CultureInfo cultureInfo)
        {
            double.TryParse(value?.ToString(), out var val);
            if (val<0) return new ValidationResult(false, Message);
            else return ValidationResult.ValidResult;
        }
    }
}