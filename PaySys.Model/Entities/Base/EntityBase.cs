using System;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using PaySys.Model.Attributes;
using PaySys.Model.Properties;
using PaySys.Model.Validation.INotifyDataErrorInfo;

namespace PaySys.Model.Entities.Base
{
    public abstract class EntityBase : Validator, INotifyPropertyChanged
    {
        public int Id { get; set; }

        /// <summary>
        /// Uses reflection to check wether a given value exists in values of all properties of object
        /// </summary>
        /// <param name="valueToSearch">Value to lookup in object</param>
        /// <param name="exactMatch">True: Property should equal to given value exactly , False: Properties that contains the given value are accepted</param>
        /// <returns>True: Value exists in object , False: Value doesn't exist in object</returns>
        public bool ContainsValue(string valueToSearch, bool exactMatch = false)
        {
            var propsToCheck = this.GetType().GetProperties()
                .Where(prop => Attribute.IsDefined(prop, typeof(IncludeInLookupAttribute)));
            var propertyInfos = propsToCheck.ToList();
            if (!propertyInfos.Any()) return false;
            if (exactMatch)
            {
                if (propertyInfos.Any(propertyInfo =>
                    propertyInfo.GetValue(this) != null &&
                    propertyInfo.GetValue(this).ToString().Equals(valueToSearch)))
                    return true;
            }
            else
            {
                if (propertyInfos.Any(propertyInfo =>
                    propertyInfo.GetValue(this) != null &&
                    propertyInfo.GetValue(this).ToString().Contains(valueToSearch)))
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