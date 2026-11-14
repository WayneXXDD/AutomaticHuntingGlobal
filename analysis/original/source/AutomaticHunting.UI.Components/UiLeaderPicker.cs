using System;
using System.Collections.Generic;
using System.Numerics;
using Dalamud.Bindings.ImGui;

namespace AutomaticHunting.UI.Components;

public sealed class UiLeaderPicker
{
	public const int MaxVisibleRows = 10;

	private const float _2001_204F = 18f;

	private const float _2001_2050 = 24f;

	private const float _2001_2051 = 30f;

	private const float _2001_2052 = 8f;

	private const float _2001_2053 = 8f;

	private const float _2001_2054 = 6f;

	private const float _2001_2055 = 4f;

	private const float _2001_2056 = 6f;

	public string Draft = string.Empty;

	public int MaxLength = 128;

	public Action<LeaderRowGeometry>? RowProbe;

	private bool _2001_2057;

	private Vector2 _2001_2058;

	private Vector2 _2001_2059;

	private string _2001_205A = "##leaderPickerPopup";

	private int _2001_205B = -1;

	private DateTime _2001_205C;

	public bool Draw(string id, PluginConfiguration config, Action? onChanged = null)
	{
		float uiScale = Theme.UiScale;
		bool flag = false;
		_2001_205A = id + "popup";
		float x = ImGui.GetContentRegionAvail().X;
		float num = 30f * uiScale;
		_2001_2058 = ImGui.GetCursorScreenPos();
		ImGui.SetNextItemWidth(Math.Max(60f, x - num - 6f * uiScale));
		ImGui.PushStyleColor(ImGuiCol.FrameBg, Theme.InputBg);
		ImGui.PushStyleColor(ImGuiCol.FrameBgHovered, Theme.InputBgHover);
		ImGui.PushStyleColor(ImGuiCol.FrameBgActive, Theme.InputBgActive);
		ImGui.PushStyleColor(ImGuiCol.Border, Theme.BorderControl);
		ImGui.PushStyleColor(ImGuiCol.Text, Theme.TextPrimary);
		ImU8String label = new ImU8String(5, 1);
		label.AppendFormatted(id);
		label.AppendLiteral("input");
		bool flag2 = ImGui.InputText(label, ref Draft, MaxLength, ImGuiInputTextFlags.EnterReturnsTrue);
		ImGui.PopStyleColor(5);
		_2001_2059 = ImGui.GetItemRectMax();
		if (ImGui.IsItemHovered())
		{
			UiButton.ShowTooltip("输入车头名字后按 回车 保存到下面的列表");
		}
		if (flag2)
		{
			string text = LeaderRoster.Normalize(Draft);
			if (text.Length > 0)
			{
				LeaderRoster.Add(config, text);
				Draft = string.Empty;
				flag = true;
			}
		}
		Vector2 vector = new Vector2(_2001_2059.X + 6f * uiScale, _2001_2058.Y);
		Vector2 vector2 = new Vector2(vector.X + num, _2001_2059.Y);
		ImGui.SetCursorScreenPos(vector);
		ImU8String strId = new ImU8String(5, 1);
		strId.AppendFormatted(id);
		strId.AppendLiteral("arrow");
		bool flag3 = ImGui.InvisibleButton(strId, vector2 - vector);
		bool flag4 = ImGui.IsItemHovered();
		ImDrawListPtr windowDrawList = ImGui.GetWindowDrawList();
		windowDrawList.AddRectFilled(col: Theme.U32(flag4 ? Theme.BtnNeutralHover : Theme.BtnNeutral), pMin: vector, pMax: vector2, rounding: 4f * uiScale);
		windowDrawList.AddRect(vector, vector2, Theme.U32(flag4 ? Theme.BrandGold : Theme.BorderControl), 4f * uiScale, 1f);
		UiIcons.DrawChevronDown(windowDrawList, (vector + vector2) * 0.5f, 5f * uiScale, Theme.U32(flag4 ? Theme.BrandGold : Theme.TextSecondary));
		if (flag3)
		{
			_2001_2057 = true;
		}
		if (flag4)
		{
			var (value, num2) = LeaderRoster.Counts(config);
			UiButton.ShowTooltip((num2 == 0) ? "还没有保存的车头；输入名字按回车即可添加" : $"已保存 {num2} 个车头，其中 {value} 个生效（点这里展开勾选）");
		}
		if (_2001_2057)
		{
			ImGui.OpenPopup(_2001_205A);
			_2001_2057 = false;
		}
		ImGui.SetNextWindowPos(new Vector2(_2001_2058.X, _2001_2059.Y + 4f * uiScale), ImGuiCond.Always);
		ImGui.SetNextWindowSize(new Vector2(x, 0f), ImGuiCond.Always);
		ImGui.PushStyleColor(ImGuiCol.PopupBg, Theme.SurfacePopup);
		ImGui.PushStyleColor(ImGuiCol.Border, Theme.BorderControl);
		if (ImGui.BeginPopup(_2001_205A))
		{
			flag |= DrawList(config, 0f, onChanged);
			ImGui.EndPopup();
		}
		else
		{
			_2001_205B = -1;
		}
		ImGui.PopStyleColor(2);
		return flag;
	}

