using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace md.PostProcess {
	public partial class Form1 {

		enum e查找公式 {
			Nothing,
			/// <summary>
			/// 【公式】xxx
			/// ＝xxx 
			/// </summary>
			公式,
		}
		private StringBuilder Convert公式行(string fullname) {
			var builder = new StringBuilder();
			using (var sr = new StreamReader(fullname)) {
				var state = e查找公式.Nothing;
				var queue = new Queue<string>();
				while (!sr.EndOfStream) {
					string line = sr.ReadLine();
					switch (state) {
						case e查找公式.Nothing:
							if (line.StartsWith("【公式】")) {
								queue.Enqueue(line.Substring("【公式】".Length).Replace(@"\|", "|"));
								state = e查找公式.公式;
							}
							else {
								while (queue.Count > 0) { builder.AppendLine(queue.Dequeue()); }
								builder.AppendLine(line);
							}
							break;
						case e查找公式.公式:
							if (line.StartsWith("【公式】")) {
								queue.Enqueue(line.Substring("【公式】".Length).Replace(@"\|", "|"));
								state = e查找公式.公式;
							}
							else if (line.StartsWith("＝")
								|| line.StartsWith("＋")
								|| line.StartsWith("－")
								|| line.StartsWith("×")
								|| line.StartsWith("÷")
								|| line.StartsWith("／")
								) {
								queue.Enqueue(line.Replace(@"\|", "|"));
								state = e查找公式.公式;
							}
							else if (line == string.Empty) {
								// nothing to do.
							}
							else {
								builder.AppendLine("```"); // 多行代码段.开始
								while (queue.Count > 0) { builder.AppendLine(queue.Dequeue()); }
								builder.AppendLine("```"); // 多行代码段.结束
								builder.AppendLine(line);
								state = e查找公式.Nothing;
							}
							break;
						default:
							MessageBox.Show($"未处理的枚举类型[{state}]", "哎呀", MessageBoxButtons.OK, MessageBoxIcon.Error);
							break;
					}
				}
				while (queue.Count > 0) { builder.AppendLine(queue.Dequeue()); }
			}

			return builder;
		}
	}
}
