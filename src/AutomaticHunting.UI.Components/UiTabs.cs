using System;
using System.Numerics;
using Dalamud.Bindings.ImGui;

namespace AutomaticHunting.UI.Components;

public sealed class UiTabs
{
	private const float field_1171 = 30f;

	private const float field_1172 = 14f;

	private const float field_1173 = 4f;

	private const float field_1174 = 36f;

	private readonly string field_1175;

	private readonly string[] field_1176;

	private readonly Func<int> field_1177;

	private readonly Action<int> field_1178;

	public UiTabs(string arg0, string[] arg1, Func<int> arg2, Action<int> arg3)
	{
		field_1175 = arg0;
		field_1176 = arg1;
		field_1177 = arg2;
		field_1178 = arg3;
	}

	public void Draw()
	{
		float uiScale = Theme.UiScale;
		Vector2 cursorScreenPos = ImGui.GetCursorScreenPos();
		ImDrawListPtr windowDrawList = ImGui.GetWindowDrawList();
		int num = Math.Clamp(field_1177(), 0, field_1176.Length - 1);
		float num2 = cursorScreenPos.X;
		for (int i = 0; i < field_1176.Length; i++)
		{
			string text = field_1176[i];
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
			Vector4 arg;
			if (flag)
			{
				arg = Theme.TabSelectedText;
			}
			else
			{
				arg = (flag2 ? Theme.TextPrimary : Theme.TabIdleText);
			}
			float y = cursorScreenPos.Y + (30f * uiScale - vector.Y) * 0.5f;
			Theme.DrawText(windowDrawList, new Vector2(num2 + 14f * uiScale, y), arg, text);
			if (flag)
			{
				float num4 = Math.Max(4f, num3 - 16f * uiScale);
				float num5 = num2 + (num3 - num4) * 0.5f;
				float num6 = cursorScreenPos.Y + 30f * uiScale - 2f * uiScale;
				windowDrawList.AddRectFilled(new Vector2(num5, num6), new Vector2(num5 + num4, num6 + 2f * uiScale), Theme.U32(Theme.TabIndicator), 0f);
			}
			ImGui.SetCursorScreenPos(new Vector2(num2, cursorScreenPos.Y));
			ImU8String strId = new ImU8String(5, 2);
			strId.AppendFormatted(field_1175);
			strId.AppendLiteral("##tab");
			strId.AppendFormatted(i);
			if (ImGui.InvisibleButton(strId, new Vector2(num3, 30f * uiScale)))
			{
				field_1178(i);
			}
			num2 += num3 + 4f * uiScale;
		}
		float y2 = cursorScreenPos.Y + 36f * uiScale - 1f;
		windowDrawList.AddLine(new Vector2(cursorScreenPos.X, y2), new Vector2(cursorScreenPos.X + ImGui.GetContentRegionAvail().X, y2), Theme.U32(Theme.BorderLine), 1f);
		ImGui.Dummy(new Vector2(1f, 36f * uiScale));
	}
}
