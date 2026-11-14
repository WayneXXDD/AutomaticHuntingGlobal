using System;
using System.Collections.Generic;
using System.Numerics;
using Dalamud.Bindings.ImGui;

namespace AutomaticHunting.UI.Components;

public sealed class UiCardList
{
	private const float field_1086 = 24f;

	private const float field_1087 = 4f;

	private const float field_1088 = 24f;

	private const float field_1089 = 120f;

	private const float field_1090 = 3f;

	private const float field_1091 = 1.5f;

	private const float field_1092 = 4f;

	private const float field_1093 = 2f;

	private const float field_1094 = 1f;

	private int field_1095 = -1;

	private int field_1096 = -1;

	private int field_1097 = -1;

	private int field_1098 = -1;

	private int field_1099 = -1;

	private DateTime field_1100;

	private string field_1101 = "預設";

	private float[] field_1102 = Array.Empty<float>();

	public Action<CardRowGeometry>? RowGeometryProbe;

	public void Draw(string arg0, string arg1, IReadOnlyList<UiListCard> arg2, Action<int, int>? arg3 = null)
	{
		field_1096 = -1;
		field_1097 = -1;
		field_1098 = -1;
		field_1101 = arg1;
		field_1102 = Method_1076(arg2);
		for (int i = 0; i < arg2.Count; i++)
		{
			Method_1073($"{arg0}{i}", arg2, i, arg2[i]);
		}
		if (arg3 != null)
		{
			if (field_1095 >= 0 && field_1096 >= 0 && field_1095 != field_1096)
			{
				arg3(field_1095, field_1096);
				field_1095 = -1;
				field_1096 = -1;
			}
			else if (field_1097 >= 0 && field_1098 >= 0 && field_1097 != field_1098)
			{
				arg3(field_1097, field_1098);
				field_1097 = -1;
				field_1098 = -1;
			}
		}
	}

	public static bool MoveItem<TMethod0>(List<TMethod0> arg0, int arg1, int arg2)
	{
		if (arg0 == null || arg0.Count == 0)
		{
			return false;
		}
		if (arg1 < 0 || arg1 >= arg0.Count)
		{
			return false;
		}
		if (arg2 < 0 || arg2 >= arg0.Count)
		{
			return false;
		}
		if (arg1 == arg2)
		{
			return false;
		}
		TMethod0 item = arg0[arg1];
		arg0.RemoveAt(arg1);
		int index = Math.Min(arg2, arg0.Count);
		arg0.Insert(index, item);
		return true;
	}

