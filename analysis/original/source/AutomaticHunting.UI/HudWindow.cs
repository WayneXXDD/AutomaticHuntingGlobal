using System;
using System.Numerics;
using System.Runtime.CompilerServices;
using AutomaticHunting.UI.Components;
using Dalamud.Bindings.ImGui;
using Dalamud.Interface.Windowing;

namespace AutomaticHunting.UI;

public sealed class HudWindow : Window
{
	[CompilerGenerated]
	private sealed class _00A0
	{
		public PluginConfiguration _2008_2002;

		public HudWindow _2008_2003;

		internal void _00A0()
		{
			if (!_2008_2002.DisclaimerAccepted)
			{
				_2008_2003._2000_203E.PrintEcho("请先在首次向导中同意免责声明，再开启自动狩猎");
				return;
			}
			_2008_2002.Enabled = !_2008_2002.Enabled;
			_2008_2003._2000_203E.ConfigService.MarkDirty();
			_2008_2003._2000_203E.PrintEcho(_2008_2002.Enabled ? "自动狩猎已启用" : "自动狩猎已关闭");
		}

		internal void _00A0(int P_0)
		{
			_2008_2003._2000_203E.AdjustHuntChainKills(P_0);
		}

		internal void _1680()
		{
			_2008_2003._2000_203E.StopHuntChain("连车: 已停止");
		}

		internal void _2000()
		{
			_2008_2003._2000_203E.StopCrossWorld();
			_2008_2003._2000_203E.PrintEcho("跨服已停止");
		}

		internal void _2001()
		{
			_2008_2003._2000_203E.StopNavigation();
			_2008_2003._2000_203E.PrintEcho("导航已停止");
		}

		internal void _2002()
		{
			_2008_2003._2000_203E.StartNavigation();
		}
	}

	private readonly AutomaticHunting _2000_203E;

	private Theme.ThemeScope? _2000_2047;

	private double _2000_2048;

	public float Alpha => Math.Clamp(_2000_203E.Config.HudAlpha, 0.62f, 1f);

	public HudWindow(AutomaticHunting plugin)
		: base("自动狩猎跟车###AutomaticHunting.Hud")
	{
		_2000_203E = plugin;
		RespectCloseHotkey = false;
		Size = new Vector2(236f, 0f);
		SizeCondition = ImGuiCond.Always;
		Flags = ImGuiWindowFlags.NoTitleBar | ImGuiWindowFlags.NoResize | ImGuiWindowFlags.NoScrollbar | ImGuiWindowFlags.NoScrollWithMouse | ImGuiWindowFlags.AlwaysAutoResize;
		BgAlpha = null;
		AllowBackgroundBlur = true;
		PositionCondition = ImGuiCond.Always;
		_2004();
	}

	public override void PreDraw()
	{
		_2000_2047?.Dispose();
		_2000_2047 = Theme.Push(ThemeContext.Hud, Alpha);
	}

	public override void PostDraw()
	{
		_2000_2047?.Dispose();
		_2000_2047 = null;
	}

	public override void Draw()
	{
		ErrorBoundary.Run(_2000_203E, "HUD 悬浮窗", _00A0);
	}

	private void _00A0()
	{
		float uiScale = Theme.UiScale;
		bool hudLocked = _2000_203E.Config.HudLocked;
		UiPanel.DrawCompensationLayer(Alpha);
		UiPanel.DrawWindowFrame(8f * uiScale, hudLocked);
		HudCard.Draw(_1680());
		_2004();
	}

