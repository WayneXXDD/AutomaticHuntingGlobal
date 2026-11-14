using System;
using System.Numerics;
using System.Runtime.CompilerServices;
using AutomaticHunting.UI.Components;
using AutomaticHunting.UI.Pages;
using Dalamud.Bindings.ImGui;
using Dalamud.Interface.Windowing;

namespace AutomaticHunting.UI;

public sealed class SettingsWindow : Window
{
	[CompilerGenerated]
	private sealed class RecoveredType_122
	{
		public PluginConfiguration field_707;

		public SettingsWindow field_708;

		internal void Method_771()
		{
			field_707.SettingsTab = 0;
			field_708.field_699.ConfigService.MarkDirty();
			field_708.field_699.OnboardingWindow.Open();
		}

		internal void Method_772()
		{
			field_708.field_699.StopCrossWorld();
			field_708.field_699.PrintEcho("跨服已停止");
		}

		internal void Method_773()
		{
			field_708.field_699.StopNavigation();
			field_708.field_699.PrintEcho("导航已停止");
		}

		internal void Method_774()
		{
			field_708.field_699.StartNavigation();
		}
	}

	private static readonly string[] field_696 = new string[5] { "概览", "跟车", "跨服", "连车", "高级" };

	private const float field_697 = 64f;

	private const float field_698 = 8f;

	private readonly AutomaticHunting field_699;

	private readonly OverviewPage field_700;

	private readonly FollowPage field_701;

	private readonly TravelPage field_702;

	private readonly ChainPage field_703;

	private readonly AdvancedPage field_704;

	private readonly UiTabs field_705;

	private Theme.ThemeScope? field_706;

	public SettingsWindow(AutomaticHunting arg0)
		: base("自动狩猎跟车###AutomaticHunting.Settings")
	{
		field_699 = arg0;
		RespectCloseHotkey = false;
		Size = new Vector2((arg0.Config.SettingsWidth > 0f) ? arg0.Config.SettingsWidth : 560f, (arg0.Config.SettingsHeight > 0f) ? arg0.Config.SettingsHeight : 680f);
		SizeCondition = ImGuiCond.FirstUseEver;
		SizeConstraints = new WindowSizeConstraints
		{
			MinimumSize = new Vector2(520f, 440f),
			MaximumSize = new Vector2(1600f, 1200f)
		};
		Flags = ImGuiWindowFlags.NoTitleBar;
		BgAlpha = null;
		AllowBackgroundBlur = true;
		field_700 = new OverviewPage(arg0);
		field_701 = new FollowPage(arg0);
		field_702 = new TravelPage(arg0);
		field_703 = new ChainPage(arg0);
		field_704 = new AdvancedPage(arg0);
		field_705 = new UiTabs("##settingsTabs", field_696, () => Math.Clamp(field_699.Config.SettingsTab, 0, field_696.Length - 1), (int settingsTab) =>
		{
			field_699.Config.SettingsTab = settingsTab;
			field_699.ConfigService.MarkDirty();
		});
	}

	public override void PreDraw()
	{
		field_706?.Dispose();
		field_706 = Theme.Push(ThemeContext.Settings, 0.97f);
	}

	public override void PostDraw()
	{
		field_706?.Dispose();
		field_706 = null;
	}

	public override void Draw()
	{
		ErrorBoundary.Run(field_699, "设置窗口", Method_759);
	}

	private void Method_759()
	{
		float uiScale = Theme.UiScale;
		Method_764();
		Method_765();
		UiPanel.DrawWindowFrame(8f * uiScale);
		Method_761();
		field_705.Draw();
		float num = Theme.ControlHeight + 16f * uiScale;
		if (ImGui.BeginChild("##settingsBody", new Vector2(0f, 0f - num)))
		{
			Method_760();
		}
		ImGui.EndChild();
		Method_762();
		UiToast.Draw();
	}

	private void Method_760()
	{
		switch (Math.Clamp(field_699.Config.SettingsTab, 0, field_696.Length - 1))
		{
		case 1:
			field_701.Draw();
			break;
		case 2:
			field_702.Draw();
			break;
		case 3:
			field_703.Draw();
			break;
		case 4:
			field_704.Draw();
			break;
		default:
			field_700.Draw();
			break;
		}
	}

