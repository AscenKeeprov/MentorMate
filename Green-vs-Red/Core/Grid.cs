using Green_vs_Red.Utilities;
using System;
using System.Linq;

namespace Green_vs_Red.Core
{
	internal class Grid
	{
		private readonly Cell[,] cells;
		private readonly object[] cellStates;
		private readonly Random rng;

		internal Grid(ushort width, ushort height)
		{
			this.cells = new Cell[width, height];
			this.cellStates = Enumeration.GetNumValues<CellState>().ToArray();
			this.rng = new Random();
		}

		internal void Generate()
		{
			for (ushort y = 0; y < cells.GetLength(0); y++)
			{
				for (ushort x = 0; x < cells.GetLength(1); x++)
				{
					object cellState = cellStates[rng.Next(0, cellStates.Length)];
					cells[y, x] = new Cell((x, y), cellState.ToString());
				}
			}
		}

		internal void Print()
		{
			ConsoleColor defaultBackColour = Console.BackgroundColor;
			ConsoleColor defaultForeColour = Console.ForegroundColor;
			Console.BackgroundColor = ConsoleColor.White;
			for (ushort y = 0; y < cells.GetLength(0); y++)
			{
				for (ushort x = 0; x < cells.GetLength(1); x++)
				{
					Cell cell = cells[y, x];
					if (Enum.TryParse(cell.State, out ConsoleColor foreColour))
						Console.ForegroundColor = foreColour;
					else Console.ForegroundColor = ConsoleColor.Gray;
					Console.Write(cell.Value);
					if (x == cells.GetLength(1) - 1) Console.Write(Environment.NewLine);
				}
			}
			Console.BackgroundColor = defaultBackColour;
			Console.ForegroundColor = defaultForeColour;
		}
	}
}
