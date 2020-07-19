using Green_vs_Red.Utilities;
using System;
using System.Linq;

namespace Green_vs_Red.Core
{
	internal class Grid
	{
		private Cell[,] cells;

		internal Grid(ushort width, ushort height)
		{
			this.cells = new Cell[width, height];
		}

		internal Cell GetCellAt(ushort x, ushort y)
		{
			return cells[y, x];
		}

		internal void Generate()
		{
			object[] cellStates = Enumeration.GetNumValues<CellState>().ToArray();
			Random rng = new Random();
			for (int y = 0; y < cells.GetLength(0); y++)
			{
				for (int x = 0; x < cells.GetLength(1); x++)
				{
					object cellState = cellStates[rng.Next(0, cellStates.Length)];
					cells[y, x] = new Cell(cellState.ToString());
				}
			}
		}

		internal void Print()
		{
			ConsoleColor defaultBackColour = Console.BackgroundColor;
			ConsoleColor defaultForeColour = Console.ForegroundColor;
			Console.BackgroundColor = ConsoleColor.White;
			for (int y = 0; y < cells.GetLength(0); y++)
			{
				for (int x = 0; x < cells.GetLength(1); x++)
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
