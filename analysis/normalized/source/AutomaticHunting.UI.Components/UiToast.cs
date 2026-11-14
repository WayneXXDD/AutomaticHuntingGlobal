using System;
using System.Collections.Generic;
using System.Numerics;
using System.Runtime.CompilerServices;
using Dalamud.Bindings.ImGui;

namespace AutomaticHunting.UI.Components;

public static class UiToast
{
	[CompilerGenerated]
	private sealed class RecoveredType_200
	{
		public double field_1187;

		internal bool Method_1208(UiToastItem arg0)
		{
			return arg0.ExpireAt <= field_1187;
		}
	}

	private const int field_1184 = 3;

	private const double field_1185 = 5.0;

	private static readonly List<UiToastItem> field_1186 = new List<UiToastItem>();

	public static void Push(string arg0, Vector4 arg1, string arg2 = "·", Action? arg3 = null)
	{
		field_1186.Add(new UiToastItem
		{
			Text = arg0,
			Color = arg1,
			Glyph = arg2,
			Undo = arg3,
			ExpireAt = ImGui.GetTime() + 5.0
		});
		while (field_1186.Count > 3)
		{
			field_1186.RemoveAt(0);
		}
	}

	public static void Success(string arg0, Action? arg1 = null)
	{
		Push(arg0, Theme.StatusSuccess, "✓", arg1);
	}

	public static void Failure(string arg0)
	{
		Push(arg0, Theme.StatusDanger, "✕");
	}

	public static void Info(string arg0)
	{
		Push(arg0, Theme.StatusInfo);
	}

	public static void Warning(string arg0)
	{
		Push(arg0, Theme.StatusWarning, "◆");
	}

	public static void Draw()
	{
		if (field_1186.Count == 0)
		{
			return;
		}
		double field_1187 = ImGui.GetTime();
		field_1186.RemoveAll((UiToastItem arg0) => arg0.ExpireAt <= field_1187);
		if (field_1186.Count == 0)
		{
			return;
		}
		float uiScale = Theme.UiScale;
		ImDrawListPtr windowDrawList = ImGui.GetWindowDrawList();
		float num = Theme.ContentRightX();
		float num2 = ImGui.GetWindowPos().Y + ImGui.GetContentRegionMax().Y;
		for (int num3 = field_1186.Count - 1; num3 >= 0; num3--)
		{
			UiToastItem uiToastItem = field_1186[num3];
			float num4 = ImGui.CalcTextSize(uiToastItem.Text).X + 40f * uiScale;
			float num5 = 28f * uiScale;
			num2 -= num5 + 4f * uiScale;
			Vector2 pMin = new Vector2(num - num4, num2);
			Vector2 pMax = new Vector2(num, num2 + num5);
			windowDrawList.AddRectFilled(pMin, pMax, Theme.U32(Theme.SurfacePopup), 6f * uiScale);
			windowDrawList.AddRect(pMin, pMax, Theme.U32(Theme.WithAlpha(uiToastItem.Color, 0.55f)), 6f * uiScale, 1f);
			Vector2 vector = ImGui.CalcTextSize(uiToastItem.Glyph);
			Theme.DrawText(windowDrawList, new Vector2(pMin.X + 10f * uiScale, pMin.Y + (num5 - vector.Y) * 0.5f), uiToastItem.Color, uiToastItem.Glyph);
			Theme.DrawText(windowDrawList, new Vector2(pMin.X + 10f * uiScale + vector.X + 6f * uiScale, pMin.Y + (num5 - vector.Y) * 0.5f), Theme.TextPrimary, Theme.Ellipsis(uiToastItem.Text, num4 - 40f * uiScale - vector.X - 6f * uiScale));
			if (uiToastItem.Undo != null)
			{
				ImGui.SetCursorScreenPos(new Vector2(pMax.X - 52f * uiScale, pMin.Y + 4f * uiScale));
				if (UiButton.Draw("撤销", UiVariant.Link, new Vector2(44f * uiScale, num5 - 8f * uiScale)))
				{
					uiToastItem.Undo();
					field_1186.Remove(uiToastItem);
				}
			}
		}
	}
}
