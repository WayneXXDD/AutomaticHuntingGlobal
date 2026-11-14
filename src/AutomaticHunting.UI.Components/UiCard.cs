using System.Numerics;
using Dalamud.Bindings.ImGui;

namespace AutomaticHunting.UI.Components;

public static class UiCard
{
	public const float PadX = 10f;

	public const float PadY = 8f;

	private static Vector2 field_1038;

	public static void Begin(string arg0, float arg1)
	{
		float uiScale = Theme.UiScale;
		field_1038 = ImGui.GetCursorScreenPos();
		float x = ImGui.GetContentRegionAvail().X;
		UiPanel.DrawCard(field_1038, field_1038 + new Vector2(x, arg1 * uiScale), 6f * uiScale);
		ImGui.PushID(arg0);
		ImGui.SetCursorScreenPos(field_1038 + new Vector2(10f * uiScale, 8f * uiScale));
	}

	public static void End(float arg0)
	{
		float uiScale = Theme.UiScale;
		ImGui.PopID();
		ImGui.SetCursorScreenPos(field_1038 + new Vector2(0f, arg0 * uiScale));
	}

	public static float ContentWidth()
	{
		return ImGui.GetContentRegionAvail().X - 20f * Theme.UiScale;
	}

	public static void Title(string arg0)
	{
		ImDrawListPtr windowDrawList = ImGui.GetWindowDrawList();
		Vector2 cursorScreenPos = ImGui.GetCursorScreenPos();
		Theme.DrawText(windowDrawList, cursorScreenPos, Theme.TextPrimary, arg0, Theme.FsSubtitle);
		ImGui.Dummy(new Vector2(1f, ImGui.GetTextLineHeight()));
	}

	public static void Line(string arg0)
	{
		Theme.DrawText(ImGui.GetWindowDrawList(), ImGui.GetCursorScreenPos(), arg3: Theme.Ellipsis(arg0, ImGui.GetContentRegionAvail().X, Theme.FsCaption), arg2: Theme.TextSecondary, arg4: Theme.FsCaption);
		ImGui.Dummy(new Vector2(1f, ImGui.GetTextLineHeight()));
	}
}
