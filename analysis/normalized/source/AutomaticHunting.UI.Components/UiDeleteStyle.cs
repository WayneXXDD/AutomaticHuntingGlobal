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

	public static bool IsArmed(int arg0, int arg1, DateTime arg2)
	{
		if (arg0 == arg1)
		{
			return DateTime.UtcNow < arg2;
		}
		return false;
	}

	public static void Draw(ImDrawListPtr arg0, Vector2 arg1, Vector2 arg2, bool arg3, bool arg4, float arg5)
	{
		if (arg3)
		{
			arg0.AddRectFilled(arg1, arg1 + arg2, Theme.U32(Theme.BtnDangerSolid), 4f * arg5);
			UiIcons.DrawCross(arg0, arg1 + arg2 * 0.5f, arg2.X * 0.5f, Theme.U32(Vector4.One));
			return;
		}
		if (arg4)
		{
			arg0.AddRectFilled(arg1, arg1 + arg2, Theme.U32(Theme.WithAlpha(Theme.StatusDanger, 0.18f)), 4f * arg5);
		}
		UiIcons.DrawCross(arg0, arg1 + arg2 * 0.5f, arg2.X * 0.45f, Theme.U32(arg4 ? Theme.StatusDanger : Theme.TextDisabled));
	}
}