	private void Method_1073(string arg0, IReadOnlyList<UiListCard> arg1, int arg2, UiListCard arg3)
	{
		float uiScale = Theme.UiScale;
		Vector2 cursorScreenPos = ImGui.GetCursorScreenPos();
		float x = ImGui.GetContentRegionAvail().X;
		float num = 32f * uiScale;
		bool flag = ImGui.IsMouseHoveringRect(cursorScreenPos, cursorScreenPos + new Vector2(x, num));
		Vector4 col;
		if (arg3.Selected)
		{
			col = Theme.WithAlpha(Theme.BrandGold, 0.07f);
		}
		else
		{
			col = (flag ? Theme.SurfaceCardHover : Theme.SurfaceCard);
		}
		ImGui.PushStyleColor(ImGuiCol.TableRowBg, col);
		ImGui.PushStyleColor(ImGuiCol.TableRowBgAlt, Theme.SurfaceCard);
		ImGui.PushStyleVar(ImGuiStyleVar.CellPadding, new Vector2(4f * uiScale, 4f * uiScale));
		ImGui.PushStyleVar(ImGuiStyleVar.ChildRounding, 6f * uiScale);
		int count = arg3.Prefixes.Count;
		float[] array = new float[count];
		float num2 = 0f;
		for (int i = 0; i < count; i++)
		{
			array[i] = ((i < field_1102.Length) ? field_1102[i] : 0f);
			num2 += array[i];
		}
		int num3 = 5 + arg3.ExtraColumns.Count + arg3.Actions.Count + count;
		float num4 = 0f;
		foreach (UiListCardAction action in arg3.Actions)
		{
			num4 = Math.Max(num4, Method_1075(action));
		}
		float arg4 = 1.5f * (float)arg3.ExtraColumns.Count + 1f * (float)arg3.Actions.Count;
		float initWidthOrWeight = Method_1074(x, num3, arg4, uiScale, num2, num4);
		ImU8String strId = new ImU8String(6, 1);
		strId.AppendLiteral("##");
		strId.AppendFormatted(arg0);
		strId.AppendLiteral("card");
		if (!ImGui.BeginTable(strId, num3, ImGuiTableFlags.SizingStretchProp | ImGuiTableFlags.NoSavedSettings | ImGuiTableFlags.RowBg))
		{
			ImGui.EndTable();
			ImGui.PopStyleVar(2);
			ImGui.PopStyleColor(2);
			return;
		}
		ImGui.TableSetupColumn("##handle", ImGuiTableColumnFlags.WidthFixed, 24f * uiScale);
		for (int j = 0; j < count; j++)
		{
			ImU8String label = new ImU8String(8, 1);
			label.AppendLiteral("##prefix");
			label.AppendFormatted(j);
			ImGui.TableSetupColumn(label, ImGuiTableColumnFlags.WidthFixed, array[j]);
		}
		ImGui.TableSetupColumn("##title", ImGuiTableColumnFlags.WidthStretch, initWidthOrWeight);
		for (int k = 0; k < arg3.ExtraColumns.Count; k++)
		{
			ImU8String label2 = new ImU8String(7, 1);
			label2.AppendLiteral("##extra");
			label2.AppendFormatted(k);
			ImGui.TableSetupColumn(label2, ImGuiTableColumnFlags.WidthStretch, 1.5f);
		}
		for (int l = 0; l < arg3.Actions.Count; l++)
		{
			ImU8String label3 = new ImU8String(5, 1);
			label3.AppendLiteral("##act");
			label3.AppendFormatted(l);
			ImGui.TableSetupColumn(label3, ImGuiTableColumnFlags.WidthStretch, 1f);
		}
		ImGui.TableSetupColumn("##up", ImGuiTableColumnFlags.WidthFixed, 24f * uiScale);
		ImGui.TableSetupColumn("##down", ImGuiTableColumnFlags.WidthFixed, 24f * uiScale);
		ImGui.TableSetupColumn("##delete", ImGuiTableColumnFlags.WidthFixed, 24f * uiScale);
		ImGui.TableNextRow();
		ImGui.TableNextColumn();
		IconButtonGeometry arg5 = Method_1079(arg0 + "handle", arg3, arg2, arg1);
		PrefixGeometry[] array2 = new PrefixGeometry[count];
		for (int m = 0; m < count; m++)
		{
			ImGui.TableNextColumn();
			array2[m] = Method_1078($"{arg0}prefix{m}", arg3.Prefixes[m], array[m]);
		}
		ImGui.TableNextColumn();
		Vector2 cursorScreenPos2 = ImGui.GetCursorScreenPos();
		float x2 = ImGui.GetContentRegionAvail().X;
		Method_1081(arg0 + "title", arg3, arg2, x2);
		for (int n = 0; n < arg3.ExtraColumns.Count; n++)
		{
			ImGui.TableNextColumn();
			arg3.ExtraColumns[n]($"{arg0}extra{n}");
		}
		float arg6 = 0f;
		for (int num5 = 0; num5 < arg3.Actions.Count; num5++)
		{
			ImGui.TableNextColumn();
			UiListCardAction uiListCardAction = arg3.Actions[num5];
			Vector2 value = new Vector2(ImGui.GetContentRegionAvail().X, Theme.ControlHeight);
			if (num5 == 0)
			{
				arg6 = value.X;
			}
			if (UiButton.Draw(uiListCardAction.Label, uiListCardAction.Variant, value, uiListCardAction.Tooltip, uiListCardAction.Enabled))
			{
				uiListCardAction.OnClick?.Invoke(arg2);
			}
		}
		ImGui.TableNextColumn();
		IconButtonGeometry arg7 = Method_1080(arg0 + "up", arg2, arg2: true, arg3, arg1.Count);
		ImGui.TableNextColumn();
		IconButtonGeometry arg8 = Method_1080(arg0 + "down", arg2, arg2: false, arg3, arg1.Count);
		ImGui.TableNextColumn();
		IconButtonGeometry arg9 = Method_1082(arg0 + "del", arg3, arg2);
		ImGui.EndTable();
		ImGui.PopStyleVar(2);
		ImGui.PopStyleColor(2);
		RowGeometryProbe?.Invoke(new CardRowGeometry(arg2, x2, arg6, array2, arg5, arg7, arg8, arg9, cursorScreenPos2));
		ImGui.SetCursorScreenPos(cursorScreenPos);
		ImU8String strId2 = new ImU8String(5, 1);
		strId2.AppendLiteral("##");
		strId2.AppendFormatted(arg0);
		strId2.AppendLiteral("row");
		ImGui.InvisibleButton(strId2, new Vector2(x, num));
		if (arg3.Draggable && ImGui.BeginDragDropTarget())
		{
			ImGuiPayloadPtr imGuiPayloadPtr = ImGui.AcceptDragDropPayload(field_1101);
			if (!imGuiPayloadPtr.IsNull && imGuiPayloadPtr.IsDelivery())
			{
				field_1096 = arg2;
			}
			ImGui.EndDragDropTarget();
		}
		ImDrawListPtr windowDrawList = ImGui.GetWindowDrawList();
		windowDrawList.AddRect(cursorScreenPos, cursorScreenPos + new Vector2(x, num), Theme.U32(Theme.BorderCard), 6f * uiScale, 1f);
		if (arg3.Selected)
		{
			windowDrawList.AddRectFilled(cursorScreenPos, cursorScreenPos + new Vector2(2f * uiScale, num), Theme.U32(Theme.BrandGold), 1f);
		}
		ImGui.SetCursorScreenPos(cursorScreenPos + new Vector2(0f, num + 4f * uiScale));
	}

