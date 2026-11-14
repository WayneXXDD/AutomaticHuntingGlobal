using System;
using System.Numerics;
using Dalamud.Bindings.ImGui;

namespace AutomaticHunting.UI.Components;

public static class UiBadge
{
	private const float _2001_2002 = 7f;

	public static void Pill(string text, Vector4 color)
	{
		Pill(text, color, color);
	}

	public static void Pill(string text, Vector4 color, Vector4 textColor)
	{
		Vector2 cursorScreenPos = ImGui.GetCursorScreenPos();
		float num = _00A0(text);
		_00A0(ImGui.GetWindowDrawList(), cursorScreenPos, text, color, textColor, num);
		ImGui.Dummy(new Vector2(num, PillHeight()));
	}

	public static void PillAt(string text, Vector4 color, float rightX, float y)
	{
		PillAt(text, color, rightX, y, color);
	}

	public static void PillAt(string text, Vector4 color, float rightX, float y, Vector4 textColor)
	{
		float num = _00A0(text);
		_00A0(ImGui.GetWindowDrawList(), new Vector2(rightX - num, y), text, color, textColor, num);
	}

	public static void PillAtLeft(string text, Vector4 color, float leftX, float y)
	{
		PillAtLeft(text, color, leftX, y, color);
	}

	public static void PillAtLeft(string text, Vector4 color, float leftX, float y, Vector4 textColor)
	{
		float num = _00A0(text);
		_00A0(ImGui.GetWindowDrawList(), new Vector2(leftX, y), text, color, textColor, num);
	}

	public static void Dot(string text, Vector4 color)
	{
		Dot("●", text, color);
	}

	public static void Dot(string glyph, string text, Vector4 color)
	{
		ImGui.AlignTextToFramePadding();
		float y = ImGui.GetStyle().FramePadding.Y;
		ImDrawListPtr windowDrawList = ImGui.GetWindowDrawList();
		string text2 = glyph + " " + text;
		Vector2 vector = ImGui.CalcTextSize(text2);
		Theme.DrawText(windowDrawList, ImGui.GetCursorScreenPos() + new Vector2(0f, y), color, text2, 0f, Theme.HudTextShadow);
		ImGui.Dummy(new Vector2(vector.X, ImGui.GetTextLineHeight()));
	}

	public static void ProgressBar(float ratio, Vector4 color, float height = 4f)
	{
		ProgressBar(ratio, color, height, -1f);
	}

	public static void ProgressBar(float ratio, Vector4 color, float height, float threshold)
	{
		float uiScale = Theme.UiScale;
		ImDrawListPtr windowDrawList = ImGui.GetWindowDrawList();
		Vector2 cursorScreenPos = ImGui.GetCursorScreenPos();
		float x = ImGui.GetContentRegionAvail().X;
		float num = height * uiScale;
		windowDrawList.AddRectFilled(cursorScreenPos, cursorScreenPos + new Vector2(x, num), Theme.U32(Theme.ProgressTrack), num * 0.5f);
		float num2 = Math.Clamp(ratio, 0f, 1f) * x;
		if (num2 > 0.5f)
		{
			windowDrawList.AddRectFilled(cursorScreenPos, cursorScreenPos + new Vector2(num2, num), Theme.U32(color), num * 0.5f);
		}
		if (threshold >= 0f)
		{
			float x2 = cursorScreenPos.X + Math.Clamp(threshold, 0f, 1f) * x;
			windowDrawList.AddLine(new Vector2(x2, cursorScreenPos.Y - 1f * uiScale), new Vector2(x2, cursorScreenPos.Y + num + 1f * uiScale), Theme.U32(Theme.TextPrimary), 1f);
		}
		ImGui.Dummy(new Vector2(x, num));
	}

	public static void ProgressBarHp(float hpPercent, float height = 4f, float threshold = -1f)
	{
		float ratio = Math.Clamp(hpPercent / 100f, 0f, 1f);
		Vector4 color;
		if (hpPercent > 60f)
		{
			color = Theme.ProgressFill;
		}
		else
		{
			color = ((hpPercent >= 30f) ? Theme.ProgressFillWarn : Theme.ProgressFillDanger);
		}
		ProgressBar(ratio, color, height, threshold);
	}

	public static float PillHeight()
	{
		return 20f * Theme.UiScale;
	}

	public static float PillWidthOf(string text)
	{
		return _00A0(text);
	}

	private static float _00A0(string P_0)
	{
		float uiScale = Theme.UiScale;
		return 14f * uiScale + ImGui.CalcTextSize(P_0).X;
	}

	private static void _00A0(ImDrawListPtr P_0, Vector2 P_1, string P_2, Vector4 P_3, Vector4 P_4, float P_5)
	{
		float uiScale = Theme.UiScale;
		float num = PillHeight();
		float num2 = 7f * uiScale;
		Vector2 pMax = P_1 + new Vector2(P_5, num);
		P_0.AddRectFilled(P_1, pMax, Theme.U32(Theme.SurfaceCard), num * 0.5f);
		P_0.AddRectFilled(P_1, pMax, Theme.U32(Theme.WithAlpha(P_3, 0.14f)), num * 0.5f);
		P_0.AddRect(P_1, pMax, Theme.U32(Theme.WithAlpha(P_3, 0.45f)), num * 0.5f);
		float fsSubtitle = Theme.FsSubtitle;
		Theme.DrawText(pos: new Vector2(P_1.X + num2, P_1.Y + (num - fsSubtitle) * 0.5f), dl: P_0, color: P_4, text: P_2, fontSize: fsSubtitle, shadow: Theme.HudTextShadow);
	}
}