	private HudCardModel _1680()
	{
		PluginConfiguration config = _2000_203E.Config;
		(string Text, string Glyph, Vector4 Color) tuple = UiState.RunStatus(_2000_203E);
		string item = tuple.Text;
		string item2 = tuple.Glyph;
		Vector4 item3 = tuple.Color;
		HudCardModel hudCardModel = new HudCardModel
		{
			StatusText = item,
			StatusGlyph = item2,
			StatusColor = item3,
			ZoneName = AutomaticHunting.GetCurrentTerritoryName(),
			Locked = config.HudLocked,
			Alpha = Alpha,
			OnboardingPending = !config.OnboardingCompleted,
			Restricted = !config.DisclaimerAccepted
		};
		hudCardModel.ToggleEnabledTooltip = (config.Enabled ? "右键：关闭自动狩猎" : "右键：启用自动狩猎");
		hudCardModel.OnToggleEnabled = () =>
		{
			if (!config.DisclaimerAccepted)
			{
				_2000_203E.PrintEcho("请先在首次向导中同意免责声明，再开启自动狩猎");
			}
			else
			{
				config.Enabled = !config.Enabled;
				_2000_203E.ConfigService.MarkDirty();
				_2000_203E.PrintEcho(config.Enabled ? "自动狩猎已启用" : "自动狩猎已关闭");
			}
		};
		(string Label, Vector4 Color, float? Distance) targetInfo = _2000_203E.GetTargetInfo();
		string item4 = targetInfo.Label;
		float? item5 = targetInfo.Distance;
		hudCardModel.TargetLabel = UiState.SanitizeGlyphs(item4);
		hudCardModel.TargetColor = UiState.TargetColor(item4);
		hudCardModel.TargetDistance = (item5.HasValue ? $"{item5.Value:0.0}m" : null);
		float? leaderDistance = _2000_203E.GetLeaderDistance();
		if (leaderDistance.HasValue)
		{
			hudCardModel.LeaderText = $"{leaderDistance.Value:0.0}m";
			hudCardModel.LeaderColor = Theme.TextPrimary;
		}
		else if (!string.IsNullOrWhiteSpace(config.LeaderName))
		{
			hudCardModel.LeaderText = config.LeaderName;
			hudCardModel.LeaderColor = Theme.TextPrimary;
		}
		else
		{
			hudCardModel.LeaderText = "未设置";
			hudCardModel.LeaderColor = Theme.TextDisabled;
		}
		AutomaticHunting.SMonitorStatus sMonitor = _2000_203E.SMonitor;
		if (sMonitor != null)
		{
			hudCardModel.HasSMob = true;
			hudCardModel.SMobName = sMonitor.MobName;
			hudCardModel.SMobHpPercent = sMonitor.HpPercent;
			hudCardModel.SMobColor = UiState.SMobColor(sMonitor);
		}
		if (_2000_203E.IsNavigating)
		{
			hudCardModel.StageText = _2000_203E.GetNavigationStageText();
		}
		if (_2000_203E.IsHuntChainRunning)
		{
			hudCardModel.HasChainRow = true;
			hudCardModel.ChainText = _2000_203E.GetHuntChainStatusText(compact: true);
			hudCardModel.ChainCanAdjust = _2000_203E.CanAdjustHuntChainKills;
			hudCardModel.ChainCanDecrease = _2000_203E.CanDecreaseHuntChainKills;
			hudCardModel.OnChainAdjust = (int P_0) =>
			{
				_2000_203E.AdjustHuntChainKills(P_0);
			};
			hudCardModel.OnChainStop = () =>
			{
				_2000_203E.StopHuntChain("连车: 已停止");
			};
		}
		hudCardModel.Compact = _2002();
		if (!config.OnboardingCompleted)
		{
			hudCardModel.MainButtonLabel = "开始配置";
			hudCardModel.MainButtonVariant = UiVariant.Primary;
			hudCardModel.MainButtonEnabled = true;
			hudCardModel.MainButtonIsStop = false;
			hudCardModel.MainButtonTooltip = "打开首次运行向导";
			hudCardModel.OnMain = _2005;
		}
		else if (_2000_203E.CrossWorldInProgress)
		{
			hudCardModel.MainButtonLabel = "停止跨服";
			hudCardModel.MainButtonVariant = UiVariant.Danger;
			hudCardModel.MainButtonEnabled = true;
			hudCardModel.MainButtonIsStop = true;
			hudCardModel.MainButtonTooltip = "停止跨服 · 中断跨服流程并留在当前服务器";
			hudCardModel.OnMain = () =>
			{
				_2000_203E.StopCrossWorld();
				_2000_203E.PrintEcho("跨服已停止");
			};
		}
		else if (_2000_203E.IsNavigating)
		{
			hudCardModel.MainButtonLabel = "停止导航";
			hudCardModel.MainButtonVariant = UiVariant.Danger;
			hudCardModel.MainButtonEnabled = true;
			hudCardModel.MainButtonIsStop = true;
			hudCardModel.MainButtonTooltip = "停止导航 · 结束当前跟车任务并停在原地";
			hudCardModel.OnMain = () =>
			{
				_2000_203E.StopNavigation();
				_2000_203E.PrintEcho("导航已停止");
			};
		}
		else
		{
			hudCardModel.MainButtonLabel = "开始导航";
			hudCardModel.MainButtonVariant = UiVariant.Primary;
			hudCardModel.MainButtonEnabled = config.AutomationEnabled;
			hudCardModel.MainButtonIsStop = false;
			hudCardModel.MainButtonTooltip = "开始跟车导航";
			hudCardModel.OnMain = () =>
			{
				_2000_203E.StartNavigation();
			};
		}
		hudCardModel.OnSettings = _2000;
		hudCardModel.SettingsOpen = _2000_203E.SettingsWindow.IsOpen;
		hudCardModel.OnCloseHud = _2001;
		hudCardModel.OnDragDelta = _00A0;
		return hudCardModel;
	}

