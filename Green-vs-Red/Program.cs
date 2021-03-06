﻿using Green_vs_Red.Core;
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
				Console.WriteLine("Press [G] if you want to see the grid on each turn.");
				bool showAllGrids = Console.ReadKey(true).Key == ConsoleKey.G;
				ulong greenTargetCellStates = targetCell.State == CellState.Green ? 1U : 0;
				for (ulong turn = 1; turn <= endTurn; turn++)
				{
					bool gridChanged = grid.Generate();
					targetCell = grid.GetCellAt(targetCellCoordinateX, targetCellCoordinateY);
					if (gridChanged)
					{
						if (targetCell.State == CellState.Green) greenTargetCellStates++;
						if (showAllGrids) grid.Print();
					}
					else
					{
						ulong skippedTurns = endTurn - turn + 1;
						if (targetCell.State == CellState.Green) greenTargetCellStates += skippedTurns;
						if (showAllGrids) Console.WriteLine($"Skipping {skippedTurns} visualizations as the grid has stopped changing.");
						break;
					}
				}
				Console.WriteLine($"Simulation ended. Target cell was green {greenTargetCellStates} time{(greenTargetCellStates == 1 ? string.Empty : "s")}.");
			}
			catch (Exception exception)
			{
				Console.WriteLine(exception.Message);
				Console.WriteLine(exception.StackTrace);
			}
		}

		/// <summary>
		/// Prompts for and attempts to parse input whilst validating it at the same time.
		/// </summary>
		/// <typeparam name="T">The type that input should be parsed to.</typeparam>
		/// <returns>An object of the desired type, upon successful validation.
		/// <para> If validation fails, makes recurring calls for correct input.</para>
		/// </returns>
		/// <param name="prompt">A text message, describing what input is expected.</param>
		/// <param name="validate">A validation delegate, invoked on the input once parsing succeeds.</param>
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

		/// <summary>
		/// Fills in a grid with cells based on user input, row by row.
		/// <para>A regular expression pattern is used to determine if input is valid.</para>
		/// </summary>
		/// <param name="grid">An instance of a grid to populate.</param>
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
					Cell cell = new Cell((x, y), cellState);
					grid.AddCell(cell);
				}
			}
		}
	}
}
