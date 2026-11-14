using System;
using System.Numerics;
using Dalamud.Bindings.ImGui;

namespace AutomaticHunting.UI.Components;

public static class UiHelpLink
{
	public const float MinHitHeight = 24f;

	public const float IconSize = 12f;

	public const float PadX = 10f;

	private const float _2001_204E = 5f;

	public static bool IsAllowedUrl(string? url)
	{
		if (string.IsNullOrWhiteSpace(url))
		{
			return false;
		}
		if (!url.StartsWith("http://", StringComparison.OrdinalIgnoreCase))
		{
			return url.StartsWith("https://", StringComparison.OrdinalIgnoreCase);
		}
		return true;
	}

	public static HelpLinkGeometry Draw(ImDrawListPtr dl, string id, string label, string url, Vector2 origin, float available, float lineH, Action<string>? onOpen)
	{
		float uiScale = Theme.UiScale;
		float num = Math.Max(24f * uiScale, lineH);
		float num2 = 10f * uiScale;
		float num3 = 12f * uiScale;
		float num4 = 5f * uiScale;
		float maxWidth = Math.Max(20f, available - num2 * 2f - num4 - num3);
		string text = Theme.Ellipsis(label, maxWidth, Theme.FsCaption);
		float num5 = Theme.MeasureTextWidth(text, Theme.FsCaption);
		float x = Math.Min(available, num5 + num4 + num3 + num2 * 2f + 1f * uiScale);
		Vector2 vector = origin;
		Vector2 vector2 = origin + new Vector2(x, num);
		ImGui.SetCursorScreenPos(vector);
		bool flag = ImGui.InvisibleButton(id, vector2 - vector);
		bool flag2 = ImGui.IsItemActive();
		bool flag3 = ImGui.IsMouseHoveringRect(vector, vector2, clip: true);
		Vector4 c;
		if (flag2)
		{
			c = Theme.BtnNeutralActive;
		}
		else
		{
			c = (flag3 ? Theme.BtnNeutralHover : Theme.BtnNeutral);
		}
		Vector4 c2 = (flag3 ? Theme.BrandGold : Theme.BorderControl);
		dl.AddRectFilled(vector, vector2, Theme.U32(c), 4f * uiScale);
		dl.AddRect(vector, vector2, Theme.U32(c2), 4f * uiScale, 1f);
		float num6 = Theme.MeasureTextHeight(Theme.FsCaption);
		Vector2 vector3 = new Vector2(vector.X + num2, vector.Y + (num - num6) * 0.5f);
		Theme.DrawText(dl, vector3, Theme.BrandGold, text, Theme.FsCaption);
		Vector2 vector4 = new Vector2(vector2.X - num2 - num3 * 0.5f, vector.Y + num * 0.5f);
		UiIcons.DrawExternalLink(dl, vector4, num3, Theme.U32(Theme.BrandGold));
		if (flag)
		{
			onOpen?.Invoke(url);
		}
		if (flag3 && !string.IsNullOrEmpty(url))
		{
			UiButton.ShowTooltip(url);
		}
		ImGui.SetCursorScreenPos(origin);
		return new HelpLinkGeometry(vector, vector2, vector3, vector3 + new Vector2(num5, num6), vector4, flag, flag3);
	}
}
