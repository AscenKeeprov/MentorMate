using System;

namespace Green_vs_Red
{
	internal class Cell
	{
		private CellState _cellState;

		internal string State
		{
			get => this._cellState.ToString();
			set
			{
				if (!Enum.TryParse(value, true, out CellState cellState)
					|| !Enum.IsDefined(typeof(CellState), cellState))
					throw new ArgumentException($"Invalid cell state: {value}");
				this._cellState = cellState;
			}
		}

		internal string Value
		{
			get
			{
				Type stateType = Enum.GetUnderlyingType(typeof(CellState));
				return Convert.ChangeType(this._cellState, stateType).ToString();
			}
		}

		internal Cell() { }

		internal Cell(string state)
		{
			this.State = state;
		}

		public override string ToString()
		{
			return $"[{this.State} ({this.Value})]";
		}
	}
}
