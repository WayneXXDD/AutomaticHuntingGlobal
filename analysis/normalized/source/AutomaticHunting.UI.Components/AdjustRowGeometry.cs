using System.Numerics;

namespace AutomaticHunting.UI.Components;

public readonly struct AdjustRowGeometry
{
	public readonly Vector2 TextMin;

	public readonly Vector2 TextMax;

	public readonly Vector2 IncreaseMin;

	public readonly Vector2 IncreaseMax;

	public readonly Vector2 DecreaseMin;

	public readonly Vector2 DecreaseMax;

	public readonly bool IncreaseEnabled;

	public readonly bool DecreaseEnabled;

	public AdjustRowGeometry(Vector2 arg0, Vector2 arg1, Vector2 arg2, Vector2 arg3, Vector2 arg4, Vector2 arg5, bool arg6, bool arg7)
	{
		TextMin = arg0;
		TextMax = arg1;
		IncreaseMin = arg2;
		IncreaseMax = arg3;
		DecreaseMin = arg4;
		DecreaseMax = arg5;
		IncreaseEnabled = arg6;
		DecreaseEnabled = arg7;
	}
}
