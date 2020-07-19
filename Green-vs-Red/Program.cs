using System;

namespace Green_vs_Red
{
	public class Program
	{
		public static void Main(string[] args)
		{
			try
			{
				Grid grid = new Grid(4, 4);
				grid.Generate();
				grid.Print();
			}
			catch (Exception exception)
			{
				Console.WriteLine(exception.Message);
				Console.WriteLine(exception.StackTrace);
			}
		}
	}
}
