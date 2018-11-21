using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PaySys.CalcLib.Converters;

namespace PaySys.ModelAndBindLib.Enums
{
    #region Enums


    [TypeConverter(typeof(EnumDescriptionTypeConverter))]
    public enum ServiceStand
    {
        [Description("منطقه عادی")] RegularZone = 0,

        [Description("منطقه آزاد")] FreeZone,

        [Description("منطقه محروم")] DeprivedZone,
    }

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

    [TypeConverter(typeof(EnumDescriptionTypeConverter))]
    public enum MaritalStatus
    {
        [Description("وارد نشده")] Unknown = 0,

        [Description("مجرد")] Single,

        [Description("متأهل")] Married
    }

    [TypeConverter(typeof(EnumDescriptionTypeConverter))]
    public enum Sex
    {
        [Description("وارد نشده")] Unknown = 0,

        [Description("مذکر")] Male,

        [Description("مؤنث")] Female
    }

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

    public enum MiscType
    {
        None,
        Payment,
        Debt
    }

    public enum ValidateOn
    {
        None,
        TextChanged,
        LostFocus
    }

    public enum NavigationType
    {
        First = -2,
        Previous = -1,
        None = 0,
        Next = 1,
        Last = 2
    }
    #endregion
}
