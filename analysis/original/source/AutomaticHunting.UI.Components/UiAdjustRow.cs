using System;
using System.Numerics;
using Dalamud.Bindings.ImGui;

namespace AutomaticHunting.UI.Components;

public sealed class UiAdjustRow
{
	public const float ButtonSize = 26f;

	public const float RowHeight = 26f;

	private const float _2001_00A0 = 10f;

	private const float _2001_1680 = 4f;

	private const float _2001_2000 = 8f;

	private const float _2001_2001 = 0.3f;

	public string Text = string.Empty;

	public Vector4 TextColor = Theme.TextDisabled;

	public string TrailingLabel = string.Empty;

	public bool TrailingEnabled = true;

	public string TrailingTooltip = string.Empty;

	public Action? OnTrailing;

	public bool ShowIncrease = true;

	public bool ShowDecrease = true;

	public bool IncreaseEnabled = true;

	public bool DecreaseEnabled = true;

	public string IncreaseTooltip = string.Empty;

	public string DecreaseTooltip = string.Empty;

	public Action<int>? OnAdjust;

	public Action<AdjustRowGeometry>? GeometryProbe;

	public Action<Vector2, Vector2>? TrailingButtonProbe;

	public void Draw(string id)
	{
		float uiScale = Theme.UiScale;
		Vector2 cursorScreenPos = ImGui.GetCursorScreenPos();
		float num = 26f * uiScale;
		float num2 = 0f;
		if (ShowIncrease)
		{
			num2 += 26f * uiScale + 10f * uiScale;
		}
		if (ShowDecrease)
		{
			num2 += 26f * uiScale + (ShowIncrease ? (4f * uiScale) : (10f * uiScale));
		}
		bool flag = !string.IsNullOrEmpty(TrailingLabel);
		float num3 = Theme.ContentRightX();
		float num4 = 0f;
		float num5 = 0f;
		if (flag)
		{
			num4 = ImGui.CalcTextSize(TrailingLabel).X + 16f * uiScale;
			num5 = num4 + 10f * uiScale;
		}
		string text = Theme.Ellipsis(maxWidth: Math.Max(0f, num3 - cursorScreenPos.X - num2 - num5), text: Text);
		Vector2 vector = ImGui.CalcTextSize(text);
		float num6 = cursorScreenPos.Y + (num - vector.Y) * 0.5f;
		Theme.DrawText(ImGui.GetWindowDrawList(), new Vector2(cursorScreenPos.X, num6), TextColor, text, Theme.FsBody);
		Vector2 textMin = new Vector2(cursorScreenPos.X, num6);
		Vector2 textMax = new Vector2(cursorScreenPos.X + vector.X, num6 + vector.Y);
		Vector2 vector2 = Vector2.Zero;
		Vector2 vector3 = Vector2.Zero;
		Vector2 vector4 = Vector2.Zero;
		Vector2 vector5 = Vector2.Zero;
		float x = textMax.X + 10f * uiScale;
		if (ShowIncrease)
		{
			vector2 = new Vector2(x, cursorScreenPos.Y);
			vector3 = vector2 + new Vector2(26f * uiScale, num);
			x = vector3.X + 4f * uiScale;
		}
		if (ShowDecrease)
		{
			vector4 = new Vector2(x, cursorScreenPos.Y);
			vector5 = vector4 + new Vector2(26f * uiScale, num);
		}
		bool flag2 = false;
		bool flag3 = false;
		if (ShowIncrease)
		{
			ImGui.SetCursorScreenPos(vector2);
			ImU8String strId = new ImU8String(3, 1);
			strId.AppendFormatted(id);
			strId.AppendLiteral("inc");
			bool flag4 = ImGui.InvisibleButton(strId, vector3 - vector2);
			flag2 = ImGui.IsItemActive();
			if (flag4 && IncreaseEnabled)
			{
				OnAdjust?.Invoke(1);
			}
		}
		if (ShowDecrease)
		{
			ImGui.SetCursorScreenPos(vector4);
			ImU8String strId2 = new ImU8String(3, 1);
			strId2.AppendFormatted(id);
			strId2.AppendLiteral("dec");
			bool flag5 = ImGui.InvisibleButton(strId2, vector5 - vector4);
			flag3 = ImGui.IsItemActive();
			if (flag5 && DecreaseEnabled)
			{
				OnAdjust?.Invoke(-1);
			}
		}
		if (ShowIncrease)
		{
			_00A0(vector2, vector3, true, IncreaseEnabled, flag2, IncreaseTooltip);
		}
		if (ShowDecrease)
		{
			_00A0(vector4, vector5, false, DecreaseEnabled, flag3, DecreaseTooltip);
		}
		if (flag)
		{
			ImGui.SetCursorScreenPos(new Vector2(num3 - num4, cursorScreenPos.Y));
			if (UiButton.Draw(TrailingLabel, UiVariant.Danger, new Vector2(num4, num), TrailingTooltip, TrailingEnabled))
			{
				OnTrailing?.Invoke();
			}
			TrailingButtonProbe?.Invoke(ImGui.GetItemRectMin(), ImGui.GetItemRectMax());
		}
		ImGui.SetCursorScreenPos(new Vector2(cursorScreenPos.X, cursorScreenPos.Y + num));
		GeometryProbe?.Invoke(new AdjustRowGeometry(textMin, textMax, vector2, vector3, vector4, vector5, IncreaseEnabled, DecreaseEnabled));
	}

	private static void _00A0(Vector2 P_0, Vector2 P_1, bool P_2, bool P_3, bool P_4, string P_5)
	{
		float uiScale = Theme.UiScale;
		ImDrawListPtr windowDrawList = ImGui.GetWindowDrawList();
		bool flag = ImGui.IsMouseHoveringRect(P_0, P_1, clip: true);
		bool flag2 = flag & P_3 & P_4;
		Vector4 c;
		if (P_3)
		{
			if (flag2)
			{
				c = Theme.BtnNeutralActive;
			}
			else
			{
				c = (flag ? Theme.BtnNeutralHover : Theme.BtnNeutral);
			}
		}
		else
		{
			c = Theme.WithAlpha(Theme.BtnNeutral, 0.45f);
		}
		windowDrawList.AddRectFilled(P_0, P_1, Theme.U32(c), 4f * uiScale);
		Vector4 c2;
		if (P_3)
		{
			c2 = (flag ? Theme.BrandGold : Theme.BorderControl);
		}
		else
		{
			c2 = Theme.WithAlpha(Theme.BorderControl, 0.45f);
		}
		windowDrawList.AddRect(P_0, P_1, Theme.U32(c2), 4f * uiScale, 1f);
		Vector4 c3;
		if (P_3)
		{
			c3 = (flag ? Theme.BrandGold : Theme.TextPrimary);
		}
		else
		{
			c3 = Theme.TextDisabled;
		}
		Vector2 center = (P_0 + P_1) * 0.5f;
		float size = (P_1.X - P_0.X) * 0.3f;
		float thickness = Math.Max(1f, 1.5f * uiScale);
		if (P_2)
		{
			UiIcons.DrawPlus(windowDrawList, center, size, thickness, Theme.U32(c3));
		}
		else
		{
			UiIcons.DrawMinus(windowDrawList, center, size, thickness, Theme.U32(c3));
		}
		if (flag && !string.IsNullOrEmpty(P_5))
		{
			UiButton.ShowTooltip(P_5);
		}
	}
}
