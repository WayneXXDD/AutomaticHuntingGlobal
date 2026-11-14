using System;
using System.Collections.Generic;
using System.Numerics;
using System.Runtime.CompilerServices;
using Dalamud.Bindings.ImGui;

namespace AutomaticHunting.UI.Components;

public sealed class UiConfirm
{
	[CompilerGenerated]
	public sealed class RecoveredType_181
	{
		public UiConfirm field_1120;

		public bool field_1121;

		internal void Method_1117(int arg0)
		{
			if (arg0 == 0)
			{
				ImGui.SetKeyboardFocusHere();
				if (UiButton.Draw("取消", UiVariant.Neutral, new Vector2(ImGui.GetContentRegionAvail().X, Theme.ControlHeight)))
				{
					field_1120.OnCancel?.Invoke();
					field_1120.Method_1114();
					ImGui.CloseCurrentPopup();
				}
				return;
			}
			if (!field_1121)
			{
				ImGui.BeginDisabled();
			}
			if (UiButton.Draw(field_1120.ConfirmLabel, field_1120.ConfirmVariant, new Vector2(ImGui.GetContentRegionAvail().X, Theme.ControlHeight)))
			{
				field_1120.OnConfirm?.Invoke();
				field_1120.Method_1114();
				ImGui.CloseCurrentPopup();
			}
			if (!field_1121)
			{
				ImGui.EndDisabled();
			}
		}
	}

	[CompilerGenerated]
	private bool field_1108;

	[CompilerGenerated]
	private string field_1109 = "##uiConfirm";

	[CompilerGenerated]
	private string field_1110 = string.Empty;

	[CompilerGenerated]
	private string field_1111 = string.Empty;

	[CompilerGenerated]
	private readonly List<string> field_1112 = new List<string>();

	[CompilerGenerated]
	private string field_1113 = string.Empty;

	[CompilerGenerated]
	private string field_1114 = "確認";

	[CompilerGenerated]
	private UiVariant field_1115 = UiVariant.Danger;

	[CompilerGenerated]
	private Action? field_1116;

	[CompilerGenerated]
	private Action? field_1117;

	private string field_1118 = string.Empty;

	private bool field_1119;

	public bool Open
	{
		[CompilerGenerated]
		get
		{
			return field_1108;
		}
		[CompilerGenerated]
		private set
		{
			field_1108 = value;
		}
	}

	public string PopupId
	{
		[CompilerGenerated]
		get
		{
			return field_1109;
		}
		[CompilerGenerated]
		set
		{
			field_1109 = value;
		}
	}

	public string Title
	{
		[CompilerGenerated]
		get
		{
			return field_1110;
		}
		[CompilerGenerated]
		set
		{
			field_1110 = value;
		}
	}

	public string Message
	{
		[CompilerGenerated]
		get
		{
			return field_1111;
		}
		[CompilerGenerated]
		set
		{
			field_1111 = value;
		}
	}

	public List<string> Impact
	{
		[CompilerGenerated]
		get
		{
			return field_1112;
		}
	}

	public string ConfirmWord
	{
		[CompilerGenerated]
		get
		{
			return field_1113;
		}
		[CompilerGenerated]
		set
		{
			field_1113 = value;
		}
	}

	public string ConfirmLabel
	{
		[CompilerGenerated]
		get
		{
			return field_1114;
		}
		[CompilerGenerated]
		set
		{
			field_1114 = value;
		}
	}

	public UiVariant ConfirmVariant
	{
		[CompilerGenerated]
		get
		{
			return field_1115;
		}
		[CompilerGenerated]
		set
		{
			field_1115 = value;
		}
	}

	public Action? OnConfirm
	{
		[CompilerGenerated]
		get
		{
			return field_1116;
		}
		[CompilerGenerated]
		set
		{
			field_1116 = value;
		}
	}

	public Action? OnCancel
	{
		[CompilerGenerated]
		get
		{
			return field_1117;
		}
		[CompilerGenerated]
		set
		{
			field_1117 = value;
		}
	}

	public void Show(string arg0, string arg1, IEnumerable<string>? arg2 = null, string arg3 = "", string arg4 = "確認", UiVariant arg5 = UiVariant.Danger, Action? arg6 = null)
	{
		Title = arg0;
		Message = arg1;
		Impact.Clear();
		if (arg2 != null)
		{
			Impact.AddRange(arg2);
		}
		ConfirmWord = arg3;
		ConfirmLabel = arg4;
		ConfirmVariant = arg5;
		OnConfirm = arg6;
		field_1118 = string.Empty;
		Open = true;
		field_1119 = true;
	}

