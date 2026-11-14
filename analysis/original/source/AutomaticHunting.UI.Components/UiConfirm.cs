using System;
using System.Collections.Generic;
using System.Numerics;
using System.Runtime.CompilerServices;
using Dalamud.Bindings.ImGui;

namespace AutomaticHunting.UI.Components;

public sealed class UiConfirm
{
	[CompilerGenerated]
	private sealed class _00A0
	{
		public UiConfirm _2008_2052;

		public bool _2008_2053;

		internal void _00A0(int P_0)
		{
			if (P_0 == 0)
			{
				ImGui.SetKeyboardFocusHere();
				if (UiButton.Draw("取消", UiVariant.Neutral, new Vector2(ImGui.GetContentRegionAvail().X, Theme.ControlHeight)))
				{
					_2008_2052.OnCancel?.Invoke();
					_2008_2052._00A0();
					ImGui.CloseCurrentPopup();
				}
				return;
			}
			if (!_2008_2053)
			{
				ImGui.BeginDisabled();
			}
			if (UiButton.Draw(_2008_2052.ConfirmLabel, _2008_2052.ConfirmVariant, new Vector2(ImGui.GetContentRegionAvail().X, Theme.ControlHeight)))
			{
				_2008_2052.OnConfirm?.Invoke();
				_2008_2052._00A0();
				ImGui.CloseCurrentPopup();
			}
			if (!_2008_2053)
			{
				ImGui.EndDisabled();
			}
		}
	}

	[CompilerGenerated]
	private bool _2001_202E;

	[CompilerGenerated]
	private string _2001_202F = "##uiConfirm";

	[CompilerGenerated]
	private string _2001_2032 = string.Empty;

	[CompilerGenerated]
	private string _2001_2035 = string.Empty;

	[CompilerGenerated]
	private readonly List<string> _2001_2033 = new List<string>();

	[CompilerGenerated]
	private string _2001_2036 = string.Empty;

	[CompilerGenerated]
	private string _2001_203E = "确认";

	[CompilerGenerated]
	private UiVariant _2001_2047 = UiVariant.Danger;

	[CompilerGenerated]
	private Action? _2001_2048;

	[CompilerGenerated]
	private Action? _2001_2049;

	private string _2001_204A = string.Empty;

	private bool _2001_204B;

	public bool Open
	{
		[CompilerGenerated]
		get
		{
			return _2001_202E;
		}
		[CompilerGenerated]
		private set
		{
			_2001_202E = flag;
		}
	}

	public string PopupId
	{
		[CompilerGenerated]
		get
		{
			return _2001_202F;
		}
		[CompilerGenerated]
		set
		{
			_2001_202F = value;
		}
	}

	public string Title
	{
		[CompilerGenerated]
		get
		{
			return _2001_2032;
		}
		[CompilerGenerated]
		set
		{
			_2001_2032 = value;
		}
	}

	public string Message
	{
		[CompilerGenerated]
		get
		{
			return _2001_2035;
		}
		[CompilerGenerated]
		set
		{
			_2001_2035 = value;
		}
	}

	public List<string> Impact
	{
		[CompilerGenerated]
		get
		{
			return _2001_2033;
		}
	}

	public string ConfirmWord
	{
		[CompilerGenerated]
		get
		{
			return _2001_2036;
		}
		[CompilerGenerated]
		set
		{
			_2001_2036 = value;
		}
	}

	public string ConfirmLabel
	{
		[CompilerGenerated]
		get
		{
			return _2001_203E;
		}
		[CompilerGenerated]
		set
		{
			_2001_203E = value;
		}
	}

	public UiVariant ConfirmVariant
	{
		[CompilerGenerated]
		get
		{
			return _2001_2047;
		}
		[CompilerGenerated]
		set
		{
			_2001_2047 = value;
		}
	}

	public Action? OnConfirm
	{
		[CompilerGenerated]
		get
		{
			return _2001_2048;
		}
		[CompilerGenerated]
		set
		{
			_2001_2048 = value;
		}
	}

	public Action? OnCancel
	{
		[CompilerGenerated]
		get
		{
			return _2001_2049;
		}
		[CompilerGenerated]
		set
		{
			_2001_2049 = value;
		}
	}

	public void Show(string title, string message, IEnumerable<string>? impact = null, string confirmWord = "", string confirmLabel = "确认", UiVariant variant = UiVariant.Danger, Action? onConfirm = null)
	{
		Title = title;
		Message = message;
		Impact.Clear();
		if (impact != null)
		{
			Impact.AddRange(impact);
		}
		ConfirmWord = confirmWord;
		ConfirmLabel = confirmLabel;
		ConfirmVariant = variant;
		OnConfirm = onConfirm;
		_2001_204A = string.Empty;
		Open = true;
		_2001_204B = true;
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
			if (_2001_204B)
			{
				_2001_204B = false;
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
				ImGui.TextWrapped(Message);
				ImGui.PopStyleColor();
				ImGui.Spacing();
			}
			if (Impact.Count > 0)
			{
				ImGui.PushStyleColor(ImGuiCol.Text, Theme.TextSecondary);
				ImGui.TextUnformatted("即将影响：");
				ImGui.PopStyleColor();
				foreach (string item in Impact)
				{
					ImGui.BulletText(item);
				}
				ImGui.Spacing();
			}
			bool flag = !string.IsNullOrEmpty(ConfirmWord);
			bool flag2 = !flag || _2001_204A == ConfirmWord;
			if (flag)
			{
				ImU8String text = new ImU8String(8, 1);
				text.AppendLiteral("请输入「");
				text.AppendFormatted(ConfirmWord);
				text.AppendLiteral("」以确认");
				ImGui.TextUnformatted(text);
				ImGui.SetNextItemWidth(-1f);
				ImGui.InputText("##confirmWord", ref _2001_204A, 32);
				ImGui.Spacing();
			}
			UiLayout.EqualColumns("##confirmButtons", 2, (int P_0) =>
			{
				if (P_0 == 0)
				{
					ImGui.SetKeyboardFocusHere();
					if (UiButton.Draw("取消", UiVariant.Neutral, new Vector2(ImGui.GetContentRegionAvail().X, Theme.ControlHeight)))
					{
						OnCancel?.Invoke();
						_00A0();
						ImGui.CloseCurrentPopup();
					}
				}
				else
				{
					if (!flag2)
					{
						ImGui.BeginDisabled();
					}
					if (UiButton.Draw(ConfirmLabel, ConfirmVariant, new Vector2(ImGui.GetContentRegionAvail().X, Theme.ControlHeight)))
					{
						OnConfirm?.Invoke();
						_00A0();
						ImGui.CloseCurrentPopup();
					}
					if (!flag2)
					{
						ImGui.EndDisabled();
					}
				}
			});
			if (flag && !flag2)
			{
				ImGui.PushStyleColor(ImGuiCol.Text, Theme.TextDisabled);
				ImU8String text2 = new ImU8String(8, 1);
				text2.AppendLiteral("请输入「");
				text2.AppendFormatted(ConfirmWord);
				text2.AppendLiteral("」以确认");
				ImGui.TextUnformatted(text2);
				ImGui.PopStyleColor();
			}
			ImGui.EndPopup();
		}
	}

	private void _00A0()
	{
		Open = false;
		_2001_204A = string.Empty;
	}
}
