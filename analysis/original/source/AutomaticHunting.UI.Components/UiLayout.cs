using System;
using System.Numerics;
using Dalamud.Bindings.ImGui;

namespace AutomaticHunting.UI.Components;

public static class UiLayout
{
	public static void EqualColumns(string id, int count, Action<int> cell)
	{
		EqualColumns(id, count, -1f, cell);
	}

	public static void EqualColumns(string id, int count, float gap, Action<int> cell)
	{
		WeightedColumns(id, new float[count], gap, 4f, cell);
	}

	public static void WeightedColumns(string id, float[] weights, float gap, Action<int> cell)
	{
		WeightedColumns(id, weights, gap, 0f, cell);
	}

	public static void WeightedColumns(string id, float[] weights, float gap, float cellPadY, Action<int> cell)
	{
		float[] array = weights ?? Array.Empty<float>();
		int num = array.Length;
		if (num <= 0)
		{
			return;
		}
		float uiScale = Theme.UiScale;
		bool flag = false;
		if (gap > 0f)
		{
			ImGui.PushStyleVar(ImGuiStyleVar.CellPadding, new Vector2(gap * 0.5f * uiScale, cellPadY * uiScale));
			flag = true;
		}
		if (!ImGui.BeginTable(id, num, ImGuiTableFlags.SizingStretchProp | ImGuiTableFlags.NoSavedSettings))
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
			cell(j);
		}
		ImGui.EndTable();
		if (flag)
		{
			ImGui.PopStyleVar();
		}
	}

	public static void MainAndSide(string id, float sideWidth, Action<int> cell)
	{
		MainAndSide(id, sideWidth, -1f, cell);
	}

	public static void MainAndSide(string id, float sideWidth, float gap, Action<int> cell)
	{
		float uiScale = Theme.UiScale;
		bool flag = false;
		if (gap >= 0f)
		{
			Vector2 cellPadding = ImGui.GetStyle().CellPadding;
			ImGui.PushStyleVar(ImGuiStyleVar.CellPadding, new Vector2(gap * 0.5f * uiScale, cellPadding.Y));
			flag = true;
		}
		if (!ImGui.BeginTable(id, 2, ImGuiTableFlags.SizingStretchProp | ImGuiTableFlags.NoSavedSettings))
		{
			ImGui.EndTable();
			if (flag)
			{
				ImGui.PopStyleVar();
			}
			return;
		}
		ImGui.TableSetupColumn("##main", ImGuiTableColumnFlags.WidthStretch, 1f);
		ImGui.TableSetupColumn("##side", ImGuiTableColumnFlags.WidthFixed, sideWidth * uiScale);
		ImGui.TableNextRow();
		ImGui.TableNextColumn();
		cell(0);
		ImGui.TableNextColumn();
		cell(1);
		ImGui.EndTable();
		if (flag)
		{
			ImGui.PopStyleVar();
		}
	}

	public static void FixedColumns(string id, float[] widths, float gap, Action<int> cell)
	{
		float uiScale = Theme.UiScale;
		float[] array = widths ?? Array.Empty<float>();
		int num = array.Length;
		if (num <= 0)
		{
			return;
		}
		bool flag = false;
		if (gap >= 0f)
		{
			Vector2 cellPadding = ImGui.GetStyle().CellPadding;
			ImGui.PushStyleVar(ImGuiStyleVar.CellPadding, new Vector2(gap * 0.5f * uiScale, cellPadding.Y));
			flag = true;
		}
		if (!ImGui.BeginTable(id, num, ImGuiTableFlags.SizingStretchProp | ImGuiTableFlags.NoSavedSettings))
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
			cell(j);
		}
		ImGui.EndTable();
		if (flag)
		{
			ImGui.PopStyleVar();
		}
	}
}
