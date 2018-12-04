using System;
using System.ComponentModel;

namespace PaySys.Model.Validation.INotifyDataErrorInfo
{
    public interface IValidationErrorContainer
    {
        bool AddError(ValidationError error, bool isWarning = false);
        bool RemoveError(string propertyName, string errorID);

        int ErrorCount { get; }
        System.Collections.IEnumerable GetPropertyErrors(string propertyName);

        event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;
    }
}
