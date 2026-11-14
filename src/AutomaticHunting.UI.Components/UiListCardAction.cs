using System;

namespace AutomaticHunting.UI.Components;

public sealed class UiListCardAction
{
	public string Label = string.Empty;

	public UiVariant Variant;

	public string? Tooltip;

	public Action<int>? OnClick;

	public bool Enabled = true;
}
