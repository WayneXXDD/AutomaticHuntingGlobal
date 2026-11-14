using System;
using System.Collections.Generic;
using System.Numerics;
using Dalamud.Bindings.ImGui;

namespace AutomaticHunting.UI.Components;

public static class UiHelpText
{
	public const string ListBullet = "  ·  ";

	public static float Draw(ImDrawListPtr dl, string line, Vector2 origin, float available, float lineH)
	{
		if (string.IsNullOrEmpty(line))
		{
			return 0f;
		}
		if (line.StartsWith("  ·  ", StringComparison.Ordinal))
		{
			float num = Theme.MeasureTextWidth("  ·  ", Theme.FsCaption);
			string text = line.Substring("  ·  ".Length);
			float maxWidth = Math.Max(20f, available - num);
			List<string> list = Theme.WrapText(text, maxWidth, Theme.FsCaption);
			Theme.DrawText(dl, origin, Theme.TextSecondary, "  ·  ", Theme.FsCaption);
			for (int i = 0; i < list.Count; i++)
			{
				Theme.DrawText(dl, new Vector2(origin.X + num, origin.Y + (float)i * lineH), Theme.TextSecondary, list[i], Theme.FsCaption);
			}
			return (float)list.Count * lineH;
		}
		List<string> list2 = Theme.WrapText(line, available, Theme.FsCaption);
		for (int j = 0; j < list2.Count; j++)
		{
			Theme.DrawText(dl, new Vector2(origin.X, origin.Y + (float)j * lineH), Theme.TextSecondary, list2[j], Theme.FsCaption);
		}
		return (float)list2.Count * lineH;
	}
}
