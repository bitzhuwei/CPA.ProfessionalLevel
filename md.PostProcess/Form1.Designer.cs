namespace md.PostProcess {
	partial class Form1 {
		/// <summary>
		/// 必需的设计器变量。
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// 清理所有正在使用的资源。
		/// </summary>
		/// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
		protected override void Dispose(bool disposing) {
			if (disposing && (components != null)) {
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows 窗体设计器生成的代码

		/// <summary>
		/// 设计器支持所需的方法 - 不要修改
		/// 使用代码编辑器修改此方法的内容。
		/// </summary>
		private void InitializeComponent() {
			this.txtPath = new System.Windows.Forms.TextBox();
			this.chkList = new System.Windows.Forms.CheckBox();
			this.chkCode = new System.Windows.Forms.CheckBox();
			this.btnGo = new System.Windows.Forms.Button();
			this.chk会计分录 = new System.Windows.Forms.CheckBox();
			this.btnClose = new System.Windows.Forms.Button();
			this.chkForceAll = new System.Windows.Forms.CheckBox();
			this.chk会计题目 = new System.Windows.Forms.CheckBox();
			this.chk表格内换行 = new System.Windows.Forms.CheckBox();
			this.chk公式行 = new System.Windows.Forms.CheckBox();
			this.chkmd公式 = new System.Windows.Forms.CheckBox();
			this.SuspendLayout();
			// 
			// txtPath
			// 
			this.txtPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.txtPath.Font = new System.Drawing.Font("宋体", 16F);
			this.txtPath.Location = new System.Drawing.Point(12, 12);
			this.txtPath.Name = "txtPath";
			this.txtPath.Size = new System.Drawing.Size(776, 32);
			this.txtPath.TabIndex = 0;
			this.txtPath.Text = "D:\\Projects\\CPA";
			// 
			// chkList
			// 
			this.chkList.AutoSize = true;
			this.chkList.Checked = true;
			this.chkList.CheckState = System.Windows.Forms.CheckState.Checked;
			this.chkList.Font = new System.Drawing.Font("宋体", 16F);
			this.chkList.Location = new System.Drawing.Point(12, 50);
			this.chkList.Name = "chkList";
			this.chkList.Size = new System.Drawing.Size(139, 26);
			this.chkList.TabIndex = 1;
			this.chkList.Text = "标题加序号";
			this.chkList.UseVisualStyleBackColor = true;
			// 
			// chkCode
			// 
			this.chkCode.AutoSize = true;
			this.chkCode.Checked = true;
			this.chkCode.CheckState = System.Windows.Forms.CheckState.Checked;
			this.chkCode.Font = new System.Drawing.Font("宋体", 16F);
			this.chkCode.Location = new System.Drawing.Point(157, 50);
			this.chkCode.Name = "chkCode";
			this.chkCode.Size = new System.Drawing.Size(161, 26);
			this.chkCode.TabIndex = 1;
			this.chkCode.Text = "加粗->代码框";
			this.chkCode.UseVisualStyleBackColor = true;
			// 
			// btnGo
			// 
			this.btnGo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnGo.Font = new System.Drawing.Font("宋体", 16F);
			this.btnGo.Location = new System.Drawing.Point(653, 236);
			this.btnGo.Name = "btnGo";
			this.btnGo.Size = new System.Drawing.Size(135, 64);
			this.btnGo.TabIndex = 2;
			this.btnGo.Text = "开始处理！";
			this.btnGo.UseVisualStyleBackColor = true;
			this.btnGo.Click += new System.EventHandler(this.btnGo_Click);
			// 
			// chk会计分录
			// 
			this.chk会计分录.AutoSize = true;
			this.chk会计分录.Checked = true;
			this.chk会计分录.CheckState = System.Windows.Forms.CheckState.Checked;
			this.chk会计分录.Font = new System.Drawing.Font("宋体", 16F);
			this.chk会计分录.Location = new System.Drawing.Point(324, 50);
			this.chk会计分录.Name = "chk会计分录";
			this.chk会计分录.Size = new System.Drawing.Size(205, 26);
			this.chk会计分录.TabIndex = 1;
			this.chk会计分录.Text = "会计分录->代码框";
			this.chk会计分录.UseVisualStyleBackColor = true;
			// 
			// btnClose
			// 
			this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnClose.Font = new System.Drawing.Font("宋体", 16F);
			this.btnClose.Location = new System.Drawing.Point(12, 236);
			this.btnClose.Name = "btnClose";
			this.btnClose.Size = new System.Drawing.Size(135, 64);
			this.btnClose.TabIndex = 2;
			this.btnClose.Text = "关闭窗口";
			this.btnClose.UseVisualStyleBackColor = true;
			this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
			// 
			// chkForceAll
			// 
			this.chkForceAll.AutoSize = true;
			this.chkForceAll.Font = new System.Drawing.Font("宋体", 16F);
			this.chkForceAll.Location = new System.Drawing.Point(627, 204);
			this.chkForceAll.Name = "chkForceAll";
			this.chkForceAll.Size = new System.Drawing.Size(161, 26);
			this.chkForceAll.TabIndex = 1;
			this.chkForceAll.Text = "强制全部处理";
			this.chkForceAll.UseVisualStyleBackColor = true;
			// 
			// chk会计题目
			// 
			this.chk会计题目.AutoSize = true;
			this.chk会计题目.Checked = true;
			this.chk会计题目.CheckState = System.Windows.Forms.CheckState.Checked;
			this.chk会计题目.Font = new System.Drawing.Font("宋体", 16F);
			this.chk会计题目.Location = new System.Drawing.Point(535, 50);
			this.chk会计题目.Name = "chk会计题目";
			this.chk会计题目.Size = new System.Drawing.Size(227, 26);
			this.chk会计题目.TabIndex = 1;
			this.chk会计题目.Text = "会计题目=>科目总结";
			this.chk会计题目.UseVisualStyleBackColor = true;
			// 
			// chk表格内换行
			// 
			this.chk表格内换行.AutoSize = true;
			this.chk表格内换行.Checked = true;
			this.chk表格内换行.CheckState = System.Windows.Forms.CheckState.Checked;
			this.chk表格内换行.Font = new System.Drawing.Font("宋体", 16F);
			this.chk表格内换行.Location = new System.Drawing.Point(12, 82);
			this.chk表格内换行.Name = "chk表格内换行";
			this.chk表格内换行.Size = new System.Drawing.Size(304, 26);
			this.chk表格内换行.TabIndex = 1;
			this.chk表格内换行.Text = "“； ”在表格内意味着换行";
			this.chk表格内换行.UseVisualStyleBackColor = true;
			// 
			// chk公式行
			// 
			this.chk公式行.AutoSize = true;
			this.chk公式行.Checked = true;
			this.chk公式行.CheckState = System.Windows.Forms.CheckState.Checked;
			this.chk公式行.Font = new System.Drawing.Font("宋体", 16F);
			this.chk公式行.Location = new System.Drawing.Point(324, 82);
			this.chk公式行.Name = "chk公式行";
			this.chk公式行.Size = new System.Drawing.Size(447, 26);
			this.chk公式行.TabIndex = 1;
			this.chk公式行.Text = "“【公式】”行(后可能跟随＝)转为代码段";
			this.chk公式行.UseVisualStyleBackColor = true;
			// 
			// chkmd公式
			// 
			this.chkmd公式.AutoSize = true;
			this.chkmd公式.Checked = true;
			this.chkmd公式.CheckState = System.Windows.Forms.CheckState.Checked;
			this.chkmd公式.Font = new System.Drawing.Font("宋体", 16F);
			this.chkmd公式.Location = new System.Drawing.Point(12, 114);
			this.chkmd公式.Name = "chkmd公式";
			this.chkmd公式.Size = new System.Drawing.Size(326, 26);
			this.chkmd公式.TabIndex = 1;
			this.chkmd公式.Text = "“【md公式】”行去掉多余的\\";
			this.chkmd公式.UseVisualStyleBackColor = true;
			// 
			// Form1
			// 
			this.AcceptButton = this.btnGo;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.btnClose;
			this.ClientSize = new System.Drawing.Size(800, 312);
			this.Controls.Add(this.btnClose);
			this.Controls.Add(this.btnGo);
			this.Controls.Add(this.chkForceAll);
			this.Controls.Add(this.chkmd公式);
			this.Controls.Add(this.chk公式行);
			this.Controls.Add(this.chk表格内换行);
			this.Controls.Add(this.chk会计题目);
			this.Controls.Add(this.chk会计分录);
			this.Controls.Add(this.chkCode);
			this.Controls.Add(this.chkList);
			this.Controls.Add(this.txtPath);
			this.Name = "Form1";
			this.Text = "CPA.md文件后处理";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TextBox txtPath;
		private System.Windows.Forms.CheckBox chkList;
		private System.Windows.Forms.CheckBox chkCode;
		private System.Windows.Forms.Button btnGo;
		private System.Windows.Forms.CheckBox chk会计分录;
		private System.Windows.Forms.Button btnClose;
		private System.Windows.Forms.CheckBox chkForceAll;
		private System.Windows.Forms.CheckBox chk会计题目;
		private System.Windows.Forms.CheckBox chk表格内换行;
		private System.Windows.Forms.CheckBox chk公式行;
		private System.Windows.Forms.CheckBox chkmd公式;
	}
}

