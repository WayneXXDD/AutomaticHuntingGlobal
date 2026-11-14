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
	private sealed class _00A0
	{
		public PluginConfiguration _2008_2004;

		public SettingsWindow _2008_2005;

		internal void _00A0()
		{
			_2008_2004.SettingsTab = 0;
			_2008_2005._2000_2058.ConfigService.MarkDirty();
			_2008_2005._2000_2058.OnboardingWindow.Open();
		}

		internal void _1680()
		{
			_2008_2005._2000_2058.StopCrossWorld();
			_2008_2005._2000_2058.PrintEcho("跨服已停止");
		}

		internal void _2000()
		{
			_2008_2005._2000_2058.StopNavigation();
			_2008_2005._2000_2058.PrintEcho("导航已停止");
		}

		internal void _2001()
		{
			_2008_2005._2000_2058.StartNavigation();
		}
	}

	private static readonly string[] _2000_2055 = new string[5] { "概览", "跟车", "跨服", "连车", "高级" };

	private const float _2000_2056 = 64f;

	private const float _2000_2057 = 8f;

	private readonly AutomaticHunting _2000_2058;

	private readonly OverviewPage _2000_2059;

	private readonly FollowPage _2000_205A;

	private readonly TravelPage _2000_205B;

	private readonly ChainPage _2000_205C;

	private readonly AdvancedPage _2000_205D;

	private readonly UiTabs _2000_205E;

	private Theme.ThemeScope? _2000_205F;

	public SettingsWindow(AutomaticHunting plugin)
		: base("自动狩猎跟车###AutomaticHunting.Settings")
	{
		_2000_2058 = plugin;
		RespectCloseHotkey = false;
		Size = new Vector2((plugin.Config.SettingsWidth > 0f) ? plugin.Config.SettingsWidth : 560f, (plugin.Config.SettingsHeight > 0f) ? plugin.Config.SettingsHeight : 680f);
		SizeCondition = ImGuiCond.FirstUseEver;
		SizeConstraints = new WindowSizeConstraints
		{
			MinimumSize = new Vector2(520f, 440f),
			MaximumSize = new Vector2(1600f, 1200f)
		};
		Flags = ImGuiWindowFlags.NoTitleBar;
		BgAlpha = null;
		AllowBackgroundBlur = true;
		_2000_2059 = new OverviewPage(plugin);
		_2000_205A = new FollowPage(plugin);
		_2000_205B = new TravelPage(plugin);
		_2000_205C = new ChainPage(plugin);
		_2000_205D = new AdvancedPage(plugin);
		_2000_205E = new UiTabs("##settingsTabs", _2000_2055, () => Math.Clamp(_2000_2058.Config.SettingsTab, 0, _2000_2055.Length - 1), (int P_0) =>
		{
			_2000_2058.Config.SettingsTab = P_0;
			_2000_2058.ConfigService.MarkDirty();
		});
	}

	public override void PreDraw()
	{
		_2000_205F?.Dispose();
		_2000_205F = Theme.Push(ThemeContext.Settings, 0.97f);
	}

	public override void PostDraw()
	{
		_2000_205F?.Dispose();
		_2000_205F = null;
	}

	public override void Draw()
	{
		ErrorBoundary.Run(_2000_2058, "设置窗口", _00A0);
	}

	private void _00A0()
	{
		float uiScale = Theme.UiScale;
		_2003();
		_2004();
		UiPanel.DrawWindowFrame(8f * uiScale);
		_2000();
		_2000_205E.Draw();
		float num = Theme.ControlHeight + 16f * uiScale;
		if (ImGui.BeginChild("##settingsBody", new Vector2(0f, 0f - num)))
		{
			_1680();
		}
		ImGui.EndChild();
		_2001();
		UiToast.Draw();
	}

	private void _1680()
	{
		switch (Math.Clamp(_2000_2058.Config.SettingsTab, 0, _2000_2055.Length - 1))
		{
		case 1:
			_2000_205A.Draw();
			break;
		case 2:
			_2000_205B.Draw();
			break;
		case 3:
			_2000_205C.Draw();
			break;
		case 4:
			_2000_205D.Draw();
			break;
		default:
			_2000_2059.Draw();
			break;
		}
	}

	private void _2000()
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
		(string Text, string Glyph, Vector4 Color) tuple = UiState.RunStatus(_2000_2058);
		string item = tuple.Text;
		string item2 = tuple.Glyph;
		Vector4 item3 = tuple.Color;
		string text = item2 + " " + item;
		float num6 = UiBadge.PillHeight();
		UiBadge.PillAt(text, item3, vector2.X - 8f * uiScale, cursorScreenPos.Y + (num - num6) * 0.5f, item3);
		ImGui.SetCursorScreenPos(cursorScreenPos);
		ImGui.Dummy(new Vector2(1f, y));
		UiSection.Divider();
	}

	private void _2001()
	{
		float uiScale = Theme.UiScale;
		ImGui.Separator();
		ImGui.Dummy(new Vector2(1f, 4f * uiScale));
		UiLayout.MainAndSide("##settingsFooter", 64f, 8f, (int P_0) =>
		{
			Vector2 value = new Vector2(ImGui.GetContentRegionAvail().X, Theme.ControlHeight);
			if (P_0 == 0)
			{
				var (label, variant, tooltip, enabled, action) = _2002();
				if (UiButton.Draw(label, variant, value, tooltip, enabled))
				{
					action();
				}
			}
			else
			{
				bool isOpen = _2000_2058.HudWindow.IsOpen;
				string tooltip2 = (isOpen ? "隐藏常驻悬浮窗（当前：显示）" : "显示常驻悬浮窗（当前：隐藏）");
				if (UiButton.Draw("HUD", UiVariant.Neutral, value, tooltip2))
				{
					_2000_2058.HudWindow.IsOpen = !isOpen;
					_2000_2058.Config.HudVisible = _2000_2058.HudWindow.IsOpen;
					_2000_2058.ConfigService.MarkDirty();
				}
			}
		});
		ImGui.Dummy(new Vector2(1f, 4f * uiScale));
	}

	private (string Label, UiVariant Variant, string Tooltip, bool Enabled, Action Action) _2002()
	{
		PluginConfiguration config = _2000_2058.Config;
		if (!config.OnboardingCompleted)
		{
			return (Label: "开始配置", Variant: UiVariant.Primary, Tooltip: "打开首次运行向导", Enabled: true, Action: () =>
			{
				config.SettingsTab = 0;
				_2000_2058.ConfigService.MarkDirty();
				_2000_2058.OnboardingWindow.Open();
			});
		}
		if (_2000_2058.CrossWorldInProgress)
		{
			return (Label: "停止跨服", Variant: UiVariant.Danger, Tooltip: "中止跨服流程并恢复普通车头识别", Enabled: true, Action: () =>
			{
				_2000_2058.StopCrossWorld();
				_2000_2058.PrintEcho("跨服已停止");
			});
		}
		if (_2000_2058.IsNavigating)
		{
			return (Label: "停止导航", Variant: UiVariant.Danger, Tooltip: "停止导航 · 结束当前跟车任务并清除当前流程", Enabled: true, Action: () =>
			{
				_2000_2058.StopNavigation();
				_2000_2058.PrintEcho("导航已停止");
			});
		}
		return (Label: "开始导航", Variant: UiVariant.Primary, Tooltip: "检测地图上已标好的 Flag，有则当作车头发送的 Flag 处理并导航；无则提示", Enabled: config.AutomationEnabled, Action: () =>
		{
			_2000_2058.StartNavigation();
		});
	}

	private void _2003()
	{
		Vector2? size = Size;
		if (!size.HasValue)
		{
			return;
		}
		Vector2 valueOrDefault = size.GetValueOrDefault();
		if (!(valueOrDefault.X <= 0f))
		{
			if (Math.Abs(valueOrDefault.X - _2000_2058.Config.SettingsWidth) > 1f)
			{
				_2000_2058.Config.SettingsWidth = valueOrDefault.X;
				_2000_2058.ConfigService.MarkDirty();
			}
			if (Math.Abs(valueOrDefault.Y - _2000_2058.Config.SettingsHeight) > 1f)
			{
				_2000_2058.Config.SettingsHeight = valueOrDefault.Y;
				_2000_2058.ConfigService.MarkDirty();
			}
		}
	}

	private void _2004()
	{
		ImGuiIOPtr iO = ImGui.GetIO();
		float val = Math.Max(320f, iO.DisplaySize.X - 40f);
		float val2 = Math.Max(240f, iO.DisplaySize.Y - 40f);
		float num = Math.Clamp(_2000_2058.Config.SettingsWidth, 520f, Math.Min(1600f, val));
		float num2 = Math.Clamp(_2000_2058.Config.SettingsHeight, 440f, Math.Min(1200f, val2));
		if (Math.Abs(num - _2000_2058.Config.SettingsWidth) > 1f || Math.Abs(num2 - _2000_2058.Config.SettingsHeight) > 1f)
		{
			_2000_2058.Config.SettingsWidth = num;
			_2000_2058.Config.SettingsHeight = num2;
			_2000_2058.ConfigService.MarkDirty();
		}
	}

	[CompilerGenerated]
	private int _2005()
	{
		return Math.Clamp(_2000_2058.Config.SettingsTab, 0, _2000_2055.Length - 1);
	}

	[CompilerGenerated]
	private void _00A0(int P_0)
	{
		_2000_2058.Config.SettingsTab = P_0;
		_2000_2058.ConfigService.MarkDirty();
	}

	[CompilerGenerated]
	private void _1680(int P_0)
	{
		Vector2 value = new Vector2(ImGui.GetContentRegionAvail().X, Theme.ControlHeight);
		if (P_0 == 0)
		{
			var (label, variant, tooltip, enabled, action) = _2002();
			if (UiButton.Draw(label, variant, value, tooltip, enabled))
			{
				action();
			}
		}
		else
		{
			bool isOpen = _2000_2058.HudWindow.IsOpen;
			string tooltip2 = (isOpen ? "隐藏常驻悬浮窗（当前：显示）" : "显示常驻悬浮窗（当前：隐藏）");
			if (UiButton.Draw("HUD", UiVariant.Neutral, value, tooltip2))
			{
				_2000_2058.HudWindow.IsOpen = !isOpen;
				_2000_2058.Config.HudVisible = _2000_2058.HudWindow.IsOpen;
				_2000_2058.ConfigService.MarkDirty();
			}
		}
	}
}
