using System.Windows.Controls;
using PaySys.Globalization;

namespace PaySys.Model.Validation.ValidationRule
{
    public class ValidationRuleForMandatory : ValidationRuleBase
    {
        public string Message => ResourceAccessor.Validation.GetString("IsMandatory").Replace("name",FurbishedFieldName);
        public override ValidationResult Validate(object value, System.Globalization.CultureInfo cultureInfo)
        {
            return string.IsNullOrEmpty((string) value) ? new ValidationResult(false, Message) : ValidationResult.ValidResult;
        }
    }
}