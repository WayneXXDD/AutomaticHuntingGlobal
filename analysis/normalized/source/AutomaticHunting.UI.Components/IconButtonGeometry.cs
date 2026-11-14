using System.Numerics;

namespace AutomaticHunting.UI.Components;

public readonly struct IconButtonGeometry
{
	public readonly int RowIndex;

	public readonly CardIconKind Kind;

	public readonly Vector2 Min;

	public readonly Vector2 Max;

	public readonly bool Enabled;

	public IconButtonGeometry(int arg0, CardIconKind arg1, Vector2 arg2, Vector2 arg3, bool arg4)
	{
		RowIndex = arg0;
		Kind = arg1;
		Min = arg2;
		Max = arg3;
		Enabled = arg4;
	}
}
