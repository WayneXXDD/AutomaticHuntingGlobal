using System.Numerics;

namespace AutomaticHunting.UI.Components;

public readonly struct HudStatusRowGeometry
{
	public readonly Vector2 RowMin;

	public readonly Vector2 RowMax;

	public readonly Vector2 PillMin;

	public readonly Vector2 PillMax;

	public readonly Vector2 LockMin;

	public readonly Vector2 LockMax;

	public readonly bool HasLock;

	public readonly string StatusText;

	public readonly float ZoneRight;

	public HudStatusRowGeometry(Vector2 arg0, Vector2 arg1, Vector2 arg2, Vector2 arg3, Vector2 arg4, Vector2 arg5, bool arg6, string arg7, float arg8)
	{
		RowMin = arg0;
		RowMax = arg1;
		PillMin = arg2;
		PillMax = arg3;
		LockMin = arg4;
		LockMax = arg5;
		HasLock = arg6;
		StatusText = arg7;
		ZoneRight = arg8;
	}
}
