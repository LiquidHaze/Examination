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

		public Start()
		{
			_i = 99;
			currQ = null;
			selectedOption = null;
			InitializeComponent();

			this.Loaded += Star_Page_Loaded;
		}
		public Start(int i)
		{
			_i = i;
			currQ = null;
			selectedOption = null;
			InitializeComponent();

			this.Loaded += Star_Page_Loaded;
		}

		private void Star_Page_Loaded(object sender, EventArgs e)
		{
			AppendActiveQuestion();

			foreach (Viewbox vb in ProgressBar.Children)
			{
				if (ProgressBarStorage.progress[ProgressBar.Children.IndexOf(vb)] == 1)
				{
					RadioButton rb = (RadioButton)vb.Child;
					rb.Background = new SolidColorBrush(Colors.Green);
				}
				else if (ProgressBarStorage.progress[ProgressBar.Children.IndexOf(vb)] == 0)
				{
					RadioButton rb = (RadioButton)vb.Child;
					rb.Background = new SolidColorBrush(Colors.Red);
				}
			}
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
			qHolder.AppendRadioButton(currQ.options);
			Question.Text = currQ.question;
		}

		public void UtilizeState(Object state)
		{
			throw new NotImplementedException();
		}

		private void Accept_Click(object sender, RoutedEventArgs e)
		{
			int index;
			//тут проверка правильности ответа
			if (!currQ.done)
			{
				index = qList.IndexOf(qList.FirstOrDefault(src =>
			ProgressBarStorage.progress[qList.IndexOf(src)] != 0
			&& qList.IndexOf(src) > qList.IndexOf(currQ)));
			}
			currQ.done = true;
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
			if (qList.IndexOf(currQ) < 9)
			{
				Switcher.Switch(new Start());
			}
			else
			{
				MessageBox.Show(ProgressBarStorage.progress.Sum().ToString());
				index = qList.IndexOf(qList.FirstOrDefault(src => ProgressBarStorage.progress[qList.IndexOf(src)] != 0));
				Switcher.Switch(new Start(index));
			}
		}

		private void Skip_Click(object sender, RoutedEventArgs e)
		{
			currQ.done = true;
			int index = qList.IndexOf(qList.FirstOrDefault(src =>
			ProgressBarStorage.progress[qList.IndexOf(src)] != 0
			&& qList.IndexOf(src) > qList.IndexOf(currQ)));
			Switcher.Switch(new Start());
		}
		/*
public static IEnumerable<Item> Enumerate(this UIElementCollection collectionItem)
{

	for (int index = 0, count = collectionItem.Count; index < count; index++)
	{
		UIElement currentItem = collectionItem. (index);
		yield return currentItem;
	}
}*/
	}
}
