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
    public class ValidationRuleForMandatory : ValidationRuleBase
    {
        public string Message => ResourceAccessor.Validation.GetString("IsMandatory").Replace("name",FurbishedFieldName);
        public override ValidationResult Validate(object value, System.Globalization.CultureInfo cultureInfo)
        {
            return string.IsNullOrEmpty((string) value) ? new ValidationResult(false, Message) : ValidationResult.ValidResult;
        }
    }
}