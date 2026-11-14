using System.Numerics;
using System.Runtime.CompilerServices;
using Dalamud.Bindings.ImGui;

namespace AutomaticHunting.UI.Components;

public static class UiBrandTag
{
	public const string DefaultTitle = "自动狩猎跟车";

	private const float _2001_2003 = 8f;

	private const string _2001_2004 = "0.0.0.0";

	[CompilerGenerated]
	private static readonly string _2001_2005 = FormatVersion(typeof(UiBrandTag).Assembly.GetName().Version?.ToString());

	public static string VersionText
	{
		[CompilerGenerated]
		get
		{
			return _2001_2005;
		}
	}

	public static string FormatVersion(string? assemblyVersion)
	{
		return "v" + (string.IsNullOrWhiteSpace(assemblyVersion) ? "0.0.0.0" : assemblyVersion);
	}

	public static BrandTagGeometry Draw(ImDrawListPtr dl, Vector2 origin, float bandH, string title = "自动狩猎跟车", string? version = null)
	{
		float uiScale = Theme.UiScale;
		string text = (string.IsNullOrEmpty(version) ? VersionText : version);
		float fsTitle = Theme.FsTitle;
		float fsCaption = Theme.FsCaption;
		float num = Theme.MeasureTextWidth(title, fsTitle);
		float num2 = Theme.MeasureTextWidth(text, fsCaption);
		Vector2 vector = new Vector2(origin.X, origin.Y + (bandH - fsTitle) * 0.5f);
		Theme.DrawText(dl, vector, Theme.TextPrimary, title, fsTitle);
		float baselineUnits = Theme.BaselineUnits;
		float num3 = vector.Y + Theme.BaselineOffset(baselineUnits, fsTitle);
		Vector2 vector2 = new Vector2(origin.X + num + 8f * uiScale, num3 - Theme.BaselineOffset(baselineUnits, fsCaption));
		Theme.DrawText(dl, vector2, Theme.TextSecondary, text, fsCaption);
		return new BrandTagGeometry(vector, new Vector2(vector.X + num, vector.Y + fsTitle), vector2, new Vector2(vector2.X + num2, vector2.Y + fsCaption));
	}
}
