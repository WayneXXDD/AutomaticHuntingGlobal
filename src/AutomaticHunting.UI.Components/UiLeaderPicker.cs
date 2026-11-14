using System;
using System.Collections.Generic;
using System.Numerics;
using Dalamud.Bindings.ImGui;

namespace AutomaticHunting.UI.Components;

public sealed class UiLeaderPicker
{
	public const int MaxVisibleRows = 10;

	private const float field_1153 = 18f;

	private const float field_1154 = 24f;

	private const float field_1155 = 30f;

	private const float field_1156 = 8f;

	private const float field_1157 = 8f;

	private const float field_1158 = 6f;

	private const float field_1159 = 4f;

	private const float field_1160 = 6f;

	public string Draft = string.Empty;

	public int MaxLength = 128;

	public Action<LeaderRowGeometry>? RowProbe;

	private bool field_1164;

	private Vector2 field_1165;

	private Vector2 field_1166;

	private string field_1167 = "##leaderPickerPopup";

	private int field_1168 = -1;

	private DateTime field_1169;

	public bool Draw(string arg0, PluginConfiguration arg1, Action? arg2 = null)
	{
		float uiScale = Theme.UiScale;
		bool flag = false;
		field_1167 = arg0 + "popup";
		float x = ImGui.GetContentRegionAvail().X;
		float num = 30f * uiScale;
		field_1165 = ImGui.GetCursorScreenPos();
		ImGui.SetNextItemWidth(Math.Max(60f, x - num - 6f * uiScale));
		ImGui.PushStyleColor(ImGuiCol.FrameBg, Theme.InputBg);
		ImGui.PushStyleColor(ImGuiCol.FrameBgHovered, Theme.InputBgHover);
		ImGui.PushStyleColor(ImGuiCol.FrameBgActive, Theme.InputBgActive);
		ImGui.PushStyleColor(ImGuiCol.Border, Theme.BorderControl);
		ImGui.PushStyleColor(ImGuiCol.Text, Theme.TextPrimary);
		ImU8String label = new ImU8String(5, 1);
		label.AppendFormatted(arg0);
		label.AppendLiteral("input");
		bool flag2 = ImGui.InputText(label, ref Draft, MaxLength, ImGuiInputTextFlags.EnterReturnsTrue);
		ImGui.PopStyleColor(5);
		field_1166 = ImGui.GetItemRectMax();
		if (ImGui.IsItemHovered())
		{
			UiButton.ShowTooltip("輸入車頭名字後按 回車 儲存到下面的列表");
		}
		if (flag2)
		{
			string text = LeaderRoster.Normalize(Draft);
			if (text.Length > 0)
			{
				LeaderRoster.Add(arg1, text);
				Draft = string.Empty;
				flag = true;
			}
		}
		Vector2 vector = new Vector2(field_1166.X + 6f * uiScale, field_1165.Y);
		Vector2 vector2 = new Vector2(vector.X + num, field_1166.Y);
		ImGui.SetCursorScreenPos(vector);
		ImU8String strId = new ImU8String(5, 1);
		strId.AppendFormatted(arg0);
		strId.AppendLiteral("arrow");
		bool flag3 = ImGui.InvisibleButton(strId, vector2 - vector);
		bool flag4 = ImGui.IsItemHovered();
		ImDrawListPtr windowDrawList = ImGui.GetWindowDrawList();
		windowDrawList.AddRectFilled(col: Theme.U32(flag4 ? Theme.BtnNeutralHover : Theme.BtnNeutral), pMin: vector, pMax: vector2, rounding: 4f * uiScale);
		windowDrawList.AddRect(vector, vector2, Theme.U32(flag4 ? Theme.BrandGold : Theme.BorderControl), 4f * uiScale, 1f);
		UiIcons.DrawChevronDown(windowDrawList, (vector + vector2) * 0.5f, 5f * uiScale, Theme.U32(flag4 ? Theme.BrandGold : Theme.TextSecondary));
		if (flag3)
		{
			field_1164 = true;
		}
		if (flag4)
		{
			var (value, num2) = LeaderRoster.Counts(arg1);
			UiButton.ShowTooltip((num2 == 0) ? "還沒有儲存的車頭；輸入名字按回車即可新增" : $"已儲存 {num2} 個車頭，其中 {value} 個生效（點這裡展開勾選）");
		}
		if (field_1164)
		{
			ImGui.OpenPopup(field_1167);
			field_1164 = false;
		}
		ImGui.SetNextWindowPos(new Vector2(field_1165.X, field_1166.Y + 4f * uiScale), ImGuiCond.Always);
		ImGui.SetNextWindowSize(new Vector2(x, 0f), ImGuiCond.Always);
		ImGui.PushStyleColor(ImGuiCol.PopupBg, Theme.SurfacePopup);
		ImGui.PushStyleColor(ImGuiCol.Border, Theme.BorderControl);
		if (ImGui.BeginPopup(field_1167))
		{
			flag |= DrawList(arg1, 0f, arg2);
			ImGui.EndPopup();
		}
		else
		{
			field_1168 = -1;
		}
		ImGui.PopStyleColor(2);
		return flag;
	}

