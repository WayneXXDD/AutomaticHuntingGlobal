using System.Numerics;

namespace AutomaticHunting.UI.Components;

public readonly struct HudInfoRowGeometry(int rowIndex, Vector2 labelMin, float labelWidth, Vector2 valueMin, float valueWidth, float rowHeight)
{
	public readonly int RowIndex = rowIndex;

	public readonly Vector2 LabelMin = labelMin;

	public readonly float LabelWidth = labelWidth;

	public readonly Vector2 ValueMin = valueMin;

	public readonly float ValueWidth = valueWidth;

	public readonly float RowHeight = rowHeight;
}
