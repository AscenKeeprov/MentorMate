using System;

namespace Green_vs_Red.Core
{
	internal class Cell
	{
		internal (ushort X, ushort Y) Coordinates { get; private set; }

		/// <summary>
		/// Gets the present state of the cell
		/// </summary>
		internal CellState State { get; set; }

		/// <summary>
		/// Gets the numeric value of the cell in its current state
		/// </summary>
		/// <returns>The value of the cell as a boxed number of the same
		/// <para> type as its state enumeration underlying type</para>
		/// </returns>
		internal object Value
		{
			get
			{
				Type valueType = Enum.GetUnderlyingType(typeof(CellState));
				return Convert.ChangeType(this.State, valueType);
			}
		}

		/// <summary>
		/// Creates a new cell to be positioned in a two-dimensional plane
		/// </summary>
		/// <param name="coordinates">The horizontal (X) and vertical (Y) coordinates of the cell</param>
		/// <param name="state">The initial state of the cell</param>
		internal Cell((ushort X, ushort Y) coordinates, CellState state)
		{
			this.Coordinates = coordinates;
			this.State = state;
		}

		public override bool Equals(object obj)
		{
			Cell that = obj as Cell;
			if (that == null) return false;
			return this.Coordinates.Equals(that.Coordinates)
				&& this.State.Equals(that.State);
		}

		public override int GetHashCode()
		{
			return this.Coordinates.GetHashCode() + this.State.GetHashCode();
		}

		public override string ToString() => $"{this.Coordinates} {this.State} [{this.Value}]";
	}
}
