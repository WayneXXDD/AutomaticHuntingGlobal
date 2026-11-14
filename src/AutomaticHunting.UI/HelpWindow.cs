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
	private readonly AutomaticHunting field_675;

	private Theme.ThemeScope? field_676;

	private int field_677;

	private int field_678;

	public HelpWindow(AutomaticHunting arg0)
		: base("使用說明###AutomaticHunting.Help")
	{
		field_675 = arg0;
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
		field_676?.Dispose();
		field_676 = Theme.Push(ThemeContext.Settings, 0.97f);
	}

	public override void PostDraw()
	{
		field_676?.Dispose();
		field_676 = null;
	}

	public override void Draw()
	{
		ErrorBoundary.Run(field_675, "使用說明視窗", Method_707);
	}

	private void Method_707()
	{
		float uiScale = Theme.UiScale;
		UiPanel.DrawWindowFrame(8f * uiScale);
		Method_708();
		IReadOnlyList<HelpGroup> groups = HelpDocument.Groups;
		if (groups.Count == 0)
		{
			UiEmpty.Show("幫助內容不可用", "內嵌幫助資源缺失，請重新安裝插件");
			return;
		}
		field_677 = Math.Clamp(field_677, 0, groups.Count - 1);
		ImGui.BeginTable("##helpLayout", 2, ImGuiTableFlags.SizingStretchProp | ImGuiTableFlags.NoSavedSettings);
		ImGui.TableSetupColumn("##nav", ImGuiTableColumnFlags.WidthFixed, 140f * uiScale);
		ImGui.TableSetupColumn("##body", ImGuiTableColumnFlags.WidthStretch, 1f);
		ImGui.TableNextRow();
		ImGui.TableNextColumn();
		Method_709(groups);
		ImGui.TableNextColumn();
		Method_710(groups[field_677]);
		ImGui.EndTable();
	}

	private void Method_708()
	{
		float uiScale = Theme.UiScale;
		Vector2 cursorScreenPos = ImGui.GetCursorScreenPos();
		ImDrawListPtr windowDrawList = ImGui.GetWindowDrawList();
		float num = 32f * uiScale;
		float num2 = 18f * uiScale;
		float num3 = cursorScreenPos.Y + (num - num2) * 0.5f;
		windowDrawList.AddRectFilled(new Vector2(cursorScreenPos.X, num3), new Vector2(cursorScreenPos.X + 3f * uiScale, num3 + num2), Theme.U32(Theme.BrandGold), 1.5f * uiScale);
		Theme.DrawText(windowDrawList, new Vector2(cursorScreenPos.X + 12f * uiScale, cursorScreenPos.Y + (num - Theme.FsTitle) * 0.5f), Theme.TextPrimary, "使用說明", Theme.FsTitle);
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

	private void Method_709(IReadOnlyList<HelpGroup> arg0)
	{
		float uiScale = Theme.UiScale;
		ImDrawListPtr windowDrawList = ImGui.GetWindowDrawList();
		for (int i = 0; i < arg0.Count; i++)
		{
			HelpGroup helpGroup = arg0[i];
			bool flag = i == field_677;
			Vector2 cursorScreenPos = ImGui.GetCursorScreenPos();
			float x = ImGui.GetContentRegionAvail().X;
			float num = 28f * uiScale;
			Vector2 vector = TraditionalText.CalcTextSize(helpGroup.Title);
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
			Vector4 arg1;
			if (flag)
			{
				arg1 = Theme.BrandGoldLight;
			}
			else
			{
				arg1 = (flag2 ? Theme.TextPrimary : Theme.TextSecondary);
			}
			Theme.DrawText(windowDrawList, new Vector2(cursorScreenPos.X + 12f * uiScale, cursorScreenPos.Y + (num - vector.Y) * 0.5f), arg1, helpGroup.Title, Theme.FsBody);
			ImGui.SetCursorScreenPos(cursorScreenPos);
			ImU8String strId = new ImU8String(11, 1);
			strId.AppendLiteral("##helpGroup");
			strId.AppendFormatted(i);
			if (ImGui.InvisibleButton(strId, new Vector2(x, num)))
			{
				field_677 = i;
			}
			ImGui.SetCursorScreenPos(cursorScreenPos + new Vector2(0f, num));
		}
	}

	private void Method_710(HelpGroup arg0)
	{
		float uiScale = Theme.UiScale;
		ImDrawListPtr windowDrawList = ImGui.GetWindowDrawList();
		field_678 = 0;
		float x = ImGui.GetContentRegionAvail().X;
		float num = Math.Max(40f, x - 12f * uiScale);
		float y = TraditionalText.CalcTextSize("Ag中").Y;
		float num2 = y + 1f * uiScale;
		List<string> list = Theme.WrapText(arg0.Title, num, Theme.FsSubtitle);
		for (int i = 0; i < list.Count; i++)
		{
			Theme.DrawText(windowDrawList, ImGui.GetCursorScreenPos(), Theme.TextPrimary, list[i], Theme.FsSubtitle);
			ImGui.Dummy(new Vector2(1f, y));
		}
		ImGui.Dummy(new Vector2(1f, 6f * uiScale));
		foreach (HelpSection section in arg0.Sections)
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
					HelpLinkGeometry helpLinkGeometry = UiHelpLink.Draw(windowDrawList, $"##helpLink{field_678++}", item.Text, item.Url, cursorScreenPos, num, num2, Method_711);
					ImGui.SetCursorScreenPos(cursorScreenPos);
					ImGui.Dummy(new Vector2(1f, helpLinkGeometry.Max.Y - helpLinkGeometry.Min.Y + 6f * uiScale));
				}
				else
				{
					Vector2 cursorScreenPos2 = ImGui.GetCursorScreenPos();
					float y2 = UiHelpText.Draw(windowDrawList, item.Text, cursorScreenPos2, num, num2);
					ImGui.SetCursorScreenPos(cursorScreenPos2);
					ImGui.Dummy(new Vector2(1f, y2));
				}
			}
			ImGui.Dummy(new Vector2(1f, 8f * uiScale));
			UiSection.Divider();
			ImGui.Dummy(new Vector2(1f, 8f * uiScale));
		}
	}

	private void Method_711(string arg0)
	{
		if (!UiHelpLink.IsAllowedUrl(arg0))
		{
			field_675.PrintEcho("該連結協議不受支持，已忽略");
			return;
		}
		try
		{
			Util.OpenLink(arg0);
			field_675.PrintEcho("已在瀏覽器中開啟連結");
		}
		catch (Exception exception)
		{
			AutomaticHunting.Log.Error(exception, "自動狩獵跟車：開啟連結失敗: " + arg0);
			field_675.PrintEcho("開啟連結失敗，請手動複製網址到瀏覽器");
		}
	}
}
