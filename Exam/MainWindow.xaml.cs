using Exam.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Exam
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();
		}

		private void Admin_Click(object sender, RoutedEventArgs e)
		{
			Helper.HideElement(RoleSelector);
			Helper.UnHideElement(Loginlayer);
			this.Width = 330;
			this.Height = 250;
		}

		private void Student_Click(object sender, RoutedEventArgs e)
		{
			Helper.HideElement(RoleSelector);
			Helper.HideElement(PasswordInfo);
			Helper.HideElement(PwdValue);
			Helper.UnHideElement(Loginlayer);
			this.Width = 330;
			this.Height = 250;
			/*
			ExaminationSwitcher examination = new ExaminationSwitcher();
			examination.Owner = this;
			this.Hide();
			examination.Show();
			*/
		}

		private void Cancel_Click(object sender, RoutedEventArgs e)
		{
			this.Width = 320;
			this.Height = 230;
			Helper.UnHideElement(RoleSelector);
			Helper.UnHideElement(PasswordInfo);
			Helper.UnHideElement(PwdValue);
			Helper.HideElement(Loginlayer);
		}

		private void Login_Click(object sender, RoutedEventArgs e)
		{
			if (PwdValue.Visibility == Visibility.Hidden)
			{
				ExaminationSwitcher examination = new ExaminationSwitcher(LoginValue.Text);
				examination.Owner = this;
				this.Hide();
				examination.Show();
			}
		}
	}
}
