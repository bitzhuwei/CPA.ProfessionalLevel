using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace CPA.Rulers {
	/// <summary>
	/// 一个T字账
	/// </summary>
	public class TBook {

		/// <summary>
		/// 账户名称
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		/// 所有与之有关的会计分录。
		/// </summary>
		public List<JournalEntry> Entry { get; private set; }

		/// <summary>
		/// 创建一个T字账
		/// </summary>
		/// <param name="name"></param>
		public TBook(string name) {
			Debug.Assert(!string.IsNullOrWhiteSpace(name));

			this.Name = name;
			this.Entry = new List<JournalEntry>();
		}

		public override string ToString() {
			return $"T字账[{Name}]({Entry.Count})";
		}
	}
}

