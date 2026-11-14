using System;
using System.Collections.Generic;
using System.Numerics;
using Dalamud.Bindings.ImGui;

namespace AutomaticHunting.UI.Components;

public sealed class UiCardList
{
	private const float _2001_2007 = 24f;

	private const float _2001_2008 = 4f;

	private const float _2001_2009 = 24f;

	private const float _2001_200A = 120f;

	private const float _2001_200B = 3f;

	private const float _2001_2010 = 1.5f;

	private const float _2001_2011 = 4f;

	private const float _2001_2012 = 2f;

	private const float _2001_2013 = 1f;

	private int _2001_2014 = -1;

	private int _2001_2015 = -1;

	private int _2001_2022 = -1;

	private int _2001_2024 = -1;

	private int _2001_2025 = -1;

	private DateTime _2001_2027;

	private string _2001_2028 = "DEFAULT";

	private float[] _2001_2029 = Array.Empty<float>();

	public Action<CardRowGeometry>? RowGeometryProbe;

	public void Draw(string idPrefix, string dragPayload, IReadOnlyList<UiListCard> cards, Action<int, int>? onReorder = null)
	{
		_2001_2015 = -1;
		_2001_2022 = -1;
		_2001_2024 = -1;
		_2001_2028 = dragPayload;
		_2001_2029 = _00A0(cards);
		for (int i = 0; i < cards.Count; i++)
		{
			_00A0($"{idPrefix}{i}", cards, i, cards[i]);
		}
		if (onReorder != null)
		{
			if (_2001_2014 >= 0 && _2001_2015 >= 0 && _2001_2014 != _2001_2015)
			{
				onReorder(_2001_2014, _2001_2015);
				_2001_2014 = -1;
				_2001_2015 = -1;
			}
			else if (_2001_2022 >= 0 && _2001_2024 >= 0 && _2001_2022 != _2001_2024)
			{
				onReorder(_2001_2022, _2001_2024);
				_2001_2022 = -1;
				_2001_2024 = -1;
			}
		}
	}

	public static bool MoveItem<T>(List<T> list, int from, int to)
	{
		if (list == null || list.Count == 0)
		{
			return false;
		}
		if (from < 0 || from >= list.Count)
		{
			return false;
		}
		if (to < 0 || to >= list.Count)
		{
			return false;
		}
		if (from == to)
		{
			return false;
		}
		T item = list[from];
		list.RemoveAt(from);
		int index = Math.Min(to, list.Count);
		list.Insert(index, item);
		return true;
	}

