using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using PaySys.ModelAndBindLib.Annotations;

namespace PaySys.ModelAndBindLib.Entities
{
    public abstract class EntityBase:Validator,INotifyPropertyChanged
    {
        public int Id { get; set; }

        public bool ValueExistsInPropertiesValues(string valueToSearch, bool exactMatch = false)
        {
            var propsToCheck = this.GetType().GetProperties().Where(a => a.PropertyType != typeof(bool));
            if (exactMatch)
            {
                if (propsToCheck.Any(propertyInfo => propertyInfo.GetValue(this).ToString().Equals(valueToSearch)))
                    return true;
            }
            else
            {
                if (propsToCheck.Any(propertyInfo => propertyInfo.GetValue(this).ToString().Contains(valueToSearch)))
                    return true;
            }

            return false;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
