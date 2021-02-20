using System;
using System.Text;
using System.Linq;

namespace CPA.Rulers {
	public class FinancialStatementItemDisplayRule : FinancialStatementRule {
		private FinancialStatementItem item;
		private DateTime lastDate;

		public FinancialStatementItemDisplay display;

		public FinancialStatementItemDisplayRule(FinancialStatementItem item, DateTime lastDate) {
			this.item = item;
			this.lastDate = lastDate;
		}

		public override void Apply() {
			var display = new FinancialStatementItemDisplay();
			double sum = 0;
			var info = JournalAccount.GetAccountInfo(item.Name);
			ESide standardSide = info != null ? info.side : ESide.Dr; // TODO:默认Dr，合适吗？
			var result = from entry in item.EntryList
						 from account in entry.AccountList
						 where (account.Name == item.Name
							 && entry.PrepareTime <= lastDate)
						 select (account.Side == standardSide ? account.Value : 0 - account.Value);
			var builder = new StringBuilder();
			foreach (var value in result) {
				sum += value;
				//builder.Append($" {value:+0,00.00;-0,00.00;}");
				builder.Append($" {(value >= 0 ? "+" : "")}{value:F2}");
			}
			display.empty = item.SumSide == ESide.Cr ? "  " : "";
			display.side = item.SumSide;
			display.name = item.Name;
			//display.value = $"{sum:+0,00.00;-0,00.00;} ={builder.ToString()};";
			display.value = $"{sum.Shake():F2} ={builder.ToString()};";

			this.display = display;
		}
	}
}

