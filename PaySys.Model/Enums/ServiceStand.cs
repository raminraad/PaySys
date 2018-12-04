using System.ComponentModel;
using PaySys.Model.Converters;

namespace PaySys.Model.Enums
{
    #region Enums


    [TypeConverter(typeof(EnumDescriptionTypeConverter))]
    public enum ServiceStand
    {
        [Description("منطقه عادی")] RegularZone = 0,

        [Description("منطقه آزاد")] FreeZone,

        [Description("منطقه محروم")] DeprivedZone,
    }
    #endregion
}
