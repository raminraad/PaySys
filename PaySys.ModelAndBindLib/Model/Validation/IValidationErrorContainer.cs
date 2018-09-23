using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace PaySys.ModelAndBindLib.Model
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
