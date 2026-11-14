using System.Numerics;

namespace AutomaticHunting.UI.Components;

public readonly struct LeaderRowGeometry(int index, Vector2 checkMin, Vector2 checkMax, Vector2 nameMin, Vector2 nameMax, Vector2 deleteMin, Vector2 deleteMax, Vector2 rowMin, Vector2 rowMax, bool isChecked)
{
	public readonly int Index = index;

	public readonly Vector2 CheckMin = checkMin;

	public readonly Vector2 CheckMax = checkMax;

	public readonly Vector2 NameMin = nameMin;

	public readonly Vector2 NameMax = nameMax;

	public readonly Vector2 DeleteMin = deleteMin;

	public readonly Vector2 DeleteMax = deleteMax;

	public readonly Vector2 RowMin = rowMin;

	public readonly Vector2 RowMax = rowMax;

	public readonly bool Checked = isChecked;
}
