using System;
using System.Collections.Generic;

namespace AutomaticHunting.UI.Components;

public sealed class UiListCard
{
	public string Id = string.Empty;

	public string Title = string.Empty;

	public string? Subtitle;

	public string? Tooltip;

	public List<UiListCardPrefix> Prefixes = new List<UiListCardPrefix>();

	public bool Draggable = true;

	public bool Selected;

	public List<UiListCardAction> Actions = new List<UiListCardAction>();

	public List<Func<string, bool>> ExtraColumns = new List<Func<string, bool>>();

	public Action<int>? OnDelete;

	public string DeleteTooltip = "刪除此設定";
}
