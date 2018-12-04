using System.ComponentModel;
using PaySys.Model.Converters;

namespace PaySys.Model.Enums
{
    [TypeConverter(typeof(EnumDescriptionTypeConverter))]
    public enum SacrificeStand
    {
        [Description("وارد نشده")] Unknown = 0,

        [Description("رزمنده")] Razmande,

        [Description("آزاده")] Azade,

        [Description("خانواده آزاده")] KhanevadeAzade,

        [Description("جانباز")] Janbaz,

        [Description("خانواده جانباز")] KhanevadeJanbaz,

        [Description("خانواده شهید")] KhanevadeShahid,

        [Description("ایثارگر")] Isargar,

        [Description("سایر موارد")] Other
    }
}