	public void Draw()
	{
		if (!Open)
		{
			return;
		}
		float uiScale = Theme.UiScale;
		using (Theme.Push(ThemeContext.Popup))
		{
			if (field_1119)
			{
				field_1119 = false;
				ImGui.OpenPopup(PopupId);
			}
			ImGui.SetNextWindowSize(new Vector2(420f * uiScale, 0f), ImGuiCond.Always);
			if (!ImGui.BeginPopupModal(PopupId, ImGuiWindowFlags.AlwaysAutoResize))
			{
				Open = false;
				return;
			}
			Vector2 cursorScreenPos = ImGui.GetCursorScreenPos();
			ImDrawListPtr windowDrawList = ImGui.GetWindowDrawList();
			windowDrawList.AddRectFilled(new Vector2(cursorScreenPos.X, cursorScreenPos.Y), new Vector2(cursorScreenPos.X + 3f * uiScale, cursorScreenPos.Y + ImGui.GetTextLineHeight()), Theme.U32(Theme.StatusDanger), 1.5f * uiScale);
			Theme.DrawText(windowDrawList, new Vector2(cursorScreenPos.X + 9f * uiScale, cursorScreenPos.Y), Theme.StatusDanger, Title, Theme.FsSubtitle);
			ImGui.Dummy(new Vector2(1f, ImGui.GetTextLineHeight() + 4f * uiScale));
			if (!string.IsNullOrEmpty(Message))
			{
				ImGui.PushStyleColor(ImGuiCol.Text, Theme.TextSecondary);
				TraditionalText.TextWrapped(Message);
				ImGui.PopStyleColor();
				ImGui.Spacing();
			}
			if (Impact.Count > 0)
			{
				ImGui.PushStyleColor(ImGuiCol.Text, Theme.TextSecondary);
				TraditionalText.TextUnformatted("即將影響：");
				ImGui.PopStyleColor();
				foreach (string item in Impact)
				{
					ImGui.BulletText(item);
				}
				ImGui.Spacing();
			}
			bool flag = !string.IsNullOrEmpty(ConfirmWord);
			bool field_1121 = !flag || field_1118 == ConfirmWord;
			if (flag)
			{
				ImU8String text = new ImU8String(8, 1);
				text.AppendLiteral("請輸入「");
				text.AppendFormatted(ConfirmWord);
				text.AppendLiteral("」以確認");
				TraditionalText.TextUnformatted(text);
				ImGui.SetNextItemWidth(-1f);
				ImGui.InputText("##confirmWord", ref field_1118, 32);
				ImGui.Spacing();
			}
			UiLayout.EqualColumns("##confirmButtons", 2, (int arg0) =>
			{
				if (arg0 == 0)
				{
					ImGui.SetKeyboardFocusHere();
					if (UiButton.Draw("取消", UiVariant.Neutral, new Vector2(ImGui.GetContentRegionAvail().X, Theme.ControlHeight)))
					{
						OnCancel?.Invoke();
						Method_1114();
						ImGui.CloseCurrentPopup();
					}
				}
				else
				{
					if (!field_1121)
					{
						ImGui.BeginDisabled();
					}
					if (UiButton.Draw(ConfirmLabel, ConfirmVariant, new Vector2(ImGui.GetContentRegionAvail().X, Theme.ControlHeight)))
					{
						OnConfirm?.Invoke();
						Method_1114();
						ImGui.CloseCurrentPopup();
					}
					if (!field_1121)
					{
						ImGui.EndDisabled();
					}
				}
			});
			if (flag && !field_1121)
			{
				ImGui.PushStyleColor(ImGuiCol.Text, Theme.TextDisabled);
				ImU8String text2 = new ImU8String(8, 1);
				text2.AppendLiteral("請輸入「");
				text2.AppendFormatted(ConfirmWord);
				text2.AppendLiteral("」以確認");
				TraditionalText.TextUnformatted(text2);
				ImGui.PopStyleColor();
			}
			ImGui.EndPopup();
		}
	}

	private void Method_1114()
	{
		Open = false;
		field_1118 = string.Empty;
	}
}
