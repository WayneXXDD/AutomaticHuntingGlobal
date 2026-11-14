using System.Numerics;

namespace AutomaticHunting.UI.Components;

public readonly struct LeaderRowGeometry
{
	public readonly int Index;

	public readonly Vector2 CheckMin;

	public readonly Vector2 CheckMax;

	public readonly Vector2 NameMin;

	public readonly Vector2 NameMax;

	public readonly Vector2 DeleteMin;

	public readonly Vector2 DeleteMax;

	public readonly Vector2 RowMin;

	public readonly Vector2 RowMax;

	public readonly bool Checked;

	public LeaderRowGeometry(int arg0, Vector2 arg1, Vector2 arg2, Vector2 arg3, Vector2 arg4, Vector2 arg5, Vector2 arg6, Vector2 arg7, Vector2 arg8, bool arg9)
	{
		Index = arg0;
		CheckMin = arg1;
		CheckMax = arg2;
		NameMin = arg3;
		NameMax = arg4;
		DeleteMin = arg5;
		DeleteMax = arg6;
		RowMin = arg7;
		RowMax = arg8;
		Checked = arg9;
	}
}
