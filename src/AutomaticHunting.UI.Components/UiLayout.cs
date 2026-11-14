using System;
using System.Numerics;
using Dalamud.Bindings.ImGui;

namespace AutomaticHunting.UI.Components;

public static class UiLayout
{
	public static void EqualColumns(string arg0, int arg1, Action<int> arg2)
	{
		EqualColumns(arg0, arg1, -1f, arg2);
	}

	public static void EqualColumns(string arg0, int arg1, float arg2, Action<int> arg3)
	{
		WeightedColumns(arg0, new float[arg1], arg2, 4f, arg3);
	}

	public static void WeightedColumns(string arg0, float[] arg1, float arg2, Action<int> arg3)
	{
		WeightedColumns(arg0, arg1, arg2, 0f, arg3);
	}

	public static void WeightedColumns(string arg0, float[] arg1, float arg2, float arg3, Action<int> arg4)
	{
		float[] array = arg1 ?? Array.Empty<float>();
		int num = array.Length;
		if (num <= 0)
		{
			return;
		}
		float uiScale = Theme.UiScale;
		bool flag = false;
		if (arg2 > 0f)
		{
			ImGui.PushStyleVar(ImGuiStyleVar.CellPadding, new Vector2(arg2 * 0.5f * uiScale, arg3 * uiScale));
			flag = true;
		}
		if (!ImGui.BeginTable(arg0, num, ImGuiTableFlags.SizingStretchProp | ImGuiTableFlags.NoSavedSettings))
		{
			ImGui.EndTable();
			if (flag)
			{
				ImGui.PopStyleVar();
			}
			return;
		}
		for (int i = 0; i < num; i++)
		{
			float num2 = array[i];
			if (num2 <= 0f)
			{
				num2 = 1f;
			}
			ImU8String label = new ImU8String(3, 1);
			label.AppendLiteral("##c");
			label.AppendFormatted(i);
			ImGui.TableSetupColumn(label, ImGuiTableColumnFlags.WidthStretch, num2);
		}
		ImGui.TableNextRow();
		for (int j = 0; j < num; j++)
		{
			ImGui.TableNextColumn();
			arg4(j);
		}
		ImGui.EndTable();
		if (flag)
		{
			ImGui.PopStyleVar();
		}
	}

	public static void MainAndSide(string arg0, float arg1, Action<int> arg2)
	{
		MainAndSide(arg0, arg1, -1f, arg2);
	}

	public static void MainAndSide(string arg0, float arg1, float arg2, Action<int> arg3)
	{
		float uiScale = Theme.UiScale;
		bool flag = false;
		if (arg2 >= 0f)
		{
			Vector2 cellPadding = ImGui.GetStyle().CellPadding;
			ImGui.PushStyleVar(ImGuiStyleVar.CellPadding, new Vector2(arg2 * 0.5f * uiScale, cellPadding.Y));
			flag = true;
		}
		if (!ImGui.BeginTable(arg0, 2, ImGuiTableFlags.SizingStretchProp | ImGuiTableFlags.NoSavedSettings))
		{
			ImGui.EndTable();
			if (flag)
			{
				ImGui.PopStyleVar();
			}
			return;
		}
		ImGui.TableSetupColumn("##main", ImGuiTableColumnFlags.WidthStretch, 1f);
		ImGui.TableSetupColumn("##side", ImGuiTableColumnFlags.WidthFixed, arg1 * uiScale);
		ImGui.TableNextRow();
		ImGui.TableNextColumn();
		arg3(0);
		ImGui.TableNextColumn();
		arg3(1);
		ImGui.EndTable();
		if (flag)
		{
			ImGui.PopStyleVar();
		}
	}

	public static void FixedColumns(string arg0, float[] arg1, float arg2, Action<int> arg3)
	{
		float uiScale = Theme.UiScale;
		float[] array = arg1 ?? Array.Empty<float>();
		int num = array.Length;
		if (num <= 0)
		{
			return;
		}
		bool flag = false;
		if (arg2 >= 0f)
		{
			Vector2 cellPadding = ImGui.GetStyle().CellPadding;
			ImGui.PushStyleVar(ImGuiStyleVar.CellPadding, new Vector2(arg2 * 0.5f * uiScale, cellPadding.Y));
			flag = true;
		}
		if (!ImGui.BeginTable(arg0, num, ImGuiTableFlags.SizingStretchProp | ImGuiTableFlags.NoSavedSettings))
		{
			ImGui.EndTable();
			if (flag)
			{
				ImGui.PopStyleVar();
			}
			return;
		}
		for (int i = 0; i < num; i++)
		{
			ImU8String label = new ImU8String(3, 1);
			label.AppendLiteral("##c");
			label.AppendFormatted(i);
			ImGui.TableSetupColumn(label, ImGuiTableColumnFlags.WidthFixed, array[i] * uiScale);
		}
		ImGui.TableNextRow();
		for (int j = 0; j < num; j++)
		{
			ImGui.TableNextColumn();
			arg3(j);
		}
		ImGui.EndTable();
		if (flag)
		{
			ImGui.PopStyleVar();
		}
	}
}
