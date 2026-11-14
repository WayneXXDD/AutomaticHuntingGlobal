using System.Numerics;

namespace AutomaticHunting.UI.Components;

public readonly struct HelpLinkGeometry(Vector2 min, Vector2 max, Vector2 textMin, Vector2 textMax, Vector2 iconCenter, bool clicked, bool hovered)
{
	public readonly Vector2 Min = min;

	public readonly Vector2 Max = max;

	public readonly Vector2 TextMin = textMin;

	public readonly Vector2 TextMax = textMax;

	public readonly Vector2 IconCenter = iconCenter;

	public readonly bool Clicked = clicked;

	public readonly bool Hovered = hovered;
}
