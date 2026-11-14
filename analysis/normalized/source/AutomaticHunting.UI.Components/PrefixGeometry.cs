using System.Numerics;

namespace AutomaticHunting.UI.Components;

public readonly struct PrefixGeometry
{
	public readonly Vector2 Min;

	public readonly Vector2 Max;

	public readonly float TextWidth;

	public readonly Vector2 TextMin;

	public readonly string Text;

	public readonly Vector2 BoxMin;

	public readonly Vector2 BoxMax;

	public PrefixGeometry(Vector2 arg0, Vector2 arg1, float arg2, Vector2 arg3, string arg4, Vector2 arg5, Vector2 arg6)
	{
		Min = arg0;
		Max = arg1;
		TextWidth = arg2;
		TextMin = arg3;
		Text = arg4;
		BoxMin = arg5;
		BoxMax = arg6;
	}
}
