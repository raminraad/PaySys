using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using PaySys.Globalization;
using PaySys.Model.ExtensionMethods;

namespace PaySys.Model.Validation.INotifyDataErrorInfo
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


        /// <summary>
        /// Calls Validation.GetHasError() on all children of type Y of given DependencyObject and returns true if all are error-free
        /// </summary>
        /// <typeparam name="Y">Type of children to check for errors</typeparam>
        /// <param name="depObject">Parent object</param>
        /// <returns>True if all children are error-free, False if there is at least an error </returns>
        public static bool ChildrenAreValid<Y>(DependencyObject depObject) where Y : DependencyObject
        {
            var invalid = System.Windows.Controls.Validation.GetHasError(depObject);
            if (!invalid)
                foreach (var dependencyObject in depObject.FindVisualChildren<Y>())
                    if (invalid = System.Windows.Controls.Validation.GetHasError(dependencyObject))
                        break;
            return !invalid;
        }

        public static void ValidateTextBox(TextBox textBox)
        {
            if (textBox == null) return;
            if (BindingOperations.GetBinding(textBox, TextBox.TextProperty)?.ValidationRules?.Count > 0)
                textBox.GetBindingExpression(TextBox.TextProperty)?.ValidateWithoutUpdate();
        }
    }
}