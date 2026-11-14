using System;
using System.Numerics;
using Dalamud.Bindings.ImGui;

namespace AutomaticHunting.UI.Components;

public static class UiPanel
{
	public static void DrawWindowFrame(float arg0, bool arg1 = false)
	{
		Vector2 windowPos = ImGui.GetWindowPos();
		Vector2 windowSize = ImGui.GetWindowSize();
		DrawFrame(windowPos, windowPos + windowSize, arg0, arg1);
	}

	public static void DrawFrame(Vector2 arg0, Vector2 arg1, float arg2, bool arg3 = false)
	{
		ImDrawListPtr windowDrawList = ImGui.GetWindowDrawList();
		float num = Math.Min(arg2, Math.Min(arg1.X - arg0.X, arg1.Y - arg0.Y) * 0.5f);
		uint col = Theme.U32(new Vector4(1f, 1f, 1f, 0.06f));
		windowDrawList.AddLine(new Vector2(arg0.X + num, arg0.Y), new Vector2(arg1.X - num, arg0.Y), col, 1f);
		Vector4 arg4 = (arg3 ? Theme.WithAlpha(Theme.BrandGold, 0.55f) : Theme.WithAlpha(Theme.BorderEdgeLight, 0.55f));
		windowDrawList.AddRect(arg0, arg1, Theme.U32(arg4), num, 1f);
	}

	public static void DrawGlass(Vector2 arg0, Vector2 arg1, float arg2, float arg3, bool arg4 = false)
	{
		ImDrawListPtr windowDrawList = ImGui.GetWindowDrawList();
		float num = Math.Clamp(arg2, 0f, 1f);
		if (Theme.HudNeedsCompensation)
		{
			float arg5 = Math.Clamp(0.82f - num, 0f, 0.82f);
			windowDrawList.AddRectFilled(arg0, arg1, Theme.U32(Theme.WithAlpha(Theme.SurfaceWindow, arg5)), arg3);
		}
		windowDrawList.AddRectFilled(arg0, arg1, Theme.U32(Theme.WithAlpha(Theme.SurfaceWindow, num)), arg3);
		DrawFrame(arg0, arg1, arg3, arg4);
	}

	public static void DrawCompensationLayer(float arg0)
	{
		if (Theme.HudNeedsCompensation)
		{
			float num = Math.Clamp(arg0, 0f, 1f);
			float arg1 = Math.Clamp(0.82f - num, 0f, 0.82f);
			Vector2 windowPos = ImGui.GetWindowPos();
			Vector2 windowSize = ImGui.GetWindowSize();
			ImGui.GetWindowDrawList().AddRectFilled(windowPos, windowPos + windowSize, Theme.U32(Theme.WithAlpha(Theme.SurfaceWindow, arg1)), 8f * Theme.UiScale);
		}
	}

	public static void DrawCard(Vector2 arg0, Vector2 arg1, float arg2, Vector4? arg3 = null)
	{
		ImDrawListPtr windowDrawList = ImGui.GetWindowDrawList();
		windowDrawList.AddRectFilled(arg0, arg1, Theme.U32(arg3 ?? Theme.SurfaceCard), arg2);
		windowDrawList.AddRect(arg0, arg1, Theme.U32(Theme.BorderCard), arg2, 1f);
	}
}
