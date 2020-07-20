using Green_vs_Red.Core;
using System;

namespace Green_vs_Red
{
	public class Program
	{
		public static void Main(string[] args)
		{
			try
			{
				Grid grid = new Grid(15, 8);
				grid.Print();
				for (int generation = 1; generation <= 6; generation++)
				{
					grid.GenerateNext();
					grid.Print();
				}
			}
			catch (Exception exception)
			{
				Console.WriteLine(exception.Message);
				Console.WriteLine(exception.StackTrace);
			}
		}
	}
}
