using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace md.PostProcess {
	public partial class Form1 {

		static string[] oldChars = null;
		static string[] newChars = null;

		// 将表格中的“； ”转换为换行符“<br/>”
		private StringBuilder Convert表格内换行(string fullname) {
			const int checkLength = 50;
			if (oldChars == null) {
				oldChars = new string[checkLength];
				newChars = new string[checkLength];
				var spaceBuilder = new StringBuilder();
				for (int i = 0; i < checkLength; i++) {
					oldChars[i] = $"<br/>{spaceBuilder.ToString()}|";
					newChars[i] = $"{spaceBuilder.ToString()}|";
					spaceBuilder.Append(" ");
				}
			}
			var builder = new StringBuilder();
			using (var sr = new StreamReader(fullname)) {
				while (!sr.EndOfStream) {
					string line = sr.ReadLine();
					if (line.StartsWith("| ") && line.EndsWith(" |")) {// this is a table(maybe)
						if (line.Contains(@"； ")) {
							line = line.Replace(@"； ", @"<br/>"); // 实现换行
							for (int i = 0; i < checkLength; i++) { // 避免多出个最后的空行
								line = line.Replace(oldChars[i], newChars[i]);
							}
						}
					}
					builder.AppendLine(line);
				}
			}

			return builder;
		}

	}
}
