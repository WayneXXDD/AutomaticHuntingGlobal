using System;
using System.Numerics;
using Dalamud.Bindings.ImGui;

namespace AutomaticHunting.UI.Components;

public sealed class UiAdjustRow
{
	public const float ButtonSize = 26f;

	public const float RowHeight = 26f;

	private const float field_992 = 10f;

	private const float field_993 = 4f;

	private const float field_994 = 8f;

	private const float field_995 = 0.3f;

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

	public void Draw(string arg0)
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
		string text = Theme.Ellipsis(arg1: Math.Max(0f, num3 - cursorScreenPos.X - num2 - num5), arg0: Text);
		Vector2 vector = ImGui.CalcTextSize(text);
		float num6 = cursorScreenPos.Y + (num - vector.Y) * 0.5f;
		Theme.DrawText(ImGui.GetWindowDrawList(), new Vector2(cursorScreenPos.X, num6), TextColor, text, Theme.FsBody);
		Vector2 arg1 = new Vector2(cursorScreenPos.X, num6);
		Vector2 arg2 = new Vector2(cursorScreenPos.X + vector.X, num6 + vector.Y);
		Vector2 vector2 = Vector2.Zero;
		Vector2 vector3 = Vector2.Zero;
		Vector2 vector4 = Vector2.Zero;
		Vector2 vector5 = Vector2.Zero;
		float x = arg2.X + 10f * uiScale;
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
		bool arg3 = false;
		bool arg4 = false;
		if (ShowIncrease)
		{
			ImGui.SetCursorScreenPos(vector2);
			ImU8String strId = new ImU8String(3, 1);
			strId.AppendFormatted(arg0);
			strId.AppendLiteral("inc");
			bool flag2 = ImGui.InvisibleButton(strId, vector3 - vector2);
			arg3 = ImGui.IsItemActive();
			if (flag2 && IncreaseEnabled)
			{
				OnAdjust?.Invoke(1);
			}
		}
		if (ShowDecrease)
		{
			ImGui.SetCursorScreenPos(vector4);
			ImU8String strId2 = new ImU8String(3, 1);
			strId2.AppendFormatted(arg0);
			strId2.AppendLiteral("dec");
			bool flag3 = ImGui.InvisibleButton(strId2, vector5 - vector4);
			arg4 = ImGui.IsItemActive();
			if (flag3 && DecreaseEnabled)
			{
				OnAdjust?.Invoke(-1);
			}
		}
		if (ShowIncrease)
		{
			Method_1026(vector2, vector3, arg2: true, IncreaseEnabled, arg3, IncreaseTooltip);
		}
		if (ShowDecrease)
		{
			Method_1026(vector4, vector5, arg2: false, DecreaseEnabled, arg4, DecreaseTooltip);
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
		GeometryProbe?.Invoke(new AdjustRowGeometry(arg1, arg2, vector2, vector3, vector4, vector5, IncreaseEnabled, DecreaseEnabled));
	}

	private static void Method_1026(Vector2 arg0, Vector2 arg1, bool arg2, bool arg3, bool arg4, string arg5)
	{
		float uiScale = Theme.UiScale;
		ImDrawListPtr windowDrawList = ImGui.GetWindowDrawList();
		bool flag = ImGui.IsMouseHoveringRect(arg0, arg1, clip: true);
		bool flag2 = flag & arg3 & arg4;
		Vector4 arg6;
		if (arg3)
		{
			if (flag2)
			{
				arg6 = Theme.BtnNeutralActive;
			}
			else
			{
				arg6 = (flag ? Theme.BtnNeutralHover : Theme.BtnNeutral);
			}
		}
		else
		{
			arg6 = Theme.WithAlpha(Theme.BtnNeutral, 0.45f);
		}
		windowDrawList.AddRectFilled(arg0, arg1, Theme.U32(arg6), 4f * uiScale);
		Vector4 arg7;
		if (arg3)
		{
			arg7 = (flag ? Theme.BrandGold : Theme.BorderControl);
		}
		else
		{
			arg7 = Theme.WithAlpha(Theme.BorderControl, 0.45f);
		}
		windowDrawList.AddRect(arg0, arg1, Theme.U32(arg7), 4f * uiScale, 1f);
		Vector4 arg8;
		if (arg3)
		{
			arg8 = (flag ? Theme.BrandGold : Theme.TextPrimary);
		}
		else
		{
			arg8 = Theme.TextDisabled;
		}
		Vector2 arg9 = (arg0 + arg1) * 0.5f;
		float arg10 = (arg1.X - arg0.X) * 0.3f;
		float arg11 = Math.Max(1f, 1.5f * uiScale);
		if (arg2)
		{
			UiIcons.DrawPlus(windowDrawList, arg9, arg10, arg11, Theme.U32(arg8));
		}
		else
		{
			UiIcons.DrawMinus(windowDrawList, arg9, arg10, arg11, Theme.U32(arg8));
		}
		if (flag && !string.IsNullOrEmpty(arg5))
		{
			UiButton.ShowTooltip(arg5);
		}
	}
}
