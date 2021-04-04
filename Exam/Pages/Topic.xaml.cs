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
using Exam.Classes;

namespace Exam.Pages
{
	/// <summary>
	/// Логика взаимодействия для Topic.xaml
	/// </summary>
	public partial class Topic : Page, ISwitchable
	{
		public Topic()
		{
			InitializeComponent();
		}

		public void UtilizeState(Object state)
		{
			throw new NotImplementedException();
		}

		private void Accept_Click(object sender, RoutedEventArgs e)
		{
			Switcher.Switch(new Start());
		}

		private void Cancel_Click(object sender, RoutedEventArgs e)
		{

		}
	}
}