	public bool DrawList(PluginConfiguration config, float width, Action? onChanged = null, int maxVisibleRows = 10)
	{
		float uiScale = Theme.UiScale;
		List<LeaderEntry> leaderEntries = config.LeaderEntries;
		bool flag = false;
		if (leaderEntries == null || leaderEntries.Count == 0)
		{
			ImGui.TextDisabled("还没有保存的车头");
			return false;
		}
		if (!(width > 0f))
		{
			ImGui.GetContentRegionAvail();
		}
		float num = Math.Max(Theme.ControlHeight, 24f * uiScale);
		bool flag2 = leaderEntries.Count > maxVisibleRows;
		bool flag3 = false;
		if (flag2)
		{
			float y = (float)maxVisibleRows * num + 16f * uiScale;
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
				_00A0(ImGui.GetWindowDrawList(), vector, vector2, enabled, flag4, uiScale);
				if (flag4)
				{
					UiButton.ShowTooltip(enabled ? "取消勾选（不再跟这个车头）" : "勾选（跟这个车头）");
				}
				float num5 = vector2.X + 8f * uiScale;
				Vector2 vector3 = new Vector2(num4 - 30f * uiScale, cursorScreenPos.Y);
				string text = Theme.Ellipsis(maxWidth: Math.Max(0f, vector3.X - 6f * uiScale - num5), text: LeaderRoster.Normalize(leaderEntry.Name));
				float num6 = cursorScreenPos.Y + (num - ImGui.GetTextLineHeight()) * 0.5f;
				Vector2 vector4 = new Vector2(num5, num6);
				Theme.DrawText(ImGui.GetWindowDrawList(), vector4, enabled ? Theme.TextPrimary : Theme.TextDisabled, text, Theme.FsBody);
				Vector2 vector5 = ImGui.CalcTextSize(text);
				Vector2 nameMax = new Vector2(num5 + vector5.X, num6 + vector5.Y);
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
				bool flag6 = UiDeleteStyle.IsArmed(_2001_205B, i, _2001_205C);
				UiDeleteStyle.Draw(ImGui.GetWindowDrawList(), vector3, vector6 - vector3, flag6, flag5, uiScale);
				if (flag5)
				{
					UiButton.ShowTooltip(flag6 ? "再点一次确认删除" : "从列表删除这个车头（不影响其他车头）");
				}
				RowProbe?.Invoke(new LeaderRowGeometry(i, vector, vector2, vector4, nameMax, vector3, vector6, cursorScreenPos, new Vector2(num4, cursorScreenPos.Y + num), enabled));
				ImGui.SetCursorScreenPos(new Vector2(cursorScreenPos.X, cursorScreenPos.Y + num));
			}
			if (num2 >= 0)
			{
				LeaderRoster.Toggle(config, num2);
				flag = true;
			}
			if (num3 >= 0)
			{
				if (UiDeleteStyle.IsArmed(_2001_205B, num3, _2001_205C))
				{
					_2001_205B = -1;
					LeaderRoster.RemoveAt(config, num3);
					flag = true;
				}
				else
				{
					_2001_205B = num3;
					_2001_205C = UiDeleteStyle.ArmNow();
				}
			}
			if (flag)
			{
				onChanged?.Invoke();
			}
		}
		if (flag2 & flag3)
		{
			ImGui.EndChild();
		}
		return flag;
	}

	public static Vector4 CheckFillColor(bool on)
	{
		if (!on)
		{
			return new Vector4(0f, 0f, 0f, 0f);
		}
		return Theme.StatusSuccess;
	}

	private static void _00A0(ImDrawListPtr P_0, Vector2 P_1, Vector2 P_2, bool P_3, bool P_4, float P_5)
	{
		Vector2 vector = (P_1 + P_2) * 0.5f;
		float num = 18f * P_5 * 0.5f;
		Vector2 pMin = new Vector2(vector.X - num, vector.Y - num);
		Vector2 pMax = new Vector2(vector.X + num, vector.Y + num);
		Vector4 c;
		if (P_3)
		{
			c = CheckFillColor(on: true);
		}
		else
		{
			c = (P_4 ? Theme.InputBgHover : CheckFillColor(on: false));
		}
		P_0.AddRectFilled(pMin, pMax, Theme.U32(c), 3f * P_5);
		Vector4 c2;
		if (P_3)
		{
			c2 = Theme.StatusSuccess;
		}
		else
		{
			c2 = (P_4 ? Theme.BrandGold : Theme.BorderControl);
		}
		P_0.AddRect(pMin, pMax, Theme.U32(c2), 3f * P_5, 1f);
	}
}
