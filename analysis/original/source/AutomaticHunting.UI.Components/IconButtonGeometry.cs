using System.Numerics;

namespace AutomaticHunting.UI.Components;

public readonly struct IconButtonGeometry(int rowIndex, CardIconKind kind, Vector2 min, Vector2 max, bool enabled)
{
	public readonly int RowIndex = rowIndex;

	public readonly CardIconKind Kind = kind;

	public readonly Vector2 Min = min;

	public readonly Vector2 Max = max;

	public readonly bool Enabled = enabled;
}
