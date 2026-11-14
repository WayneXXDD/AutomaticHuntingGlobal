using System;
using System.Collections.Generic;
using System.Numerics;
using AutomaticHunting.UI.Components;
using Dalamud.Bindings.ImGui;
using Dalamud.Interface.Windowing;
using Dalamud.Utility;

namespace AutomaticHunting.UI;

public sealed class HelpWindow : Window
{
	private readonly AutomaticHunting _2000_2032;

	private Theme.ThemeScope? _2000_2035;

	private int _2000_2033;

	private int _2000_2036;

	public HelpWindow(AutomaticHunting plugin)
		: base("使用说明###AutomaticHunting.Help")
	{
		_2000_2032 = plugin;
		RespectCloseHotkey = false;
		Size = new Vector2(600f, 480f);
		SizeCondition = ImGuiCond.FirstUseEver;
		SizeConstraints = new WindowSizeConstraints
		{
			MinimumSize = new Vector2(480f, 360f),
			MaximumSize = new Vector2(1200f, 1000f)
		};
		Flags = ImGuiWindowFlags.NoTitleBar;
		BgAlpha = null;
	}

	public override void PreDraw()
	{
		_2000_2035?.Dispose();
		_2000_2035 = Theme.Push(ThemeContext.Settings, 0.97f);
	}

	public override void PostDraw()
	{
		_2000_2035?.Dispose();
		_2000_2035 = null;
	}

	public override void Draw()
	{
		ErrorBoundary.Run(_2000_2032, "使用说明窗口", _00A0);
	}

	private void _00A0()
	{
		float uiScale = Theme.UiScale;
		UiPanel.DrawWindowFrame(8f * uiScale);
		_1680();
		IReadOnlyList<HelpGroup> groups = HelpDocument.Groups;
		if (groups.Count == 0)
		{
			UiEmpty.Show("帮助内容不可用", "内嵌帮助资源缺失，请重新安装插件");
			return;
		}
		_2000_2033 = Math.Clamp(_2000_2033, 0, groups.Count - 1);
		ImGui.BeginTable("##helpLayout", 2, ImGuiTableFlags.SizingStretchProp | ImGuiTableFlags.NoSavedSettings);
		ImGui.TableSetupColumn("##nav", ImGuiTableColumnFlags.WidthFixed, 140f * uiScale);
		ImGui.TableSetupColumn("##body", ImGuiTableColumnFlags.WidthStretch, 1f);
		ImGui.TableNextRow();
		ImGui.TableNextColumn();
		_00A0(groups);
		ImGui.TableNextColumn();
		_00A0(groups[_2000_2033]);
		ImGui.EndTable();
	}

	private void _1680()
	{
		float uiScale = Theme.UiScale;
		Vector2 cursorScreenPos = ImGui.GetCursorScreenPos();
		ImDrawListPtr windowDrawList = ImGui.GetWindowDrawList();
		float num = 32f * uiScale;
		float num2 = 18f * uiScale;
		float num3 = cursorScreenPos.Y + (num - num2) * 0.5f;
		windowDrawList.AddRectFilled(new Vector2(cursorScreenPos.X, num3), new Vector2(cursorScreenPos.X + 3f * uiScale, num3 + num2), Theme.U32(Theme.BrandGold), 1.5f * uiScale);
		Theme.DrawText(windowDrawList, new Vector2(cursorScreenPos.X + 12f * uiScale, cursorScreenPos.Y + (num - Theme.FsTitle) * 0.5f), Theme.TextPrimary, "使用说明", Theme.FsTitle);
		float num4 = 20f * uiScale;
		Vector2 vector = new Vector2(Theme.ContentRightX() - num4, cursorScreenPos.Y + (num - num4) * 0.5f);
		ImGui.SetCursorScreenPos(vector);
		bool flag = ImGui.InvisibleButton("##closeHelp", new Vector2(num4, num4));
		bool flag2 = ImGui.IsItemHovered();
		if (flag2)
		{
			windowDrawList.AddRectFilled(vector, vector + new Vector2(num4, num4), Theme.U32(Theme.WithAlpha(Theme.StatusDanger, 0.18f)), 4f * uiScale);
		}
		UiIcons.DrawCross(windowDrawList, vector + new Vector2(num4 * 0.5f, num4 * 0.5f), num4 * 0.8f, Theme.U32(flag2 ? Theme.StatusDanger : Theme.TextSecondary));
		if (flag)
		{
			IsOpen = false;
		}
		ImGui.SetCursorScreenPos(cursorScreenPos);
		ImGui.Dummy(new Vector2(1f, 44f * uiScale));
		UiSection.Divider();
	}

