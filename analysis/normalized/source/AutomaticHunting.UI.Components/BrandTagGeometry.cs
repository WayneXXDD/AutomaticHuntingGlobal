using System.Numerics;

namespace AutomaticHunting.UI.Components;

public readonly struct BrandTagGeometry
{
	public readonly Vector2 TitleMin;

	public readonly Vector2 TitleMax;

	public readonly Vector2 VersionMin;

	public readonly Vector2 VersionMax;

	public BrandTagGeometry(Vector2 arg0, Vector2 arg1, Vector2 arg2, Vector2 arg3)
	{
		TitleMin = arg0;
		TitleMax = arg1;
		VersionMin = arg2;
		VersionMax = arg3;
	}
}
