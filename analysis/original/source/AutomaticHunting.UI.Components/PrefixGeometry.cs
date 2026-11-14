using System.Numerics;

namespace AutomaticHunting.UI.Components;

public readonly struct PrefixGeometry(Vector2 min, Vector2 max, float textWidth, Vector2 textMin, string text, Vector2 boxMin, Vector2 boxMax)
{
	public readonly Vector2 Min = min;

	public readonly Vector2 Max = max;

	public readonly float TextWidth = textWidth;

	public readonly Vector2 TextMin = textMin;

	public readonly string Text = text;

	public readonly Vector2 BoxMin = boxMin;

	public readonly Vector2 BoxMax = boxMax;
}
