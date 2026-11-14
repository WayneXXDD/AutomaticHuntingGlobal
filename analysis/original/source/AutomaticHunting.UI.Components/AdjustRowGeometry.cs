using System.Numerics;

namespace AutomaticHunting.UI.Components;

public readonly struct AdjustRowGeometry(Vector2 textMin, Vector2 textMax, Vector2 increaseMin, Vector2 increaseMax, Vector2 decreaseMin, Vector2 decreaseMax, bool increaseEnabled, bool decreaseEnabled)
{
	public readonly Vector2 TextMin = textMin;

	public readonly Vector2 TextMax = textMax;

	public readonly Vector2 IncreaseMin = increaseMin;

	public readonly Vector2 IncreaseMax = increaseMax;

	public readonly Vector2 DecreaseMin = decreaseMin;

	public readonly Vector2 DecreaseMax = decreaseMax;

	public readonly bool IncreaseEnabled = increaseEnabled;

	public readonly bool DecreaseEnabled = decreaseEnabled;
}
