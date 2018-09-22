﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace PaySys.UI.ExtensionMethods
{
    public static class UcMethods
    {
	    public static IEnumerable<T> FindVisualChildren<T>(this DependencyObject depObj) where T : DependencyObject
	    {
		    if (depObj == null)
			    yield break;
		    for (var i = 0; i < VisualTreeHelper.GetChildrenCount(depObj); i++)
		    {
			    var child = VisualTreeHelper.GetChild(depObj, i);
			    if (child != null && child is T) yield return (T)child;

		        foreach (var childOfChild in FindVisualChildren<T>(child)) yield return childOfChild;
		    }
	    }

	}
}
