using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace Exam.Classes
{
	public static class Helper
	{
		public static void HideElement(UIElement obj)
		{
			obj.IsEnabled = false;
			obj.Visibility = Visibility.Hidden;
		}

		public static void UnHideElement(UIElement obj)
		{
			obj.IsEnabled = true;
			obj.Visibility = Visibility.Visible;
		}

		/// <summary>
		/// Создание <see cref="RadioButton"/> из <see cref="List{T}"/> для <see cref="StackPanel"/> - метод расширения
		/// </summary>
		/// <param name="sp">Родительский <see cref="StackPanel"/></param>
		/// <param name="topics"><see cref="List{T}"/> содержащий <see cref="TopicDM"/> для генерации <see cref="RadioButton"/></param>
		public static void AppendRadioButton(this StackPanel sp, List<TopicDM> topics)
		{
			foreach (TopicDM topic in topics)
			{
				RadioButton rb = new RadioButton()
				{
					Content = topic.content,
					Name = topic.title,
					Margin = new Thickness(5),
					GroupName = "Topics"
				};
				rb.Checked += (s, e) =>
				{
					ExaminationSwitcher.SelectedTopic = rb.Name;
				};
				sp.Children.Add(rb);
			}
		}

		/// <summary>
		/// Создание <see cref="RadioButton"/> из <see cref="List{T}"/> для <see cref="StackPanel"/> - метод расширения
		/// </summary>
		/// <param name="sp">Родительский <see cref="StackPanel"/></param>
		/// <param name="topics"><see cref="List{T}"/> содержащий <see cref="string"/> для генерации <see cref="RadioButton"/></param>
		public static void AppendRadioButton(this StackPanel sp, List<string> options)
		{
			foreach (string option in options)
			{
				RadioButton rb = new RadioButton()
				{
					Content = option,
					Margin = new Thickness(5),
					GroupName = "Options",
					FontSize = 20
				};
				rb.Checked += (s, e) =>
				{
					Exam.Pages.Start.selectedOption = rb.Content.ToString();
				};
				sp.Children.Add(rb);
			}
		}

		public static void WriteDoc()
		{
			string writePath = "testdata";
			string excrs = "";
			Random rnd = new Random();
			for (int i = 0; i < 30; i++)
			{
				string tmp = @"{""question"":""" + (i + 1).ToString() + @" - Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur.Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum."",""options"":[""1 Lorem ipsum dolor sit amet"",""2 Lorem ipsum dolor sit amet"",""3 Lorem ipsum dolor sit amet"",""4 Lorem ipsum dolor sit amet""],""correctIndex"":""" + rnd.Next(0, 3).ToString() + @"""},";
				excrs += tmp;
			}
			excrs = excrs.Remove(excrs.Length - 1);
			string data = @"[{""title"":""Tema1"",""content"":""1 - Lorem ipsum dolor sit amet"",""exercises"":[" + excrs + @"]},{""title"":""Tema2"",""content"":""2 - Lorem ipsum dolor sit amet"",""exercises"":[" + excrs + @"]}]";
			try
			{
				using (StreamWriter sw = new StreamWriter(writePath, false, System.Text.Encoding.Default))
				{
					sw.WriteLine(Convert.ToBase64String(Encoding.ASCII.GetBytes(data)));
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
	}
}
