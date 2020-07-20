using Green_vs_Red.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Green_vs_Red.Core
{
	internal class Grid
	{
		private Cell[,] cells;
		private readonly (CellState State, object Value)[] cellStates;
		private readonly ushort height;
		private readonly ushort width;

		internal Grid(ushort width, ushort height)
		{
			this.cells = new Cell[height, width];
			this.cellStates = Enumeration.GetEntries<CellState>();
			this.height = height;
			this.width = width;
			Generate();
		}

		private void Generate()
		{
			Random rng = new Random();
			for (ushort y = 0; y < this.height; y++)
			{
				for (ushort x = 0; x < this.width; x++)
				{
					int index = rng.Next(0, this.cellStates.Length);
					CellState state = this.cellStates[index].State;
					this.cells[y, x] = new Cell((x, y), state);
				}
			}
		}

		internal void GenerateNext()
		{
			Cell[,] newCells = new Cell[this.height, this.width];
			for (ushort y = 0; y < this.height; y++)
			{
				for (ushort x = 0; x < this.width; x++)
				{
					Cell cell = this.cells[y, x];
					List<Cell> cellNeighbours = GetCellNeighbours(cell);
					int greenNeighboursCount = cellNeighbours
						.Where(c => c.State == CellState.Green).Count();
					switch (greenNeighboursCount)
					{
						case 0:
							if (cell.State == CellState.Green)
							{
								newCells[y, x] = new Cell((x, y), CellState.Red);
								continue;
							}
							goto default;
						case 1: goto case 0;
						case 2: goto default;
						case 3:
							if (cell.State == CellState.Red)
							{
								newCells[y, x] = new Cell((x, y), CellState.Green);
								continue;
							}
							goto default;
						case 4: goto case 0;
						case 5: goto case 0;
						case 6: goto case 3;
						case 7: goto case 0;
						case 8: goto case 0;
						default:
							newCells[y, x] = new Cell((x, y), cell.State);
							break;
					}
				}
			}
			this.cells = newCells;
		}

		private List<Cell> GetCellNeighbours(Cell cell)
		{
			var cellNeighbours = new List<Cell>();
			for (var y = cell.Coordinates.Y - 1; y <= cell.Coordinates.Y + 1; y++)
			{
				if (y < 0 || y >= this.height) continue;
				for (var x = cell.Coordinates.X - 1; x <= cell.Coordinates.X + 1; x++)
				{
					if (x < 0 || x >= this.width || cell.Equals(this.cells[y, x])) continue;
					cellNeighbours.Add(this.cells[y, x]);
				}
			}
			return cellNeighbours;
		}

		internal void Print()
		{
			ConsoleColor defaultBackColour = Console.BackgroundColor;
			ConsoleColor defaultForeColour = Console.ForegroundColor;
			Console.Write(Environment.NewLine);
			Console.BackgroundColor = ConsoleColor.White;
			for (ushort y = 0; y < this.height; y++)
			{
				for (ushort x = 0; x < this.width; x++)
				{
					Cell cell = this.cells[y, x];
					if (Enum.TryParse(cell.State.ToString(), out ConsoleColor foreColour))
						Console.ForegroundColor = foreColour;
					else Console.ForegroundColor = ConsoleColor.Gray;
					Console.Write(cell.Value);
					if (x == this.width - 1) Console.Write(Environment.NewLine);
				}
			}
			Console.BackgroundColor = defaultBackColour;
			Console.ForegroundColor = defaultForeColour;
		}
	}
}
