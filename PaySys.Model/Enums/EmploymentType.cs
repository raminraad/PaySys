using System.ComponentModel;
using PaySys.Model.Converters;

namespace PaySys.Model.Enums
{
    [TypeConverter(typeof(EnumDescriptionTypeConverter))]
    public enum EmploymentType
    {
        [Description("وارد نشده")] Unknown = 0,

        [Description("رسمی آزمایشی")] Rasmiazmayeshi,

        [Description("رسمی قطعی")] Rasmiqatei,

        [Description("شرکتی")] Sherkati,

        [Description("پیمانی")] Peymani,

        [Description("قراردادی")] Qarardadi,

        [Description("تعاونی")] Taavoni,

        [Description("فصلی")] Fasli,

        [Description("خرید خدمتی")] Kharidkhedmati,

        [Description("روز مزد")] Ruzmozd,

        [Description("سایر موارد")] Other
    }
}