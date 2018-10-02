using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PaySys.Globalization;

namespace PaySys.ModelAndBindLib.Entities
{
    public class Validator : ValidationErrorContainer
    {
        public const string Constraint_Mandatory = "IsMandatory";
        public const string Constraint_MustBeNonNegative = "NonNegative";

        public virtual void ValidateProperty(string propertyName)
        {
        }

        protected void ValidateNonNegative(double? value, string fieldName)
        {
            if (value.HasValue && value.Value < 0.0)
                AddError(new ValidationError(fieldName, Constraint_MustBeNonNegative,
                    ResourceAccessor.Messages.GetString("NegativeValueNotSupported").Replace("name", ResourceAccessor.Labels.GetString("Field"))));
            else
                RemoveError(fieldName, Constraint_MustBeNonNegative);
        }

        protected void ValidateMandatory(double? value, string fieldName)
        {
            if (!value.HasValue)
                AddError(new ValidationError(fieldName, Constraint_Mandatory,
                    ResourceAccessor.Messages.GetString("IsMandatory")));
            else
                RemoveError(fieldName, Constraint_Mandatory);
        }
    }
}