	private void Method_761()
	{
		float uiScale = Theme.UiScale;
		Vector2 cursorScreenPos = ImGui.GetCursorScreenPos();
		ImDrawListPtr windowDrawList = ImGui.GetWindowDrawList();
		float num = 32f * uiScale;
		float y = 44f * uiScale;
		float num2 = 18f * uiScale;
		float num3 = cursorScreenPos.Y + (num - num2) * 0.5f;
		windowDrawList.AddRectFilled(new Vector2(cursorScreenPos.X, num3), new Vector2(cursorScreenPos.X + 3f * uiScale, num3 + num2), Theme.U32(Theme.BrandGold), 1.5f * uiScale);
		Vector2 vector = new Vector2(cursorScreenPos.X + 3f * uiScale + 9f * uiScale, num3 + num2 * 0.5f);
		float num4 = 5f * uiScale;
		windowDrawList.AddLine(new Vector2(vector.X, vector.Y - num4), new Vector2(vector.X + num4, vector.Y), Theme.U32(Theme.BrandGold), 1.5f * uiScale);
		windowDrawList.AddLine(new Vector2(vector.X + num4, vector.Y), new Vector2(vector.X, vector.Y + num4), Theme.U32(Theme.BrandGold), 1.5f * uiScale);
		windowDrawList.AddLine(new Vector2(vector.X, vector.Y + num4), new Vector2(vector.X - num4, vector.Y), Theme.U32(Theme.BrandGold), 1.5f * uiScale);
		windowDrawList.AddLine(new Vector2(vector.X - num4, vector.Y), new Vector2(vector.X, vector.Y - num4), Theme.U32(Theme.BrandGold), 1.5f * uiScale);
		float x = vector.X + num4 + 9f * uiScale;
		UiBrandTag.Draw(windowDrawList, new Vector2(x, cursorScreenPos.Y), num);
		float num5 = 20f * uiScale;
		Vector2 vector2 = new Vector2(Theme.ContentRightX() - num5, cursorScreenPos.Y + (num - num5) * 0.5f);
		ImGui.SetCursorScreenPos(vector2);
		bool flag = ImGui.InvisibleButton("##closeSettings", new Vector2(num5, num5));
		bool flag2 = ImGui.IsItemHovered();
		if (flag2)
		{
			windowDrawList.AddRectFilled(vector2, vector2 + new Vector2(num5, num5), Theme.U32(Theme.WithAlpha(Theme.StatusDanger, 0.18f)), 4f * uiScale);
		}
		UiIcons.DrawCross(windowDrawList, vector2 + new Vector2(num5 * 0.5f, num5 * 0.5f), num5 * 0.8f, Theme.U32(flag2 ? Theme.StatusDanger : Theme.TextSecondary));
		if (flag)
		{
			IsOpen = false;
		}
		(string Text, string Glyph, Vector4 Color) tuple = UiState.RunStatus(field_699);
		string item = tuple.Text;
		string item2 = tuple.Glyph;
		Vector4 item3 = tuple.Color;
		string arg = item2 + " " + item;
		float num6 = UiBadge.PillHeight();
		UiBadge.PillAt(arg, item3, vector2.X - 8f * uiScale, cursorScreenPos.Y + (num - num6) * 0.5f, item3);
		ImGui.SetCursorScreenPos(cursorScreenPos);
		ImGui.Dummy(new Vector2(1f, y));
		UiSection.Divider();
	}

	private void Method_762()
	{
		float uiScale = Theme.UiScale;
		ImGui.Separator();
		ImGui.Dummy(new Vector2(1f, 4f * uiScale));
		UiLayout.MainAndSide("##settingsFooter", 64f, 8f, (int arg0) =>
		{
			Vector2 value = new Vector2(ImGui.GetContentRegionAvail().X, Theme.ControlHeight);
			if (arg0 == 0)
			{
				var (arg1, arg2, arg3, arg4, action) = Method_763();
				if (UiButton.Draw(arg1, arg2, value, arg3, arg4))
				{
					action();
				}
			}
			else
			{
				bool isOpen = field_699.HudWindow.IsOpen;
				string arg5 = (isOpen ? "隐藏常驻悬浮窗（当前：显示）" : "显示常驻悬浮窗（当前：隐藏）");
				if (UiButton.Draw("HUD", UiVariant.Neutral, value, arg5))
				{
					field_699.HudWindow.IsOpen = !isOpen;
					field_699.Config.HudVisible = field_699.HudWindow.IsOpen;
					field_699.ConfigService.MarkDirty();
				}
			}
		});
		ImGui.Dummy(new Vector2(1f, 4f * uiScale));
	}

