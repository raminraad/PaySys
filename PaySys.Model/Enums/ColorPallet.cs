using System.ComponentModel;
using PaySys.Model.Converters;

namespace PaySys.Model.Enums
{
    [TypeConverter(typeof(EnumDescriptionTypeConverter))]
    public enum ColorPallet
    {
        [Description("وارد نشده")] Unknown = 0,
        YellowGreen,
        White,
        SeaGreen,
        Red,
        Purple,
        Peru,
        PaleGoldenrod,
        Navy,
        MediumOrchid,
        Maroon,
        Lime,
        LightPink,
        LightCoral,
        Goldenrod,
        Gold,
        GhostWhite,
        ForestGreen,
        DodgerBlue,
        DarkSlateBlue,
        DarkRed,
        Teal,
        CornflowerBlue
    }
}