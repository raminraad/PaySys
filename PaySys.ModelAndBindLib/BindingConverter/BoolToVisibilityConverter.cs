using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace PaySys.ModelAndBindLib.BindingConverter
{
	public class BoolToVisibilityConverter:IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
      {
          var flag = false;
	      if( value is bool b )
		      flag = b;

	      //if false is passed as a converter parameter then reverse the value of input value
	      if( parameter == null )
		      return flag ? Visibility.Visible : Visibility.Collapsed;

	      if (bool.TryParse(parameter.ToString(), out var par) && !par) flag = !flag;

	      return flag ? Visibility.Visible : Visibility.Collapsed;
      }
    
      public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
      {
          if (value is Visibility visibility)
          {
              return visibility == Visibility.Visible;
          }
          return false;
      }
      
	}
}
