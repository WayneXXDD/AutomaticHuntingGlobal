using System;
using System.Numerics;
using Dalamud.Bindings.ImGui;

namespace AutomaticHunting.UI.Components;

public static class UiEmpty
{
	public static void Show(string arg0, string? arg1 = null)
	{
		Show("◇", arg0, arg1);
	}

	public static void Show(string arg0, string arg1, string? arg2 = null)
	{
		float uiScale = Theme.UiScale;
		Vector2 contentRegionAvail = ImGui.GetContentRegionAvail();
		ImDrawListPtr windowDrawList = ImGui.GetWindowDrawList();
		ImGui.Dummy(new Vector2(1f, 16f * uiScale));
		float num = 26f * uiScale;
		float num2 = ImGui.GetCursorScreenPos().X + (contentRegionAvail.X - num) * 0.5f;
		float y = ImGui.GetCursorScreenPos().Y;
		Vector2 vector = new Vector2(num2 + num * 0.5f, y + num * 0.5f);
		float num3 = num * 0.42f;
		windowDrawList.AddLine(new Vector2(vector.X, vector.Y - num3), new Vector2(vector.X + num3, vector.Y), Theme.U32(Theme.WithAlpha(Theme.TextDisabled, 0.45f)), 1.5f * uiScale);
		windowDrawList.AddLine(new Vector2(vector.X + num3, vector.Y), new Vector2(vector.X, vector.Y + num3), Theme.U32(Theme.WithAlpha(Theme.TextDisabled, 0.45f)), 1.5f * uiScale);
		windowDrawList.AddLine(new Vector2(vector.X, vector.Y + num3), new Vector2(vector.X - num3, vector.Y), Theme.U32(Theme.WithAlpha(Theme.TextDisabled, 0.45f)), 1.5f * uiScale);
		windowDrawList.AddLine(new Vector2(vector.X - num3, vector.Y), new Vector2(vector.X, vector.Y - num3), Theme.U32(Theme.WithAlpha(Theme.TextDisabled, 0.45f)), 1.5f * uiScale);
		ImGui.Dummy(new Vector2(1f, num));
		Vector2 vector2 = TraditionalText.CalcTextSize(arg1);
		ImGui.SetCursorPosX(ImGui.GetCursorPosX() + Math.Max(0f, (contentRegionAvail.X - vector2.X) * 0.5f));
		ImGui.PushStyleColor(ImGuiCol.Text, Theme.TextSecondary);
		TraditionalText.TextUnformatted(arg1);
		ImGui.PopStyleColor();
		if (!string.IsNullOrEmpty(arg2))
		{
			Vector2 vector3 = TraditionalText.CalcTextSize(arg2);
			ImGui.SetCursorPosX(ImGui.GetCursorPosX() + Math.Max(0f, (contentRegionAvail.X - vector3.X) * 0.5f));
			ImGui.PushStyleColor(ImGuiCol.Text, Theme.TextDisabled);
			TraditionalText.TextUnformatted(arg2);
			ImGui.PopStyleColor();
		}
		ImGui.Dummy(new Vector2(1f, 16f * uiScale));
	}

	public static void ShowLoading(string arg0 = "載入中…")
	{
		float uiScale = Theme.UiScale;
		Vector2 contentRegionAvail = ImGui.GetContentRegionAvail();
		ImDrawListPtr windowDrawList = ImGui.GetWindowDrawList();
		Vector2 cursorScreenPos = ImGui.GetCursorScreenPos();
		float num = 4f * uiScale;
		float num2 = 6f * uiScale;
		float num3 = num * 3f + num2 * 2f;
		float num4 = cursorScreenPos.X + (contentRegionAvail.X - num3) * 0.5f;
		float y = cursorScreenPos.Y + ImGui.GetTextLineHeight() * 0.5f;
		float num5 = (float)ImGui.GetTime();
		for (int i = 0; i < 3; i++)
		{
			float num6 = (num5 / 1.2f - (float)i * 0.3333f) % 1f;
			if (num6 < 0f)
			{
				num6++;
			}
			float num7 = MathF.Sin(num6 * (float)Math.PI);
			float arg1 = 0.25f + 0.75f * num7;
			float num8 = num4 + (float)i * (num + num2);
			windowDrawList.AddCircleFilled(new Vector2(num8 + num * 0.5f, y), num * 0.5f, Theme.U32(Theme.WithAlpha(Theme.TextDisabled, arg1)));
		}
		Vector2 vector = TraditionalText.CalcTextSize(arg0);
		ImGui.SetCursorPosX(ImGui.GetCursorPosX() + Math.Max(0f, (contentRegionAvail.X - vector.X) * 0.5f));
		ImGui.PushStyleColor(ImGuiCol.Text, Theme.TextDisabled);
		TraditionalText.TextUnformatted(arg0);
		ImGui.PopStyleColor();
	}
}
