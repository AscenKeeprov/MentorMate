using System;

namespace Green_vs_Red.Core
{
	internal class Cell
	{
		internal (ushort X, ushort Y) Coordinates { get; private set; }

		internal CellState State { get; set; }

		internal object Value
		{
			get
			{
				Type valueType = Enum.GetUnderlyingType(typeof(CellState));
				return Convert.ChangeType(this.State, valueType);
			}
		}

		internal Cell((ushort X, ushort Y) coordinates, CellState state)
		{
			this.Coordinates = coordinates;
			this.State = state;
		}

		public override string ToString() => $"{this.Coordinates} {this.State} [{this.Value}]";
	}
}
