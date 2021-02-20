using System;
using System.Collections.Generic;

namespace CPA.Rulers {
	/// <summary>
	/// 一问。
	/// </summary>
	public class Question {
		/// <summary>
		/// 提问的内容。
		/// </summary>
		public string Content { get; set; }

		/// <summary>
		/// 由会计分录组成的答案。
		/// </summary>
		public List<JournalEntry> EntryList { get; private set; }

		/// <summary>
		/// 一问。
		/// </summary>
		public Question() {
			this.EntryList = new List<JournalEntry>();
		}
	}
}
