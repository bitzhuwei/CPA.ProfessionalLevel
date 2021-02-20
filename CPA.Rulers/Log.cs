using System;
using System.IO;
using System.Text;

namespace CPA.Rulers {
	public static partial class Log {
		private const string logFilePrefix = "CPA.Rulers";
		/// <summary>
		/// 在日志里写上一笔。
		/// </summary>
		/// <param name="comment"></param>
		/// <param name="newLine"></param>
		public static void DumpLog(this string comment, bool newLine = true) {
			DateTime now = DateTime.Now;
			string filename = string.Format("{0}.{1:yyyyMMdd}.log", logFilePrefix, now);
			File.AppendAllText(filename, string.Format("{0:HHmmss}: ", now));
			File.AppendAllText(filename, comment);
			if (newLine) {
				File.AppendAllText(filename, Environment.NewLine);
			}
		}

		/// <summary>
		/// 在日志里写上一笔。
		/// </summary>
		/// <param name="comment"></param>
		/// <param name="newLine"></param>
		public static void DumpLog(this StringBuilder comment, bool newLine = true) {
			DateTime now = DateTime.Now;
			string filename = string.Format("{0}.{1:yyyyMMdd}.log", logFilePrefix, now);
			File.AppendAllText(filename, string.Format("{0:HHmmss}: ", now));
			File.AppendAllText(filename, comment.ToString());
			if (newLine) {
				File.AppendAllText(filename, Environment.NewLine);
			}
		}

	}
}
