using System;
using System.Collections.Generic;
using System.Linq;

namespace Green_vs_Red.Core
{
	internal class Grid
	{
		private Cell[,] cells;

		internal ushort Height { get; private set; }
		internal ushort Width { get; private set; }

		internal Grid(ushort width, ushort height)
		{
			this.cells = new Cell[height, width];
			this.Height = height;
			this.Width = width;
		}

		internal void AddCell(ushort x, ushort y, CellState state) => this.cells[y, x] = new Cell((x, y), state);

		internal void Generate()
		{
			Cell[,] newCells = new Cell[this.Height, this.Width];
			for (ushort y = 0; y < this.Height; y++)
			{
				for (ushort x = 0; x < this.Width; x++)
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

		internal Cell GetCellAt(ushort x, ushort y) => cells[y, x];

		private List<Cell> GetCellNeighbours(Cell cell)
		{
			var cellNeighbours = new List<Cell>();
			for (var y = cell.Coordinates.Y - 1; y <= cell.Coordinates.Y + 1; y++)
			{
				if (y < 0 || y >= this.Height) continue;
				for (var x = cell.Coordinates.X - 1; x <= cell.Coordinates.X + 1; x++)
				{
					if (x < 0 || x >= this.Width || cell.Equals(this.cells[y, x])) continue;
					cellNeighbours.Add(this.cells[y, x]);
				}
			}
			return cellNeighbours;
		}

		internal void Print()
		{
			ConsoleColor defaultBackColour = Console.BackgroundColor;
			ConsoleColor defaultForeColour = Console.ForegroundColor;
			Console.BackgroundColor = ConsoleColor.White;
			for (ushort y = 0; y < this.Height; y++)
			{
				for (ushort x = 0; x < this.Width; x++)
				{
					Cell cell = this.cells[y, x];
					if (Enum.TryParse(cell.State.ToString(), out ConsoleColor foreColour))
						Console.ForegroundColor = foreColour;
					else Console.ForegroundColor = ConsoleColor.Gray;
					Console.Write(cell.Value);
					if (x == this.Width - 1) Console.Write(Environment.NewLine);
				}
			}
			Console.BackgroundColor = defaultBackColour;
			Console.ForegroundColor = defaultForeColour;
			Console.Write(Environment.NewLine);
		}
	}
}
