using Microsoft.Office.Interop.Word;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design.Serialization;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace md.PostProcess {
	public partial class Form1 : Form {

		string logFilename;

		public Form1() {
			InitializeComponent();

			//this.txtPath.Text = ".";
			//this.chkCode.Checked = false;
			//this.chkList.Checked = false;
			//this.chk会计分录.Checked = false;
		}

		class PostProcessParams {
			public bool code;
			public bool list;
			public bool 会计分录;
			public bool 会计题目;
			public bool 表格内换行;
			public bool 公式行;
			public bool md公式;

		}
		private void btnGo_Click(object sender, EventArgs e) {
			string path = txtPath.Text;
			if (!Directory.Exists(path)) {
				MessageBox.Show("您输入的路径不存在！");
				return;
			}

			this.btnGo.Enabled = false;

			logFilename = string.Format("md.PostProcess.{0:yyyyMMdd-HHmmss}.log", DateTime.Now);
		
			var application = new Microsoft.Office.Interop.Word.ApplicationClass();
			int saveFormat = 0;
			bool foundMDConverter = false;
			foreach (FileConverter fileConverter in application.FileConverters) {
				if (fileConverter.ClassName == "Writage") {
					saveFormat = fileConverter.SaveFormat;
					foundMDConverter = true;
					break;
				}
			}

			object nothing = Missing.Value;
			int processedCount = 0;
			if (foundMDConverter) {
				var ppParams = new PostProcessParams {
					code = this.chkCode.Checked,
					list = this.chkList.Checked,
					会计分录 = this.chk会计分录.Checked,
					会计题目 = this.chk会计题目.Checked,
					表格内换行 = this.chk表格内换行.Checked,
					公式行 = this.chk公式行.Checked,
					md公式 = this.chkmd公式.Checked,
				};
				bool forceAll = this.chkForceAll.Checked;

				var documents = from docFullname in Directory.GetFiles(path, "*.docx", SearchOption.AllDirectories)
								where (!Path.GetFileName(docFullname).StartsWith("~$"))
								select Path.GetFullPath(docFullname);
				foreach (var docFullname in documents) {
					var directory = Path.GetDirectoryName(docFullname);
					var fileName = Path.GetFileNameWithoutExtension(docFullname);
					string mdFullname = Path.Combine(directory, fileName + ".md");
					bool process = forceAll || DocUpdated(docFullname, mdFullname);
					if (process) {
						var doc = application.Documents.Add(docFullname, nothing, nothing);
						doc.SaveAs2(mdFullname, saveFormat, nothing, nothing, nothing, nothing, nothing, nothing,
							nothing, nothing, nothing, nothing/*Encoding.UTF8.CodePage*/, nothing, nothing, nothing);
						doc.Close();
						PostProcess(mdFullname, ppParams);
						processedCount++;
					}
				}
			}

			application.Quit(nothing, nothing, nothing);

			if (File.Exists((logFilename))) { Process.Start("notepad", logFilename); }

			MessageBox.Show($"后处理完成！处理了[{processedCount}]个文件！", $"[{processedCount}]");

			this.btnGo.Enabled = true;
		}
		
		private bool DocUpdated(string docFullname, string mdFullname) {
			bool updated = false;

			var mdInfo = new FileInfo(mdFullname);
			if (!mdInfo.Exists) {
				updated = true;
			}
			else {
				var docInfo = new FileInfo(docFullname);
				if (mdInfo.LastWriteTime < docInfo.LastWriteTime) {
					updated = true;
				}
			}

			return updated;
		}

		private void PostProcess(string fullname, PostProcessParams ppParams) {
			if (ppParams.code) { // 将Bold改为用方框括起来
				StringBuilder builder = ConvertCode(fullname);
				File.WriteAllText(fullname, builder.ToString());
			}
			if (ppParams.list) { // 给各标题加上序号
				StringBuilder builder = ConvertList(fullname);
				File.WriteAllText(fullname, builder.ToString());
			}
			if (ppParams.会计分录) { // 将表格形式转换为代码框形式
				StringBuilder builder = Convert会计分录(fullname);
				File.WriteAllText(fullname, builder.ToString());
			}
			if (ppParams.会计题目) { // 将表格形式转换为代码框形式
				StringBuilder builder = Convert会计题目(fullname);
				File.WriteAllText(fullname, builder.ToString());
			}
			if (ppParams.表格内换行) { // 将表格中的“； ”转换为换行符“<br/>”
				StringBuilder builder = Convert表格内换行(fullname);
				File.WriteAllText(fullname, builder.ToString());
			}
			if (ppParams.公式行) {
				StringBuilder builder = Convert公式行(fullname);
				File.WriteAllText(fullname, builder.ToString());
			}
			if (ppParams.md公式) {
				StringBuilder builder = Convertmd公式(fullname);
				File.WriteAllText(fullname, builder.ToString());
			}
			{ // 将对.docx的链接改为对.md的链接
				StringBuilder builder = Link2md(fullname);
				File.WriteAllText(fullname, builder.ToString());
			}
			{ // 修改错别字
				StringBuilder builder = FixGhostWords(fullname);
				File.WriteAllText(fullname, builder.ToString());
			}
			{ // 将真题答案放到单独的.md文件中
				if (fullname.Contains("专题")
					|| fullname.EndsWith(".本章真题.md")) {
					StringBuilder builder = Convert本章真题(fullname);
					File.WriteAllText(fullname, builder.ToString());
				}
			}
		}

		private void btnClose_Click(object sender, EventArgs e) {
			this.Close();
		}
	}
}