	public bool DrawList(PluginConfiguration arg0, float arg1, Action? arg2 = null, int arg3 = 10)
	{
		float uiScale = Theme.UiScale;
		List<LeaderEntry> leaderEntries = arg0.LeaderEntries;
		bool flag = false;
		if (leaderEntries == null || leaderEntries.Count == 0)
		{
			ImGui.TextDisabled("還沒有儲存的車頭");
			return false;
		}
		if (!(arg1 > 0f))
		{
			ImGui.GetContentRegionAvail();
		}
		float num = Math.Max(Theme.ControlHeight, 24f * uiScale);
		bool flag2 = leaderEntries.Count > arg3;
		bool flag3 = false;
		if (flag2)
		{
			float y = (float)arg3 * num + 16f * uiScale;
			flag3 = ImGui.BeginChild("##leaderListScroll", new Vector2(0f, y));
		}
		if (!flag2 | flag3)
		{
			int num2 = -1;
			int num3 = -1;
			for (int i = 0; i < leaderEntries.Count; i++)
			{
				LeaderEntry leaderEntry = leaderEntries[i];
				bool enabled = leaderEntry.Enabled;
				Vector2 cursorScreenPos = ImGui.GetCursorScreenPos();
				cursorScreenPos.X = ImGui.GetCursorScreenPos().X;
				float num4 = ImGui.GetWindowPos().X + ImGui.GetContentRegionMax().X;
				Vector2 vector = new Vector2(cursorScreenPos.X, cursorScreenPos.Y);
				Vector2 vector2 = new Vector2(vector.X + 24f * uiScale, vector.Y + num);
				ImGui.SetCursorScreenPos(vector);
				ImU8String strId = new ImU8String(11, 1);
				strId.AppendLiteral("##leaderChk");
				strId.AppendFormatted(i);
				if (ImGui.InvisibleButton(strId, vector2 - vector))
				{
					num2 = i;
				}
				bool flag4 = Theme.IsHoveredInWindow(vector, vector2);
				Method_1172(ImGui.GetWindowDrawList(), vector, vector2, enabled, flag4, uiScale);
				if (flag4)
				{
					UiButton.ShowTooltip(enabled ? "取消勾選（不再跟這個車頭）" : "勾選（跟這個車頭）");
				}
				float num5 = vector2.X + 8f * uiScale;
				Vector2 vector3 = new Vector2(num4 - 30f * uiScale, cursorScreenPos.Y);
				string text = Theme.Ellipsis(arg1: Math.Max(0f, vector3.X - 6f * uiScale - num5), arg0: LeaderRoster.Normalize(leaderEntry.Name));
				float num6 = cursorScreenPos.Y + (num - ImGui.GetTextLineHeight()) * 0.5f;
				Vector2 vector4 = new Vector2(num5, num6);
				Theme.DrawText(ImGui.GetWindowDrawList(), vector4, enabled ? Theme.TextPrimary : Theme.TextDisabled, text, Theme.FsBody);
				Vector2 vector5 = TraditionalText.CalcTextSize(text);
				Vector2 arg4 = new Vector2(num5 + vector5.X, num6 + vector5.Y);
				Vector2 vector6 = new Vector2(num4, cursorScreenPos.Y + num);
				ImGui.SetCursorScreenPos(vector3);
				ImU8String strId2 = new ImU8String(11, 1);
				strId2.AppendLiteral("##leaderDel");
				strId2.AppendFormatted(i);
				if (ImGui.InvisibleButton(strId2, vector6 - vector3))
				{
					num3 = i;
				}
				bool flag5 = Theme.IsHoveredInWindow(vector3, vector6);
				bool flag6 = UiDeleteStyle.IsArmed(field_1168, i, field_1169);
				UiDeleteStyle.Draw(ImGui.GetWindowDrawList(), vector3, vector6 - vector3, flag6, flag5, uiScale);
				if (flag5)
				{
					UiButton.ShowTooltip(flag6 ? "再點一次確認刪除" : "從列表刪除這個車頭（不影響其他車頭）");
				}
				RowProbe?.Invoke(new LeaderRowGeometry(i, vector, vector2, vector4, arg4, vector3, vector6, cursorScreenPos, new Vector2(num4, cursorScreenPos.Y + num), enabled));
				ImGui.SetCursorScreenPos(new Vector2(cursorScreenPos.X, cursorScreenPos.Y + num));
			}
			if (num2 >= 0)
			{
				LeaderRoster.Toggle(arg0, num2);
				flag = true;
			}
			if (num3 >= 0)
			{
				if (UiDeleteStyle.IsArmed(field_1168, num3, field_1169))
				{
					field_1168 = -1;
					LeaderRoster.RemoveAt(arg0, num3);
					flag = true;
				}
				else
				{
					field_1168 = num3;
					field_1169 = UiDeleteStyle.ArmNow();
				}
			}
			if (flag)
			{
				arg2?.Invoke();
			}
		}
		if (flag2 & flag3)
		{
			ImGui.EndChild();
		}
		return flag;
	}

	public static Vector4 CheckFillColor(bool arg0)
	{
		if (!arg0)
		{
			return new Vector4(0f, 0f, 0f, 0f);
		}
		return Theme.StatusSuccess;
	}

	private static void Method_1172(ImDrawListPtr arg0, Vector2 arg1, Vector2 arg2, bool arg3, bool arg4, float arg5)
	{
		Vector2 vector = (arg1 + arg2) * 0.5f;
		float num = 18f * arg5 * 0.5f;
		Vector2 pMin = new Vector2(vector.X - num, vector.Y - num);
		Vector2 pMax = new Vector2(vector.X + num, vector.Y + num);
		Vector4 arg6;
		if (arg3)
		{
			arg6 = CheckFillColor(arg0: true);
		}
		else
		{
			arg6 = (arg4 ? Theme.InputBgHover : CheckFillColor(arg0: false));
		}
		arg0.AddRectFilled(pMin, pMax, Theme.U32(arg6), 3f * arg5);
		Vector4 arg7;
		if (arg3)
		{
			arg7 = Theme.StatusSuccess;
		}
		else
		{
			arg7 = (arg4 ? Theme.BrandGold : Theme.BorderControl);
		}
		arg0.AddRect(pMin, pMax, Theme.U32(arg7), 3f * arg5, 1f);
	}
}
