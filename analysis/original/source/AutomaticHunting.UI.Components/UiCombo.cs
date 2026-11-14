using System.Collections.Generic;
using Dalamud.Bindings.ImGui;

namespace AutomaticHunting.UI.Components;

public static class UiCombo
{
	public static bool Draw(string label, string preview, IReadOnlyList<string> items, out string selected)
	{
		selected = preview;
		ImGui.SetNextItemWidth(-1f);
		ImGui.PushStyleColor(ImGuiCol.PopupBg, Theme.SurfacePopup);
		ImGui.PushStyleColor(ImGuiCol.FrameBg, Theme.InputBg);
		ImGui.PushStyleColor(ImGuiCol.FrameBgHovered, Theme.InputBgHover);
		ImGui.PushStyleColor(ImGuiCol.FrameBgActive, Theme.InputBgActive);
		ImGui.PushStyleColor(ImGuiCol.Border, Theme.BorderControl);
		ImGui.PushStyleColor(ImGuiCol.TextSelectedBg, Theme.StateSelected);
		if (!ImGui.BeginCombo(label, preview))
		{
			ImGui.PopStyleColor(6);
			return false;
		}
		bool result = false;
		foreach (string item in items)
		{
			bool flag = item == preview;
			if (ImGui.Selectable(item, flag))
			{
				selected = item;
				result = true;
			}
			if (flag)
			{
				ImGui.SetItemDefaultFocus();
			}
		}
		ImGui.EndCombo();
		ImGui.PopStyleColor(6);
		return result;
	}
}
