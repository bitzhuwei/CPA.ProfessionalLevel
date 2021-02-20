using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace md.PostProcess {
	public partial class Form1 {

		private StringBuilder Link2md(string fullname) {
			var builder = new StringBuilder();
			using (var sr = new StreamReader(fullname)) {
				while (!sr.EndOfStream) {
					string line = sr.ReadLine();
					MatchCollection collection = Regex.Matches(line, @"\[.+\]\(.+\.docx\)");
					int index = 0;
					foreach (Match item in collection) {
						builder.Append(line.Substring(index, item.Index - index));
						builder.Append(item.Value.Substring(0, item.Value.Length - ".docx)".Length));
						builder.Append(".md)");
						index = item.Index + item.Value.Length;
					}
					builder.AppendLine(line.Substring(index));
				}
			}

			return builder;
		}


	}
}
