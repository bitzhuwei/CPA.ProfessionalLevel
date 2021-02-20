using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LinesNto1 {
	public partial class Form1 : Form {
		public Form1() {
			InitializeComponent();
			this.txtSource.GotFocus += TxtSource_GotFocus;
			this.txtSource.LostFocus += TxtSource_LostFocus;
			this.txtDest.GotFocus += TxtDest_GotFocus;
			this.txtDest.LostFocus += TxtDest_LostFocus;

		}

		private void TxtDest_LostFocus(object sender, EventArgs e) {
			this.txtDest.BackColor = Color.White;
		}

		private void TxtDest_GotFocus(object sender, EventArgs e) {
			this.txtDest.BackColor = Color.Gold;
		}

		private void TxtSource_LostFocus(object sender, EventArgs e) {
			this.txtSource.BackColor = Color.White;
		}

		private void TxtSource_GotFocus(object sender, EventArgs e) {
			this.txtSource.BackColor = Color.Gold;
		}

		static readonly char[] lineSeparator = new char[] { '\r', '\n' };

		private void txtSource_TextChanged(object sender, EventArgs e) {
			string newContent = this.txtSource.Text;
			string[] lines = newContent.Split(lineSeparator, StringSplitOptions.RemoveEmptyEntries);
			var builder = new StringBuilder();
			foreach (var item in lines) { builder.Append(item); }
			string forcedOne = builder.ToString();
			MatchCollection collection = Regex.Matches(forcedOne, @"\(.*?）");
			if (collection.Count > 0) { // ① ② ③ 。。 按行排列好。
				int index = 0;
				builder.Clear();
				for (int i = 0; i < collection.Count; i++) {
					Match item = collection[i];
					string node = forcedOne.Substring(index, item.Index - index);
					string[] nodeParts = node.Split(lineSeparator, StringSplitOptions.RemoveEmptyEntries);
					foreach (var part in nodeParts) { builder.Append(part); }
					if (item.Index > 0) { builder.Append(Environment.NewLine); }
					builder.Append($"{(char)('①' + i)}");
					index = item.Index + item.Value.Length;
				}
				{
					string node = forcedOne.Substring(index);
					string[] nodeParts = node.Split(lineSeparator, StringSplitOptions.RemoveEmptyEntries);
					foreach (var part in nodeParts) { builder.Append(part); }
				}
				this.txtDest.AppendText(builder.ToString());
			}
			else { // 改为单行。
				this.txtDest.AppendText(forcedOne);
			}

			this.txtDest.Focus();
			this.txtDest.SelectAll();
		}

		private void Form1_Activated(object sender, EventArgs e) {
			this.txtSource.Focus();
			this.txtSource.SelectAll();
		}
	}
}
