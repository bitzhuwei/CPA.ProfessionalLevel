using System.Linq.Expressions;

namespace CPA.Rulers {
	/// <summary>
	/// 会计分录中的一个会计账户
	/// </summary>
	public partial class JournalAccount {
		/// <summary>
		/// 借方Dr还是贷方Cr？
		/// </summary>
		public ESide Side { get; set; }

		/// <summary>
		/// 会计账户名称？固定资产、应付职工薪酬等
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		/// 数值
		/// </summary>
		public double Value { get; set; }

		/// <summary>
		/// 会计分录中的一个会计账户
		/// </summary>
		public JournalAccount() { }

		/// <summary>
		/// 会计分录中的一个会计账户
		/// </summary>
		/// <param name="side">借方Dr还是贷方Cr？</param>
		/// <param name="name">会计账户名称？固定资产、应付职工薪酬等</param>
		/// <param name="value">数值</param>
		public JournalAccount(ESide side, string name, double value) {
			this.Side = side; this.Name = name; this.Value = value;
		}

		public override string ToString() {
			return $"{(Side == ESide.Cr ? "    " : "")}{Side}: {Name} {Value}";
		}
	}
}

