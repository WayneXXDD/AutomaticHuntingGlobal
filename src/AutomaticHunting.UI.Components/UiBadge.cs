using System;
using System.Numerics;
using Dalamud.Bindings.ImGui;

namespace AutomaticHunting.UI.Components;

public static class UiBadge
{
	private const float field_1011 = 7f;

	public static void Pill(string arg0, Vector4 arg1)
	{
		Pill(arg0, arg1, arg1);
	}

	public static void Pill(string arg0, Vector4 arg1, Vector4 arg2)
	{
		Vector2 cursorScreenPos = ImGui.GetCursorScreenPos();
		float num = Method_1041(arg0);
		Method_1042(ImGui.GetWindowDrawList(), cursorScreenPos, arg0, arg1, arg2, num);
		ImGui.Dummy(new Vector2(num, PillHeight()));
	}

	public static void PillAt(string arg0, Vector4 arg1, float arg2, float arg3)
	{
		PillAt(arg0, arg1, arg2, arg3, arg1);
	}

	public static void PillAt(string arg0, Vector4 arg1, float arg2, float arg3, Vector4 arg4)
	{
		float num = Method_1041(arg0);
		Method_1042(ImGui.GetWindowDrawList(), new Vector2(arg2 - num, arg3), arg0, arg1, arg4, num);
	}

	public static void PillAtLeft(string arg0, Vector4 arg1, float arg2, float arg3)
	{
		PillAtLeft(arg0, arg1, arg2, arg3, arg1);
	}

	public static void PillAtLeft(string arg0, Vector4 arg1, float arg2, float arg3, Vector4 arg4)
	{
		float arg5 = Method_1041(arg0);
		Method_1042(ImGui.GetWindowDrawList(), new Vector2(arg2, arg3), arg0, arg1, arg4, arg5);
	}

	public static void Dot(string arg0, Vector4 arg1)
	{
		Dot("●", arg0, arg1);
	}

	public static void Dot(string arg0, string arg1, Vector4 arg2)
	{
		ImGui.AlignTextToFramePadding();
		float y = ImGui.GetStyle().FramePadding.Y;
		ImDrawListPtr windowDrawList = ImGui.GetWindowDrawList();
		string text = arg0 + " " + arg1;
		Vector2 vector = ImGui.CalcTextSize(text);
		Theme.DrawText(windowDrawList, ImGui.GetCursorScreenPos() + new Vector2(0f, y), arg2, text, 0f, Theme.HudTextShadow);
		ImGui.Dummy(new Vector2(vector.X, ImGui.GetTextLineHeight()));
	}

	public static void ProgressBar(float arg0, Vector4 arg1, float arg2 = 4f)
	{
		ProgressBar(arg0, arg1, arg2, -1f);
	}

	public static void ProgressBar(float arg0, Vector4 arg1, float arg2, float arg3)
	{
		float uiScale = Theme.UiScale;
		ImDrawListPtr windowDrawList = ImGui.GetWindowDrawList();
		Vector2 cursorScreenPos = ImGui.GetCursorScreenPos();
		float x = ImGui.GetContentRegionAvail().X;
		float num = arg2 * uiScale;
		windowDrawList.AddRectFilled(cursorScreenPos, cursorScreenPos + new Vector2(x, num), Theme.U32(Theme.ProgressTrack), num * 0.5f);
		float num2 = Math.Clamp(arg0, 0f, 1f) * x;
		if (num2 > 0.5f)
		{
			windowDrawList.AddRectFilled(cursorScreenPos, cursorScreenPos + new Vector2(num2, num), Theme.U32(arg1), num * 0.5f);
		}
		if (arg3 >= 0f)
		{
			float x2 = cursorScreenPos.X + Math.Clamp(arg3, 0f, 1f) * x;
			windowDrawList.AddLine(new Vector2(x2, cursorScreenPos.Y - 1f * uiScale), new Vector2(x2, cursorScreenPos.Y + num + 1f * uiScale), Theme.U32(Theme.TextPrimary), 1f);
		}
		ImGui.Dummy(new Vector2(x, num));
	}

	public static void ProgressBarHp(float arg0, float arg1 = 4f, float arg2 = -1f)
	{
		float arg3 = Math.Clamp(arg0 / 100f, 0f, 1f);
		Vector4 arg4;
		if (arg0 > 60f)
		{
			arg4 = Theme.ProgressFill;
		}
		else
		{
			arg4 = ((arg0 >= 30f) ? Theme.ProgressFillWarn : Theme.ProgressFillDanger);
		}
		ProgressBar(arg3, arg4, arg1, arg2);
	}

	public static float PillHeight()
	{
		return 20f * Theme.UiScale;
	}

	public static float PillWidthOf(string arg0)
	{
		return Method_1041(arg0);
	}

	private static float Method_1041(string arg0)
	{
		float uiScale = Theme.UiScale;
		return 14f * uiScale + ImGui.CalcTextSize(arg0).X;
	}

	private static void Method_1042(ImDrawListPtr arg0, Vector2 arg1, string arg2, Vector4 arg3, Vector4 arg4, float arg5)
	{
		float uiScale = Theme.UiScale;
		float num = PillHeight();
		float num2 = 7f * uiScale;
		Vector2 pMax = arg1 + new Vector2(arg5, num);
		arg0.AddRectFilled(arg1, pMax, Theme.U32(Theme.SurfaceCard), num * 0.5f);
		arg0.AddRectFilled(arg1, pMax, Theme.U32(Theme.WithAlpha(arg3, 0.14f)), num * 0.5f);
		arg0.AddRect(arg1, pMax, Theme.U32(Theme.WithAlpha(arg3, 0.45f)), num * 0.5f);
		float fsSubtitle = Theme.FsSubtitle;
		Theme.DrawText(arg1: new Vector2(arg1.X + num2, arg1.Y + (num - fsSubtitle) * 0.5f), arg0: arg0, arg2: arg4, arg3: arg2, arg4: fsSubtitle, arg5: Theme.HudTextShadow);
	}
}
