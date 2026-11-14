using System.Numerics;
using System.Runtime.CompilerServices;
using Dalamud.Bindings.ImGui;

namespace AutomaticHunting.UI.Components;

public sealed class UiCollapsible
{
	private readonly string _2001_202A;

	[CompilerGenerated]
	private string _2001_202B;

	[CompilerGenerated]
	private bool _2001_202C;

	[CompilerGenerated]
	private string? _2001_202D;

	public string Title
	{
		[CompilerGenerated]
		get
		{
			return _2001_202B;
		}
		[CompilerGenerated]
		set
		{
			_2001_202B = value;
		}
	}

	public bool IsOpen
	{
		[CompilerGenerated]
		get
		{
			return _2001_202C;
		}
		[CompilerGenerated]
		set
		{
			_2001_202C = value;
		}
	}

	public string? Summary
	{
		[CompilerGenerated]
		get
		{
			return _2001_202D;
		}
		[CompilerGenerated]
		set
		{
			_2001_202D = value;
		}
	}

	public UiCollapsible(string id, string title, bool defaultOpen = false)
	{
		_2001_202A = id;
		Title = title;
		IsOpen = defaultOpen;
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
		ImGui.SetCursorScreenPos(new Vector2(num3 + ImGui.CalcTextSize(Title).X + 8f * uiScale, cursorScreenPos.Y));
		ImGui.Dummy(new Vector2(1f, textLineHeight));
		if (!string.IsNullOrEmpty(Summary))
		{
			Vector2 vector2 = ImGui.CalcTextSize(Summary);
			float x = Theme.ContentRightX() - vector2.X;
			Theme.DrawText(windowDrawList, new Vector2(x, cursorScreenPos.Y + (textLineHeight - vector2.Y) * 0.5f), Theme.TextSecondary, Summary, Theme.FsCaption);
		}
		ImGui.SetCursorScreenPos(cursorScreenPos);
		ImU8String strId = new ImU8String(10, 1);
		strId.AppendFormatted(_2001_202A);
		strId.AppendLiteral("##collapse");
		if (ImGui.InvisibleButton(strId, new Vector2(ImGui.GetContentRegionAvail().X, textLineHeight)))
		{
			IsOpen = !IsOpen;
		}
		ImGui.Dummy(new Vector2(1f, 8f * uiScale));
		return IsOpen;
	}
}
