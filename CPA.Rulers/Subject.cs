using System;
using System.Collections.Generic;

namespace CPA.Rulers {
	/// <summary>
	/// 一道题目。
	/// </summary>
	public class Subject {
		/// <summary>
		/// 题干。
		/// </summary>
		public string Content { get; set; }

		/// <summary>
		/// 所有的提问。
		/// </summary>
		public List<Question> QuestionList { get; private set; }

		/// <summary>
		/// 一道题目。
		/// </summary>
		public Subject() {
			this.QuestionList = new List<Question>();
		}
	}
}
