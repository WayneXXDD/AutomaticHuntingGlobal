using System;
using System.Numerics;
using Dalamud.Bindings.ImGui;

namespace AutomaticHunting.UI.Components;

public static class UiPanel
{
	public static void DrawWindowFrame(float radius, bool locked = false)
	{
		Vector2 windowPos = ImGui.GetWindowPos();
		Vector2 windowSize = ImGui.GetWindowSize();
		DrawFrame(windowPos, windowPos + windowSize, radius, locked);
	}

	public static void DrawFrame(Vector2 min, Vector2 max, float radius, bool locked = false)
	{
		ImDrawListPtr windowDrawList = ImGui.GetWindowDrawList();
		float num = Math.Min(radius, Math.Min(max.X - min.X, max.Y - min.Y) * 0.5f);
		uint col = Theme.U32(new Vector4(1f, 1f, 1f, 0.06f));
		windowDrawList.AddLine(new Vector2(min.X + num, min.Y), new Vector2(max.X - num, min.Y), col, 1f);
		Vector4 c = (locked ? Theme.WithAlpha(Theme.BrandGold, 0.55f) : Theme.WithAlpha(Theme.BorderEdgeLight, 0.55f));
		windowDrawList.AddRect(min, max, Theme.U32(c), num, 1f);
	}

	public static void DrawGlass(Vector2 min, Vector2 max, float alpha, float radius, bool locked = false)
	{
		ImDrawListPtr windowDrawList = ImGui.GetWindowDrawList();
		float num = Math.Clamp(alpha, 0f, 1f);
		if (Theme.HudNeedsCompensation)
		{
			float a = Math.Clamp(0.82f - num, 0f, 0.82f);
			windowDrawList.AddRectFilled(min, max, Theme.U32(Theme.WithAlpha(Theme.SurfaceWindow, a)), radius);
		}
		windowDrawList.AddRectFilled(min, max, Theme.U32(Theme.WithAlpha(Theme.SurfaceWindow, num)), radius);
		DrawFrame(min, max, radius, locked);
	}

	public static void DrawCompensationLayer(float alpha)
	{
		if (Theme.HudNeedsCompensation)
		{
			float num = Math.Clamp(alpha, 0f, 1f);
			float a = Math.Clamp(0.82f - num, 0f, 0.82f);
			Vector2 windowPos = ImGui.GetWindowPos();
			Vector2 windowSize = ImGui.GetWindowSize();
			ImGui.GetWindowDrawList().AddRectFilled(windowPos, windowPos + windowSize, Theme.U32(Theme.WithAlpha(Theme.SurfaceWindow, a)), 8f * Theme.UiScale);
		}
	}

	public static void DrawCard(Vector2 min, Vector2 max, float radius, Vector4? fill = null)
	{
		ImDrawListPtr windowDrawList = ImGui.GetWindowDrawList();
		windowDrawList.AddRectFilled(min, max, Theme.U32(fill ?? Theme.SurfaceCard), radius);
		windowDrawList.AddRect(min, max, Theme.U32(Theme.BorderCard), radius, 1f);
	}
}