	private static float Method_1074(float arg0, int arg1, float arg2, float arg3, float arg4 = 0f, float arg5 = 0f)
	{
		if (arg2 <= 0f)
		{
			return 3f;
		}
		float num = 120f * arg3;
		float num2 = 96f * arg3 + arg4;
		float num3 = 8f * arg3 * (float)(arg1 - 1);
		float num4 = arg0 - num2 - num3;
		if (num4 <= num)
		{
			return 3f;
		}
		float num5 = num * arg2 / (num4 - num);
		float num6 = ((num5 > 3f) ? num5 : 3f);
		if (arg5 > 0f)
		{
			float num7 = num4 / arg5 - arg2;
			if (num7 >= num5 && num6 > num7)
			{
				num6 = num7;
			}
		}
		return num6;
	}

	private static float Method_1075(UiListCardAction arg0)
	{
		float uiScale = Theme.UiScale;
		float x = TraditionalText.CalcTextSize(arg0.Label).X;
		float x2 = ImGui.GetStyle().FramePadding.X;
		return x + x2 * 2f + 2f * uiScale;
	}

	private static float[] Method_1076(IReadOnlyList<UiListCard> arg0)
	{
		int num = 0;
		foreach (UiListCard item in arg0)
		{
			if (item.Prefixes.Count > num)
			{
				num = item.Prefixes.Count;
			}
		}
		float[] array = new float[num];
		for (int i = 0; i < num; i++)
		{
			float num2 = 0f;
			float num3 = 0f;
			bool flag = false;
			foreach (UiListCard item2 in arg0)
			{
				if (i < item2.Prefixes.Count)
				{
					UiListCardPrefix uiListCardPrefix = item2.Prefixes[i];
					flag = true;
					if (uiListCardPrefix.Chars > num3)
					{
						num3 = uiListCardPrefix.Chars;
					}
					float x = TraditionalText.CalcTextSize(uiListCardPrefix.Text ?? string.Empty).X;
					if (x > num2)
					{
						num2 = x;
					}
				}
			}
			if (flag)
			{
				float num4 = 4f * Theme.UiScale;
				float val = ((num3 > 0f) ? Method_1077(num3) : 0f);
				array[i] = Math.Max(val, num2 + num4 * 2f);
			}
		}
		return array;
	}

