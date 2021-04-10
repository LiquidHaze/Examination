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
using System.Media;
using System.Threading;

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
		public static string _User;
		public static List<ExerciseDM> qList;

		public ExaminationSwitcher(string user)
		{
			InitializeComponent();
			_User = user;
			Switcher.pageSwitcher = this;
			ReadDoc();
			TopicsList = JsonConvert.DeserializeObject<List<TopicDM>>(_data);
			//Helper.SuperMario();
			Topic TopicPage = new Topic();
			Switcher.Switch(TopicPage);
			this.Closing += ExaminationSwitcher_Closing;
			this.Closed += ExaminationSwitcher_Closed;
			
			var result = JsonConvert.DeserializeObject<List<TopicDM>>(_data);
		}

		private void ExaminationSwitcher_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			//elper.MissionImpossible();
			//SystemSounds.Beep.Play();
			MessageBoxResult result = MessageBox.Show("Если вы закроете окно тест прервётся.\r\nТекущий результат будет созранен как финальный!\r\nПересдача теста без разрешения администратора запрещена!!!\r\nВ случае перезапуска файл ответов будет помечен как \"Недействительный\".", "Прервать тестирование?", MessageBoxButton.YesNo);
			if (result != MessageBoxResult.Yes)
			{
				e.Cancel = true;
			}
			//тут надо сохранить результаты в файл
		}

		private void ExaminationSwitcher_Closed(object sender, EventArgs e)
		{
			Owner.Show();
		}

		static void ReadDoc()
		{
			string path = "data";

			try
			{
				using (StreamReader sr = new StreamReader(path))
				{
					_data = Encoding.UTF8.GetString(Convert.FromBase64String(sr.ReadToEnd()));
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
