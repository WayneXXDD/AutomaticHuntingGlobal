using System;
using System.Collections.Generic;
using System.Numerics;
using Dalamud.Bindings.ImGui;

namespace AutomaticHunting.UI.Components;

public static class UiHelpText
{
	public const string ListBullet = "  ·  ";

	public static float Draw(ImDrawListPtr arg0, string arg1, Vector2 arg2, float arg3, float arg4)
	{
		if (string.IsNullOrEmpty(arg1))
		{
			return 0f;
		}
		if (arg1.StartsWith("  ·  ", StringComparison.Ordinal))
		{
			float num = Theme.MeasureTextWidth("  ·  ", Theme.FsCaption);
			string arg5 = arg1.Substring("  ·  ".Length);
			float arg6 = Math.Max(20f, arg3 - num);
			List<string> list = Theme.WrapText(arg5, arg6, Theme.FsCaption);
			Theme.DrawText(arg0, arg2, Theme.TextSecondary, "  ·  ", Theme.FsCaption);
			for (int i = 0; i < list.Count; i++)
			{
				Theme.DrawText(arg0, new Vector2(arg2.X + num, arg2.Y + (float)i * arg4), Theme.TextSecondary, list[i], Theme.FsCaption);
			}
			return (float)list.Count * arg4;
		}
		List<string> list2 = Theme.WrapText(arg1, arg3, Theme.FsCaption);
		for (int j = 0; j < list2.Count; j++)
		{
			Theme.DrawText(arg0, new Vector2(arg2.X, arg2.Y + (float)j * arg4), Theme.TextSecondary, list2[j], Theme.FsCaption);
		}
		return (float)list2.Count * arg4;
	}
}
