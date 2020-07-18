using System;

namespace Green_vs_Red
{
	public class Program
	{
		public static void Main(string[] args)
		{
			try
			{
				Cell cell = new Cell(1);
				Console.WriteLine(cell);
			}
			catch (Exception exception)
			{
				Console.WriteLine(exception.Message);
				Console.WriteLine(exception.StackTrace);
			}
		}
	}
}