	private void _00A0(string P_0, IReadOnlyList<UiListCard> P_1, int P_2, UiListCard P_3)
	{
		float uiScale = Theme.UiScale;
		Vector2 cursorScreenPos = ImGui.GetCursorScreenPos();
		float x = ImGui.GetContentRegionAvail().X;
		float num = 32f * uiScale;
		bool flag = ImGui.IsMouseHoveringRect(cursorScreenPos, cursorScreenPos + new Vector2(x, num));
		Vector4 col;
		if (P_3.Selected)
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
		int count = P_3.Prefixes.Count;
		float[] array = new float[count];
		float num2 = 0f;
		for (int i = 0; i < count; i++)
		{
			array[i] = ((i < _2001_2029.Length) ? _2001_2029[i] : 0f);
			num2 += array[i];
		}
		int num3 = 5 + P_3.ExtraColumns.Count + P_3.Actions.Count + count;
		float num4 = 0f;
		foreach (UiListCardAction action in P_3.Actions)
		{
			num4 = Math.Max(num4, _00A0(action));
		}
		float num5 = 1.5f * (float)P_3.ExtraColumns.Count + 1f * (float)P_3.Actions.Count;
		float initWidthOrWeight = _00A0(x, num3, num5, uiScale, num2, num4);
		ImU8String strId = new ImU8String(6, 1);
		strId.AppendLiteral("##");
		strId.AppendFormatted(P_0);
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
		for (int k = 0; k < P_3.ExtraColumns.Count; k++)
		{
			ImU8String label2 = new ImU8String(7, 1);
			label2.AppendLiteral("##extra");
			label2.AppendFormatted(k);
			ImGui.TableSetupColumn(label2, ImGuiTableColumnFlags.WidthStretch, 1.5f);
		}
		for (int l = 0; l < P_3.Actions.Count; l++)
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
		IconButtonGeometry handle = _00A0(P_0 + "handle", P_3, P_2, P_1);
		PrefixGeometry[] array2 = new PrefixGeometry[count];
		for (int m = 0; m < count; m++)
		{
			ImGui.TableNextColumn();
			array2[m] = _00A0($"{P_0}prefix{m}", P_3.Prefixes[m], array[m]);
		}
		ImGui.TableNextColumn();
		Vector2 cursorScreenPos2 = ImGui.GetCursorScreenPos();
		float x2 = ImGui.GetContentRegionAvail().X;
		_00A0(P_0 + "title", P_3, P_2, x2);
		for (int n = 0; n < P_3.ExtraColumns.Count; n++)
		{
			ImGui.TableNextColumn();
			P_3.ExtraColumns[n]($"{P_0}extra{n}");
		}
		float actionColumnWidth = 0f;
		for (int num6 = 0; num6 < P_3.Actions.Count; num6++)
		{
			ImGui.TableNextColumn();
			UiListCardAction uiListCardAction = P_3.Actions[num6];
			Vector2 value = new Vector2(ImGui.GetContentRegionAvail().X, Theme.ControlHeight);
			if (num6 == 0)
			{
				actionColumnWidth = value.X;
			}
			if (UiButton.Draw(uiListCardAction.Label, uiListCardAction.Variant, value, uiListCardAction.Tooltip, uiListCardAction.Enabled))
			{
				uiListCardAction.OnClick?.Invoke(P_2);
			}
		}
		ImGui.TableNextColumn();
		IconButtonGeometry up = _00A0(P_0 + "up", P_2, true, P_3, P_1.Count);
		ImGui.TableNextColumn();
		IconButtonGeometry down = _00A0(P_0 + "down", P_2, false, P_3, P_1.Count);
		ImGui.TableNextColumn();
		IconButtonGeometry delete = _00A0(P_0 + "del", P_3, P_2);
		ImGui.EndTable();
		ImGui.PopStyleVar(2);
		ImGui.PopStyleColor(2);
		RowGeometryProbe?.Invoke(new CardRowGeometry(P_2, x2, actionColumnWidth, array2, handle, up, down, delete, cursorScreenPos2));
		ImGui.SetCursorScreenPos(cursorScreenPos);
		ImU8String strId2 = new ImU8String(5, 1);
		strId2.AppendLiteral("##");
		strId2.AppendFormatted(P_0);
		strId2.AppendLiteral("row");
		ImGui.InvisibleButton(strId2, new Vector2(x, num));
		if (P_3.Draggable && ImGui.BeginDragDropTarget())
		{
			ImGuiPayloadPtr imGuiPayloadPtr = ImGui.AcceptDragDropPayload(_2001_2028);
			if (!imGuiPayloadPtr.IsNull && imGuiPayloadPtr.IsDelivery())
			{
				_2001_2015 = P_2;
			}
			ImGui.EndDragDropTarget();
		}
		ImDrawListPtr windowDrawList = ImGui.GetWindowDrawList();
		windowDrawList.AddRect(cursorScreenPos, cursorScreenPos + new Vector2(x, num), Theme.U32(Theme.BorderCard), 6f * uiScale, 1f);
		if (P_3.Selected)
		{
			windowDrawList.AddRectFilled(cursorScreenPos, cursorScreenPos + new Vector2(2f * uiScale, num), Theme.U32(Theme.BrandGold), 1f);
		}
		ImGui.SetCursorScreenPos(cursorScreenPos + new Vector2(0f, num + 4f * uiScale));
	}

	private static float _00A0(float P_0, int P_1, float P_2, float P_3, float P_4 = 0f, float P_5 = 0f)
	{
		if (P_2 <= 0f)
		{
			return 3f;
		}
		float num = 120f * P_3;
		float num2 = 96f * P_3 + P_4;
		float num3 = 8f * P_3 * (float)(P_1 - 1);
		float num4 = P_0 - num2 - num3;
		if (num4 <= num)
		{
			return 3f;
		}
		float num5 = num * P_2 / (num4 - num);
		float num6 = ((num5 > 3f) ? num5 : 3f);
		if (P_5 > 0f)
		{
			float num7 = num4 / P_5 - P_2;
			if (num7 >= num5 && num6 > num7)
			{
				num6 = num7;
			}
		}
		return num6;
	}

	private static float _00A0(UiListCardAction P_0)
	{
		float uiScale = Theme.UiScale;
		float x = ImGui.CalcTextSize(P_0.Label).X;
		float x2 = ImGui.GetStyle().FramePadding.X;
		return x + x2 * 2f + 2f * uiScale;
	}