	private static float Method_1077(float arg0)
	{
		int num = (int)Math.Ceiling(arg0);
		if (num <= 0)
		{
			return 0f;
		}
		return TraditionalText.CalcTextSize(new string('中', num)).X;
	}

	private static PrefixGeometry Method_1078(string arg0, UiListCardPrefix arg1, float arg2)
	{
		float uiScale = Theme.UiScale;
		ImDrawListPtr windowDrawList = ImGui.GetWindowDrawList();
		Vector2 cursorScreenPos = ImGui.GetCursorScreenPos();
		string text = arg1.Text ?? string.Empty;
		float x = TraditionalText.CalcTextSize(text).X;
		float num = 4f * uiScale;
		float x2 = (arg1.Center ? (cursorScreenPos.X + (arg2 - x) * 0.5f) : (cursorScreenPos.X + num));
		float num2 = cursorScreenPos.Y + Math.Max(0f, (24f * uiScale - Theme.FsBody) * 0.5f);
		Vector2 vector = new Vector2(x2, num2);
		Vector2 vector2 = Vector2.Zero;
		Vector2 vector3 = Vector2.Zero;
		if (arg1.Box)
		{
			vector2 = new Vector2(cursorScreenPos.X + 1f * uiScale, num2 - 2f * uiScale);
			vector3 = new Vector2(cursorScreenPos.X + arg2 - 1f * uiScale, num2 + Theme.FsBody + 2f * uiScale);
			windowDrawList.AddRect(vector2, vector3, Theme.U32(Theme.TextDisabled), 3f * uiScale, 1f);
		}
		Theme.DrawText(windowDrawList, vector, arg1.Color ?? Theme.TextSecondary, text, Theme.FsBody);
		ImGui.SetCursorScreenPos(cursorScreenPos);
		ImU8String strId = new ImU8String(8, 1);
		strId.AppendLiteral("##");
		strId.AppendFormatted(arg0);
		strId.AppendLiteral("prefix");
		ImGui.InvisibleButton(strId, new Vector2(arg2, 24f * uiScale));
		Vector2 itemRectMin = ImGui.GetItemRectMin();
		Vector2 itemRectMax = ImGui.GetItemRectMax();
		return new PrefixGeometry(itemRectMin, itemRectMax, x, vector, text, vector2, vector3);
	}

