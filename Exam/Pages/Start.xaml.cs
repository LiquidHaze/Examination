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
			qNumber.Content = qList.IndexOf(currQ).ToString();
			qMarker.Foreground = new SolidColorBrush(Colors.Black);
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
				if(qList.IndexOf(currQ) == ProgressBar.Children.IndexOf(vb))
				{
					rb.Background = new SolidColorBrush(Colors.Yellow);
					rb.IsChecked = true;
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
			//int index;
			//тут проверка правильности ответа
			/*я не помню нахуй этот кусок кода, он ни на что не влияет
			 * 
			 * if (!currQ.done)
			{
				index = qList.IndexOf(qList.FirstOrDefault(src =>
					ProgressBarStorage.progress[qList.IndexOf(src)] != 0
					&& qList.IndexOf(src) > qList.IndexOf(currQ)));

				if (index == -1)
				{
					index = qList.IndexOf(qList.FirstOrDefault(src =>
						ProgressBarStorage.progress[qList.IndexOf(src)] != 0
						&& qList.IndexOf(src) != qList.IndexOf(currQ)));
				}
			}*/
			if (currQ.done == true && ProgressBarStorage.progress[qList.IndexOf(currQ)] != 2)
				return;

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

			if (qList.IndexOf(currQ) < 9 && currQ.done == false)
			{
				currQ.done = true;
				Switcher.Switch(new Start());
			}
			else if (ProgressBarStorage.progress.Where(src => src != 2).Count() == 10)
			{
				Helper.UnHideElement(Done);
			}
			else
			{
				/*
				 * так было
				index = qList.IndexOf(qList.FirstOrDefault(src => ProgressBarStorage.progress[qList.IndexOf(src)] != 2));
				Switcher.Switch(new Start(index));
				*/
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
		}

		private void Done_Click(object sender, RoutedEventArgs e)
		{
			MessageBox.Show(ProgressBarStorage.progress.Sum().ToString());
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
			SolidColorBrush red = new SolidColorBrush(Colors.Red);
			SolidColorBrush green = new SolidColorBrush(Colors.Green);
			int index = ProgressBar.Children.IndexOf(vb);
			if (ProgressBarStorage.progress[index] !=2)
			{
				Switcher.Switch(new Start(index));
			}
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
