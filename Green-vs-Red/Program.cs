using Green_vs_Red.Core;
using Green_vs_Red.Utilities;
using System;

namespace Green_vs_Red
{
	public class Program
	{
		public static void Main(string[] args)
		{
			try
			{
				Console.WriteLine("Welcome to \"Green vs. Red\"!");
				Console.WriteLine("Please configure game settings to begin playing.");
				ushort gridWidth = ParseSetting<ushort>("Grid width [2-999 cells]: ",
					(w) => w > 1 && w < 1000);
				ushort gridHeight = ParseSetting<ushort>("Grid height [2-999 cells]: ",
					(h) => h > 1 && h < 1000);
				ushort targetCellCoordinateX = ParseSetting<ushort>("Target cell X coordinate: ",
					(x) => x >= 0 && x < gridWidth);
				ushort targetCellCoordinateY = ParseSetting<ushort>("Target cell Y coordinate: ",
					(y) => y >= 0 && y < gridHeight);
				ulong endGeneration = ParseSetting<ulong>("End generation number: ", (n) => n > 0);
				Grid grid = new Grid(gridWidth, gridHeight);
				grid.Print();
				for (ulong generation = 1; generation <= endGeneration; generation++)
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

		private static T ParseSetting<T>(string prompt, Func<T, bool> validate)
		{
			Console.Write(prompt);
			if (!Parser.TryParse(Console.ReadLine(), out T setting) || !validate(setting))
			{
				Console.WriteLine("Invalid setting!");
				return ParseSetting(prompt, validate);
			}
			return setting;
		}
	}
}
