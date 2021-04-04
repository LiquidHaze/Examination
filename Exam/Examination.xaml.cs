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

		public ExaminationSwitcher()
		{
			InitializeComponent();
			Switcher.pageSwitcher = this;
			Switcher.Switch(new Topic());
			this.Closed += MainWindow_Closed;
			WriteDoc();
			ReadDoc();
			//List<ExerciseDM> edm = JsonSerializer.Deserialize<List<ExerciseDM>>(_data);
			var result = JsonConvert.DeserializeObject<List<TopicDM>>(_data);
		}

		private void MainWindow_Closed(object sender, EventArgs e)
		{
			Owner.Show();
		}

		static async Task ReadDoc()
		{
			//string path = @"C:\SomeDir\hta.txt";
			string path = "test2.txt";

			try
			{
				using (StreamReader sr = new StreamReader(path))
				{
					_data = Convert.ToBase64String(Encoding.ASCII.GetBytes(sr.ReadToEnd()));
				}
				// асинхронное чтение
				/*using (StreamReader sr = new StreamReader(path))
				{
					Console.WriteLine(await sr.ReadToEndAsync());
				}*/
			}
			catch (Exception e)
			{
				Console.WriteLine(e.Message);
			}
		}

		void WriteDoc()
		{
			string writePath = "testdata";
			string text = @"[{""title"":""Tema 1"",""content"":""Lorem ipsum dolor sit amet"",""exercises"":[{""question"":""1 - Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur.Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum."",""options"":[""1 Lorem ipsum dolor sit amet"",""2 Lorem ipsum dolor sit amet"",""3 Lorem ipsum dolor sit amet"",""4 Lorem ipsum dolor sit amet""]},{""question"":""2 - Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur.Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum."",""options"":[""1 Lorem ipsum dolor sit amet"",""2 Lorem ipsum dolor sit amet"",""3 Lorem ipsum dolor sit amet"",""4 Lorem ipsum dolor sit amet""]},{""question"":""3 - Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur.Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum."",""options"":[""1 Lorem ipsum dolor sit amet"",""2 Lorem ipsum dolor sit amet"",""3 Lorem ipsum dolor sit amet"",""4 Lorem ipsum dolor sit amet""]},{""question"":""4 - Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur.Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum."",""options"":[""1 Lorem ipsum dolor sit amet"",""2 Lorem ipsum dolor sit amet"",""3 Lorem ipsum dolor sit amet"",""4 Lorem ipsum dolor sit amet""]}]},{""title"":""Tema 1"",""content"":""Lorem ipsum dolor sit amet"",""exercises"":[{""question"":""1 - Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur.Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum."",""options"":[""1 Lorem ipsum dolor sit amet"",""2 Lorem ipsum dolor sit amet"",""3 Lorem ipsum dolor sit amet"",""4 Lorem ipsum dolor sit amet""]},{""question"":""2 - Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur.Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum."",""options"":[""1 Lorem ipsum dolor sit amet"",""2 Lorem ipsum dolor sit amet"",""3 Lorem ipsum dolor sit amet"",""4 Lorem ipsum dolor sit amet""]},{""question"":""3 - Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur.Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum."",""options"":[""1 Lorem ipsum dolor sit amet"",""2 Lorem ipsum dolor sit amet"",""3 Lorem ipsum dolor sit amet"",""4 Lorem ipsum dolor sit amet""]},{""question"":""4 - Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur.Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum."",""options"":[""1 Lorem ipsum dolor sit amet"",""2 Lorem ipsum dolor sit amet"",""3 Lorem ipsum dolor sit amet"",""4 Lorem ipsum dolor sit amet""]}]}]";
			try
			{
				using (StreamWriter sw = new StreamWriter(writePath, false, System.Text.Encoding.Default))
				{
					sw.WriteLine(Convert.FromBase64String(text));
				}

				/*using (StreamWriter sw = new StreamWriter(writePath, true, System.Text.Encoding.Default))
				{
					sw.WriteLine("Дозапись");
					sw.Write(4.5);
				}*/
				Console.WriteLine("Запись выполнена");
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
