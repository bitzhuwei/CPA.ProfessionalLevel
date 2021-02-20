using CPA.Rulers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Linq;
using System.Windows.Forms;

namespace md.PostProcess {
	public partial class Form1 {

		enum e会计题目 {
			Nothing,
			会计题目开始,
			提问, 回答,
			会计题目结束,
		}
		
		private StringBuilder Convert会计题目(string fullname) {
			var builder = new StringBuilder();// 更新后的内容。

			using (var sr = new StreamReader(fullname)) {
				var state = e会计题目.Nothing; // 目前的状态。
				var queue = new Queue<string>(); // buffer.【回答】...【结束s】

				while (!sr.EndOfStream) {
					string line = sr.ReadLine();
					switch (state) {
						case e会计题目.Nothing:
							if (line.StartsWith("【会计题目开始】")) {
								state = e会计题目.会计题目开始;
							}
							builder.AppendLine(line);
							break;
						case e会计题目.会计题目开始:
							if (line.StartsWith("【提问】")) {
								state = e会计题目.提问;
							}
							else if (line.StartsWith("【回答】")) {
								state = e会计题目.回答;
							}
							else if (line.StartsWith("【会计题目结束】")) {
								state = e会计题目.会计题目结束;
							}
							builder.AppendLine(line);
							break;
						case e会计题目.提问:
							if (line.StartsWith("【回答】")) {
								state = e会计题目.回答;
							}
							else if (line.StartsWith("【会计题目结束】")) {
								state = e会计题目.会计题目结束;
							}
							builder.AppendLine(line);
							break;
						case e会计题目.回答:
							if (line.StartsWith("【提问】")) {
								EndQuestion(builder, queue);
								state = e会计题目.提问;
							}
							else if (line.StartsWith("【会计题目结束】")) {
								EndQuestion(builder, queue);
								state = e会计题目.会计题目结束;
							}
							else {
								queue.Enqueue(line);
							}
							break;
						case e会计题目.会计题目结束:
							state = e会计题目.Nothing;
							builder.AppendLine(line);
							break;
						default:
							break;
					}
				}
				while (queue.Count > 0) { builder.AppendLine(queue.Dequeue()); }
			}

			return builder;
		}

		/// <summary>
		/// 分析queue缓存，改写【分录】【时间】
		/// </summary>
		/// <param name="queue"></param>
		/// <param name="builder"></param>
		private void EndQuestion(StringBuilder builder, Queue<string> queue) {
			var itemDict = new Dictionary<string, FinancialStatementItem>();
			while (queue.Count > 0) {
				string line = queue.Dequeue();
				if (line.StartsWith("【分录】【")) {
					DateTime date = ParseDateTime(line);
					builder.Append(":moneybag:");
					builder.AppendLine(line); // 【分录】【2017年12月31日】摊销的利息进财务费用
					line = queue.Dequeue(); // empty
					line = queue.Dequeue(); // content
					JournalEntry entry = ParseEntry(line);
					entry.PrepareTime = date;
					for (int i = 0; i < entry.AccountList.Count; i++) {
						var account = entry.AccountList[i];
						FinancialStatementItem item = itemDict.Provide(account.Name);
						item.Name = account.Name;
						item.EntryList.Add(entry);
					}
					DoConvert会计分录(builder, line); // 用代码框重写会计分录
					DumpFinancialStatements(builder, date, itemDict); // 给出当前对各个项目的影响
					line = queue.Dequeue(); // |--------------------------------------------|
				}
				else {
					builder.AppendLine(line);
				}
			}
		}

		private void DumpFinancialStatements(StringBuilder builder, DateTime lastDate,
			Dictionary<string, FinancialStatementItem> itemDict) {
            builder.AppendLine(":checkered_flag:到这一步的汇总情况：");
			builder.AppendLine("```");
			foreach (var kvPair in itemDict) {
				FinancialStatementItem item = kvPair.Value;
				var rule = new FinancialStatementItemDisplayRule(item, lastDate);
				rule.Apply();
				FinancialStatementItemDisplay display = rule.display;
				builder.AppendLine(display.ToString());
			}
			builder.AppendLine("```");
		}

		private JournalEntry ParseEntry(string content) {
			string[] parts = content.Split(会计分录chartSeparator, StringSplitOptions.RemoveEmptyEntries);
			// 清除各种注释
			for (int i = 0; i < parts.Length; i++) {
				string lexi = parts[i].Trim();
				if (lexi.StartsWith("///")) { // 不调整位置的注释
					parts[i] = string.Empty;
				}
				else if (lexi.StartsWith("//")) { // 要调整位置的注释
					if (lexi == "//") { // x注释
						parts[i] = string.Empty;
						if (i + 1 < parts.Length) {
							parts[i + 1] = string.Empty;
						}
					}
					else { //x注释
						parts[i] = string.Empty;
					}
				}
			}

			var entry = new JournalEntry();
			bool is借方 = true;
			for (int i = 0; i < parts.Length; i++) {
				string part = parts[i].Trim();
				if (part == string.Empty) { continue; }

				if (part.StartsWith("借")) { is借方 = true; }
				else if (part.StartsWith("贷")) { is借方 = false; }

				string pureAccountN = CleanUpAccountN(part);
				string accountName; double value;
				SplitAccountN(pureAccountN, out accountName, out value);
				if (is借方) {
					var account = new JournalAccount(ESide.Dr, accountName, value);
					entry.AccountList.Add(account);
				}
				else {
					var account = new JournalAccount(ESide.Cr, accountName, value);
					entry.AccountList.Add(account);
				}
			}
			return entry;
		}

		private void SplitAccountN(string line, out string account, out double value) {
			int accountLength = 0;
			for (int i = 0; i < line.Length; i++) {
				char c = line[i];
				if ('0' <= c && c <= '9') { break; }
				if (c == '.' || c == '-' || c == '+') { break; }

				accountLength++;
			}

			account = line.Substring(0, accountLength);
			string strValue = line.Substring(accountLength);
			value = double.Parse(strValue);
		}

		private static char[] DrCrSeparator = new char[] { '借', '贷', '：', ':' };

		private string CleanUpAccountN(string line) {
			string[] parts = line.Split(DrCrSeparator, StringSplitOptions.RemoveEmptyEntries);
			return parts[parts.Length - 1];
		}

		private static char[] dateTimeSeparator = new char[] { '【', '】' };
		/// <summary>
		/// 【分录】【datetime】
		/// </summary>
		/// <param name="line"></param>
		/// <returns></returns>
		private DateTime ParseDateTime(string line) {
			string[] parts = line.Split(dateTimeSeparator, StringSplitOptions.RemoveEmptyEntries);
			DateTime dateTime = DateTime.Parse(parts[1]);

			return dateTime;
		}


	}
}
