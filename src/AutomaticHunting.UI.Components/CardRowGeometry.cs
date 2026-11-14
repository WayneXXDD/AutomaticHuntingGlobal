using System.Numerics;

namespace AutomaticHunting.UI.Components;

public readonly struct CardRowGeometry
{
	public readonly int RowIndex;

	public readonly float TitleColumnWidth;

	public readonly Vector2 TitleMin;

	public readonly float ActionColumnWidth;

	public readonly IconButtonGeometry Handle;

	public readonly PrefixGeometry[] Prefixes;

	public readonly IconButtonGeometry Up;

	public readonly IconButtonGeometry Down;

	public readonly IconButtonGeometry Delete;

	public CardRowGeometry(int arg0, float arg1, float arg2, PrefixGeometry[] arg3, IconButtonGeometry arg4, IconButtonGeometry arg5, IconButtonGeometry arg6, IconButtonGeometry arg7, Vector2 arg8)
	{
		RowIndex = arg0;
		TitleColumnWidth = arg1;
		ActionColumnWidth = arg2;
		TitleMin = arg8;
		Prefixes = arg3;
		Handle = arg4;
		Up = arg5;
		Down = arg6;
		Delete = arg7;
	}
}
