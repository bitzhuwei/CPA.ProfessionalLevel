using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace md.PostProcess {
	public partial class Form1 {

		private StringBuilder Convertmd公式(string fullname) {
			var builder = new StringBuilder();
			using (var sr = new StreamReader(fullname)) {
				while (!sr.EndOfStream) {
					string line = sr.ReadLine();
					if (line.StartsWith("【md公式】")) {
						//// markdown插件存在讨厌的bug，我不得不在此补救。
						//if (line.EndsWith(@"\\times")) {
						//	string line2 = TryGetNextLine(sr);
						//	if (line2 != string.Empty) {
						//		line = $"{line} {line2}";
						//	}
						//}
						// 独立行的公式
						builder.Append("$$");
						TrimSlash(line, builder, "【md公式】".Length);
						builder.AppendLine("$$");
					}
					else {
						// 行内的公式
						string pattern = @"\\\$.+\\\$";
						MatchCollection collection = Regex.Matches(line, pattern);
						int index = 0;
						foreach (Match item in collection) {
							builder.Append(line.Substring(index, item.Index - index));
							TrimSlash(item.Value, builder);
							index = item.Index + item.Value.Length;
						}
						builder.AppendLine(line.Substring(index));
					}
				}

				return builder;
			}
		}

		private string TryGetNextLine(StreamReader sr) {
			if (sr.EndOfStream) { return string.Empty; }
			else { return sr.ReadLine(); }
		}

		private void TrimSlash(string line, StringBuilder builder, int firstIndex = 0) {
			var state = true;
			for (int i = firstIndex; i < line.Length; i++) {
				if (line[i] == '\\') {
					if (state) {
						state = false;
					}
					else {
						builder.Append(line[i]);
						state = true;
					}
				}
				else {
					builder.Append(line[i]);
					state = true;
				}
			}

		}
	}
}
