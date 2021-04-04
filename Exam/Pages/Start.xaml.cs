using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using static Exam.ExaminationSwitcher;

namespace Exam.Pages
{
	/// <summary>
	/// Логика взаимодействия для Start.xaml
	/// </summary>
	public partial class Start : Page, ISwitchable
	{
		public Start()
		{
			InitializeComponent();
		}

		public void UtilizeState(Object state)
		{
			throw new NotImplementedException();
		}
	}
}