	private static float[] _00A0(IReadOnlyList<UiListCard> P_0)
	{
		int num = 0;
		foreach (UiListCard item in P_0)
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
			foreach (UiListCard item2 in P_0)
			{
				if (i < item2.Prefixes.Count)
				{
					UiListCardPrefix uiListCardPrefix = item2.Prefixes[i];
					flag = true;
					if (uiListCardPrefix.Chars > num3)
					{
						num3 = uiListCardPrefix.Chars;
					}
					float x = ImGui.CalcTextSize(uiListCardPrefix.Text ?? string.Empty).X;
					if (x > num2)
					{
						num2 = x;
					}
				}
			}
			if (flag)
			{
				float num4 = 4f * Theme.UiScale;
				float val = ((num3 > 0f) ? _00A0(num3) : 0f);
				array[i] = Math.Max(val, num2 + num4 * 2f);
			}
		}
		return array;
	}

	private static float _00A0(float P_0)
	{
		int num = (int)Math.Ceiling(P_0);
		if (num <= 0)
		{
			return 0f;
		}
		return ImGui.CalcTextSize(new string('中', num)).X;
	}

	private static PrefixGeometry _00A0(string P_0, UiListCardPrefix P_1, float P_2)
	{
		float uiScale = Theme.UiScale;
		ImDrawListPtr windowDrawList = ImGui.GetWindowDrawList();
		Vector2 cursorScreenPos = ImGui.GetCursorScreenPos();
		string text = P_1.Text ?? string.Empty;
		float x = ImGui.CalcTextSize(text).X;
		float num = 4f * uiScale;
		float x2 = (P_1.Center ? (cursorScreenPos.X + (P_2 - x) * 0.5f) : (cursorScreenPos.X + num));
		float num2 = cursorScreenPos.Y + Math.Max(0f, (24f * uiScale - Theme.FsBody) * 0.5f);
		Vector2 vector = new Vector2(x2, num2);
		Vector2 vector2 = Vector2.Zero;
		Vector2 vector3 = Vector2.Zero;
		if (P_1.Box)
		{
			vector2 = new Vector2(cursorScreenPos.X + 1f * uiScale, num2 - 2f * uiScale);
			vector3 = new Vector2(cursorScreenPos.X + P_2 - 1f * uiScale, num2 + Theme.FsBody + 2f * uiScale);
			windowDrawList.AddRect(vector2, vector3, Theme.U32(Theme.TextDisabled), 3f * uiScale, 1f);
		}
		Theme.DrawText(windowDrawList, vector, P_1.Color ?? Theme.TextSecondary, text, Theme.FsBody);
		ImGui.SetCursorScreenPos(cursorScreenPos);
		ImU8String strId = new ImU8String(8, 1);
		strId.AppendLiteral("##");
		strId.AppendFormatted(P_0);
		strId.AppendLiteral("prefix");
		ImGui.InvisibleButton(strId, new Vector2(P_2, 24f * uiScale));
		Vector2 itemRectMin = ImGui.GetItemRectMin();
		Vector2 itemRectMax = ImGui.GetItemRectMax();
		return new PrefixGeometry(itemRectMin, itemRectMax, x, vector, text, vector2, vector3);
	}

	private IconButtonGeometry _00A0(string P_0, UiListCard P_1, int P_2, IReadOnlyList<UiListCard> P_3)
	{
		float uiScale = Theme.UiScale;
		Vector2 size = new Vector2(24f * uiScale, 24f * uiScale);
		ImGui.SetCursorScreenPos(ImGui.GetCursorScreenPos());
		ImU8String strId = new ImU8String(2, 1);
		strId.AppendLiteral("##");
		strId.AppendFormatted(P_0);
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
		if (P_1.Draggable && ImGui.BeginDragDropSource())
		{
			_2001_2014 = P_2;
			ImGui.SetDragDropPayload(_2001_2028, default);
			ImGui.TextUnformatted(P_1.Title);
			ImGui.EndDragDropSource();
		}
		if (flag)
		{
			ImGui.SetTooltip(P_1.Draggable ? "按住拖动调整顺序" : "此条目不可拖动");
		}
		return new IconButtonGeometry(P_2, CardIconKind.Handle, itemRectMin, itemRectMax, P_1.Draggable);
	}

	private IconButtonGeometry _00A0(string P_0, int P_1, bool P_2, UiListCard P_3, int P_4)
	{
		float uiScale = Theme.UiScale;
		Vector2 size = new Vector2(24f * uiScale, 24f * uiScale);
		bool flag = P_1 <= 0;
		bool flag2 = P_1 >= P_4 - 1;
		bool flag3 = P_3.Draggable && (P_2 ? (!flag) : (!flag2));
		string text;
		if (P_2)
		{
			text = (flag ? "上移（已是第一项）" : "上移");
		}
		else
		{
			text = (flag2 ? "下移（已是最后一项）" : "下移");
		}
		if (!P_3.Draggable)
		{
			text = (P_2 ? "上移（此条目不可排序）" : "下移（此条目不可排序）");
		}
		ImGui.GetCursorScreenPos();
		if (!flag3)
		{
			ImGui.BeginDisabled();
		}
		bool flag4 = ImGui.InvisibleButton(P_0, size);
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
		Vector4 c;
		if (flag3)
		{
			c = (flag5 ? Theme.BrandGold : Theme.TextSecondary);
		}
		else
		{
			c = Theme.TextDisabled;
		}
		Vector2 center = (itemRectMin + itemRectMax) * 0.5f;
		float size2 = 24f * uiScale * 0.38f;
		if (P_2)
		{
			UiIcons.DrawChevronUp(windowDrawList, center, size2, Theme.U32(c));
		}
		else
		{
			UiIcons.DrawChevronDown(windowDrawList, center, size2, Theme.U32(c));
		}
		if (flag5)
		{
			UiButton.ShowTooltip(text);
		}
		if (flag4 & flag3)
		{
			_2001_2022 = P_1;
			_2001_2024 = (P_2 ? (P_1 - 1) : (P_1 + 1));
		}
		return new IconButtonGeometry(P_1, P_2 ? CardIconKind.Up : CardIconKind.Down, itemRectMin, itemRectMax, flag3);
	}

	private void _00A0(string P_0, UiListCard P_1, int P_2, float P_3)
	{
		float uiScale = Theme.UiScale;
		ImDrawListPtr windowDrawList = ImGui.GetWindowDrawList();
		Vector2 cursorScreenPos = ImGui.GetCursorScreenPos();
		bool flag = !string.IsNullOrEmpty(P_1.Subtitle);
		Vector2 vector = ImGui.CalcTextSize(P_1.Title);
		Vector2 vector2 = (flag ? ImGui.CalcTextSize(P_1.Subtitle) : Vector2.Zero);
		float y;
		if (flag)
		{
			Theme.DrawText(windowDrawList, cursorScreenPos, Theme.TextPrimary, Theme.Ellipsis(P_1.Title, P_3), Theme.FsBody);
			Theme.DrawText(windowDrawList, new Vector2(cursorScreenPos.X, cursorScreenPos.Y + vector.Y + 1f * uiScale), Theme.TextSecondary, Theme.Ellipsis(P_1.Subtitle, P_3, Theme.FsCaption), Theme.FsCaption);
			y = vector.Y + vector2.Y + 1f * uiScale;
		}
		else
		{
			float y2 = cursorScreenPos.Y + Math.Max(0f, (24f * uiScale - Theme.FsBody) * 0.5f);
			Theme.DrawText(windowDrawList, new Vector2(cursorScreenPos.X, y2), Theme.TextPrimary, Theme.Ellipsis(P_1.Title, P_3), Theme.FsBody);
			y = Math.Max(vector.Y, 24f * uiScale);
		}
		ImGui.SetCursorScreenPos(cursorScreenPos);
		ImU8String strId = new ImU8String(5, 1);
		strId.AppendLiteral("##");
		strId.AppendFormatted(P_0);
		strId.AppendLiteral("hit");
		ImGui.InvisibleButton(strId, new Vector2(P_3, y));
		Vector2 itemRectMin = ImGui.GetItemRectMin();
		Vector2 itemRectMax = ImGui.GetItemRectMax();
		if (P_1.Tooltip != null && ImGui.IsMouseHoveringRect(itemRectMin, itemRectMax, clip: true))
		{
			UiButton.ShowTooltip(P_1.Tooltip);
		}
	}

	private IconButtonGeometry _00A0(string P_0, UiListCard P_1, int P_2)
	{
		float uiScale = Theme.UiScale;
		Vector2 size = new Vector2(24f * uiScale, 24f * uiScale);
		Vector2 cursorScreenPos = ImGui.GetCursorScreenPos();
		ImGui.SetCursorScreenPos(cursorScreenPos);
		ImU8String strId = new ImU8String(2, 1);
		strId.AppendLiteral("##");
		strId.AppendFormatted(P_0);
		bool flag = ImGui.InvisibleButton(strId, size);
		Vector2 itemRectMin = ImGui.GetItemRectMin();
		Vector2 itemRectMax = ImGui.GetItemRectMax();
		bool flag2 = ImGui.IsItemHovered();
		bool flag3 = UiDeleteStyle.IsArmed(_2001_2025, P_2, _2001_2027);
		UiDeleteStyle.Draw(ImGui.GetWindowDrawList(), cursorScreenPos, size, flag3, flag2, uiScale);
		if (flag2)
		{
			ImGui.SetTooltip(flag3 ? "再点一次确认删除" : P_1.DeleteTooltip);
		}
		if (flag)
		{
			if (flag3)
			{
				_2001_2025 = -1;
				P_1.OnDelete?.Invoke(P_2);
			}
			else
			{
				_2001_2025 = P_2;
				_2001_2027 = UiDeleteStyle.ArmNow();
			}
		}
		return new IconButtonGeometry(P_2, CardIconKind.Delete, itemRectMin, itemRectMax, enabled: true);
	}
}
