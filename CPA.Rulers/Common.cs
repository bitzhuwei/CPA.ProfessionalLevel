using System;

namespace CPA.Rulers {
	static class Common {
		private const double epsilon = 1E-10;

		/// <summary>
		/// 若给定的<paramref name="value"/>很小，则返回0。
		/// 否则返回原值。
		/// </summary>
		/// <param name="value"></param>
		/// <returns></returns>
		public static double Shake(this double value) {
			if (Math.Abs(value) <= epsilon) {
				return 0;
			}
			else {
				return value;
			}
		}
	}
}
