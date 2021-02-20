using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace CPA.Rulers {
	/// <summary>
	/// 财务报表项目(or自定义项目)显示出来的样式
	/// </summary>
	public class FinancialStatementItemDisplay {
		/// <summary>
		/// 最前面是否显示2个空格？
		/// </summary>
		public string empty;

		public ESide side;

		/// <summary>
		/// 项目名称
		/// </summary>
		public string name;

		/// <summary>
		/// 数值
		/// </summary>
		public string value;
	
		public override string ToString() {
			return ($"{empty}{side}: {name} {value}");
		}
	}
}

