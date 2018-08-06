using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaySys.CalcLib.Converters
{
	public static class DateTool
	{
		public static int GetDay( string date )
		{
			date = date.Trim().Replace( @"/", string.Empty );
			switch( date.Length )
			{
				case 6:
					return Convert.ToInt16( date.Substring( 0, 2 ) );
				case 8:
					return Convert.ToInt16( date.Substring( 0, 4 ) );
			}
			return 0;
		}
	}
}
