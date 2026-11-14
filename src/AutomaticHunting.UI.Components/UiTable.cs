using System.Numerics;
using Dalamud.Bindings.ImGui;

namespace AutomaticHunting.UI.Components;

public static class UiTable
{
	public const float DragColumnWidth = 32f;

	public static void DragHandle(string arg0, string arg1)
	{
		float uiScale = Theme.UiScale;
		ImGui.PushStyleVar(ImGuiStyleVar.FrameBorderSize, 1f);
		ImGui.PushStyleVar(ImGuiStyleVar.FrameRounding, 6f * uiScale);
		ImGui.PushStyleColor(ImGuiCol.Button, Theme.SurfaceTableHeader);
		ImGui.PushStyleColor(ImGuiCol.ButtonHovered, Theme.SurfaceCardHover);
		ImGui.PushStyleColor(ImGuiCol.ButtonActive, Theme.Shade(Theme.SurfaceCardHover, 0.85f));
		ImGui.PushStyleColor(ImGuiCol.Border, Theme.BorderCard);
		ImGui.Button(arg0, new Vector2(ImGui.GetContentRegionAvail().X, 0f));
		Method_1191();
		ImGui.PopStyleColor(4);
		ImGui.PopStyleVar(2);
		if (ImGui.IsItemHovered())
		{
			ImGui.SetTooltip(arg1);
		}
	}

	private static void Method_1191()
	{
		float uiScale = Theme.UiScale;
		Vector2 itemRectMin = ImGui.GetItemRectMin();
		Vector2 itemRectMax = ImGui.GetItemRectMax();
		ImDrawListPtr windowDrawList = ImGui.GetWindowDrawList();
		uint col = (ImGui.IsItemHovered() ? Theme.U32(Theme.BrandGold) : Theme.U32(Theme.TextDisabled));
		bool flag = ImGui.IsItemActive();
		float num = (itemRectMin.X + itemRectMax.X) * 0.5f;
		float num2 = (itemRectMin.Y + itemRectMax.Y) * 0.5f + (flag ? (1f * uiScale) : 0f);
		float num3 = (flag ? (1f * uiScale) : 0f);
		float num4 = 2.2f * uiScale;
		for (int i = 0; i < 3; i++)
		{
			float num5 = ((i == 1) ? 7f : 5.5f) * uiScale - num3;
			float y = num2 + (float)(i - 1) * 4f * uiScale;
			Vector2 vector = new Vector2(num - num5, y);
			Vector2 vector2 = new Vector2(num + num5, y);
			windowDrawList.AddLine(vector, vector2, col, num4);
			windowDrawList.AddCircleFilled(vector, num4 * 0.5f, col);
			windowDrawList.AddCircleFilled(vector2, num4 * 0.5f, col);
		}
	}

