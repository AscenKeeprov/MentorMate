using System;
using System.ComponentModel;

namespace Green_vs_Red.Utilities
{
	public static class Validation
	{
		/// <summary>
		/// Attempts to parse textual input to the provided type
		/// </summary>
		/// <typeparam name="T">The type to convert to</typeparam>
		/// <returns>If parse succeeds, outputs an object converted to the specified type</returns>
		public static bool TryParse<T>(string input, out T output)
		{
			output = default;
			try
			{
				TypeConverter converter = TypeDescriptor.GetConverter(typeof(T));
				output = (T)converter.ConvertFromString(input);
				return true;
			}
			catch (Exception)
			{
				return false;
			}
		}
	}
}
