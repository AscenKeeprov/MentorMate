using System;
using System.Collections.Generic;
using System.Linq;

namespace Green_vs_Red.Core
{
	internal class Grid
	{
		private Cell[,] cells;

		/// <summary>
		/// The grid's vertical dimension.
		/// </summary>
		internal ushort Height { get; private set; }
		/// <summary>
		/// The grid's horizontal dimension.
		/// </summary>
		internal ushort Width { get; private set; }

		/// <summary>
		/// Creates a two-dimensional grid.
		/// </summary>
		/// <param name="width">The size of the grid's horizontal axis.</param>
		/// <param name="height">The size of the grid's vertical axis.</param>
		internal Grid(ushort width, ushort height)
		{
			this.cells = new Cell[height, width];
			this.Height = height;
			this.Width = width;
		}

		/// <summary>
		/// Adds a cell to the grid based on its coordinates.
		/// </summary>
		/// <param name="cell">The cell that is to be placed on the grid.</param>
		internal void AddCell(Cell cell) => this.cells[cell.Coordinates.Y, cell.Coordinates.X] = cell;

		/// <summary>
		/// Generates a new layout for the grid according to a set of rules.
		/// <para>The grid's cells may or may not change their states.</para>
		/// </summary>
		/// <returns>A boolean denoting whether any of the cells has changed its state.</returns>
		internal bool Generate()
		{
			Cell[,] newCells = new Cell[this.Height, this.Width];
			bool statesChanged = false;
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
								statesChanged = true;
								continue;
							}
							goto default;
						case 1: goto case 0;
						case 2: goto default;
						case 3:
							if (cell.State == CellState.Red)
							{
								newCells[y, x] = new Cell((x, y), CellState.Green);
								statesChanged = true;
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
			return statesChanged;
		}

		/// <summary>
		/// Retrieves a cell from a specified position on the grid.
		/// </summary>
		/// <returns>A cell located at the provided coordinates.</returns>
		/// <param name="x">The horizontal position of the cell.</param>
		/// <param name="y">The vertical position of the cell.</param>
		internal Cell GetCellAt(ushort x, ushort y) => cells[y, x];

		/// <summary>
		/// Collects all cells surrounding a given cell.
		/// </summary>
		/// <returns>A collection of cells.</returns>
		/// <param name="cell">A cell, the adjacent cells of which should be returned.</param>
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

		/// <summary>
		/// Prints a visual representation of the grid's current layout on the console.
		/// </summary>
		internal void Print()
		{
			ConsoleColor defaultBackColour = Console.BackgroundColor;
			ConsoleColor defaultForeColour = Console.ForegroundColor;
			for (ushort y = 0; y < this.Height; y++)
			{
				Console.BackgroundColor = ConsoleColor.White;
				for (ushort x = 0; x < this.Width; x++)
				{
					Cell cell = this.cells[y, x];
					if (Enum.TryParse(cell.State.ToString(), out ConsoleColor foreColour))
						Console.ForegroundColor = foreColour;
					else Console.ForegroundColor = ConsoleColor.Gray;
					Console.Write(cell.Value);
				}
				Console.BackgroundColor = defaultBackColour;
				Console.ForegroundColor = defaultForeColour;
				Console.Write(Environment.NewLine);
			}
			Console.Write(Environment.NewLine);
		}
	}
}
