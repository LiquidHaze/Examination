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
using System.Linq;

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
			this.Loaded += Topic_Page_Loaded;
		}

		private void Topic_Page_Loaded(object sender, EventArgs e)
		{
			TopicList.AppendRadioButton(TopicsList);
		}

		public void UtilizeState(Object state)
		{
			//toplist = (List<TopicDM>)state;
			throw new NotImplementedException();
		}

		private void Accept_Click(object sender, RoutedEventArgs e)
		{
			GenerateQuestionList();
			Switcher.Switch(new Start());
		}

		private void GenerateQuestionList()
		{
			TopicDM selectedTopic = TopicsList.FirstOrDefault(src => src.title == SelectedTopic);
			qList = selectedTopic.exercises;
			Random rnd = new Random();
			while (qList.Count > 10)
			{
				qList.RemoveAt(rnd.Next(0, qList.Count()-1));
			}
		}

		private void Cancel_Click(object sender, RoutedEventArgs e)
		{
			Window win = Window.GetWindow(this);
			win.Close();
		}
	}
}
