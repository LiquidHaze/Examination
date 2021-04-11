using Exam.Classes;
using System;
using System.Linq;
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
		public ExerciseDM currQ;
		public int _i = 99;
		public static string selectedOption;

		public Start(bool reset = false)
		{
			_i = 99;
			currQ = null;
			selectedOption = null;
			if (reset)
				ProgressBarStorage.ProgressReset();
			InitializeComponent();

			this.Loaded += Star_Page_Loaded;
		}
		public Start(int i)
		{
			_i = i;
			currQ = null;
			selectedOption = null;
			InitializeComponent();
			if (!ProgressBarStorage.progress.Contains(2))
				Helper.UnHideElement(Done);
			this.Loaded += Star_Page_Loaded;
		}

		private void Star_Page_Loaded(object sender, EventArgs e)
		{
			AppendActiveQuestion();
			qNumber.Content = (qList.IndexOf(currQ) + 1).ToString();
			SolidColorBrush qMrakerColor;
			switch (ProgressBarStorage.progress[qList.IndexOf(currQ)])
			{
				case 1:
					qMrakerColor = new SolidColorBrush(Colors.Green);
					break;
				case 0:
					qMrakerColor = new SolidColorBrush(Colors.Red);
					break;
				case 2:
					qMrakerColor = new SolidColorBrush(Colors.Black);
					break;
				default:
					qMrakerColor = new SolidColorBrush(Colors.Black);
					break;
			}
			qMarker.Foreground = qMrakerColor;
			foreach (Viewbox vb in ProgressBar.Children)
			{
				RadioButton rb = (RadioButton)vb.Child;
				if (ProgressBarStorage.progress[ProgressBar.Children.IndexOf(vb)] == 1)
				{
					rb.Background = new SolidColorBrush(Colors.Green);
				}
				else if (ProgressBarStorage.progress[ProgressBar.Children.IndexOf(vb)] == 0)
				{
					rb.Background = new SolidColorBrush(Colors.Red);
				}
				if (qList.IndexOf(currQ) == ProgressBar.Children.IndexOf(vb))
				{
					rb.Background = new SolidColorBrush(Colors.Yellow);
					rb.IsChecked = true;
				}
			}
			/*
			if (qHolder.ActualHeight > 450)
			{
				this.Height = qHolder.ActualHeight + 200;
			}
			*/
		}

		private void AppendActiveQuestion()
		{
			if (_i != 99)
			{
				currQ = qList[_i];
			}
			else
			{
				currQ = qList.FirstOrDefault(src => src.done == false);
			}
			if (_i < 10 && ProgressBarStorage.progress[_i] != 2)
			{
				int[] opt = { currQ.correctIndex, currQ.currentIndex };
				qHolder.AppendRadioButton(currQ.options, opt);
			}
			else
			{
				qHolder.AppendRadioButton(currQ.options);
			}

			Question.Text = currQ.question;
		}

		public void UtilizeState(Object state)
		{
			throw new NotImplementedException();
		}

		private void Accept_Click(object sender, RoutedEventArgs e)
		{
			//тут проверка правильности ответа
			if ((currQ.done == true && ProgressBarStorage.progress[qList.IndexOf(currQ)] != 2) || string.IsNullOrEmpty(selectedOption))
			{ 
				Skip_Click(sender, e);
				return;
			}
			currQ.currentIndex = currQ.options.IndexOf(selectedOption);
			foreach (Viewbox vb in ProgressBar.Children)
			{
				if (ProgressBar.Children.IndexOf(vb) == qList.IndexOf(currQ))
				{
					if (currQ.correctIndex == currQ.currentIndex)
					{
						ProgressBarStorage.progress[ProgressBar.Children.IndexOf(vb)] = 1;
					}
					else
					{
						ProgressBarStorage.progress[ProgressBar.Children.IndexOf(vb)] = 0;
					}
				}
			}

			if (ProgressBarStorage.progress.Where(src => src != 2).Count() == 10)
			{
				if (currQ.correctIndex == currQ.currentIndex)
				{
					((RadioButton)((Viewbox)ProgressBar.Children[9]).Child).Background = new SolidColorBrush(Colors.Green);
				}
				else
				{
					((RadioButton)((Viewbox)ProgressBar.Children[9]).Child).Background = new SolidColorBrush(Colors.Red);
				}
				Helper.UnHideElement(Done);
			}
			else if (qList.IndexOf(currQ) < 9 && currQ.done == false)
			{
				currQ.done = true;
				Switcher.Switch(new Start());
			}
			else
			{
				currQ.done = true;
				Switcher.Switch(new Start(qList.IndexOf(qList.FirstOrDefault(src => ProgressBarStorage.progress[qList.IndexOf(src)] == 2))));
			}
		}

		private void Skip_Click(object sender, RoutedEventArgs e)
		{
			currQ.done = true;
			int index = qList.IndexOf(qList.FirstOrDefault(src =>
					ProgressBarStorage.progress[qList.IndexOf(src)] == 2
					&& qList.IndexOf(src) > qList.IndexOf(currQ)));

			if (index == -1)
			{
				index = qList.IndexOf(qList.FirstOrDefault(src =>
					ProgressBarStorage.progress[qList.IndexOf(src)] == 2
					&& qList.IndexOf(src) != qList.IndexOf(currQ)));
			}
			if (index != -1)
			{
				Switcher.Switch(new Start(index));
			}
			Helper.UnHideElement(Done);
		}

		private void Done_Click(object sender, RoutedEventArgs e)
		{
			//Helper.StarWars();
			StringBuilder sb = new StringBuilder();
			sb.AppendLine("Верно отвеченные вопросы:");
			sb.AppendLine();
			foreach (ExerciseDM edm in qList)
			{
				if (edm.correctIndex == edm.currentIndex)
				{
					sb.AppendLine("- " + edm.question);
				}
			}
			sb.AppendLine();
			sb.AppendLine();
			sb.AppendLine("Неверно отвеченные вопросы:");
			sb.AppendLine();
			foreach (ExerciseDM edm in qList)
			{
				if (edm.correctIndex != edm.currentIndex)
				{
					sb.AppendLine("- " + edm.question);
				}
			}
			sb.AppendLine();
			sb.AppendLine();
			sb.Append("ОЦЕНКА: ");
			if (ProgressBarStorage.progress.Sum() >= 9)
			{
				sb.AppendLine("5 - Отлично");
			}
			else if (ProgressBarStorage.progress.Sum() == 8)
			{
				sb.AppendLine("4 - Хорошо");
			}
			else if (ProgressBarStorage.progress.Sum() == 7)
			{
				sb.AppendLine("3 - Удовлетворительно");
			}
			else if (ProgressBarStorage.progress.Sum() < 7)
			{
				sb.AppendLine("НЕУДОВЛЕТВОРИТЕЛЬНО!");
			}
			Helper.WriteDoc(_User + ".txt", sb.ToString(), false);

			MessageBox.Show(sb.ToString());

			Window win = Window.GetWindow(this);
			win.Close();
		}

		private void RadioButton_Click(object sender, RoutedEventArgs e)
		{
			RadioButton rb = e.OriginalSource as RadioButton;
			DependencyObject parent = rb.Parent;
			while (!(parent is RadioButton || parent is Viewbox))
			{
				parent = rb.Parent;
			}
			Viewbox vb = parent as Viewbox;
			int index = ProgressBar.Children.IndexOf(vb);
			if (ProgressBarStorage.progress[index] != 2)
			{
				Switcher.Switch(new Start(index));
			}
		}
	}
}
