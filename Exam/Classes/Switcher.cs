using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Controls;

namespace Exam.Classes
{
	class Switcher
	{
		public static ExaminationSwitcher pageSwitcher;

		public static void Switch(Page newPage)
		{
			pageSwitcher.Navigate(newPage);
		}

		public static void Switch(Page newPage, object state)
		{
			pageSwitcher.Navigate(newPage, state);
		}
	}
}
