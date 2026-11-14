using System.Collections.Generic;
using Dalamud.Bindings.ImGui;

namespace AutomaticHunting.UI.Components;

public static class UiCombo
{
	public static bool Draw(string arg0, string arg1, IReadOnlyList<string> arg2, out string arg3)
	{
		arg3 = arg1;
		ImGui.SetNextItemWidth(-1f);
		ImGui.PushStyleColor(ImGuiCol.PopupBg, Theme.SurfacePopup);
		ImGui.PushStyleColor(ImGuiCol.FrameBg, Theme.InputBg);
		ImGui.PushStyleColor(ImGuiCol.FrameBgHovered, Theme.InputBgHover);
		ImGui.PushStyleColor(ImGuiCol.FrameBgActive, Theme.InputBgActive);
		ImGui.PushStyleColor(ImGuiCol.Border, Theme.BorderControl);
		ImGui.PushStyleColor(ImGuiCol.TextSelectedBg, Theme.StateSelected);
		if (!ImGui.BeginCombo(arg0, arg1))
		{
			ImGui.PopStyleColor(6);
			return false;
		}
		bool result = false;
		foreach (string item in arg2)
		{
			bool flag = item == arg1;
			if (ImGui.Selectable(item, flag))
			{
				arg3 = item;
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
