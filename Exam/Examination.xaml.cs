using Exam.Classes;
using Exam.Pages;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
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
	public partial class ExaminationSwitcher : Window
	{
		static string _data;
		public static List<TopicDM> TopicsList;
		public static string SelectedTopic;
		public static List<ExerciseDM> qList;

		public ExaminationSwitcher()
		{
			InitializeComponent();
			Switcher.pageSwitcher = this;
			ReadDoc();
			TopicsList = JsonConvert.DeserializeObject<List<TopicDM>>(_data);
			Topic TopicPage = new Topic();
			Switcher.Switch(TopicPage);
			this.Closed += MainWindow_Closed;
			
			var result = JsonConvert.DeserializeObject<List<TopicDM>>(_data);
		}

		private void MainWindow_Closed(object sender, EventArgs e)
		{
			Owner.Show();
		}

		static async Task ReadDoc()
		{
			//string path = @"C:\SomeDir\hta.txt";
			string path = "testdata";

			try
			{
				using (StreamReader sr = new StreamReader(path))
				{
					_data = Encoding.ASCII.GetString(Convert.FromBase64String(sr.ReadToEnd()));
				}
			}
			catch (Exception e)
			{
				Console.WriteLine(e.Message);
			}
		}

		public void Navigate(Page nextPage)
		{
			this.Content = nextPage;
		}

		public void Navigate(Page nextPage, object state)
		{
			this.Content = nextPage;
			ISwitchable s = nextPage as ISwitchable;

			if (s != null)
				s.UtilizeState(state);
			else
				throw new ArgumentException("NextPage is not ISwitchable! "
				  + nextPage.Name.ToString());
		}
		public interface ISwitchable
		{
			void UtilizeState(object state);
		}
	}
}
