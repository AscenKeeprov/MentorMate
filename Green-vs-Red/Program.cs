using Green_vs_Red.Core;
using Green_vs_Red.Utilities;
using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace Green_vs_Red
{
	public class Program
	{
		public static void Main(string[] args)
		{
			try
			{
				Console.WriteLine("Welcome to \"Green vs. Red\"!");
				Console.WriteLine("Please take a moment to configure game settings.");
				ushort gridWidth = ParseInput<ushort>("Grid width [2-999 cells]: ",
					(w) => w > 1 && w < 1000);
				ushort gridHeight = ParseInput<ushort>("Grid height [2-999 cells]: ",
					(h) => h > 1 && h < 1000);
				Grid grid = new Grid(gridWidth, gridHeight);
				Console.WriteLine("Grid dimensions set. Let's build it.");
				Console.WriteLine($"A row may contain {gridWidth} cells (digits) and nothing else.");
				PopulateGrid(grid);
				grid.Print();
				Console.WriteLine("Initial grid layout ready. Now pick a cell to track.");
				Console.WriteLine("Each one has corresponding coordinates (X,Y) on the grid.");
				Console.WriteLine("[e.g. (1,0) designate the second cell in the top row]");
				ushort targetCellCoordinateX = ParseInput<ushort>("Target cell X coordinate: ",
					(x) => x >= 0 && x < gridWidth);
				ushort targetCellCoordinateY = ParseInput<ushort>("Target cell Y coordinate: ",
					(y) => y >= 0 && y < gridHeight);
				Cell targetCell = grid.GetCellAt(targetCellCoordinateX, targetCellCoordinateY);
				Console.WriteLine($"You have chosen cell {targetCell}");
				Console.WriteLine("Finally, specify until which turn the cell should be tracked.");
				ulong endTurn = ParseInput<ulong>("End turn number: ", (n) => n > 0);
				Console.WriteLine("Configuration complete. Pressing a key will run the scenario.");
				Console.WriteLine("Press [G] if you want to see the grid after each turn.");
				bool showAllGrids = Console.ReadKey(true).Key == ConsoleKey.G;
				for (ulong generation = 1; generation <= endTurn; generation++)
				{
					grid.Generate();
					if (showAllGrids) grid.Print();
				}
			}
			catch (Exception exception)
			{
				Console.WriteLine(exception.Message);
				Console.WriteLine(exception.StackTrace);
			}
		}

		private static T ParseInput<T>(string prompt, Func<T, bool> validate)
		{
			Console.Write(prompt);
			if (!Validation.TryParse(Console.ReadLine().Trim(), out T parsedValue)
				|| !validate(parsedValue))
			{
				Console.WriteLine("Invalid input!");
				return ParseInput(prompt, validate);
			}
			return parsedValue;
		}

		private static void PopulateGrid(Grid grid)
		{
			(CellState State, object Value)[] cellStates = Enumeration.GetEntries<CellState>();
			var cellStateValues = cellStates.Select(cs => cs.Value);
			string gridRowPattern = $@"^[{string.Join(string.Empty, cellStateValues)}]{{{grid.Width}}}$";
			for (ushort y = 0; y < grid.Height; y++)
			{
				string gridRow = ParseInput<string>($"Enter values for grid row {y + 1}: ",
					(gr) => Regex.IsMatch(gr, gridRowPattern));
				for (ushort x = 0; x < gridRow.Length; x++)
				{
					CellState cellState = cellStates.FirstOrDefault(cs
						=> cs.Value.ToString().Equals(gridRow[x].ToString())).State;
					grid.AddCell(x, y, cellState);
				}
			}
		}
	}
}
