using System.Numerics;
using Dalamud.Bindings.ImGui;

namespace AutomaticHunting.UI.Components;

public static class UiCard
{
	public const float PadX = 10f;

	public const float PadY = 8f;

	private static Vector2 _2001_2006;

	public static void Begin(string id, float height)
	{
		float uiScale = Theme.UiScale;
		_2001_2006 = ImGui.GetCursorScreenPos();
		float x = ImGui.GetContentRegionAvail().X;
		UiPanel.DrawCard(_2001_2006, _2001_2006 + new Vector2(x, height * uiScale), 6f * uiScale);
		ImGui.PushID(id);
		ImGui.SetCursorScreenPos(_2001_2006 + new Vector2(10f * uiScale, 8f * uiScale));
	}

	public static void End(float height)
	{
		float uiScale = Theme.UiScale;
		ImGui.PopID();
		ImGui.SetCursorScreenPos(_2001_2006 + new Vector2(0f, height * uiScale));
	}

	public static float ContentWidth()
	{
		return ImGui.GetContentRegionAvail().X - 20f * Theme.UiScale;
	}

	public static void Title(string text)
	{
		ImDrawListPtr windowDrawList = ImGui.GetWindowDrawList();
		Vector2 cursorScreenPos = ImGui.GetCursorScreenPos();
		Theme.DrawText(windowDrawList, cursorScreenPos, Theme.TextPrimary, text, Theme.FsSubtitle);
		ImGui.Dummy(new Vector2(1f, ImGui.GetTextLineHeight()));
	}

	public static void Line(string text)
	{
		Theme.DrawText(ImGui.GetWindowDrawList(), ImGui.GetCursorScreenPos(), text: Theme.Ellipsis(text, ImGui.GetContentRegionAvail().X, Theme.FsCaption), color: Theme.TextSecondary, fontSize: Theme.FsCaption);
		ImGui.Dummy(new Vector2(1f, ImGui.GetTextLineHeight()));
	}
}
