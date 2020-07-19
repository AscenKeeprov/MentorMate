using System;
using System.Collections.Generic;
using System.Linq;

namespace Green_vs_Red.Utilities
{
	public static class Enumeration
	{
		/// <summary>
		/// Retrieves the numeric values of an enumeration type
		/// </summary>
		/// <typeparam name="T">The enum type to extract values from</typeparam>
		/// <returns>An enumerable collection of boxed numbers</returns>
		public static IEnumerable<object> GetNumValues<T>()
		{
			Type underlyingType = Enum.GetUnderlyingType(typeof(T));
			IEnumerable<T> namedValues = Enum.GetValues(typeof(T)).Cast<T>();
			IEnumerable<object> numericValues = namedValues
				.Select(v => Convert.ChangeType(v, underlyingType));
			return numericValues;
		}
	}
}
