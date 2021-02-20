using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace md.PostProcess {
	public partial class Form1 {

		private StringBuilder Convert本章真题(string fullname) {
			var builder = new StringBuilder();
			using (var sr = new StreamReader(fullname)) {
				bool answering = false;
				var title = string.Empty;
				var queue = new Queue<MatchCollection>();
				while (!sr.EndOfStream) {
					string line = sr.ReadLine();
					if (answering) {
						MatchCollection collection = Regex.Matches(line, @"\!\[\]\(.+\)");
						queue.Enqueue(collection);

						if (line.StartsWith("#")) {
							answering = false;
							BuildAnswerPage(builder, title, queue, fullname);
							queue.Clear();

							builder.AppendLine(line);

							title = line;
						}
					}
					else {
						builder.AppendLine(line);

						if (line.StartsWith("#")) {
							title = line;
						}

						if (line.StartsWith("【答案】")) {
							answering = true;
						}
					}
				}

				if(answering) // 最后一个题目的答案
				{
					answering = false;
					BuildAnswerPage(builder, title, queue, fullname);
					queue.Clear();

					builder.AppendLine();
				}

				return builder;
			}
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="builder"></param>
		/// <param name="title">4.15.2 连续编制合并财务报表</param>
		/// <param name="queue"></param>
		/// <param name="fullname"></param>
		private void BuildAnswerPage(StringBuilder builder, string title,
			Queue<MatchCollection> queue, string fullname) {
			var picAnswers = new List<string>();
			while (queue.Count > 0) {
				var collection = queue.Dequeue();
				foreach (Match item in collection) {
					picAnswers.Add(item.Value);
				}
			}

			if (picAnswers.Count > 0) {
				string strAnswerPage = picAnswers[0];
				int leftBrace = strAnswerPage.IndexOf("(");
				int rightBrace = strAnswerPage.IndexOf(")");
				string name = strAnswerPage.Substring(leftBrace + 1, rightBrace - leftBrace - 1);
				string newFilename = Path.Combine(Path.GetDirectoryName(fullname), name + ".md");
				//[查看题目](1会计/会计总论.本章真题.md#41512-连续编制合并财务报表)
				// 4.15.2 连续编制合并财务报表 -> #41512-连续编制合并财务报表
				//string mainMD = GetMainMD(fullname);
				string sequence = GetSequence(title);
				using (var sw = new StreamWriter(newFilename, false)) {
					foreach (var item in picAnswers) {
						sw.WriteLine(item.Replace("![](media/", "![]("));
						sw.WriteLine();
					}

				sw.WriteLine($"[查看题目](../{Path.GetFileName(fullname)}#{sequence})");
					sw.WriteLine();
				}

				builder.AppendLine($"[查看解析和答案]({name}.md)");
			}
			else {
				builder.AppendLine("没有找到【答案】附属的图片！");
			}
		}

		private string GetSequence(string title) {
			string[] parts = title.Split(' ');
			return $"{parts[1].Replace(".", "")}-{parts[2]}";
		}

		private string GetMainMD(string fullname) {
			string[] parts = fullname.Split('\\', '/');
			int count = parts.Length;
			return $"{parts[count - 2]}/{parts[count - 1]}";
		}
	}
}
