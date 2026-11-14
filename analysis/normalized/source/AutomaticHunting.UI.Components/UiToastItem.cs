using System;
using System.Numerics;

namespace AutomaticHunting.UI.Components;

public sealed class UiToastItem
{
	public string Text = string.Empty;

	public Vector4 Color = Theme.TextPrimary;

	public string Glyph = "·";

	public Action? Undo;

	public double ExpireAt;
}