	private (string Label, UiVariant Variant, string Tooltip, bool Enabled, Action Action) Method_763()
	{
		PluginConfiguration field_707 = field_699.Config;
		if (!field_707.OnboardingCompleted)
		{
			return (Label: "开始配置", Variant: UiVariant.Primary, Tooltip: "打开首次运行向导", Enabled: true, Action: () =>
			{
				field_707.SettingsTab = 0;
				field_699.ConfigService.MarkDirty();
				field_699.OnboardingWindow.Open();
			});
		}
		if (field_699.CrossWorldInProgress)
		{
			return (Label: "停止跨服", Variant: UiVariant.Danger, Tooltip: "中止跨服流程并恢复普通车头识别", Enabled: true, Action: () =>
			{
				field_699.StopCrossWorld();
				field_699.PrintEcho("跨服已停止");
			});
		}
		if (field_699.IsNavigating)
		{
			return (Label: "停止导航", Variant: UiVariant.Danger, Tooltip: "停止导航 · 结束当前跟车任务并清除当前流程", Enabled: true, Action: () =>
			{
				field_699.StopNavigation();
				field_699.PrintEcho("导航已停止");
			});
		}
		return (Label: "开始导航", Variant: UiVariant.Primary, Tooltip: "检测地图上已标好的 Flag，有则当作车头发送的 Flag 处理并导航；无则提示", Enabled: field_707.AutomationEnabled, Action: () =>
		{
			field_699.StartNavigation();
		});
	}

	private void Method_764()
	{
		Vector2? size = Size;
		if (!size.HasValue)
		{
			return;
		}
		Vector2 valueOrDefault = size.GetValueOrDefault();
		if (!(valueOrDefault.X <= 0f))
		{
			if (Math.Abs(valueOrDefault.X - field_699.Config.SettingsWidth) > 1f)
			{
				field_699.Config.SettingsWidth = valueOrDefault.X;
				field_699.ConfigService.MarkDirty();
			}
			if (Math.Abs(valueOrDefault.Y - field_699.Config.SettingsHeight) > 1f)
			{
				field_699.Config.SettingsHeight = valueOrDefault.Y;
				field_699.ConfigService.MarkDirty();
			}
		}
	}

	private void Method_765()
	{
		ImGuiIOPtr iO = ImGui.GetIO();
		float val = Math.Max(320f, iO.DisplaySize.X - 40f);
		float val2 = Math.Max(240f, iO.DisplaySize.Y - 40f);
		float num = Math.Clamp(field_699.Config.SettingsWidth, 520f, Math.Min(1600f, val));
		float num2 = Math.Clamp(field_699.Config.SettingsHeight, 440f, Math.Min(1200f, val2));
		if (Math.Abs(num - field_699.Config.SettingsWidth) > 1f || Math.Abs(num2 - field_699.Config.SettingsHeight) > 1f)
		{
			field_699.Config.SettingsWidth = num;
			field_699.Config.SettingsHeight = num2;
			field_699.ConfigService.MarkDirty();
		}
	}

	[CompilerGenerated]
	private int Method_767()
	{
		return Math.Clamp(field_699.Config.SettingsTab, 0, field_696.Length - 1);
	}

	[CompilerGenerated]
	private void Method_768(int arg0)
	{
		field_699.Config.SettingsTab = arg0;
		field_699.ConfigService.MarkDirty();
	}

	[CompilerGenerated]
	private void Method_769(int arg0)
	{
		Vector2 value = new Vector2(ImGui.GetContentRegionAvail().X, Theme.ControlHeight);
		if (arg0 == 0)
		{
			var (arg1, arg2, arg3, arg4, action) = Method_763();
			if (UiButton.Draw(arg1, arg2, value, arg3, arg4))
			{
				action();
			}
		}
		else
		{
			bool isOpen = field_699.HudWindow.IsOpen;
			string arg5 = (isOpen ? "隐藏常驻悬浮窗（当前：显示）" : "显示常驻悬浮窗（当前：隐藏）");
			if (UiButton.Draw("HUD", UiVariant.Neutral, value, arg5))
			{
				field_699.HudWindow.IsOpen = !isOpen;
				field_699.Config.HudVisible = field_699.HudWindow.IsOpen;
				field_699.ConfigService.MarkDirty();
			}
		}
	}
}
