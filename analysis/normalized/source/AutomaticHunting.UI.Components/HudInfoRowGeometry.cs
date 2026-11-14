using System.Numerics;

namespace AutomaticHunting.UI.Components;

public readonly struct HudInfoRowGeometry
{
	public readonly int RowIndex;

	public readonly Vector2 LabelMin;

	public readonly float LabelWidth;

	public readonly Vector2 ValueMin;

	public readonly float ValueWidth;

	public readonly float RowHeight;

	public HudInfoRowGeometry(int arg0, Vector2 arg1, float arg2, Vector2 arg3, float arg4, float arg5)
	{
		RowIndex = arg0;
		LabelMin = arg1;
		LabelWidth = arg2;
		ValueMin = arg3;
		ValueWidth = arg4;
		RowHeight = arg5;
	}
}
