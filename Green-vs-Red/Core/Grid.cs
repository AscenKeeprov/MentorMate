using Green_vs_Red.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Green_vs_Red.Core
{
	internal class Grid
	{
		private readonly Cell[,] cells;
		private readonly Random rng;
		private readonly (CellState State, object Value)[] states;

		internal Grid(ushort width, ushort height)
		{
			this.cells = new Cell[width, height];
			this.rng = new Random();
			this.states = Enumeration.GetEntries<CellState>();
		}

		internal void Generate()
		{
			for (ushort y = 0; y < cells.GetLength(0); y++)
			{
				for (ushort x = 0; x < cells.GetLength(1); x++)
				{
					ref Cell cell = ref cells[y, x];
					if (cell is null)
					{
						int index = rng.Next(0, states.Length);
						CellState state = states[index].State;
						cell = new Cell((x, y), state);
					}
					else
					{
						List<Cell> cellNeighbours = GetCellNeighbours(cell);
						var greenNeighbours = cellNeighbours.Where(c => c.State == CellState.Green);
						var redNeighbours = cellNeighbours.Where(c => c.State == CellState.Red);
						Console.WriteLine($"Stats for cell {cell}:");
						Console.WriteLine($"-Green neighbours count: {greenNeighbours.Count()}");
						Console.WriteLine($"-Red neighbours count: {redNeighbours.Count()}");
						//TODO: APPLY GAME RULES TO GENERATE NEXT GRID STATE
						return;
					}
				}
			}
		}

		private List<Cell> GetCellNeighbours(Cell cell)
		{
			var cellNeighbours = new List<Cell>();
			for (var y = cell.Coordinates.Y - 1; y <= cell.Coordinates.Y + 1; y++)
			{
				if (y < 0 || y >= cells.GetLength(0)) continue;
				for (var x = cell.Coordinates.X - 1; x <= cell.Coordinates.X + 1; x++)
				{
					if (x < 0 || x >= cells.GetLength(1) || cell.Equals(cells[y, x])) continue;
					cellNeighbours.Add(cells[y, x]);
				}
			}
			return cellNeighbours;
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
					if (Enum.TryParse(cell.State.ToString(), out ConsoleColor foreColour))
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
