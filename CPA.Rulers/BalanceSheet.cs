using System;
using System.Collections.Generic;

namespace CPA.Rulers {
	/// <summary>
	/// 资产负债表
	/// </summary>
	public class BalanceSheet {

		/// <summary>
		/// 编制时间
		/// </summary>
		public DateTime PrepareTime { get; set; }

		/// <summary>
		/// 各个报表项目。
		/// </summary>
		public List<FinancialStatementItem> ItemList { get; private set; }

		public BalanceSheet() {
			this.ItemList = new List<FinancialStatementItem>();
		}
		///// <summary>
		///// 流动资产
		///// </summary>
		//public List<FinancialStatementItem> CurrentAssets { get; private set; }

		///// <summary>
		///// 非流动资产
		///// </summary>
		//public List<FinancialStatementItem> NonCurrentAssets { get; private set; }

		///// <summary>
		///// 流动负债
		///// </summary>
		//public List<FinancialStatementItem> CurrentLiability { get; private set; }

		///// <summary>
		///// 非流动负债
		///// </summary>
		//public List<FinancialStatementItem> NonCurrentLiability { get; private set; }

		///// <summary>
		///// 所有者权益
		///// </summary>
		//public List<FinancialStatementItem> OwnersEquity { get; private set; }

	}
}

