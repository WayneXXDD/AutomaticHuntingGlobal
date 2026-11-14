using System.Numerics;
using Dalamud.Bindings.ImGui;

namespace AutomaticHunting.UI.Components;

public static class UiSection
{
	public static void Header(string title, string? hint = null)
	{
		float uiScale = Theme.UiScale;
		float textLineHeight = ImGui.GetTextLineHeight();
		Vector2 cursorScreenPos = ImGui.GetCursorScreenPos();
		ImDrawListPtr windowDrawList = ImGui.GetWindowDrawList();
		windowDrawList.AddRectFilled(new Vector2(cursorScreenPos.X, cursorScreenPos.Y + 1f * uiScale), new Vector2(cursorScreenPos.X + 3f * uiScale, cursorScreenPos.Y + textLineHeight - 1f * uiScale), Theme.U32(Theme.BrandGold), 1.5f * uiScale);
		Theme.DrawText(pos: new Vector2(cursorScreenPos.X + 9f * uiScale, cursorScreenPos.Y), dl: windowDrawList, color: Theme.TextPrimary, text: title, fontSize: Theme.FsSubtitle);
		Vector2 vector = ImGui.CalcTextSize(title);
		ImGui.SetCursorScreenPos(new Vector2(cursorScreenPos.X + 9f * uiScale + vector.X + 8f * uiScale, cursorScreenPos.Y));
		ImGui.Dummy(new Vector2(1f, textLineHeight));
		if (!string.IsNullOrEmpty(hint))
		{
			Vector2 vector2 = ImGui.CalcTextSize(hint);
			float x = Theme.ContentRightX() - vector2.X;
			Theme.DrawText(windowDrawList, new Vector2(x, cursorScreenPos.Y + (textLineHeight - vector2.Y) * 0.5f), Theme.HudSecondaryText, hint, Theme.FsCaption);
		}
		ImGui.Dummy(new Vector2(1f, 8f * uiScale));
	}

	public static void Divider()
	{
		_ = Theme.UiScale;
		float textLineHeight = ImGui.GetTextLineHeight();
		Vector2 cursorScreenPos = ImGui.GetCursorScreenPos();
		ImDrawListPtr windowDrawList = ImGui.GetWindowDrawList();
		float y = cursorScreenPos.Y + textLineHeight * 0.5f;
		windowDrawList.AddLine(new Vector2(cursorScreenPos.X, y), new Vector2(cursorScreenPos.X + ImGui.GetContentRegionAvail().X, y), Theme.U32(Theme.BorderLine), 1f);
		ImGui.Dummy(new Vector2(1f, textLineHeight));
	}

	public static void Gap(float height = 16f)
	{
		ImGui.Dummy(new Vector2(1f, height * Theme.UiScale));
	}
}
