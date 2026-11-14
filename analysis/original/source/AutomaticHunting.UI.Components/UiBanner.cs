using System;
using System.Numerics;
using Dalamud.Bindings.ImGui;

namespace AutomaticHunting.UI.Components;

public static class UiBanner
{
	public static void Draw(string text, Vector4 color, string? actionLabel = null, Action? onAction = null)
	{
		float uiScale = Theme.UiScale;
		Vector2 cursorScreenPos = ImGui.GetCursorScreenPos();
		ImDrawListPtr windowDrawList = ImGui.GetWindowDrawList();
		float x = ImGui.GetContentRegionAvail().X;
		Vector2 vector = ImGui.CalcTextSize(text);
		float num = ((actionLabel != null) ? (92f * uiScale) : 0f);
		float num2 = Math.Max(vector.Y + 12f * uiScale, Theme.ControlHeight + 6f * uiScale);
		Vector2 pMin = cursorScreenPos;
		Vector2 pMax = cursorScreenPos + new Vector2(x, num2);
		windowDrawList.AddRectFilled(pMin, pMax, Theme.U32(Theme.WithAlpha(color, 0.1f)), 6f * uiScale);
		windowDrawList.AddRect(pMin, pMax, Theme.U32(Theme.WithAlpha(color, 0.55f)), 6f * uiScale, 1f);
		windowDrawList.AddRectFilled(new Vector2(pMin.X, pMin.Y), new Vector2(pMin.X + 3f * uiScale, pMax.Y), Theme.U32(color), 1.5f * uiScale);
		Theme.DrawText(windowDrawList, new Vector2(pMin.X + 12f * uiScale, pMin.Y + (num2 - vector.Y) * 0.5f), color, Theme.Ellipsis(text, x - 24f * uiScale - num), Theme.FsBody);
		if (actionLabel != null && onAction != null)
		{
			ImGui.SetCursorScreenPos(new Vector2(pMax.X - num, pMin.Y + (num2 - Theme.ControlHeight) * 0.5f));
			if (UiButton.Draw(actionLabel, UiVariant.Neutral, new Vector2(num - 8f * uiScale, Theme.ControlHeight)))
			{
				onAction();
			}
		}
		ImGui.SetCursorScreenPos(new Vector2(cursorScreenPos.X, cursorScreenPos.Y + num2 + 6f * uiScale));
	}

	public static void Show(string title, string? hint, Vector4 color)
	{
		float uiScale = Theme.UiScale;
		Vector2 cursorScreenPos = ImGui.GetCursorScreenPos();
		ImDrawListPtr windowDrawList = ImGui.GetWindowDrawList();
		float x = ImGui.GetContentRegionAvail().X;
		Vector2 vector = ImGui.CalcTextSize(title);
		Vector2 vector2 = ((hint == null) ? Vector2.Zero : ImGui.CalcTextSize(hint));
		float num = vector.Y + ((hint == null) ? 0f : (vector2.Y + 4f * uiScale)) + 16f * uiScale;
		Vector2 pMin = cursorScreenPos;
		Vector2 pMax = cursorScreenPos + new Vector2(x, num);
		windowDrawList.AddRectFilled(pMin, pMax, Theme.U32(Theme.WithAlpha(color, 0.1f)), 6f * uiScale);
		windowDrawList.AddRect(pMin, pMax, Theme.U32(Theme.WithAlpha(color, 0.55f)), 6f * uiScale, 1f);
		windowDrawList.AddRectFilled(new Vector2(pMin.X, pMin.Y), new Vector2(pMin.X + 3f * uiScale, pMax.Y), Theme.U32(color), 1.5f * uiScale);
		float num2 = pMin.Y + 8f * uiScale;
		Theme.DrawText(windowDrawList, new Vector2(pMin.X + 12f * uiScale, num2), color, Theme.Ellipsis(title, x - 24f * uiScale), Theme.FsBody);
		num2 += vector.Y;
		if (hint != null)
		{
			num2 += 4f * uiScale;
			Theme.DrawText(windowDrawList, new Vector2(pMin.X + 12f * uiScale, num2), Theme.TextSecondary, Theme.Ellipsis(hint, x - 24f * uiScale, Theme.FsCaption), Theme.FsCaption);
		}
		ImGui.SetCursorScreenPos(new Vector2(cursorScreenPos.X, cursorScreenPos.Y + num + 6f * uiScale));
	}
}