	private void _2000()
	{
		SettingsWindow settingsWindow = _2000_203E.SettingsWindow;
		settingsWindow.IsOpen = HudCard.ToggleSettingsOpen(settingsWindow.IsOpen);
		_2000_203E.PrintEcho(settingsWindow.IsOpen ? "插件设置已打开" : "插件设置已关闭");
	}

	private void _2001()
	{
		IsOpen = false;
		_2000_203E.Config.HudVisible = false;
		_2000_203E.ConfigService.MarkDirty();
		_2000_203E.PrintEcho("悬浮窗已隐藏");
	}

	private bool _2002()
	{
		AutomaticHunting.SMonitorStatus sMonitor = _2000_203E.SMonitor;
		if (sMonitor != null && !sMonitor.Finished)
		{
			_2000_2048 = 0.0;
			return false;
		}
		if (_2000_2048 > 0.0 && ImGui.GetTime() < _2000_2048)
		{
			return false;
		}
		if (_2000_2048 > 0.0)
		{
			_2000_2048 = 0.0;
		}
		return _2000_203E.Config.HudCompact;
	}

	public void NotifySMonitorFinished()
	{
		_2000_2048 = ImGui.GetTime() + 2.0;
	}

	private void _00A0(Vector2 P_0)
	{
		float num = Math.Max(0.01f, Theme.UiScale);
		_2000_203E.Config.HudPosX += P_0.X / num;
		_2000_203E.Config.HudPosY += P_0.Y / num;
		_2003();
		_2000_203E.ConfigService.MarkDirty();
	}

	private void _2003()
	{
		ImGuiIOPtr iO = ImGui.GetIO();
		float num = Math.Max(0.01f, Theme.UiScale);
		float num2 = iO.DisplaySize.X / num - 236f;
		float num3 = iO.DisplaySize.Y / num;
		if (_2000_203E.Config.HudPosX < 0f || _2000_203E.Config.HudPosY < 0f || _2000_203E.Config.HudPosX > num2 || _2000_203E.Config.HudPosY > num3)
		{
			_2000_203E.Config.HudPosX = 16f;
			_2000_203E.Config.HudPosY = 16f;
			AutomaticHunting.Log.Warning("AutomaticHunting: HUD 位置超出屏幕，已复位到左上角");
			AutomaticHunting.PrintEchoAlways("HUD 位置超出屏幕，已复位到左上角");
		}
	}

	private void _2004()
	{
		PluginConfiguration config = _2000_203E.Config;
		if (!(config.HudPosX < 0f) && !(config.HudPosY < 0f))
		{
			Position = new Vector2(config.HudPosX, config.HudPosY);
		}
	}

	public void ResetPosition()
	{
		_2000_203E.Config.HudPosX = 16f;
		_2000_203E.Config.HudPosY = 16f;
		_2000_203E.ConfigService.MarkDirty();
		_2004();
	}

	private void _2005()
	{
		_2000_203E.Config.SettingsTab = 0;
		_2000_203E.ConfigService.MarkDirty();
		_2000_203E.SettingsWindow.IsOpen = true;
	}
}
