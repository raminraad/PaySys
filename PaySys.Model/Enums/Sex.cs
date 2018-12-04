using System.ComponentModel;
using PaySys.Model.Converters;

namespace PaySys.Model.Enums
{
    [TypeConverter(typeof(EnumDescriptionTypeConverter))]
    public enum Sex
    {
        [Description("وارد نشده")] Unknown = 0,

        [Description("مذکر")] Male,

        [Description("مؤنث")] Female
    }
}