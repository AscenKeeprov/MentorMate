using System;
using System.Collections.Generic;
using System.Linq;

namespace Green_vs_Red.Utilities
{
	public static class Enumeration
	{
		/// <summary>
		/// Retrieves the entries of an enumeration type in the form of key-value tuples
		/// </summary>
		/// <typeparam name="T">The enum type to extract values from</typeparam>
		/// <returns>An array of tuples where the first item is the base enum value and
		/// <para> the second is an object boxing its corresponding numeric value</para>
		/// </returns>
		public static (T, object)[] GetEntries<T>()
		{
			Type valueType = Enum.GetUnderlyingType(typeof(T));
			IEnumerable<T> values = Enum.GetValues(typeof(T)).Cast<T>();
			var entries = new (T, object)[values.Count()];
			for (int i = 0; i < values.Count(); i++)
			{
				T value = values.ElementAt(i);
				entries[i] = (value, Convert.ChangeType(value, valueType));
			}
			return entries;
		}
	}
}
