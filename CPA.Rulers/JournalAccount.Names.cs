using System;
using System.Collections.Generic;
using System.IO;

namespace CPA.Rulers {
	public partial class JournalAccount {
		private static readonly Dictionary<string, JournalAccountInfo> accountInfoDict = new Dictionary<string, JournalAccountInfo>();

		/// <summary>
		/// 获取指定<paramref name="name"/>的会计账户\报表项目等的自身相关信息。
		/// 如果没有，则返回null。
		/// </summary>
		/// <param name="name"></param>
		/// <returns></returns>
		public static JournalAccountInfo GetAccountInfo(string name) {
			JournalAccountInfo info = null;
			if (!accountInfoDict.TryGetValue(name, out info)) {
				info = null;
			}

			return info;
		}

		//下面是对accountInfoDict的初始化。
		enum eState {
			/// <summary>
			/// 在```和```之外（不是要解析的内容）
			/// </summary>
			outSide,
			/// <summary>
			/// 在```和```之间（是要解析的内容）
			/// </summary>
			Inside,
		}
		static JournalAccount() {
			LoadDefaultJournalAccountInfo();
			try {
				string filename = "JournalAccount.Names.md";
				using (var sr = new StreamReader(filename)) {
					var state = eState.outSide;
					while (!sr.EndOfStream) {
						string line = sr.ReadLine().Trim();
						switch (state) {
							case eState.outSide:
								if (line == "```") {
									state = eState.Inside;
								}
								else {
									// nothing to do.
								}
								break;
							case eState.Inside:
								if (line == "```") {
									state = eState.outSide;
								}
								else { // parse items.
									JournalAccountInfo info = ParseInfo(line);
									if (accountInfoDict.ContainsKey(info.name)) {
										accountInfoDict[info.name] = info;
									}
									else {
										accountInfoDict.Add(info.name, info);
									}
								}
								break;
							default:
								break;
						}
					}
				}
			} catch (Exception ex) {
				Log.DumpLog(ex.ToString());
			}
		}

		static char[] separators = new char[] { '，', ',', ' ', '\t', };
		private static JournalAccountInfo ParseInfo(string line) {
			string[] parts = line.Split(separators);
			string name; ESide side; string classification;
			{
				name = parts[0];
			}
			{
				if (parts[1] == "借" || parts[1] == "Dr") {
					side = ESide.Dr;
				}
				else if (parts[1] == "贷" || parts[1] == "Cr") {
					side = ESide.Cr;
				}
				else {
					side = ESide.Unknown;
				}
			}
			{
				classification = parts[2];
			}

			return new JournalAccountInfo(name, side, classification);
		}

		private static void LoadDefaultJournalAccountInfo() {
			AddInfo(new JournalAccountInfo("银行存款", ESide.Dr, "资产类"));
			AddInfo(new JournalAccountInfo("库存商品", ESide.Dr, "资产类"));
			AddInfo(new JournalAccountInfo("存货跌价准备", ESide.Cr, "资产类"));
			AddInfo(new JournalAccountInfo("固定资产", ESide.Dr, "资产类"));
			AddInfo(new JournalAccountInfo("未确认融资费用", ESide.Dr, "负债类"));
			AddInfo(new JournalAccountInfo("财务费用", ESide.Dr, "损益类"));
			AddInfo(new JournalAccountInfo("长期应付款", ESide.Cr, "负债类"));
			// TODO:添加更多会计账户/报表项目等的相关信息...

		}

		private static void AddInfo(JournalAccountInfo journalAccountInfo) {
			accountInfoDict.Add(journalAccountInfo.name, journalAccountInfo);
		}
	}

	/// <summary>
	/// 会计账户\报表项目等的自身相关信息。
	/// </summary>
	public class JournalAccountInfo {
		/// <summary>
		/// 会计账户名称。
		/// </summary>
		public string name;
		/// <summary>
		/// 默认在借方or贷方？
		/// </summary>
		public ESide side;
		/// <summary>
		/// 资产类？负债类？
		/// </summary>
		public string classification;

		/// <summary>
		/// 
		/// </summary>
		/// <param name="name"></param>
		/// <param name="side"></param>
		/// <param name="classification"></param>
		public JournalAccountInfo(string name, ESide side, string classification) {
			this.name = name;
			this.side = side;
			this.classification = classification;
		}

		public override string ToString() {
			return $"[{name}] [{side}] [{classification}]";
		}
	}
}
