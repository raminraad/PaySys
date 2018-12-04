using System.ComponentModel;
using PaySys.Model.Converters;

namespace PaySys.Model.Enums
{
    [TypeConverter(typeof(EnumDescriptionTypeConverter))]
    public enum EducationStand
    {
        [Description("وارد نشده")] Unknown = 0,

        [Description("بی سواد")] Bisavad,

        [Description("سیکل")] Sikl,

        [Description("زیر دیپلم")] Zildiplom,

        [Description("دیپلم")] Diplom,

        [Description("کاردانی")] Kardani,

        [Description("کارشناسی")] Karshenasi,

        [Description("فوق لیسانس")] Foqlisans,

        [Description("دکترا")] Doctora,

        [Description("فوق دکترا")] Foqoctora,

        [Description("سایر موارد")] Other
    }
}