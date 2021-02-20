using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace md.PostProcess {
	public partial class Form1 {

		enum e查找会计分录 {
			Nothing,
			/// <summary>
			/// 【会计分录】XXX
			/// </summary>
			会计分录,
			/// <summary>
			/// |借：银行存款 贷：股本 资本公积——股本溢价|
			/// </summary>
			内容,
			/// <summary>
			/// |---------------|
			/// </summary>
			结尾行,
		}
		private StringBuilder Convert会计分录(string fullname) {
			var builder = new StringBuilder();
			using (var sr = new StreamReader(fullname)) {
				var state = e查找会计分录.Nothing;
				var queue = new Queue<string>();
				while (!sr.EndOfStream) {
					string line = sr.ReadLine();
					switch (state) {
						case e查找会计分录.Nothing:
							if (line.StartsWith("【会计分录】")) {
								queue.Enqueue(line);
								if (!sr.EndOfStream) { // 尝试吸收后面的空行。
									line = sr.ReadLine();
									queue.Enqueue(line);
								}
								state = e查找会计分录.会计分录;
							}
							else {
								while (queue.Count > 0) { builder.AppendLine(queue.Dequeue()); }
								builder.AppendLine(line);
							}
							break;
						case e查找会计分录.会计分录:
							if (line.StartsWith("|") && line.EndsWith("|")) {
								queue.Enqueue(line);
								state = e查找会计分录.内容;
							}
							else {
								while (queue.Count > 0) { builder.AppendLine(queue.Dequeue()); }
								builder.AppendLine(line);
								state = e查找会计分录.Nothing;
							}
							break;
						case e查找会计分录.内容:
							if (line.StartsWith("|") && line.EndsWith("|")) {
								queue.Enqueue(line);
								state = e查找会计分录.结尾行;
							}
							else {
								while (queue.Count > 0) { builder.AppendLine(queue.Dequeue()); }
								builder.AppendLine(line);
								state = e查找会计分录.Nothing;
							}
							break;
						case e查找会计分录.结尾行:
							builder.Append(":moneybag:");
							builder.AppendLine(queue.Dequeue()); // 【会计分录】
							builder.AppendLine(queue.Dequeue()); // 空行
							string content = queue.Dequeue(); // 内容所在行。
							queue.Clear(); // 结尾行。
							DoConvert会计分录(builder, content);
							state = e查找会计分录.Nothing;
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

		private void DoConvert会计分录(StringBuilder builder, string content) {
			string[] parts = content.Split(会计分录chartSeparator, StringSplitOptions.RemoveEmptyEntries);
			for (int i = 0; i < parts.Length; i++) {
				string lexi = parts[i].Trim();
				if (lexi.StartsWith("//") // 要调整位置的注释
					&& (!lexi.StartsWith("///"))) { // 不调整位置的注释
					if (lexi == "//") { // 注释
						var commentBuilder = new StringBuilder();
						if (i > 0) {
							commentBuilder.Append(parts[i - 1]);
							commentBuilder.Append(" ");
							commentBuilder.Append(parts[i]);
							parts[i] = string.Empty;
						}
						if (i + 1 < parts.Length) {
							commentBuilder.Append(" ");
							commentBuilder.Append(parts[i + 1]);
							parts[i + 1] = string.Empty;
						}

						if (i > 0) { parts[i - 1] = commentBuilder.ToString(); }
						else { parts[i] = commentBuilder.ToString(); }
					}
					else { //注释
						var commentBuilder = new StringBuilder();
						if (i > 0) {
							commentBuilder.Append(parts[i - 1]);
							commentBuilder.Append(" ");
							commentBuilder.Append(parts[i]);
							parts[i] = string.Empty;
						}

						if (i > 0) { parts[i - 1] = commentBuilder.ToString(); }
						else { parts[i] = commentBuilder.ToString(); }
					}
				}
			}

			builder.AppendLine("```"); // code start.
			bool is借方 = true;
			for (int i = 0; i < parts.Length; i++) {
				string part = parts[i].Trim();
				if (part == string.Empty) { continue; }

				if (part.StartsWith("///")) { builder.AppendLine(part); }
				else {
					if (part.StartsWith("借")) { is借方 = true; }
					else if (part.StartsWith("贷")) { is借方 = false; }

					if (is借方) {
						if (part.StartsWith("借")) { builder.AppendLine(part); }
						else { builder.AppendLine($"    {part}"); }
					}
					else {
						if (part.StartsWith("贷")) { builder.AppendLine($"  {part}"); }
						else { builder.AppendLine($"      {part}"); }
					}
				}
			}
			builder.AppendLine("```"); // code end.
		}

	}
}
