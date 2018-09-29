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
    public class ValidationRuleForNumberRange : ValidationRuleBase
    {
        public double? Min { get; set; } = null;
        public double? Max { get; set; } = null;

        public string Message
        {
            get
            {
                if (Min.HasValue && Max.HasValue)
                    return ResourceAccessor.Validation.GetString("NumberValueRange")
                        ?.Replace("name", FurbishedFieldName).Replace("min", Min.ToString())
                        .Replace("max", Max.ToString());
                else if (Min.HasValue)
                    return ResourceAccessor.Validation.GetString("NumberValueMin")?.Replace("name", FurbishedFieldName)
                        .Replace("min", Min.ToString());
                else if (Max.HasValue)
                    return ResourceAccessor.Validation.GetString("NumberValueMax")?.Replace("name", FurbishedFieldName)
                        .Replace("max", Min.ToString());
                return string.Empty;
            }
        }

        public override ValidationResult Validate(object value, System.Globalization.CultureInfo cultureInfo)
        {
            double.TryParse(value?.ToString(), out var val);
            if (((Min.HasValue && Max.HasValue) && (val < Min || val > Max)) || (Min.HasValue && val < Min) ||
                (Max.HasValue && val > Max)) return new ValidationResult(false, Message);
            else return ValidationResult.ValidResult;
        }
    }
}