	private IconButtonGeometry Method_1079(string arg0, UiListCard arg1, int arg2, IReadOnlyList<UiListCard> arg3)
	{
		float uiScale = Theme.UiScale;
		Vector2 size = new Vector2(24f * uiScale, 24f * uiScale);
		ImGui.SetCursorScreenPos(ImGui.GetCursorScreenPos());
		ImU8String strId = new ImU8String(2, 1);
		strId.AppendLiteral("##");
		strId.AppendFormatted(arg0);
		ImGui.InvisibleButton(strId, size);
		bool flag = ImGui.IsItemHovered();
		Vector2 itemRectMin = ImGui.GetItemRectMin();
		Vector2 itemRectMax = ImGui.GetItemRectMax();
		ImDrawListPtr windowDrawList = ImGui.GetWindowDrawList();
		if (flag)
		{
			windowDrawList.AddRectFilled(itemRectMin, itemRectMax, Theme.U32(Theme.SurfaceCardHover), 4f * uiScale);
			windowDrawList.AddRect(itemRectMin, itemRectMax, Theme.U32(Theme.BorderControl), 4f * uiScale, 1f);
		}
		UiIcons.DrawDragDots(windowDrawList, itemRectMin, itemRectMax, Theme.U32(flag ? Theme.BrandGold : Theme.TextDisabled));
		if (arg1.Draggable && ImGui.BeginDragDropSource())
		{
			field_1095 = arg2;
			ImGui.SetDragDropPayload(field_1101, default);
			TraditionalText.TextUnformatted(arg1.Title);
			ImGui.EndDragDropSource();
		}
		if (flag)
		{
			TraditionalText.SetTooltip(arg1.Draggable ? "按住拖動調整順序" : "此條目不可拖動");
		}
		return new IconButtonGeometry(arg2, CardIconKind.Handle, itemRectMin, itemRectMax, arg1.Draggable);
	}

	private IconButtonGeometry Method_1080(string arg0, int arg1, bool arg2, UiListCard arg3, int arg4)
	{
		float uiScale = Theme.UiScale;
		Vector2 size = new Vector2(24f * uiScale, 24f * uiScale);
		bool flag = arg1 <= 0;
		bool flag2 = arg1 >= arg4 - 1;
		bool flag3 = arg3.Draggable && (arg2 ? (!flag) : (!flag2));
		string arg5;
		if (arg2)
		{
			arg5 = (flag ? "上移（已是第一項）" : "上移");
		}
		else
		{
			arg5 = (flag2 ? "下移（已是最後一項）" : "下移");
		}
		if (!arg3.Draggable)
		{
			arg5 = (arg2 ? "上移（此條目不可排序）" : "下移（此條目不可排序）");
		}
		ImGui.GetCursorScreenPos();
		if (!flag3)
		{
			ImGui.BeginDisabled();
		}
		bool flag4 = ImGui.InvisibleButton(arg0, size);
		Vector2 itemRectMin = ImGui.GetItemRectMin();
		Vector2 itemRectMax = ImGui.GetItemRectMax();
		if (!flag3)
		{
			ImGui.EndDisabled();
		}
		bool flag5 = ImGui.IsMouseHoveringRect(itemRectMin, itemRectMax, clip: true);
		ImDrawListPtr windowDrawList = ImGui.GetWindowDrawList();
		if (flag5 & flag3)
		{
			windowDrawList.AddRectFilled(itemRectMin, itemRectMax, Theme.U32(Theme.SurfaceCardHover), 4f * uiScale);
		}
		Vector4 arg6;
		if (flag3)
		{
			arg6 = (flag5 ? Theme.BrandGold : Theme.TextSecondary);
		}
		else
		{
			arg6 = Theme.TextDisabled;
		}
		Vector2 arg7 = (itemRectMin + itemRectMax) * 0.5f;
		float arg8 = 24f * uiScale * 0.38f;
		if (arg2)
		{
			UiIcons.DrawChevronUp(windowDrawList, arg7, arg8, Theme.U32(arg6));
		}
		else
		{
			UiIcons.DrawChevronDown(windowDrawList, arg7, arg8, Theme.U32(arg6));
		}
		if (flag5)
		{
			UiButton.ShowTooltip(arg5);
		}
		if (flag4 & flag3)
		{
			field_1097 = arg1;
			field_1098 = (arg2 ? (arg1 - 1) : (arg1 + 1));
		}
		return new IconButtonGeometry(arg1, arg2 ? CardIconKind.Up : CardIconKind.Down, itemRectMin, itemRectMax, flag3);
	}

