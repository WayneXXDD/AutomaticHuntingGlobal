using System.Numerics;

namespace AutomaticHunting.UI.Components;

public readonly struct HudStatusRowGeometry(Vector2 rowMin, Vector2 rowMax, Vector2 pillMin, Vector2 pillMax, Vector2 lockMin, Vector2 lockMax, bool hasLock, string statusText, float zoneRight)
{
	public readonly Vector2 RowMin = rowMin;

	public readonly Vector2 RowMax = rowMax;

	public readonly Vector2 PillMin = pillMin;

	public readonly Vector2 PillMax = pillMax;

	public readonly Vector2 LockMin = lockMin;

	public readonly Vector2 LockMax = lockMax;

	public readonly bool HasLock = hasLock;

	public readonly string StatusText = statusText;

	public readonly float ZoneRight = zoneRight;
}
