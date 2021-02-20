using System.IO;
using System.Text;

namespace md.PostProcess {
	public partial class Form1 {

		private StringBuilder ConvertCode(string fullname) {
			var builder = new StringBuilder();
			bool odd = false; // 找到了奇数次的Bold符号
			using (var sr = new StreamReader(fullname)) {
				while (!sr.EndOfStream) {
					string line = sr.ReadLine();

					for (int i = 0; i < line.Length - 1; i++) {
						if (line[i] == '*' && line[i + 1] == '*') { odd = !odd; i++; }
					}

					string newLine = line.Replace("**", "`");
					builder.AppendLine(newLine);
				}
			}

			if (odd) {
				File.AppendAllText(logFilename, $"error-CovertCode: odd Bold in file [{fullname}]");
			}

			return builder;
		}

	}
}
