using System;
using System.Numerics;
using System.Runtime.CompilerServices;
using Dalamud.Bindings.ImGui;

namespace AutomaticHunting.UI.Components;

public static class UiForm
{
	[CompilerGenerated]
	public sealed class RecoveredType_185
	{
		public bool field_1126;

		public string field_1127;

		public Action<bool> field_1128;

		public string field_1129;

		internal void Method_1142(int arg0)
		{
			if (arg0 == 0)
			{
				bool v = field_1126;
				ImU8String label = new ImU8String(2, 1);
				label.AppendLiteral("##");
				label.AppendFormatted(field_1127);
				if (ImGui.Checkbox(label, ref v))
				{
					field_1128(v);
				}
			}
			else
			{
				ImGui.TextDisabled(field_1129);
			}
		}
	}

	private static bool field_1124;

	private static bool field_1125;

	public static void Begin(string arg0, float arg1 = 96f, float arg2 = 0f)
	{
		Method_1129(arg0, arg1, arg2, 0f);
	}

	public static void BeginAction(string arg0, float arg1 = 96f, float arg2 = 0f, float arg3 = 32f)
	{
		Method_1129(arg0, arg1, arg2, arg3);
	}

	public static void BeginMeasured(string arg0, params string[] arg1)
	{
		Method_1129(arg0, Method_1128(arg1), 0f, 0f);
	}

	public static void BeginActionMeasured(string arg0, float arg1 = 32f, params string[] arg2)
	{
		Method_1129(arg0, Method_1128(arg2), 0f, arg1);
	}

	private static float Method_1128(string[] arg0)
	{
		if (arg0 == null || arg0.Length == 0)
		{
			return 96f;
		}
		float uiScale = Theme.UiScale;
		float num = 0f;
		foreach (string text in arg0)
		{
			if (!string.IsNullOrEmpty(text))
			{
				num = Math.Max(num, TraditionalText.CalcTextSize(text).X);
			}
		}
		return num / Math.Max(0.01f, uiScale) + 10f;
	}

	private static void Method_1129(string arg0, float arg1, float arg2, float arg3)
	{
		float uiScale = Theme.UiScale;
		ImGui.PushStyleVar(ImGuiStyleVar.CellPadding, new Vector2(8f * uiScale, 4f * uiScale));
		bool flag = arg3 > 0f;
		int column = (flag ? 3 : 2);
		ImGuiTableFlags imGuiTableFlags = ImGuiTableFlags.NoSavedSettings;
		imGuiTableFlags = (ImGuiTableFlags)((int)imGuiTableFlags | ((arg2 > 0f) ? 8192 : 24576));
		field_1124 = ImGui.BeginTable(arg0, column, imGuiTableFlags);
		field_1125 = flag;
		if (field_1124)
		{
			ImGui.TableSetupColumn("##label", ImGuiTableColumnFlags.WidthFixed, arg1 * uiScale);
			ImGui.TableSetupColumn("##value", (arg2 > 0f) ? ImGuiTableColumnFlags.WidthFixed : ImGuiTableColumnFlags.WidthStretch, (arg2 > 0f) ? (arg2 * uiScale) : 1f);
			if (flag)
			{
				ImGui.TableSetupColumn("##action", ImGuiTableColumnFlags.WidthFixed, arg3 * uiScale);
			}
		}
	}

	public static void End()
	{
		ImGui.EndTable();
		ImGui.PopStyleVar();
		field_1124 = false;
		field_1125 = false;
	}

	public static void Row(string arg0, string? arg1 = null)
	{
		if (field_1124)
		{
			ImGui.TableNextRow();
			ImGui.TableNextColumn();
			ImGui.AlignTextToFramePadding();
			ImGui.PushStyleColor(ImGuiCol.Text, Theme.TextSecondary);
			TraditionalText.TextUnformatted(arg0);
			ImGui.PopStyleColor();
			if (arg1 != null && ImGui.IsItemHovered())
			{
				TraditionalText.SetTooltip(arg1);
			}
			ImGui.TableNextColumn();
		}
	}

	public static void ActionCell()
	{
		if (field_1124 && field_1125)
		{
			ImGui.TableNextColumn();
		}
	}

	public static void Fill()
	{
		ImGui.SetNextItemWidth(-1f);
	}

	public static float Split(int arg0, float? arg1 = null)
	{
		float num = (arg1 ?? Theme.ItemSpacingX) * Theme.UiScale;
		return Math.Max(40f * Theme.UiScale, (ImGui.GetContentRegionAvail().X - num * (float)(arg0 - 1)) / (float)arg0);
	}

	public static void ToggleWithState(string arg0, bool arg1, Action<bool> arg2, string arg3)
	{
		UiLayout.EqualColumns("##" + arg0 + "row", 2, 8f, (int num) =>
		{
			if (num == 0)
			{
				bool v = arg1;
				ImU8String label = new ImU8String(2, 1);
				label.AppendLiteral("##");
				label.AppendFormatted(arg0);
				if (ImGui.Checkbox(label, ref v))
				{
					arg2(v);
				}
			}
			else
			{
				ImGui.TextDisabled(arg3);
			}
		});
	}

	public static void SliderWithReset(string arg0, Func<float> arg1, Action<float> arg2, float arg3, float arg4, string arg5, float arg6)
	{
		float v = arg1();
		ImGui.SetNextItemWidth(-1f);
		if (ImGui.SliderFloat(arg0, ref v, arg3, arg4, arg5))
		{
			arg2(v);
		}
		ActionCell();
		if (DrawResetButton("##reset" + arg0, arg6))
		{
			arg2(arg6);
		}
	}

	public static void InputWithReset(string arg0, Func<string> arg1, Action<string> arg2, int arg3, string arg4)
	{
		string buf = arg1();
		ImGui.SetNextItemWidth(-1f);
		if (ImGui.InputText(arg0, ref buf, arg3))
		{
			arg2(buf);
		}
		ActionCell();
		if (DrawResetButton("##reset" + arg0, arg4))
		{
			arg2(arg4);
		}
	}

	public static bool DrawResetButton(string arg0, double arg1)
	{
		return Method_1140(arg0, arg1.ToString("0.##"));
	}

	public static bool DrawResetButton(string arg0, string arg1)
	{
		return Method_1140(arg0, arg1);
	}

	private static bool Method_1140(string arg0, string arg1)
	{
		Vector2 size = new Vector2(Theme.ControlHeight, Theme.ControlHeight);
		bool result = ImGui.InvisibleButton(arg0, size);
		Vector2 itemRectMin = ImGui.GetItemRectMin();
		Vector2 itemRectMax = ImGui.GetItemRectMax();
		bool flag = ImGui.IsItemHovered();
		UiIcons.DrawReset(ImGui.GetWindowDrawList(), arg3: flag ? Theme.U32(Theme.BrandGold) : Theme.U32(Theme.TextDisabled), arg1: (itemRectMin + itemRectMax) * 0.5f, arg2: size.X * 0.28f);
		if (flag)
		{
			TraditionalText.SetTooltip(string.IsNullOrWhiteSpace(arg1) ? "恢復預設值" : ("恢復預設值（" + arg1 + "）"));
		}
		return result;
	}
}
