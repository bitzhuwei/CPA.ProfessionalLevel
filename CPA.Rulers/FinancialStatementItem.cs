using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace CPA.Rulers {
	/// <summary>
	/// 财务报表项目(or自定义项目)
	/// </summary>
	public class FinancialStatementItem {
		/// <summary>
		/// 项目名称
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		/// 对本项目有影响的
		/// </summary>
		public List<JournalEntry> EntryList { get; set; }

		/// <summary>
		/// 默认在借方or贷方汇总？
		/// </summary>
		public ESide SumSide {
			get {
				var info = JournalAccount.GetAccountInfo(this.Name);
				if (info == null) { return ESide.Unknown; }

				return info.side;
			}
		}

		/// <summary>
		/// 所有相关项目的汇总。
		/// </summary>
		public double Sum {
			get {
				var info = JournalAccount.GetAccountInfo(this.Name);
				ESide standardSide = info != null ? info.side : ESide.Dr; // TODO:默认Dr，合适吗？
				double value = (from entry in this.EntryList
								from account in entry.AccountList
								where account.Name == this.Name
								select (account.Side == standardSide ?
								account.Value : 0 - account.Value)).Sum();
				return value;
			}
		}

		/// <summary>
		/// 财务报表项目(or自定义项目)
		/// </summary>
		public FinancialStatementItem() {
			this.EntryList = new List<JournalEntry>();
		}

		/// <summary>
		/// 在指定的<paramref name="dateTime"/>之前发生的相关项目的总和。
		/// </summary>
		/// <param name="dateTime"></param>
		/// <returns></returns>
		public double SumBefore(DateTime dateTime) {
			var info = JournalAccount.GetAccountInfo(this.Name);
			ESide standardSide = info != null ? info.side : ESide.Dr; // TODO:默认Dr，合适吗？
			double value = (from entry in this.EntryList
							from account in entry.AccountList
							where (account.Name == this.Name
							    && entry.PrepareTime <= dateTime)
							select (account.Side == standardSide ? account.Value : 0 - account.Value)).Sum();
			return value;
		}

		public override string ToString() {
			return $"{Name}: ({Sum})";
		}
	}
}

