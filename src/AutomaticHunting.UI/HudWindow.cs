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
	public sealed class RecoveredType_119
	{
		public PluginConfiguration field_682;

		public HudWindow field_683;

		internal void Method_729()
		{
			if (!field_682.DisclaimerAccepted)
			{
				field_683.field_679.PrintEcho("請先在首次設定精靈中同意免責宣告，再開啟自動狩獵");
				return;
			}
			field_682.Enabled = !field_682.Enabled;
			field_683.field_679.ConfigService.MarkDirty();
			field_683.field_679.PrintEcho(field_682.Enabled ? "自動狩獵已啟用" : "自動狩獵已關閉");
		}

		internal void Method_730(int arg0)
		{
			field_683.field_679.AdjustHuntChainKills(arg0);
		}

		internal void Method_731()
		{
			field_683.field_679.StopHuntChain("連車: 已停止");
		}

		internal void Method_732()
		{
			field_683.field_679.StopCrossWorld();
			field_683.field_679.PrintEcho("跨服已停止");
		}

		internal void Method_733()
		{
			field_683.field_679.StopNavigation();
			field_683.field_679.PrintEcho("導航已停止");
		}

		internal void Method_734()
		{
			field_683.field_679.StartNavigation();
		}
	}

	private readonly AutomaticHunting field_679;

	private Theme.ThemeScope? field_680;

	private double field_681;

	public float Alpha => Math.Clamp(field_679.Config.HudAlpha, 0.62f, 1f);

	public HudWindow(AutomaticHunting arg0)
		: base("自動狩獵跟車###AutomaticHunting.Hud")
	{
		field_679 = arg0;
		RespectCloseHotkey = false;
		Size = new Vector2(236f, 0f);
		SizeCondition = ImGuiCond.Always;
		Flags = ImGuiWindowFlags.NoTitleBar | ImGuiWindowFlags.NoResize | ImGuiWindowFlags.NoScrollbar | ImGuiWindowFlags.NoScrollWithMouse | ImGuiWindowFlags.AlwaysAutoResize;
		BgAlpha = null;
		AllowBackgroundBlur = true;
		PositionCondition = ImGuiCond.Always;
		Method_725();
	}

	public override void PreDraw()
	{
		field_680?.Dispose();
		field_680 = Theme.Push(ThemeContext.Hud, Alpha);
	}

	public override void PostDraw()
	{
		field_680?.Dispose();
		field_680 = null;
	}

	public override void Draw()
	{
		ErrorBoundary.Run(field_679, "狀態浮窗 狀態浮窗", Method_717);
	}

	private void Method_717()
	{
		float uiScale = Theme.UiScale;
		bool hudLocked = field_679.Config.HudLocked;
		UiPanel.DrawCompensationLayer(Alpha);
		UiPanel.DrawWindowFrame(8f * uiScale, hudLocked);
		HudCard.Draw(Method_718());
		Method_725();
	}

	private HudCardModel Method_718()
	{
		PluginConfiguration field_682 = field_679.Config;
		(string Text, string Glyph, Vector4 Color) tuple = UiState.RunStatus(field_679);
		string item = tuple.Text;
		string item2 = tuple.Glyph;
		Vector4 item3 = tuple.Color;
		HudCardModel hudCardModel = new HudCardModel
		{
			StatusText = item,
			StatusGlyph = item2,
			StatusColor = item3,
			ZoneName = AutomaticHunting.GetCurrentTerritoryName(),
			Locked = field_682.HudLocked,
			Alpha = Alpha,
			OnboardingPending = !field_682.OnboardingCompleted,
			Restricted = !field_682.DisclaimerAccepted
		};
		hudCardModel.ToggleEnabledTooltip = (field_682.Enabled ? "右鍵：關閉自動狩獵" : "右鍵：啟用自動狩獵");
		hudCardModel.OnToggleEnabled = () =>
		{
			if (!field_682.DisclaimerAccepted)
			{
				field_679.PrintEcho("請先在首次設定精靈中同意免責宣告，再開啟自動狩獵");
			}
			else
			{
				field_682.Enabled = !field_682.Enabled;
				field_679.ConfigService.MarkDirty();
				field_679.PrintEcho(field_682.Enabled ? "自動狩獵已啟用" : "自動狩獵已關閉");
			}
		};
		(string Label, Vector4 Color, float? Distance) targetInfo = field_679.GetTargetInfo();
		string item4 = targetInfo.Label;
		float? item5 = targetInfo.Distance;
		hudCardModel.TargetLabel = UiState.SanitizeGlyphs(item4);
		hudCardModel.TargetColor = UiState.TargetColor(item4);
		hudCardModel.TargetDistance = (item5.HasValue ? $"{item5.Value:0.0}m" : null);
		float? leaderDistance = field_679.GetLeaderDistance();
		if (leaderDistance.HasValue)
		{
			hudCardModel.LeaderText = $"{leaderDistance.Value:0.0}m";
			hudCardModel.LeaderColor = Theme.TextPrimary;
		}
		else if (!string.IsNullOrWhiteSpace(field_682.LeaderName))
		{
			hudCardModel.LeaderText = field_682.LeaderName;
			hudCardModel.LeaderColor = Theme.TextPrimary;
		}
		else
		{
			hudCardModel.LeaderText = "未設定";
			hudCardModel.LeaderColor = Theme.TextDisabled;
		}
		AutomaticHunting.SMonitorStatus sMonitor = field_679.SMonitor;
		if (sMonitor != null)
		{
			hudCardModel.HasSMob = true;
			hudCardModel.SMobName = sMonitor.MobName;
			hudCardModel.SMobHpPercent = sMonitor.HpPercent;
			hudCardModel.SMobColor = UiState.SMobColor(sMonitor);
		}
		if (field_679.IsNavigating)
		{
			hudCardModel.StageText = field_679.GetNavigationStageText();
		}
		if (field_679.IsHuntChainRunning)
		{
			hudCardModel.HasChainRow = true;
			hudCardModel.ChainText = field_679.GetHuntChainStatusText(arg0: true);
			hudCardModel.ChainCanAdjust = field_679.CanAdjustHuntChainKills;
			hudCardModel.ChainCanDecrease = field_679.CanDecreaseHuntChainKills;
			hudCardModel.OnChainAdjust = (int arg0) =>
			{
				field_679.AdjustHuntChainKills(arg0);
			};
			hudCardModel.OnChainStop = () =>
			{
				field_679.StopHuntChain("連車: 已停止");
			};
		}
		hudCardModel.Compact = Method_721();
		if (!field_682.OnboardingCompleted)
		{
			hudCardModel.MainButtonLabel = "開始設定";
			hudCardModel.MainButtonVariant = UiVariant.Primary;
			hudCardModel.MainButtonEnabled = true;
			hudCardModel.MainButtonIsStop = false;
			hudCardModel.MainButtonTooltip = "開啟首次執行設定精靈";
			hudCardModel.OnMain = Method_727;
		}
		else if (field_679.CrossWorldInProgress)
		{
			hudCardModel.MainButtonLabel = "停止跨服";
			hudCardModel.MainButtonVariant = UiVariant.Danger;
			hudCardModel.MainButtonEnabled = true;
			hudCardModel.MainButtonIsStop = true;
			hudCardModel.MainButtonTooltip = "停止跨服 · 中斷跨服流程並留在目前伺服器";
			hudCardModel.OnMain = () =>
			{
				field_679.StopCrossWorld();
				field_679.PrintEcho("跨服已停止");
			};
		}
		else if (field_679.IsNavigating)
		{
			hudCardModel.MainButtonLabel = "停止導航";
			hudCardModel.MainButtonVariant = UiVariant.Danger;
			hudCardModel.MainButtonEnabled = true;
			hudCardModel.MainButtonIsStop = true;
			hudCardModel.MainButtonTooltip = "停止導航 · 結束目前跟車任務並停在原地";
			hudCardModel.OnMain = () =>
			{
				field_679.StopNavigation();
				field_679.PrintEcho("導航已停止");
			};
		}
		else
		{
			hudCardModel.MainButtonLabel = "開始導航";
			hudCardModel.MainButtonVariant = UiVariant.Primary;
			hudCardModel.MainButtonEnabled = field_682.AutomationEnabled;
			hudCardModel.MainButtonIsStop = false;
			hudCardModel.MainButtonTooltip = "開始跟車導航";
			hudCardModel.OnMain = () =>
			{
				field_679.StartNavigation();
			};
		}
		hudCardModel.OnSettings = Method_719;
		hudCardModel.SettingsOpen = field_679.SettingsWindow.IsOpen;
		hudCardModel.OnCloseHud = Method_720;
		hudCardModel.OnDragDelta = Method_723;
		return hudCardModel;
	}

	private void Method_719()
	{
		SettingsWindow settingsWindow = field_679.SettingsWindow;
		settingsWindow.IsOpen = HudCard.ToggleSettingsOpen(settingsWindow.IsOpen);
		field_679.PrintEcho(settingsWindow.IsOpen ? "插件設定已開啟" : "插件設定已關閉");
	}

	private void Method_720()
	{
		IsOpen = false;
		field_679.Config.HudVisible = false;
		field_679.ConfigService.MarkDirty();
		field_679.PrintEcho("狀態浮窗已隱藏");
	}

	private bool Method_721()
	{
		AutomaticHunting.SMonitorStatus sMonitor = field_679.SMonitor;
		if (sMonitor != null && !sMonitor.Finished)
		{
			field_681 = 0.0;
			return false;
		}
		if (field_681 > 0.0 && ImGui.GetTime() < field_681)
		{
			return false;
		}
		if (field_681 > 0.0)
		{
			field_681 = 0.0;
		}
		return field_679.Config.HudCompact;
	}

	public void NotifySMonitorFinished()
	{
		field_681 = ImGui.GetTime() + 2.0;
	}

	private void Method_723(Vector2 arg0)
	{
		float num = Math.Max(0.01f, Theme.UiScale);
		field_679.Config.HudPosX += arg0.X / num;
		field_679.Config.HudPosY += arg0.Y / num;
		Method_724();
		field_679.ConfigService.MarkDirty();
	}

	private void Method_724()
	{
		ImGuiIOPtr iO = ImGui.GetIO();
		float num = Math.Max(0.01f, Theme.UiScale);
		float num2 = iO.DisplaySize.X / num - 236f;
		float num3 = iO.DisplaySize.Y / num;
		if (field_679.Config.HudPosX < 0f || field_679.Config.HudPosY < 0f || field_679.Config.HudPosX > num2 || field_679.Config.HudPosY > num3)
		{
			field_679.Config.HudPosX = 16f;
			field_679.Config.HudPosY = 16f;
			AutomaticHunting.Log.Warning("自動狩獵跟車：狀態浮窗 位置超出螢幕，已復位到左上角");
			AutomaticHunting.PrintEchoAlways("狀態浮窗 位置超出螢幕，已復位到左上角");
		}
	}

	private void Method_725()
	{
		PluginConfiguration config = field_679.Config;
		if (!(config.HudPosX < 0f) && !(config.HudPosY < 0f))
		{
			Position = new Vector2(config.HudPosX, config.HudPosY);
		}
	}

	public void ResetPosition()
	{
		field_679.Config.HudPosX = 16f;
		field_679.Config.HudPosY = 16f;
		field_679.ConfigService.MarkDirty();
		Method_725();
	}

	private void Method_727()
	{
		field_679.Config.SettingsTab = 0;
		field_679.ConfigService.MarkDirty();
		field_679.SettingsWindow.IsOpen = true;
	}
}
