using System;
using System.Numerics;
using Dalamud.Bindings.ImGui;

namespace AutomaticHunting.UI.Components;

public static class UiDeleteStyle
{
	public const double ConfirmSeconds = 4.0;

	public const string RetryTooltip = "再点一次确认删除";

	public static DateTime ArmNow()
	{
		return DateTime.UtcNow.AddSeconds(4.0);
	}

	public static bool IsArmed(int armedIndex, int index, DateTime armedUntil)
	{
		if (armedIndex == index)
		{
			return DateTime.UtcNow < armedUntil;
		}
		return false;
	}

	public static void Draw(ImDrawListPtr dl, Vector2 min, Vector2 size, bool armed, bool hovered, float s)
	{
		if (armed)
		{
			dl.AddRectFilled(min, min + size, Theme.U32(Theme.BtnDangerSolid), 4f * s);
			UiIcons.DrawCross(dl, min + size * 0.5f, size.X * 0.5f, Theme.U32(Vector4.One));
			return;
		}
		if (hovered)
		{
			dl.AddRectFilled(min, min + size, Theme.U32(Theme.WithAlpha(Theme.StatusDanger, 0.18f)), 4f * s);
		}
		UiIcons.DrawCross(dl, min + size * 0.5f, size.X * 0.45f, Theme.U32(hovered ? Theme.StatusDanger : Theme.TextDisabled));
	}
}
