using System.Numerics;
using System.Runtime.CompilerServices;
using Dalamud.Bindings.ImGui;

namespace AutomaticHunting.UI.Components;

public static class UiBrandTag
{
	public const string DefaultTitle = "自動狩獵跟車";

	private const float field_1017 = 8f;

	private const string field_1018 = "0.0.0.0";

	[CompilerGenerated]
	private static readonly string field_1019 = FormatVersion(typeof(UiBrandTag).Assembly.GetName().Version?.ToString());

	public static string VersionText
	{
		[CompilerGenerated]
		get
		{
			return field_1019;
		}
	}

	public static string FormatVersion(string? arg0)
	{
		return "v" + (string.IsNullOrWhiteSpace(arg0) ? "0.0.0.0" : arg0);
	}

	public static BrandTagGeometry Draw(ImDrawListPtr arg0, Vector2 arg1, float arg2, string arg3 = "自動狩獵跟車", string? arg4 = null)
	{
		float uiScale = Theme.UiScale;
		string text = (string.IsNullOrEmpty(arg4) ? VersionText : arg4);
		float fsTitle = Theme.FsTitle;
		float fsCaption = Theme.FsCaption;
		float num = Theme.MeasureTextWidth(arg3, fsTitle);
		float num2 = Theme.MeasureTextWidth(text, fsCaption);
		Vector2 vector = new Vector2(arg1.X, arg1.Y + (arg2 - fsTitle) * 0.5f);
		Theme.DrawText(arg0, vector, Theme.TextPrimary, arg3, fsTitle);
		float baselineUnits = Theme.BaselineUnits;
		float num3 = vector.Y + Theme.BaselineOffset(baselineUnits, fsTitle);
		Vector2 vector2 = new Vector2(arg1.X + num + 8f * uiScale, num3 - Theme.BaselineOffset(baselineUnits, fsCaption));
		Theme.DrawText(arg0, vector2, Theme.TextSecondary, text, fsCaption);
		return new BrandTagGeometry(vector, new Vector2(vector.X + num, vector.Y + fsTitle), vector2, new Vector2(vector2.X + num2, vector2.Y + fsCaption));
	}
}
