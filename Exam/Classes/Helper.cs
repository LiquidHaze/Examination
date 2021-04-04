using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;

namespace Exam.Classes
{
	public static class Helper
	{
		public static void HideElement(UIElement obj)
		{
			obj.IsEnabled = false;
			obj.Visibility = Visibility.Hidden;
		}

		public static void UnHideElement(UIElement obj)
		{
			obj.IsEnabled = true;
			obj.Visibility = Visibility.Visible;
		}
	}
}
