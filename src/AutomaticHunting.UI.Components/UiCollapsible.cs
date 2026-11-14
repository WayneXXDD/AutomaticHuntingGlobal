using System.Numerics;
using System.Runtime.CompilerServices;
using Dalamud.Bindings.ImGui;

namespace AutomaticHunting.UI.Components;

public sealed class UiCollapsible
{
	private readonly string field_1104;

	[CompilerGenerated]
	private string field_1105;

	[CompilerGenerated]
	private bool field_1106;

	[CompilerGenerated]
	private string? field_1107;

	public string Title
	{
		[CompilerGenerated]
		get
		{
			return field_1105;
		}
		[CompilerGenerated]
		set
		{
			field_1105 = value;
		}
	}

	public bool IsOpen
	{
		[CompilerGenerated]
		get
		{
			return field_1106;
		}
		[CompilerGenerated]
		set
		{
			field_1106 = value;
		}
	}

	public string? Summary
	{
		[CompilerGenerated]
		get
		{
			return field_1107;
		}
		[CompilerGenerated]
		set
		{
			field_1107 = value;
		}
	}

	public UiCollapsible(string arg0, string arg1, bool arg2 = false)
	{
		field_1104 = arg0;
		Title = arg1;
		IsOpen = arg2;
	}

	public bool Begin()
	{
		float uiScale = Theme.UiScale;
		float textLineHeight = ImGui.GetTextLineHeight();
		Vector2 cursorScreenPos = ImGui.GetCursorScreenPos();
		ImDrawListPtr windowDrawList = ImGui.GetWindowDrawList();
		windowDrawList.AddRectFilled(new Vector2(cursorScreenPos.X, cursorScreenPos.Y + 1f * uiScale), new Vector2(cursorScreenPos.X + 3f * uiScale, cursorScreenPos.Y + textLineHeight - 1f * uiScale), Theme.U32(Theme.BrandGold), 1.5f * uiScale);
		float num = cursorScreenPos.X + 9f * uiScale;
		float num2 = 5f * uiScale;
		Vector2 vector = new Vector2(num + num2 * 0.5f, cursorScreenPos.Y + textLineHeight * 0.5f);
		if (IsOpen)
		{
			windowDrawList.AddTriangleFilled(new Vector2(vector.X, vector.Y + num2 * 0.5f), new Vector2(vector.X - num2 * 0.6f, vector.Y - num2 * 0.4f), new Vector2(vector.X + num2 * 0.6f, vector.Y - num2 * 0.4f), Theme.U32(Theme.BrandGold));
		}
		else
		{
			windowDrawList.AddTriangleFilled(new Vector2(vector.X + num2 * 0.5f, vector.Y), new Vector2(vector.X - num2 * 0.4f, vector.Y - num2 * 0.6f), new Vector2(vector.X - num2 * 0.4f, vector.Y + num2 * 0.6f), Theme.U32(Theme.TextSecondary));
		}
		float num3 = num + num2 + 6f * uiScale;
		Theme.DrawText(windowDrawList, new Vector2(num3, cursorScreenPos.Y), Theme.TextPrimary, Title, Theme.FsSubtitle);
		ImGui.SetCursorScreenPos(new Vector2(num3 + TraditionalText.CalcTextSize(Title).X + 8f * uiScale, cursorScreenPos.Y));
		ImGui.Dummy(new Vector2(1f, textLineHeight));
		if (!string.IsNullOrEmpty(Summary))
		{
			Vector2 vector2 = TraditionalText.CalcTextSize(Summary);
			float x = Theme.ContentRightX() - vector2.X;
			Theme.DrawText(windowDrawList, new Vector2(x, cursorScreenPos.Y + (textLineHeight - vector2.Y) * 0.5f), Theme.TextSecondary, Summary, Theme.FsCaption);
		}
		ImGui.SetCursorScreenPos(cursorScreenPos);
		ImU8String strId = new ImU8String(10, 1);
		strId.AppendFormatted(field_1104);
		strId.AppendLiteral("##collapse");
		if (ImGui.InvisibleButton(strId, new Vector2(ImGui.GetContentRegionAvail().X, textLineHeight)))
		{
			IsOpen = !IsOpen;
		}
		ImGui.Dummy(new Vector2(1f, 8f * uiScale));
		return IsOpen;
	}
}
