using System.IO;
using System.Text;

namespace md.PostProcess {
	public partial class Form1 {

		private StringBuilder ConvertList(string fullname) {
			var builder = new StringBuilder();
			int id1 = 1; int id2 = 1; int id3 = 1; int id4 = 1; int id5 = 1; int id6 = 1;
			using (var sr = new StreamReader(fullname)) {
				while (!sr.EndOfStream) {
					string line = sr.ReadLine();
					if (line.StartsWith("# ")) {
						builder.AppendLine($"# {id1++}. {line.Substring(2)}");
					}
					else if (line.StartsWith("## ")) {
						builder.AppendLine($"## {id1 - 1}.{id2++}. {line.Substring(3)}");
					}
					else if (line.StartsWith("### ")) {
						builder.AppendLine($"### {id1 - 1}.{id2 - 1}.{id3++}. {line.Substring(4)}");

					}
					else if (line.StartsWith("#### ")) {
						builder.AppendLine($"#### {id1 - 1}.{id2 - 1}.{id3 - 1}.{id4++}. {line.Substring(5)}");

					}
					else if (line.StartsWith("##### ")) {
						builder.AppendLine($"##### {id1 - 1}.{id2 - 1}.{id3 - 1}.{id4 - 1}.{id5++}. {line.Substring(6)}");

					}
					else if (line.StartsWith("###### ")) {
						builder.AppendLine($"###### {id1 - 1}.{id2 - 1}.{id3 - 1}.{id4 - 1}.{id5 - 1}.{id6++}. {line.Substring(7)}");

					}
					else {
						builder.AppendLine(line);
					}
				}
			}

			return builder;
		}

	}
}
