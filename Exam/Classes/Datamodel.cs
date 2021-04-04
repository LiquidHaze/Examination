using System;
using System.Collections.Generic;
using System.Text;

namespace Exam.Classes
{
	class ExerciseDM
	{
		public string question;
		public List<string> options;
		private int correctIndex;
		public int currentIndex;
		public bool done;

		public ExerciseDM()
		{

		}

	}

	class TopicDM
	{
		public string title;
		public string content;
		public List<ExerciseDM> exercises;

		public TopicDM()
		{

		}

	}

	class temp1
	{
		public string Name { get; set; }
		public int Age { get; set; }
	}
}
