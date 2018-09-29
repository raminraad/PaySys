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
    public abstract class ValidationRuleBase : ValidationRule
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