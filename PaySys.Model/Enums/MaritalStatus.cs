using System.ComponentModel;
using PaySys.Model.Converters;

namespace PaySys.Model.Enums
{
    [TypeConverter(typeof(EnumDescriptionTypeConverter))]
    public enum MaritalStatus
    {
        [Description("وارد نشده")] Unknown = 0,

        [Description("مجرد")] Single,

        [Description("متأهل")] Married
    }
}