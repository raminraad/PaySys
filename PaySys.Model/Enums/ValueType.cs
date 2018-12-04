using System.ComponentModel;
using PaySys.Model.Converters;

namespace PaySys.Model.Enums
{
    [TypeConverter(typeof(EnumDescriptionTypeConverter))]
    public enum ValueType
    {
        [Description("وارد نشده")] Unknown = 0,

        [Description("مطلق")] Absolute,

        [Description("درصد")] Percent,

        [Description("ریـال")] Rial,

        [Description("تاریخ")] Date,

        [Description("رشته")] String,
    }
}