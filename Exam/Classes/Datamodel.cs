using System;
using System.Collections.Generic;
using System.Text;

namespace Exam.Classes
{
	public class ExerciseDM
	{
		public string question;
		public List<string> options;
		public int correctIndex;
		public int currentIndex;
		public bool done;

		public ExerciseDM()
		{

		}

	}

	public static class ProgressBarStorage
	{
		public static int[] progress = new int[10] {2,2,2,2,2,2,2,2,2,2};
		public static void ProgressReset()
		{
			progress = new int[10] { 2, 2, 2, 2, 2, 2, 2, 2, 2, 2 };
		}
	}

	public class TopicDM
	{
		public string title;
		public bool isActive;
		public string content;
		public List<ExerciseDM> exercises;

		public TopicDM()
		{

		}

	}

	public class UserDM
	{
		public string Name { get; set; }
		public bool IsInterupted { get; set; }
	}
}
