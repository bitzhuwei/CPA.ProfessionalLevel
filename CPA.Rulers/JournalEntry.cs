using System;
using System.Collections.Generic;

namespace CPA.Rulers {
	/// <summary>
	/// 一个会计分录
	/// </summary>
	public class JournalEntry : IComparable<JournalEntry> {

		/// <summary>
		/// 这个会计分录是做什么用的？
		/// </summary>
		public string Desc { get; set; }

		/// <summary>
		/// 何时编制此分录？
		/// </summary>
		public DateTime PrepareTime { get; set; }

		/// <summary>
		/// 涉及到的各个会计账户
		/// </summary>
		public List<JournalAccount> AccountList { get; private set; }

		/// <summary>
		/// 创建一个会计分录
		/// </summary>
		public JournalEntry() {
			this.AccountList = new List<JournalAccount>();
		}

		/// <summary>
		/// 创建一个会计分录
		/// </summary>
		/// <param name="name">这个会计分录是做什么用的？</param>
		public JournalEntry(string name) {
			this.Desc = name;
			this.AccountList = new List<JournalAccount>();
		}

		public override string ToString() {
			return $"{(string.IsNullOrEmpty(Desc) ? "会计分录" : Desc)}({this.AccountList.Count})";
		}

		public int CompareTo(JournalEntry other) {
			if (other == null) { return -1; }

			return this.PrepareTime.CompareTo(other.PrepareTime);
		}
	}
}

