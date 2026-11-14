using System.Numerics;

namespace AutomaticHunting.UI.Components;

public readonly struct HelpLinkGeometry
{
	public readonly Vector2 Min;

	public readonly Vector2 Max;

	public readonly Vector2 TextMin;

	public readonly Vector2 TextMax;

	public readonly Vector2 IconCenter;

	public readonly bool Clicked;

	public readonly bool Hovered;

	public HelpLinkGeometry(Vector2 arg0, Vector2 arg1, Vector2 arg2, Vector2 arg3, Vector2 arg4, bool arg5, bool arg6)
	{
		Min = arg0;
		Max = arg1;
		TextMin = arg2;
		TextMax = arg3;
		IconCenter = arg4;
		Clicked = arg5;
		Hovered = arg6;
	}
}
