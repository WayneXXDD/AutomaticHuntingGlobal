using System;
using System.Numerics;
using System.Runtime.CompilerServices;
using Dalamud.Bindings.ImGui;

namespace AutomaticHunting.UI.Components;

public static class UiForm
{
	[CompilerGenerated]
	private sealed class _00A0
	{
		public bool _2008_2054;

		public string _2008_2055;

		public Action<bool> _2008_2056;

		public string _2008_2057;

		internal void _00A0(int P_0)
		{
			if (P_0 == 0)
			{
				bool v = _2008_2054;
				ImU8String label = new ImU8String(2, 1);
				label.AppendLiteral("##");
				label.AppendFormatted(_2008_2055);
				if (ImGui.Checkbox(label, ref v))
				{
					_2008_2056(v);
				}
			}
			else
			{
				ImGui.TextDisabled(_2008_2057);
			}
		}
	}

	private static bool _2001_204C;

	private static bool _2001_204D;

	public static void Begin(string id, float labelWidth = 96f, float valueWidth = 0f)
	{
		_00A0(id, labelWidth, valueWidth, 0f);
	}

	public static void BeginAction(string id, float labelWidth = 96f, float valueWidth = 0f, float actionWidth = 32f)
	{
		_00A0(id, labelWidth, valueWidth, actionWidth);
	}

	public static void BeginMeasured(string id, params string[] labelSamples)
	{
		_00A0(id, _00A0(labelSamples), 0f, 0f);
	}

	public static void BeginActionMeasured(string id, float actionWidth = 32f, params string[] labelSamples)
	{
		_00A0(id, _00A0(labelSamples), 0f, actionWidth);
	}

	private static float _00A0(string[] P_0)
	{
		if (P_0 == null || P_0.Length == 0)
		{
			return 96f;
		}
		float uiScale = Theme.UiScale;
		float num = 0f;
		foreach (string text in P_0)
		{
			if (!string.IsNullOrEmpty(text))
			{
				num = Math.Max(num, ImGui.CalcTextSize(text).X);
			}
		}
		return num / Math.Max(0.01f, uiScale) + 10f;
	}

	private static void _00A0(string P_0, float P_1, float P_2, float P_3)
	{
		float uiScale = Theme.UiScale;
		ImGui.PushStyleVar(ImGuiStyleVar.CellPadding, new Vector2(8f * uiScale, 4f * uiScale));
		bool flag = P_3 > 0f;
		int column = (flag ? 3 : 2);
		ImGuiTableFlags imGuiTableFlags = ImGuiTableFlags.NoSavedSettings;
		imGuiTableFlags = (ImGuiTableFlags)((int)imGuiTableFlags | ((P_2 > 0f) ? 8192 : 24576));
		_2001_204C = ImGui.BeginTable(P_0, column, imGuiTableFlags);
		_2001_204D = flag;
		if (_2001_204C)
		{
			ImGui.TableSetupColumn("##label", ImGuiTableColumnFlags.WidthFixed, P_1 * uiScale);
			ImGui.TableSetupColumn("##value", (P_2 > 0f) ? ImGuiTableColumnFlags.WidthFixed : ImGuiTableColumnFlags.WidthStretch, (P_2 > 0f) ? (P_2 * uiScale) : 1f);
			if (flag)
			{
				ImGui.TableSetupColumn("##action", ImGuiTableColumnFlags.WidthFixed, P_3 * uiScale);
			}
		}
	}

	public static void End()
	{
		ImGui.EndTable();
		ImGui.PopStyleVar();
		_2001_204C = false;
		_2001_204D = false;
	}

	public static void Row(string label, string? tooltip = null)
	{
		if (_2001_204C)
		{
			ImGui.TableNextRow();
			ImGui.TableNextColumn();
			ImGui.AlignTextToFramePadding();
			ImGui.PushStyleColor(ImGuiCol.Text, Theme.TextSecondary);
			ImGui.TextUnformatted(label);
			ImGui.PopStyleColor();
			if (tooltip != null && ImGui.IsItemHovered())
			{
				ImGui.SetTooltip(tooltip);
			}
			ImGui.TableNextColumn();
		}
	}

	public static void ActionCell()
	{
		if (_2001_204C && _2001_204D)
		{
			ImGui.TableNextColumn();
		}
	}

	public static void Fill()
	{
		ImGui.SetNextItemWidth(-1f);
	}

	public static float Split(int count, float? gap = null)
	{
		float num = (gap ?? Theme.ItemSpacingX) * Theme.UiScale;
		return Math.Max(40f * Theme.UiScale, (ImGui.GetContentRegionAvail().X - num * (float)(count - 1)) / (float)count);
	}

	public static void ToggleWithState(string id, bool value, Action<bool> set, string stateText)
	{
		UiLayout.EqualColumns("##" + id + "row", 2, 8f, (int P_0) =>
		{
			if (P_0 == 0)
			{
				bool v = value;
				ImU8String label = new ImU8String(2, 1);
				label.AppendLiteral("##");
				label.AppendFormatted(id);
				if (ImGui.Checkbox(label, ref v))
				{
					set(v);
				}
			}
			else
			{
				ImGui.TextDisabled(stateText);
			}
		});
	}

	public static void SliderWithReset(string id, Func<float> get, Action<float> set, float min, float max, string format, float defaultValue)
	{
		float v = get();
		ImGui.SetNextItemWidth(-1f);
		if (ImGui.SliderFloat(id, ref v, min, max, format))
		{
			set(v);
		}
		ActionCell();
		if (DrawResetButton("##reset" + id, defaultValue))
		{
			set(defaultValue);
		}
	}

	public static void InputWithReset(string id, Func<string> get, Action<string> set, int maxLength, string defaultValue)
	{
		string buf = get();
		ImGui.SetNextItemWidth(-1f);
		if (ImGui.InputText(id, ref buf, maxLength))
		{
			set(buf);
		}
		ActionCell();
		if (DrawResetButton("##reset" + id, defaultValue))
		{
			set(defaultValue);
		}
	}

	public static bool DrawResetButton(string id, double defaultValue)
	{
		return _00A0(id, defaultValue.ToString("0.##"));
	}

	public static bool DrawResetButton(string id, string defaultValue)
	{
		return _00A0(id, defaultValue);
	}

	private static bool _00A0(string P_0, string P_1)
	{
		Vector2 size = new Vector2(Theme.ControlHeight, Theme.ControlHeight);
		bool result = ImGui.InvisibleButton(P_0, size);
		Vector2 itemRectMin = ImGui.GetItemRectMin();
		Vector2 itemRectMax = ImGui.GetItemRectMax();
		bool flag = ImGui.IsItemHovered();
		UiIcons.DrawReset(ImGui.GetWindowDrawList(), color: flag ? Theme.U32(Theme.BrandGold) : Theme.U32(Theme.TextDisabled), center: (itemRectMin + itemRectMax) * 0.5f, radius: size.X * 0.28f);
		if (flag)
		{
			ImGui.SetTooltip(string.IsNullOrWhiteSpace(P_1) ? "恢复默认值" : ("恢复默认值（" + P_1 + "）"));
		}
		return result;
	}
}
