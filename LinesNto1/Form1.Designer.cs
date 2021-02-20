namespace LinesNto1 {
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
			this.txtDest = new System.Windows.Forms.TextBox();
			this.txtSource = new System.Windows.Forms.TextBox();
			this.SuspendLayout();
			// 
			// txtDest
			// 
			this.txtDest.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.txtDest.Font = new System.Drawing.Font("宋体", 16F);
			this.txtDest.Location = new System.Drawing.Point(12, 12);
			this.txtDest.Multiline = true;
			this.txtDest.Name = "txtDest";
			this.txtDest.Size = new System.Drawing.Size(866, 445);
			this.txtDest.TabIndex = 0;
			// 
			// txtSource
			// 
			this.txtSource.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.txtSource.Font = new System.Drawing.Font("宋体", 16F);
			this.txtSource.Location = new System.Drawing.Point(12, 463);
			this.txtSource.Multiline = true;
			this.txtSource.Name = "txtSource";
			this.txtSource.Size = new System.Drawing.Size(866, 220);
			this.txtSource.TabIndex = 0;
			this.txtSource.TextChanged += new System.EventHandler(this.txtSource_TextChanged);
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(890, 695);
			this.Controls.Add(this.txtSource);
			this.Controls.Add(this.txtDest);
			this.Name = "Form1";
			this.Text = "多行转为1行";
			this.Activated += new System.EventHandler(this.Form1_Activated);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TextBox txtDest;
		private System.Windows.Forms.TextBox txtSource;
	}
}

