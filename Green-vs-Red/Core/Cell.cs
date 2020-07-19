using System;

namespace Green_vs_Red.Core
{
	internal class Cell
	{
		private (ushort X, ushort Y) coordinates;
		private CellState state;

		internal (ushort X, ushort Y) Coordinates
		{
			get => this.coordinates;
			private set => this.coordinates = value;
		}

		internal string State
		{
			get => this.state.ToString();
			set
			{
				if (!Enum.TryParse(value, true, out CellState cellState)
					|| !Enum.IsDefined(typeof(CellState), cellState))
					throw new ArgumentException($"Invalid cell state: {value}");
				this.state = cellState;
			}
		}

		internal string Value
		{
			get
			{
				Type stateType = Enum.GetUnderlyingType(typeof(CellState));
				return Convert.ChangeType(this.state, stateType).ToString();
			}
		}

		internal Cell((ushort X, ushort Y) coordinates, string state)
		{
			this.Coordinates = coordinates;
			this.State = state;
		}

		public override string ToString() => $"[{this.State} ({this.Value})]";
	}
}
