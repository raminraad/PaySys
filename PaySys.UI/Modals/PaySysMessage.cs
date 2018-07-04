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
			String strmsg = ResourceAccessor.Messages.GetString("DeleteItem", CultureInfo.CurrentCulture);
			return MessageBox.Show(strmsg, "Delete", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No);
		}

	}
}