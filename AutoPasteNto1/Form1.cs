using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AutoPasteNto1 {
    public partial class Form1 : Form {
        public Form1() {
            InitializeComponent();

            var ptr = (IntPtr)SetClipboardViewer((int)this.Handle);
            if (ptr != null) { this.nextClipboardViewer = ptr; this.isStarted = true; }
            else { MessageBox.Show("监听剪切板失败，这活儿没法干了！", "哎呀！", MessageBoxButtons.OK, MessageBoxIcon.Error); }

            this.txtSource.GotFocus += TxtSource_GotFocus;
            this.txtSource.LostFocus += TxtSource_LostFocus;
            this.txtDest.GotFocus += TxtDest_GotFocus;
            this.txtDest.LostFocus += TxtDest_LostFocus;

            InitTitleFormat();
        }

        const string configFilename = "title.config";
        const string configDefault = @"#如果您使用的不是Adobe Reader或Foxit Reader，
#那么您就需要在本文件中添加您使用的pdf阅读器的【标题】中【必然包含的字符】，然后再打开本程序。
#这样，您在您使用的pdf阅读器中做了“复制”操作时，本程序才会识别这一操作，并将其转换为单行。
#例如，Adobe Reader的标题必然是“XXX.pdf - Adobe Reader”这样的格式，
#那么，在本文件的一行里写上“.pdf - Adobe Reader”（不包括引号）即可。
#注意，在一行里不要输入标题中没有的空格等特殊字符。
#以#号开头的行是注释用的，本程序会忽略掉。
#空行，本程序也会忽略掉。
.pdf - Adobe Reader
.pdf - Foxit Reader
";

        string[] titleFormats = new string[0];
        private void InitTitleFormat() {
            if (!File.Exists(configFilename)) { File.WriteAllText(configFilename, configDefault); }

            var list = new List<string>();
            using (var sr = new StreamReader(configFilename)) {
                while (!sr.EndOfStream) {
                    string line = sr.ReadLine();
                    if ((!line.StartsWith("#"))
                       && (line != null)
                        && (line.Length > 0)) {
                        list.Add(line);
                    }
                }
            }

            if (list.Count == 0) {
                MessageBox.Show("您的config文件里一个title格式都没有，本程序是不会有什么作用的！", "喵",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else {
                this.titleFormats = list.Distinct().ToArray();
            }
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

        //
        //定义几个方法
        //
        [DllImport("User32.dll")]
        protected static extern int SetClipboardViewer(int hWndNewViewer);

        [DllImport("User32.dll", CharSet = CharSet.Auto)]
        public static extern bool ChangeClipboardChain(IntPtr hWndRemove, IntPtr hWndNewNext);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern int SendMessage(IntPtr hwnd, int wMsg, IntPtr wParam, IntPtr lParam);

        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        public static extern IntPtr GetForegroundWindow();

        [DllImport("user32", SetLastError = true)] 
        public static extern int GetWindowText(
            IntPtr hWnd,//窗口句柄 
            StringBuilder lpString,//标题 
            int nMaxCount //最大值 
            );
        //
        //定义几个方法
        //

        IntPtr nextClipboardViewer;

        private bool isStarted;
        private int id;

        protected override void WndProc(ref Message m) {
            if (isStarted && nextClipboardViewer != null) {
                const int WM_DRAWCLIPBOARD = 0x308;
                const int WM_CHANGECBCHAIN = 0x030D;
                switch (m.Msg) {
                    case WM_CHANGECBCHAIN:
                        //DisplayClipboardData();
                        if (m.WParam == nextClipboardViewer) { nextClipboardViewer = m.LParam; }
                        else { SendMessage(nextClipboardViewer, m.Msg, m.WParam, m.LParam); }
                        break;
                    case WM_DRAWCLIPBOARD:
                        if (OnPdf()) {
                            ConvertNultipleLinesTo1();
                        }

                        //将WM_DRAWCLIPBOARD 消息传递到下一个观察链中的窗口
                        SendMessage(nextClipboardViewer, m.Msg, m.WParam, m.LParam);
                        //将WM_DRAWCLIPBOARD 消息传递到下一个观察链中的窗口 

                        break;
                    default:
                        base.WndProc(ref m);
                        break;
                }
            }
            else {
                base.WndProc(ref m);
            }
        }

        StringBuilder titleBuilder = new StringBuilder(1024);

        private bool OnPdf() {
            IntPtr formHandle = GetForegroundWindow();
            GetWindowText(formHandle, titleBuilder, titleBuilder.Capacity);//得到窗口的标题
            string title = titleBuilder.ToString();
            titleBuilder.Clear();

            bool result = false;
            foreach (var item in this.titleFormats) {
                if (title.Contains(item)) {
                    result = true; break;
                }
            }

            return result;
        }

		static readonly char[] lineSeparator = new char[] { '\r', '\n' };
        void ConvertNultipleLinesTo1() {
            var data = System.Windows.Forms.Clipboard.GetDataObject();
            var newContent = (System.String)(data.GetData(typeof(System.String)));
            if (newContent != null) {
                this.txtDest.Text = string.Empty;
                string[] lines = newContent.Split(lineSeparator, StringSplitOptions.None);
                foreach (var line in lines) {
                    this.txtDest.AppendText(line);
                }

                this.txtDest.Focus();
                this.txtDest.SelectAll();
                // 放入剪切板
                string oneLine = this.txtDest.Text;
                try {
                    System.Windows.Forms.Clipboard.SetDataObject(oneLine, true, 5, 20);
                    System.Media.SystemSounds.Asterisk.Play();
                    //System.Media.SystemSounds.Beep.Play();
                    //System.Media.SystemSounds.Exclamation.Play();
                    //System.Media.SystemSounds.Question.Play(); // 没声。

                } catch (Exception ex) {
                    System.Media.SystemSounds.Hand.Play();
                    MessageBox.Show(ex.ToString(), "出错了", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void Form1_Activated(object sender, EventArgs e) {
            this.txtSource.Focus();
            this.txtSource.SelectAll();
        }
    }
}
