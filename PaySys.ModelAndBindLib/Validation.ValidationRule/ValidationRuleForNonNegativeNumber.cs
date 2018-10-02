using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using PaySys.Globalization;
using PaySys.ModelAndBindLib.Annotations;

namespace PaySys.ModelAndBindLib.Validation
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