﻿using System;
using System.ComponentModel;

namespace Green_vs_Red.Utilities
{
	public static class Parser
	{
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