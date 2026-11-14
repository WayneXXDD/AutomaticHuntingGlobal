using System.Numerics;

namespace AutomaticHunting.UI.Components;

public readonly struct CardRowGeometry(int rowIndex, float titleColumnWidth, float actionColumnWidth, PrefixGeometry[] prefixes, IconButtonGeometry handle, IconButtonGeometry up, IconButtonGeometry down, IconButtonGeometry delete, Vector2 titleMin)
{
	public readonly int RowIndex = rowIndex;

	public readonly float TitleColumnWidth = titleColumnWidth;

	public readonly Vector2 TitleMin = titleMin;

	public readonly float ActionColumnWidth = actionColumnWidth;

	public readonly IconButtonGeometry Handle = handle;

	public readonly PrefixGeometry[] Prefixes = prefixes;

	public readonly IconButtonGeometry Up = up;

	public readonly IconButtonGeometry Down = down;

	public readonly IconButtonGeometry Delete = delete;
}
