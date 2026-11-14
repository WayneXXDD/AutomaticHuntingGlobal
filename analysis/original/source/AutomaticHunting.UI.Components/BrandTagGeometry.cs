using System.Numerics;

namespace AutomaticHunting.UI.Components;

public readonly struct BrandTagGeometry(Vector2 titleMin, Vector2 titleMax, Vector2 versionMin, Vector2 versionMax)
{
	public readonly Vector2 TitleMin = titleMin;

	public readonly Vector2 TitleMax = titleMax;

	public readonly Vector2 VersionMin = versionMin;

	public readonly Vector2 VersionMax = versionMax;
}
