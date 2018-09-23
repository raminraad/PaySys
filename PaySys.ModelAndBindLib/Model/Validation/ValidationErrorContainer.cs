using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace PaySys.ModelAndBindLib.Model
{
    public class ValidationErrorContainer : IValidationErrorContainer
    {
        protected string lastPropertyValidated = null;

        public virtual bool AddError(ValidationError error, bool isWarning = false)
        {
            Debug.Assert(error != null);
            string propertyName = error.propertyName;

            if (!errors.ContainsKey(propertyName))
                errors[propertyName] = new List<ValidationError>();

            lastPropertyValidated = propertyName;
            if (!errors[propertyName].Contains(error))
            {
                if (isWarning)
                    errors[propertyName].Add(error);
                else
                    errors[propertyName].Insert(0, error);

                NotifyErrorsChanged(propertyName);
                return true;
            }
            else
                return false;
        }

        public virtual bool RemoveError(string propertyName, string errorID)
        {

            ValidationError error = new ValidationError(propertyName, errorID, "");
            if (!errors.ContainsKey(propertyName) || !errors[propertyName].Contains(error))
                return false;

            errors[propertyName].Remove(error);
            if (errors[propertyName].Count == 0)
            {
                errors.Remove(propertyName);
                if (errors.Count == 0)
                    lastPropertyValidated = null; // No errors. Good.
                else
                {
                    Dictionary<string, List<ValidationError>>.Enumerator p = errors.GetEnumerator();
                    p.MoveNext();
                    lastPropertyValidated = p.Current.Key;
                }
            }
            NotifyErrorsChanged(propertyName);
            return true;
        }

        public int ErrorCount
        {
            get { return errors.Count; }
        }

        protected void NotifyErrorsChanged(string propertyName)
        {
            if (ErrorsChanged != null)
                this.ErrorsChanged(this, new DataErrorsChangedEventArgs(propertyName));
        }

        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;

        public IEnumerable GetPropertyErrors(string propertyName)
        {
            if (propertyName == "")
                return null;
            if (errors.Keys.Contains(propertyName))
                return errors[propertyName];
            else
                return null;
        }

        protected Dictionary<string, List<ValidationError>> errors = new Dictionary<string, List<ValidationError>>();
    }
}