	public static void PresetLabel(WorldTravelPreset arg0)
	{
		float uiScale = Theme.UiScale;
		ImGui.PushStyleColor(ImGuiCol.Button, new Vector4(0f, 0f, 0f, 0f));
		ImGui.PushStyleColor(ImGuiCol.ButtonHovered, Theme.StateHover);
		ImGui.Button("##presetLabel", new Vector2(ImGui.GetContentRegionAvail().X, 0f));
		ImGui.PopStyleColor(2);
		Vector2 itemRectMin = ImGui.GetItemRectMin();
		Vector2 itemRectMax = ImGui.GetItemRectMax();
		ImDrawListPtr windowDrawList = ImGui.GetWindowDrawList();
		float fontSize = ImGui.GetFontSize();
		float y = itemRectMin.Y + (itemRectMax.Y - itemRectMin.Y - fontSize) * 0.5f;
		float num = itemRectMin.X + 8f * uiScale;
		uint col = Theme.U32(Theme.StatusInfo);
		uint col2 = Theme.U32(Theme.TextPrimary);
		uint col3 = Theme.U32(Theme.TextSecondary);
		uint col4 = Theme.U32(Theme.TextSecondary);
		windowDrawList.PushClipRect(itemRectMin + Vector2.One, itemRectMax - Vector2.One, intersectWithCurrentClipRect: true);
		Vector2 vector = ImGui.CalcTextSize(arg0.ServerName);
		float x = ImGui.CalcTextSize("服务器名称").X;
		windowDrawList.AddText(new Vector2(num + (x - vector.X) * 0.5f, y), col, arg0.ServerName);
		num += x + 7f * uiScale;
		string text = $"{arg0.ExVersion + 2}.0";
		Vector2 vector2 = ImGui.CalcTextSize(text);
		float num2 = fontSize + 2f * uiScale;
		float x2 = vector2.X + 8f * uiScale;
		Vector2 vector3 = new Vector2(num, itemRectMin.Y + (itemRectMax.Y - itemRectMin.Y - num2) * 0.5f);
		Vector2 pMax = vector3 + new Vector2(x2, num2);
		windowDrawList.AddRectFilled(vector3, pMax, Theme.U32(Theme.SurfaceTableHeader), 3f * uiScale);
		windowDrawList.AddRect(vector3, pMax, Theme.U32(Theme.BorderCard), 3f * uiScale, 1f);
		float y2 = vector3.Y + (num2 - vector2.Y) * 0.5f;
		windowDrawList.AddText(new Vector2(vector3.X + 4f * uiScale, y2), Theme.U32(Theme.TextSecondary), text);
		num = pMax.X + 7f * uiScale;
		windowDrawList.AddText(new Vector2(num, y), col2, arg0.MapName);
		num += ImGui.CalcTextSize(arg0.MapName).X + 6f * uiScale;
		windowDrawList.AddText(new Vector2(num, y), col3, "·");
		num += ImGui.CalcTextSize("·").X + 6f * uiScale;
		windowDrawList.AddText(new Vector2(num, y), col4, arg0.AetheryteName);
		windowDrawList.PopClipRect();
	}

	public static void RowHighlight(float arg0 = 32f)
	{
		float uiScale = Theme.UiScale;
		float y = ImGui.GetCursorScreenPos().Y;
		float x = ImGui.GetCursorScreenPos().X;
		float x2 = x + ImGui.GetContentRegionAvail().X;
		float num = arg0 * uiScale;
		if (ImGui.IsMouseHoveringRect(new Vector2(x, y), new Vector2(x2, y + num), clip: false))
		{
			ImGui.GetWindowDrawList().AddRectFilled(new Vector2(x, y), new Vector2(x2, y + num), Theme.U32(Theme.StateHover), 0f);
		}
	}

	public static void SelectedMarker(float arg0 = 32f)
	{
		float uiScale = Theme.UiScale;
		float y = ImGui.GetCursorScreenPos().Y;
		float x = ImGui.GetCursorScreenPos().X;
		float num = arg0 * uiScale;
		ImGui.GetWindowDrawList().AddRectFilled(new Vector2(x, y + 4f * uiScale), new Vector2(x + 2f * uiScale, y + num - 4f * uiScale), Theme.U32(Theme.BrandGold), 1f);
	}

	public static void PushStyle()
	{
		float uiScale = Theme.UiScale;
		ImGui.PushStyleVar(ImGuiStyleVar.CellPadding, new Vector2(8f * uiScale, 4f * uiScale));
		ImGui.PushStyleColor(ImGuiCol.TableRowBg, new Vector4(0f, 0f, 0f, 0f));
		ImGui.PushStyleColor(ImGuiCol.TableRowBgAlt, new Vector4(1f, 1f, 1f, 0.022f));
		ImGui.PushStyleColor(ImGuiCol.TableBorderLight, Theme.WithAlpha(Theme.BorderCard, 0.8f));
		ImGui.PushStyleColor(ImGuiCol.TableBorderStrong, Theme.BorderLineStrong);
		ImGui.PushStyleColor(ImGuiCol.TableHeaderBg, Theme.SurfaceTableHeader);
	}

	public static void PopStyle()
	{
		ImGui.PopStyleColor(5);
		ImGui.PopStyleVar();
	}
}
