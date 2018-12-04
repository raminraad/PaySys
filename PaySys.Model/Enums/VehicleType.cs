using System.ComponentModel;
using PaySys.Model.Converters;

namespace PaySys.Model.Enums
{
    [TypeConverter(typeof(EnumDescriptionTypeConverter))]
    public enum VehicleType
    {
        //todo: fill enum
        [Description("وارد نشده")] Unknown = 0,

        [Description("تاکسی")] Cap,

        [Description("هواپیما")] Plane,

        [Description("قطار")] Train,

        [Description("خودرو شخصی")] Personal,

        [Description("نقلیه عمومی")] PublicTransport
    }
}