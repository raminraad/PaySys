using System;
using Arash;

namespace PaySys.Model.Static
{
	public static class PaySysSetting
	{
		public static int CurrentYear
		{
			get => CurrentYearStartPers.Year;
			set
			{
				CurrentYearStartPers=new PersianDate(value,1,1);
				CurrentYearEndPers=new PersianDate(value+1,1,1).AddDays( -1 );
				CurrentYearStartGreg = CurrentYearStartPers.ToDateTime();
				CurrentYearEndGreg = CurrentYearEndPers.ToDateTime();
			}
		}

		public static DateTime CurrentYearStartGreg { set; get; }
		public static DateTime CurrentYearEndGreg { set; get; }
		public static PersianDate CurrentYearStartPers { set; get; }
		public static PersianDate CurrentYearEndPers { set; get; }

		public static int CurrentMonth
		{
			get => CurrentMonthStartPers.Month;
			set
			{
				CurrentMonthStartPers=new PersianDate(CurrentYear,value,1);
				CurrentMonthEndPers = value < 12 ? new PersianDate( CurrentYear, value + 1, 1 ).AddDays( -1 ) : CurrentYearEndPers;
				CurrentMonthStartGreg = CurrentMonthStartPers.ToDateTime();
				CurrentMonthEndGreg = CurrentMonthEndPers.ToDateTime();
			}
		}

		public static DateTime CurrentMonthStartGreg { set; get; }
		public static DateTime CurrentMonthEndGreg { set; get; }
		public static PersianDate CurrentMonthStartPers { set; get; }
		public static PersianDate CurrentMonthEndPers { set; get; }
	}
}
