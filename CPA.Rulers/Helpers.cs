using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace CPA.Rulers {
	public static class __Helpers {
		/// <summary>
		/// 给出具有指定<paramref name="key"/>的<typeparamref name="TValue"/>对象。
		/// 若不存在，则创建并返回之。
		/// </summary>
		/// <typeparam name="TKey"></typeparam>
		/// <typeparam name="TValue"></typeparam>
		/// <param name="dict"></param>
		/// <param name="key"></param>
		/// <returns></returns>
		public static TValue Provide<TKey, TValue>(this Dictionary<TKey, TValue> dict, TKey key)
			where TValue : new() {
			TValue result;
			if (!dict.TryGetValue(key, out result)) {
				result = new TValue();
				dict.Add(key, result);
			}

			return result;
		}
	}
}
