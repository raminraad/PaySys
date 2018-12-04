using System.ComponentModel;
using PaySys.Model.Converters;

namespace PaySys.Model.Enums
{
    [TypeConverter(typeof(EnumDescriptionTypeConverter))]
    public enum FormCurrentState
    {
        Unknown = 0,
        Select,
        Edit,
        Add,
        AddMaster,
        AddDetails,
        Delete
    }
}