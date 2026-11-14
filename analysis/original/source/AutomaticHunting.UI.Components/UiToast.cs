using System;
using System.Collections.Generic;
using System.Numerics;
using System.Runtime.CompilerServices;
using Dalamud.Bindings.ImGui;

namespace AutomaticHunting.UI.Components;

public static class UiToast
{
	[CompilerGenerated]
	private sealed class _00A0
	{
		public double _2008_2058;

		internal bool _00A0(UiToastItem P_0)
		{
			return P_0.ExpireAt <= _2008_2058;
		}
	}

	private const int _2001_206A = 3;

	private const double _2001_206B = 5.0;

	private static readonly List<UiToastItem> _2001_206C = new List<UiToastItem>();

	public static void Push(string text, Vector4 color, string glyph = "·", Action? undo = null)
	{
		_2001_206C.Add(new UiToastItem
		{
			Text = text,
			Color = color,
			Glyph = glyph,
			Undo = undo,
			ExpireAt = ImGui.GetTime() + 5.0
		});
		while (_2001_206C.Count > 3)
		{
			_2001_206C.RemoveAt(0);
		}
	}

	public static void Success(string text, Action? undo = null)
	{
		Push(text, Theme.StatusSuccess, "✓", undo);
	}

	public static void Failure(string text)
	{
		Push(text, Theme.StatusDanger, "✕");
	}

	public static void Info(string text)
	{
		Push(text, Theme.StatusInfo);
	}

	public static void Warning(string text)
	{
		Push(text, Theme.StatusWarning, "◆");
	}

	public static void Draw()
	{
		if (_2001_206C.Count == 0)
		{
			return;
		}
		double time = ImGui.GetTime();
		_2001_206C.RemoveAll((UiToastItem P_0) => P_0.ExpireAt <= time);
		if (_2001_206C.Count == 0)
		{
			return;
		}
		float uiScale = Theme.UiScale;
		ImDrawListPtr windowDrawList = ImGui.GetWindowDrawList();
		float num = Theme.ContentRightX();
		float num2 = ImGui.GetWindowPos().Y + ImGui.GetContentRegionMax().Y;
		for (int num3 = _2001_206C.Count - 1; num3 >= 0; num3--)
		{
			UiToastItem uiToastItem = _2001_206C[num3];
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
					_2001_206C.Remove(uiToastItem);
				}
			}
		}
	}
}
