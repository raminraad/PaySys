using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using PaySys.Globalization;

namespace PaySys.UI.Modals
{
	public static class PaySysMessage
	{

		public static MessageBoxResult GetDeleteItemConfirmation()
		{
			String strmsg = ResourceAccessor.Messages.GetString("DeleteConfirmationOfItem", CultureInfo.CurrentCulture);
			return ShowDefaultYesNoMessage(strmsg);
		}

		public static MessageBoxResult GetDeleteSubGroupMiscConfirmation(string miscTitle,string miscsListTitle)
		{
			String strmsg = ResourceAccessor.Messages.GetString("DeleteConfirmationOfMisc", CultureInfo.CurrentCulture);
			strmsg = strmsg.Replace("XXX", miscTitle).Replace("YYY", miscsListTitle);
			return ShowDefaultYesNoMessage(strmsg);
		}

		private static MessageBoxResult ShowDefaultYesNoMessage(string strmsg)
		{
			return MessageBox.Show(strmsg, "Delete", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No,MessageBoxOptions.RtlReading);
		}
	}
}