	private void Method_1081(string arg0, UiListCard arg1, int arg2, float arg3)
	{
		float uiScale = Theme.UiScale;
		ImDrawListPtr windowDrawList = ImGui.GetWindowDrawList();
		Vector2 cursorScreenPos = ImGui.GetCursorScreenPos();
		bool flag = !string.IsNullOrEmpty(arg1.Subtitle);
		Vector2 vector = TraditionalText.CalcTextSize(arg1.Title);
		Vector2 vector2 = (flag ? TraditionalText.CalcTextSize(arg1.Subtitle) : Vector2.Zero);
		float y;
		if (flag)
		{
			Theme.DrawText(windowDrawList, cursorScreenPos, Theme.TextPrimary, Theme.Ellipsis(arg1.Title, arg3), Theme.FsBody);
			Theme.DrawText(windowDrawList, new Vector2(cursorScreenPos.X, cursorScreenPos.Y + vector.Y + 1f * uiScale), Theme.TextSecondary, Theme.Ellipsis(arg1.Subtitle, arg3, Theme.FsCaption), Theme.FsCaption);
			y = vector.Y + vector2.Y + 1f * uiScale;
		}
		else
		{
			float y2 = cursorScreenPos.Y + Math.Max(0f, (24f * uiScale - Theme.FsBody) * 0.5f);
			Theme.DrawText(windowDrawList, new Vector2(cursorScreenPos.X, y2), Theme.TextPrimary, Theme.Ellipsis(arg1.Title, arg3), Theme.FsBody);
			y = Math.Max(vector.Y, 24f * uiScale);
		}
		ImGui.SetCursorScreenPos(cursorScreenPos);
		ImU8String strId = new ImU8String(5, 1);
		strId.AppendLiteral("##");
		strId.AppendFormatted(arg0);
		strId.AppendLiteral("hit");
		ImGui.InvisibleButton(strId, new Vector2(arg3, y));
		Vector2 itemRectMin = ImGui.GetItemRectMin();
		Vector2 itemRectMax = ImGui.GetItemRectMax();
		if (arg1.Tooltip != null && ImGui.IsMouseHoveringRect(itemRectMin, itemRectMax, clip: true))
		{
			UiButton.ShowTooltip(arg1.Tooltip);
		}
	}

	private IconButtonGeometry Method_1082(string arg0, UiListCard arg1, int arg2)
	{
		float uiScale = Theme.UiScale;
		Vector2 vector = new Vector2(24f * uiScale, 24f * uiScale);
		Vector2 cursorScreenPos = ImGui.GetCursorScreenPos();
		ImGui.SetCursorScreenPos(cursorScreenPos);
		ImU8String strId = new ImU8String(2, 1);
		strId.AppendLiteral("##");
		strId.AppendFormatted(arg0);
		bool flag = ImGui.InvisibleButton(strId, vector);
		Vector2 itemRectMin = ImGui.GetItemRectMin();
		Vector2 itemRectMax = ImGui.GetItemRectMax();
		bool flag2 = ImGui.IsItemHovered();
		bool flag3 = UiDeleteStyle.IsArmed(field_1099, arg2, field_1100);
		UiDeleteStyle.Draw(ImGui.GetWindowDrawList(), cursorScreenPos, vector, flag3, flag2, uiScale);
		if (flag2)
		{
			TraditionalText.SetTooltip(flag3 ? "再點一次確認刪除" : arg1.DeleteTooltip);
		}
		if (flag)
		{
			if (flag3)
			{
				field_1099 = -1;
				arg1.OnDelete?.Invoke(arg2);
			}
			else
			{
				field_1099 = arg2;
				field_1100 = UiDeleteStyle.ArmNow();
			}
		}
		return new IconButtonGeometry(arg2, CardIconKind.Delete, itemRectMin, itemRectMax, arg4: true);
	}
}
