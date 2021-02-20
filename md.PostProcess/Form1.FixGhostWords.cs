using System.IO;
using System.Text;

namespace md.PostProcess {
	public partial class Form1 {

		private static readonly string[] ghostWords = new string[] { "井", "．", "（★）", "（★★）", "（★★★）" };
		private static readonly string[] rightWords = new string[] { "并", "，", ":star: ", ":star: :star: ", ":star: :star: :star: " };
		private StringBuilder FixGhostWords(string fullname) {
			var builder = new StringBuilder();
			using (var sr = new StreamReader(fullname)) {
				while (!sr.EndOfStream) {
					string line = sr.ReadLine();
					for (int i = 0; i < ghostWords.Length; i++) {
						line = line.Replace(ghostWords[i], rightWords[i]);
					}
					builder.AppendLine(line);
				}
			}

			return builder;
		}

	}
}
