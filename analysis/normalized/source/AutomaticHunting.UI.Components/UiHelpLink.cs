using System;
using System.Numerics;
using Dalamud.Bindings.ImGui;

namespace AutomaticHunting.UI.Components;

public static class UiHelpLink
{
	public const float MinHitHeight = 24f;

	public const float IconSize = 12f;

	public const float PadX = 10f;

	private const float field_1140 = 5f;

	public static bool IsAllowedUrl(string? arg0)
	{
		if (string.IsNullOrWhiteSpace(arg0))
		{
			return false;
		}
		if (!arg0.StartsWith("http://", StringComparison.OrdinalIgnoreCase))
		{
			return arg0.StartsWith("https://", StringComparison.OrdinalIgnoreCase);
		}
		return true;
	}

	public static HelpLinkGeometry Draw(ImDrawListPtr arg0, string arg1, string arg2, string arg3, Vector2 arg4, float arg5, float arg6, Action<string>? arg7)
	{
		float uiScale = Theme.UiScale;
		float num = Math.Max(24f * uiScale, arg6);
		float num2 = 10f * uiScale;
		float num3 = 12f * uiScale;
		float num4 = 5f * uiScale;
		float arg8 = Math.Max(20f, arg5 - num2 * 2f - num4 - num3);
		string text = Theme.Ellipsis(arg2, arg8, Theme.FsCaption);
		float num5 = Theme.MeasureTextWidth(text, Theme.FsCaption);
		float x = Math.Min(arg5, num5 + num4 + num3 + num2 * 2f + 1f * uiScale);
		Vector2 vector = arg4;
		Vector2 vector2 = arg4 + new Vector2(x, num);
		ImGui.SetCursorScreenPos(vector);
		bool flag = ImGui.InvisibleButton(arg1, vector2 - vector);
		bool flag2 = ImGui.IsItemActive();
		bool flag3 = ImGui.IsMouseHoveringRect(vector, vector2, clip: true);
		Vector4 arg9;
		if (flag2)
		{
			arg9 = Theme.BtnNeutralActive;
		}
		else
		{
			arg9 = (flag3 ? Theme.BtnNeutralHover : Theme.BtnNeutral);
		}
		Vector4 arg10 = (flag3 ? Theme.BrandGold : Theme.BorderControl);
		arg0.AddRectFilled(vector, vector2, Theme.U32(arg9), 4f * uiScale);
		arg0.AddRect(vector, vector2, Theme.U32(arg10), 4f * uiScale, 1f);
		float num6 = Theme.MeasureTextHeight(Theme.FsCaption);
		Vector2 vector3 = new Vector2(vector.X + num2, vector.Y + (num - num6) * 0.5f);
		Theme.DrawText(arg0, vector3, Theme.BrandGold, text, Theme.FsCaption);
		Vector2 vector4 = new Vector2(vector2.X - num2 - num3 * 0.5f, vector.Y + num * 0.5f);
		UiIcons.DrawExternalLink(arg0, vector4, num3, Theme.U32(Theme.BrandGold));
		if (flag)
		{
			arg7?.Invoke(arg3);
		}
		if (flag3 && !string.IsNullOrEmpty(arg3))
		{
			UiButton.ShowTooltip(arg3);
		}
		ImGui.SetCursorScreenPos(arg4);
		return new HelpLinkGeometry(vector, vector2, vector3, vector3 + new Vector2(num5, num6), vector4, flag, flag3);
	}
}
