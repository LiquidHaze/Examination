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
using System.Windows.Shapes;

namespace Exam
{
	/// <summary>
	/// Interaction logic for Logo.xaml
	/// </summary>
	public partial class Logo : Window
	{
		public Logo()
		{
			InitializeComponent();
		}

		private void logo_Click(object sender, RoutedEventArgs e)
		{
			this.Close();
			this.Owner.Show();
		}
	}
}