	private void _00A0(IReadOnlyList<HelpGroup> P_0)
	{
		float uiScale = Theme.UiScale;
		ImDrawListPtr windowDrawList = ImGui.GetWindowDrawList();
		for (int i = 0; i < P_0.Count; i++)
		{
			HelpGroup helpGroup = P_0[i];
			bool flag = i == _2000_2033;
			Vector2 cursorScreenPos = ImGui.GetCursorScreenPos();
			float x = ImGui.GetContentRegionAvail().X;
			float num = 28f * uiScale;
			Vector2 vector = ImGui.CalcTextSize(helpGroup.Title);
			bool flag2 = ImGui.IsMouseHoveringRect(cursorScreenPos, cursorScreenPos + new Vector2(x, num));
			if (flag)
			{
				windowDrawList.AddRectFilled(cursorScreenPos, cursorScreenPos + new Vector2(x, num), Theme.U32(Theme.TabSelectedBg), 4f * uiScale);
			}
			else if (flag2)
			{
				windowDrawList.AddRectFilled(cursorScreenPos, cursorScreenPos + new Vector2(x, num), Theme.U32(Theme.TabHoverBg), 4f * uiScale);
			}
			if (flag)
			{
				windowDrawList.AddRectFilled(cursorScreenPos, cursorScreenPos + new Vector2(3f * uiScale, num), Theme.U32(Theme.BrandGold), 1.5f * uiScale);
			}
			Vector4 color;
			if (flag)
			{
				color = Theme.BrandGoldLight;
			}
			else
			{
				color = (flag2 ? Theme.TextPrimary : Theme.TextSecondary);
			}
			Theme.DrawText(windowDrawList, new Vector2(cursorScreenPos.X + 12f * uiScale, cursorScreenPos.Y + (num - vector.Y) * 0.5f), color, helpGroup.Title, Theme.FsBody);
			ImGui.SetCursorScreenPos(cursorScreenPos);
			ImU8String strId = new ImU8String(11, 1);
			strId.AppendLiteral("##helpGroup");
			strId.AppendFormatted(i);
			if (ImGui.InvisibleButton(strId, new Vector2(x, num)))
			{
				_2000_2033 = i;
			}
			ImGui.SetCursorScreenPos(cursorScreenPos + new Vector2(0f, num));
		}
	}

	private void _00A0(HelpGroup P_0)
	{
		float uiScale = Theme.UiScale;
		ImDrawListPtr windowDrawList = ImGui.GetWindowDrawList();
		_2000_2036 = 0;
		float x = ImGui.GetContentRegionAvail().X;
		float num = Math.Max(40f, x - 12f * uiScale);
		float y = ImGui.CalcTextSize("Ag中").Y;
		float lineH = y + 1f * uiScale;
		List<string> list = Theme.WrapText(P_0.Title, num, Theme.FsSubtitle);
		for (int i = 0; i < list.Count; i++)
		{
			Theme.DrawText(windowDrawList, ImGui.GetCursorScreenPos(), Theme.TextPrimary, list[i], Theme.FsSubtitle);
			ImGui.Dummy(new Vector2(1f, y));
		}
		ImGui.Dummy(new Vector2(1f, 6f * uiScale));
		foreach (HelpSection section in P_0.Sections)
		{
			List<string> list2 = Theme.WrapText(section.Title, num, Theme.FsBody);
			for (int j = 0; j < list2.Count; j++)
			{
				Theme.DrawText(windowDrawList, ImGui.GetCursorScreenPos(), Theme.BrandGoldLight, list2[j], Theme.FsBody);
				ImGui.Dummy(new Vector2(1f, y));
			}
			ImGui.Dummy(new Vector2(1f, 2f * uiScale));
			foreach (HelpLine item in section.Body)
			{
				if (string.IsNullOrWhiteSpace(item.Text))
				{
					ImGui.Dummy(new Vector2(1f, 4f * uiScale));
				}
				else if (item.IsLink)
				{
					Vector2 cursorScreenPos = ImGui.GetCursorScreenPos();
					HelpLinkGeometry helpLinkGeometry = UiHelpLink.Draw(windowDrawList, $"##helpLink{_2000_2036++}", item.Text, item.Url, cursorScreenPos, num, lineH, _00A0);
					ImGui.SetCursorScreenPos(cursorScreenPos);
					ImGui.Dummy(new Vector2(1f, helpLinkGeometry.Max.Y - helpLinkGeometry.Min.Y + 6f * uiScale));
				}
				else
				{
					Vector2 cursorScreenPos2 = ImGui.GetCursorScreenPos();
					float y2 = UiHelpText.Draw(windowDrawList, item.Text, cursorScreenPos2, num, lineH);
					ImGui.SetCursorScreenPos(cursorScreenPos2);
					ImGui.Dummy(new Vector2(1f, y2));
				}
			}
			ImGui.Dummy(new Vector2(1f, 8f * uiScale));
			UiSection.Divider();
			ImGui.Dummy(new Vector2(1f, 8f * uiScale));
		}
	}

	private void _00A0(string P_0)
	{
		if (!UiHelpLink.IsAllowedUrl(P_0))
		{
			_2000_2032.PrintEcho("该链接协议不受支持，已忽略");
			return;
		}
		try
		{
			Util.OpenLink(P_0);
			_2000_2032.PrintEcho("已在浏览器中打开链接");
		}
		catch (Exception exception)
		{
			AutomaticHunting.Log.Error(exception, "AutomaticHunting: 打开链接失败: " + P_0);
			_2000_2032.PrintEcho("打开链接失败，请手动复制网址到浏览器");
		}
	}
}
