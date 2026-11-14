using System;
using System.Numerics;
using Dalamud.Bindings.ImGui;

namespace AutomaticHunting.UI.Components;

public sealed class UiTabs
{
	private const float _2001_205D = 30f;

	private const float _2001_205E = 14f;

	private const float _2001_205F = 4f;

	private const float _2001_2060 = 36f;

	private readonly string _2001_2061;

	private readonly string[] _2001_2062;

	private readonly Func<int> _2001_2063;

	private readonly Action<int> _2001_2064;

	public UiTabs(string id, string[] labels, Func<int> getSelected, Action<int> setSelected)
	{
		_2001_2061 = id;
		_2001_2062 = labels;
		_2001_2063 = getSelected;
		_2001_2064 = setSelected;
	}

	public void Draw()
	{
		float uiScale = Theme.UiScale;
		Vector2 cursorScreenPos = ImGui.GetCursorScreenPos();
		ImDrawListPtr windowDrawList = ImGui.GetWindowDrawList();
		int num = Math.Clamp(_2001_2063(), 0, _2001_2062.Length - 1);
		float num2 = cursorScreenPos.X;
		for (int i = 0; i < _2001_2062.Length; i++)
		{
			string text = _2001_2062[i];
			Vector2 vector = ImGui.CalcTextSize(text);
			float num3 = vector.X + 28f * uiScale;
			bool flag = i == num;
			if (flag)
			{
				Vector2 pMin = new Vector2(num2, cursorScreenPos.Y);
				Vector2 pMax = new Vector2(num2 + num3, cursorScreenPos.Y + 30f * uiScale);
				windowDrawList.AddRectFilled(pMin, pMax, Theme.U32(Theme.TabSelectedBg), 4f * uiScale);
				windowDrawList.AddRectFilled(new Vector2(num2, pMax.Y - 4f * uiScale), pMax, Theme.U32(Theme.TabSelectedBg), 0f);
			}
			bool flag2 = ImGui.IsMouseHoveringRect(new Vector2(num2, cursorScreenPos.Y), new Vector2(num2 + num3, cursorScreenPos.Y + 30f * uiScale));
			if (flag2 && !flag)
			{
				windowDrawList.AddRectFilled(new Vector2(num2, cursorScreenPos.Y), new Vector2(num2 + num3, cursorScreenPos.Y + 30f * uiScale), Theme.U32(Theme.TabHoverBg), 4f * uiScale);
			}
			Vector4 color;
			if (flag)
			{
				color = Theme.TabSelectedText;
			}
			else
			{
				color = (flag2 ? Theme.TextPrimary : Theme.TabIdleText);
			}
			float y = cursorScreenPos.Y + (30f * uiScale - vector.Y) * 0.5f;
			Theme.DrawText(windowDrawList, new Vector2(num2 + 14f * uiScale, y), color, text);
			if (flag)
			{
				float num4 = Math.Max(4f, num3 - 16f * uiScale);
				float num5 = num2 + (num3 - num4) * 0.5f;
				float num6 = cursorScreenPos.Y + 30f * uiScale - 2f * uiScale;
				windowDrawList.AddRectFilled(new Vector2(num5, num6), new Vector2(num5 + num4, num6 + 2f * uiScale), Theme.U32(Theme.TabIndicator), 0f);
			}
			ImGui.SetCursorScreenPos(new Vector2(num2, cursorScreenPos.Y));
			ImU8String strId = new ImU8String(5, 2);
			strId.AppendFormatted(_2001_2061);
			strId.AppendLiteral("##tab");
			strId.AppendFormatted(i);
			if (ImGui.InvisibleButton(strId, new Vector2(num3, 30f * uiScale)))
			{
				_2001_2064(i);
			}
			num2 += num3 + 4f * uiScale;
		}
		float y2 = cursorScreenPos.Y + 36f * uiScale - 1f;
		windowDrawList.AddLine(new Vector2(cursorScreenPos.X, y2), new Vector2(cursorScreenPos.X + ImGui.GetContentRegionAvail().X, y2), Theme.U32(Theme.BorderLine), 1f);
		ImGui.Dummy(new Vector2(1f, 36f * uiScale));
	}
}
